using System;
using System.Collections.Generic;
using System.Text;
using ZoneFiveSoftware.Common.Data.Fitness;
using TrainerPower.Settings;
using System.ComponentModel;

namespace TrainerPower.Data
{
    public static class Shared
    {
        public static event EventHandler EvalPollingExpired;

        public static ITrainer GetTrainer(IEquipmentItem item)
        {
            foreach (Trainer trainer in GlobalSettings.FullTrainerList)
            {
                if (trainer.EquipmentList.Contains(item.ReferenceId))
                {
                    return trainer;
                }
            }

            return null;
        }

        internal static void RaiseEvalPollingExpiredEvent(object sender, EventArgs e)
        {
            if (EvalPollingExpired != null)
                EvalPollingExpired.Invoke(sender, e);
        }
    }
}
