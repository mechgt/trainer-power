using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using TrainerPower.Settings;
using TrainerPower.Util;
using TrainerPower.Data;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using System.Text;
using ZoneFiveSoftware.Common.Visuals.Fitness;

namespace TrainerPower
{
    class PluginMain : IPlugin
    {
        #region IPlugin Members

        public IApplication Application
        {
            set
            {
                m_App = value;
            }
        }

        public Guid Id
        {
            get { return GUIDs.PluginMain; }
        }

        public string Name
        {
            get { return Resources.Strings.Label_TrainerPower; }
        }

        public void ReadOptions(XmlDocument xmlDoc, XmlNamespaceManager nsmgr, XmlElement pluginNode)
        {
            // Import Primary Trainer file as distributed with plugin
            try
            {
                // Deserialization
                XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));

                string path = Path.Combine(PluginMain.PluginFolder, TrainerFile);
                FileStream fs = new FileStream(path, FileMode.Open);
                GlobalSettings settings = new GlobalSettings();

                // Read Main Trainer list distributed with plugin
                settings = (GlobalSettings)xs.Deserialize(fs);

                fs.Close();

                GlobalSettings.Main.Trainers = settings.Trainers;
            }
            catch
            { }

            // Import custom trainer file.  Entries here should take precedence over those in the main file
            try
            {
                // Deserialization
                XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));

                string path = Path.Combine(PluginMain.DataFolder, TrainerFile);
                FileStream fs = new FileStream(path, FileMode.Open);
                GlobalSettings settings = new GlobalSettings();

                // Read Main Trainer list distributed with plugin
                settings = (GlobalSettings)xs.Deserialize(fs);

                fs.Close();

                GlobalSettings.Custom.Trainers = settings.Trainers;
                GlobalSettings.Custom.Units = settings.Units;
            }
            catch
            { }
        }

        public void WriteOptions(XmlDocument xmlDoc, XmlElement pluginNode)
        {
            // Serialization
            XmlSerializer xs = new XmlSerializer(typeof(GlobalSettings));

            string path = Path.Combine(PluginMain.DataFolder, TrainerFile);
            XmlTextWriter xmlTextWriter = new XmlTextWriter(path, Encoding.UTF8);

            xs.Serialize(xmlTextWriter, GlobalSettings.Custom);
        }

        public string Version
        {
            get { return GetType().Assembly.GetName().Version.ToString(3); }
        }

        #endregion

        /// <summary>
        /// Plugin folder - plugins\data\{guid}\
        /// </summary>
        internal static string DataFolder
        {
            get
            {
                string path = Path.Combine(m_App.Configuration.CommonPluginsDataFolder, GUIDs.PluginMain.ToString("D"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        /// <summary>
        /// Plugin folder - plugins\installed\{guid}\
        /// </summary>
        internal static string PluginFolder
        {
            get
            {
                string path = Path.Combine(m_App.Configuration.CommonPluginsInstalledFolder, GUIDs.PluginMain.ToString("D"));

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                return path;
            }
        }

        #region Mechgt Licensing

        /// <summary>
        /// Plugin product Id as listed in license application
        /// </summary>
        internal static string ProductId
        {
            get
            {
                return "tp";
            }
        }

        /// <summary>
        /// Number of minutes to be used for evaluation purposes.
        /// This is the amount of data that will be processed.
        /// </summary>
        internal static int EvalMins
        {
            get { return 20; }
        }

        internal static string SupportEmail
        {
            get
            {
                return "support@mechgt.com";
            }
        }

        #endregion

        public static IApplication GetApplication()
        {
            return m_App;
        }

        private static readonly string TrainerFile = "TrainerData.xml";
        private static IApplication m_App = null;
    }
}
