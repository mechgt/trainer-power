using TrainerPower.Settings;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using TrainerPower.Data;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Visuals.Util;

namespace TrainerPower.Actions
{
    class CreatePowerTrack : IAction
    {
        #region Fields

        private const string newLine = "\r\n";

        #endregion

        #region Constructors

        public CreatePowerTrack(IView view)
        {
        }

        #endregion

        #region IAction Members

        public bool Enabled
        {
            get
            {
                List<IActivity> activities = GetActivities() as List<IActivity>;
                bool found = false;

                foreach (IActivity activity in activities)
                {
                    // DistanceMetersTrack would be the only way to get a distance track when on a trainer.  
                    //  GPSTrack would be void, so that's why it's not checked.
                    if (GetTrainer(activity) != null && activity.DistanceMetersTrack != null)
                    {
                        found = true;
                        break;
                    }
                }

                return found;
            }
        }

        public bool HasMenuArrow
        {
            get { return false; }
        }

        public Image Image
        {
            get
            {
                return Resources.Images.trainer_icon;
            }
        }

        public IList<string> MenuPath
        {
            get { return new List<string> { CommonResources.Text.LabelPower }; }
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public void Run(Rectangle rectButton)
        {
            IEnumerable<IActivity> activities = GetActivities();

            foreach (IActivity activity in activities)
            {
                DialogResult result = DialogResult.Yes;

                if (activity.PowerWattsTrack != null)
                {
                    // Warn if replacing an existing track
                    result = MessageDialog.Show(Resources.Strings.Text_OverwritePowerTrack + newLine
                        + activity.StartTime.Add(activity.TimeZoneUtcOffset) + newLine
                        + CommonResources.Text.LabelAvgPower + ": " + activity.PowerWattsTrack.Avg.ToString("0") + " " + CommonResources.Text.LabelWatts,
                        Resources.Strings.Label_TrainerPower + ": " + activity.StartTime.Add(activity.TimeZoneUtcOffset), System.Windows.Forms.MessageBoxButtons.YesNo);
                }

                if (result == DialogResult.Yes)
                {
                    INumericTimeDataSeries powerTrack = GetPowerTrack(activity);

                    if (powerTrack != null)
                    {
                        Trainer trainer = GetTrainer(activity);
                        activity.PowerWattsTrack = powerTrack;
                        activity.Notes = string.Format(Resources.Strings.Text_NotesMessage, trainer.Name, Resources.Strings.Label_TrainerPower, new PluginMain().Version) + newLine + newLine + activity.Notes;
                    }
                }
            }
        }

        public string Title
        {
            get
            {
                string title = string.Empty;

                List<IActivity> activities = GetActivities() as List<IActivity>;

                foreach (IActivity activity in activities)
                {
                    Trainer trainer = GetTrainer(activity);

                    if (trainer != null)
                    {
                        if (title == string.Empty)
                        {
                            // Assign first model found
                            title = ": " + trainer.Model;
                        }
                        else if (title != ": " + trainer.Model)
                        {
                            // Multiple models found
                            title = Resources.Strings.Label_CreatePowerTrack + " (" + Resources.Strings.Label_Multiple + ")";
                            return title;
                        }
                    }
                }

                title = Resources.Strings.Label_CreatePowerTrack + title + " ";
                return title;
            }
        }

        public bool Visible
        {
            get
            {
                List<IActivity> activities = GetActivities() as List<IActivity>;

                if (activities.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Methods

        internal INumericTimeDataSeries GetPowerTrack(IActivity activity)
        {
            if (activity.DistanceMetersTrack == null || activity.DistanceMetersTrack.Count == 0)
            {
                // Bad or no data
                return null;
            }

            Trainer trainer = GetTrainer(activity);

            // Bad data check
            if (trainer == null) return null;

            INumericTimeDataSeries powerTrack = trainer.GetPowerTrack(activity.DistanceMetersTrack);

            return powerTrack;
        }

        internal Trainer GetTrainer(IActivity activity)
        {
            foreach (Trainer trainer in GlobalSettings.FullTrainerList)
            {
                foreach (IEquipmentItem item in activity.EquipmentUsed)
                {
                    if (trainer.EquipmentList.Contains(item.ReferenceId))
                    {
                        return trainer;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Get all activities, or only those selected
        /// </summary>
        /// <param name="all"></param>
        /// <returns></returns>
        internal IEnumerable<IActivity> GetActivities()
        {
            IList<IActivity> activities = new List<IActivity>();

            // Prevent null ref error during startup
            if (PluginMain.GetApplication().Logbook == null ||
                PluginMain.GetApplication().ActiveView == null)
            {
                return activities;
            }

            IView view = PluginMain.GetApplication().ActiveView;

            if (view != null && view.Id == GUIDs.DailyActivityView)
            {
                IDailyActivityView activityView = view as IDailyActivityView;
                activities = CollectionUtils.GetItemsOfType<IActivity>(activityView.SelectionProvider.SelectedItems);
            }
            else if (view != null && view.Id == GUIDs.ActivityReportsView)
            {
                IActivityReportsView reportsView = view as IActivityReportsView;
                activities = CollectionUtils.GetItemsOfType<IActivity>(reportsView.SelectionProvider.SelectedItems);
            }

            return activities;
        }

        #endregion
    }
}
