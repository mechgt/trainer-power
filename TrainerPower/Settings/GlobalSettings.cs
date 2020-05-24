namespace TrainerPower.Settings
{
    using TrainerPower.Data;
    using System;
    using System.Xml.Serialization;
    using System.Collections.Generic;
    using System.IO;
    using ZoneFiveSoftware.Common.Data.Measurement;

    /// <summary>
    /// Global settings
    /// </summary>
    [XmlRootAttribute(ElementName = "TrainerPower", IsNullable = false)]
    public class GlobalSettings
    {
        private List<Trainer> trainers;
        private static List<Trainer> fullTrainers;
        private static GlobalSettings main;
        private static GlobalSettings custom;

        /// <summary>
        /// All stored trainers.
        /// </summary>
        /// <remarks>This is the instanced property, so this could be either the Custom list, or Main list.  
        /// This is utilized during (de)serialization.</remarks>
        public List<Trainer> Trainers
        {
            get
            {
                if (trainers == null)
                    trainers = new List<Trainer>();

                return trainers;
            }
            set
            {
                trainers = value;
            }
        }

        /// <summary>
        /// Primary units to display on settings.
        /// </summary>
        public Trainer.SpeedUnits Units
        {
            get;
            set;
        }

        /// <summary>
        /// File version
        /// </summary>
        public string Version
        {
            get;
            set;
        }

        /// <summary>
        /// Add this trainer to the custom list.  Update it if it already exists.
        /// </summary>
        /// <param name="trainer"></param>
        public static void UpdateTrainer(Trainer trainer)
        {
            // Remove from existing lists.
            Custom.Trainers.Remove(trainer);  // Remove from custom list
            fullTrainers.Remove(trainer); // Remove from combined list

            Custom.Trainers.Add(trainer);
            fullTrainers.Add(trainer);
            fullTrainers.Sort((x, y) => string.Compare(x.Name, y.Name));
        }

        public static bool RemoveTrainer(Trainer trainer)
        {
            if (GlobalSettings.Custom.Trainers.Remove(trainer))
            {
                fullTrainers.Remove(trainer);

                if (Main.Trainers.IndexOf(trainer) != -1)
                {
                    // Replace with default trainer definition if it exists
                    fullTrainers.Add(Main.Trainers[Main.Trainers.IndexOf(trainer)]);
                    fullTrainers.Sort((x, y) => string.Compare(x.Name, y.Name));
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Combined list of Main trainers, and custom trainers.
        /// </summary>
        internal static List<Trainer> FullTrainerList
        {
            get
            {
                if (fullTrainers == null)
                {
                    fullTrainers = new List<Trainer>(Custom.Trainers);

                    if (fullTrainers == null)
                        fullTrainers = new List<Trainer>();

                    foreach (Trainer trainer in Main.Trainers)
                    {
                        if (!fullTrainers.Contains(trainer))
                            fullTrainers.Add(trainer);
                    }

                    // Sort list alphabetically by trainer name
                    fullTrainers.Sort((x, y) => string.Compare(x.Name, y.Name));
                }

                return fullTrainers;
            }
        }

        /// <summary>
        /// Main trainer definitions
        /// </summary>
        internal static GlobalSettings Main
        {
            get
            {
                if (main == null)
                    main = new GlobalSettings();

                return main;
            }
        }

        /// <summary>
        /// Custom trainer definitions.
        /// </summary>
        internal static GlobalSettings Custom
        {
            get
            {
                if (custom == null)
                    custom = new GlobalSettings();

                return custom;
            }
        }
    }
}
