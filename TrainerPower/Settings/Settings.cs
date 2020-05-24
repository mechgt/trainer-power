using System.Globalization;
using System.ComponentModel;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
namespace TrainerPower.Settings
{
    class Settings : ISettingsPage
    {
        #region Fields

        private static SettingsControl control;

        #endregion

        #region ISettingsPage Members

        public Guid Id
        {
            get { return GUIDs.SettingsPage; }
        }

        public IList<ISettingsPage> SubPages
        {
            get { return null; }
        }

        #endregion

        #region IDialogPage Members

        public Control CreatePageControl()
        {
            if (control == null)
            {
                control = new SettingsControl();
            }

            return control;
        }

        public bool HidePage()
        {
            return true;
        }

        public string PageName
        {
            get { return Resources.Strings.Label_TrainerPower; }
        }

        public void ShowPage(string bookmark)
        {

        }

        public IPageStatus Status
        {
            get { return null; }
        }

        public void ThemeChanged(ITheme visualTheme)
        {
            control.ThemeChanged(visualTheme);
        }

        public string Title
        {
            get { return Resources.Strings.Label_TrainerPower; }
        }

        public void UICultureChanged(CultureInfo culture)
        {
            control.UICultureChanged(culture);
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
