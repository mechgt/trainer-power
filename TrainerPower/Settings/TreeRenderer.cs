using System.Drawing;
using TrainerPower.Data;
using ZoneFiveSoftware.Common.Visuals;

namespace TrainerPower.Settings
{

    class TreeRenderer : TreeList.DefaultRowDataRenderer
    {
        public TreeRenderer(TreeList tree)
            : base(tree)
        {

        }

        protected override FontStyle GetCellFontStyle(object element, TreeList.Column column)
        {
            FontStyle style = base.GetCellFontStyle(element, column);

            Trainer trainer = element as Trainer;
            if (trainer != null)
            {
                if (GlobalSettings.Custom.Trainers.Contains(trainer))
                {
                    style = FontStyle.Bold;
                }
            }

            return style;
        }
    }
}
