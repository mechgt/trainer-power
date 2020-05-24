using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using ZoneFiveSoftware.Common.Data.Measurement;
using System.Xml.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using TrainerPower.Settings;

namespace TrainerPower.Data
{
    [XmlRootAttribute(ElementName = "Trainer", IsNullable = false)]
    public class Trainer : ITrainer
    {
        #region Fields

        private AlgorithmType trainerType;
        private string model;
        private string manufacturer;
        private Guid id;
        private double constantA;
        private double constantB;
        private double constantC;
        private double constantD;
        private SpeedUnits units;
        private List<string> equipmentList;

        private static DateTime lastRequest;
        private static DateTime startRequest;
        private static bool expired;

        #endregion

        #region Enums

        public enum AlgorithmType
        {
            A,
            B
        }
        public enum SpeedUnits
        {
            mph, // miles / hour
            kmph, // km / hour
            mps // m/s
        }

        #endregion

        #region Constructors

        public Trainer()
        { }

        public Trainer(string manufacturer, string model, AlgorithmType trainerType, SpeedUnits units, double constA, double constB, double constC, double constD)
        {
            this.model = model;
            this.manufacturer = manufacturer;
            this.trainerType = trainerType;
            this.units = units;
            this.constantA = constA;
            this.constantB = constB;
            this.constantC = constC;
            this.ConstantD = constD;
            this.id = Guid.NewGuid();
        }

        #endregion

        #region Properties

        [XmlIgnore()]
        public AlgorithmType TrainerType
        {
            get { return AlgorithmType.A; }
            set { trainerType = value; }
        }

        public SpeedUnits AlgoSpeedUnits
        {
            get { return units; }
            set { units = value; }
        }

        public double ConstantA
        {
            get { return constantA; }
            set { constantA = value; }
        }

        public double ConstantB
        {
            get { return constantB; }
            set { constantB = value; }
        }

        public double ConstantC
        {
            get { return constantC; }
            set { constantC = value; }
        }

        public double ConstantD
        {
            get { return constantD; }
            set { constantD = value; }
        }

        /// <summary>
        /// Gets Model info of this trainer.
        /// </summary>
        public string Model
        {
            get { return model; }
            set { model = value; }
        }

        /// <summary>
        /// Gets Manufacturer of this trainer.
        /// </summary>
        public string Manufacturer
        {
            get { return manufacturer; }
            set { manufacturer = value; }
        }

        /// <summary>
        /// Gets the display name of this trainer (Manufacturer - Model).
        /// </summary>
        public string Name
        {
            get { return Manufacturer + " - " + Model; }
        }

        /// <summary>
        /// Gets a unique id referencing this trainer.
        /// </summary>
        public string ReferenceId
        {
            get
            {
                if (id == null || id == Guid.Empty)
                {
                    id = Guid.NewGuid();
                }

                return id.ToString("D");
            }
            set { id = new Guid(value); }
        }

        public List<string> EquipmentList
        {
            get
            {
                if (equipmentList == null)
                {
                    equipmentList = new List<string>();
                }
                return equipmentList;
            }
            set
            {
                equipmentList = value;
            }
        }

        /// <summary>
        /// Gets a list of equipment associated with this trainer.
        /// </summary>
        [XmlIgnore()]
        public List<IEquipmentItem> EquipmentItems
        {
            get
            {
                if (equipmentList == null) return null;

                List<IEquipmentItem> equipment = new List<IEquipmentItem>();

                foreach (IEquipmentItem item in PluginMain.GetApplication().Logbook.Equipment)
                {
                    if (equipmentList.Contains(item.ReferenceId))
                    {
                        equipment.Add(item);
                    }
                }

                return equipment;
            }
        }

        #endregion

        #region Methods

        public override bool Equals(object obj)
        {
            Trainer trainer = obj as Trainer;
            if (trainer == null)
            {
                return false;
            }

            return this.ReferenceId == trainer.ReferenceId;
        }

        public override int GetHashCode()
        {
            return this.ReferenceId.GetHashCode();
        }

        /// <summary>
        /// Convert Speed to match expected equation engineering units.
        /// For instance, if the curves are calibrated for speed input of mph, 
        /// then calculations will need to be done in mph as other units will
        /// result in error.  This converts from m/s to the selected speed unit.
        /// This is NOT necessarily the units the user likes for display.
        /// </summary>
        /// <param name="metersPerSecond">Speed in meters / second</param>
        /// <returns>Speed in algorithm scaled units (AlgoSpeedUnits)</returns>
        internal double ConvertSpeed(double metersPerSecond, Trainer.SpeedUnits units)
        {
            double speed;

            switch (units)
            {
                case SpeedUnits.mph:
                    speed = Length.Convert(metersPerSecond, Length.Units.Meter, Length.Units.Mile) * 3600;
                    break;

                case SpeedUnits.kmph:
                    speed = metersPerSecond * 3.6;
                    break;
                default:
                case SpeedUnits.mps:
                    return metersPerSecond;
            }

            return speed;
        }

        internal INumericTimeDataSeries GetPowerTrack(IDistanceDataTrack distanceTrack)
        {
            NumericTimeDataSeries powerTrack = new NumericTimeDataSeries();
            double deltaSeconds, speed, power, deltaDist;
            DateTime time, prevTime = DateTime.MinValue;
            ITimeValueEntry<float> prevPoint = null;

            foreach (ITimeValueEntry<float> item in distanceTrack)
            {
                // Store current point time
                time = distanceTrack.EntryDateTime(item);

                if (prevPoint != null)
                {
                    deltaDist = item.Value - prevPoint.Value;
                    if (deltaDist >= 0)
                    {
                        deltaSeconds = (time - prevTime).TotalSeconds;
                        speed = deltaDist / deltaSeconds; // in meters/second

                        power = GetPower(speed);

                        // Add previous time to cover first point and maintain start time.
                        powerTrack.Add(prevTime, (float)power);
                    }
                }

                prevPoint = item;
                prevTime = time;
            }

            float min, max;
            return Util.Utilities.STSmooth(powerTrack, 1, out min, out max);
        }

        /// <summary>
        /// Given a specific speed (in m/s), return the calculated power.
        /// Each trainer has a specific speed to power relationship,
        /// this method will apply the calculations specific to this trainer
        /// to return a power value (in watts).  
        /// </summary>
        /// <param name="speedMetersPerSec">Input speed, in meters/second.</param>
        /// <returns>Calculated power value (in watts).</returns>
        public double CalculatePower(double speedMetersPerSec)
        {
            return GetPower(speedMetersPerSec);
        }

        internal double GetPower(double speedMetersPerSec)
        {
            double power;
            double speed = ConvertSpeed(speedMetersPerSec, AlgoSpeedUnits); // in algorithm units

            switch (TrainerType)
            {
                // NOTE: Algorithm definitions here
                case Trainer.AlgorithmType.A:
                    power = ConstantA + ConstantB * speed + ConstantC * Math.Pow(speed, 2) + ConstantD * Math.Pow(speed, 3);
                    break;

                case Trainer.AlgorithmType.B:
                    power = ConstantB * Math.Pow(speed, ConstantC);
                    break;

                default:
                    power = 0;
                    break;
            }

            return power;
        }

        internal static string SpeedText(SpeedUnits units)
        {
            switch (units)
            {
                case SpeedUnits.kmph:
                    return Speed.Label(Speed.Units.Speed, new Length(1, Length.Units.Kilometer));
                case SpeedUnits.mph:
                    return Speed.Label(Speed.Units.Speed, new Length(1, Length.Units.Mile));
                case SpeedUnits.mps:
                    return Speed.Label(Speed.Units.Speed, new Length(1, Length.Units.Meter));
            }

            return units.ToString();
        }

        #endregion
    }
}
