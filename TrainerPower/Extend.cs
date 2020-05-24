using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using TrainerPower.Actions;

namespace TrainerPower
{
    class Extend : IExtendDailyActivityViewActions, IExtendActivityReportsViewActions, IExtendSettingsPages
    {

        #region IExtendDailyActivityViewActions Members

        public IList<IAction> GetActions(IDailyActivityView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                List<IAction> list = new List<IAction> { new CreatePowerTrack(view) };
                return list;
            }

            return null;
        }

        #endregion

        #region IExtendActivityReportsViewActions Members

        public IList<IAction> GetActions(IActivityReportsView view, ExtendViewActions.Location location)
        {
            if (location == ExtendViewActions.Location.EditMenu)
            {
                List<IAction> list = new List<IAction> { new CreatePowerTrack(view) };
                return list;
            }

            return null;
        }

        #endregion

        #region IExtendSettingsPages Members

        public IList<ISettingsPage> SettingsPages
        {
            get
            {
                List<ISettingsPage> list = new List<ISettingsPage> { new Settings.Settings() };
                return list;
            }
        }

        #endregion


    }
}
