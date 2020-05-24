using System;
using System.Collections.Generic;
using System.Text;

namespace TrainerPower.Data
{
    public interface ITrainer
    {
        /// <summary>
        /// Gets Model info of this trainer.
        /// </summary>
        string Model { get; }
        /// <summary>
        /// Gets Manufacturer of this trainer.
        /// </summary>
        string Manufacturer { get; }
        /// <summary>
        /// Gets the Name of this trainer.
        /// </summary>
        string Name { get; }
        /// <summary>
        /// Gets a unique id referencing this trainer.
        /// </summary>
        string ReferenceId { get; }

        /// <summary>
        /// Given a specific speed (in m/s), return the calculated power.
        /// Each trainer has a specific speed to power relationship,
        /// this method will apply the calculations specific to this trainer
        /// to return a power value (in watts).  
        /// </summary>
        /// <param name="speedMetersPerSec">Input speed, in meters/second.</param>
        /// <returns>Calculated power value (in watts).</returns>
        double CalculatePower(double speedMetersPerSec);
    }
}
