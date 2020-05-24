using System.Collections;
using ZoneFiveSoftware.Common.Visuals.Chart;
using TrainerPower.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using ZoneFiveSoftware.Common.Data;
using ZoneFiveSoftware.Common.Data.Fitness;
using ZoneFiveSoftware.Common.Visuals;
using ZoneFiveSoftware.Common.Visuals.Fitness;
using TrainerPower.Util;
using ZoneFiveSoftware.Common.Data.Measurement;

namespace TrainerPower.Settings
{
    public partial class SettingsControl : UserControl
    {
        #region Fields

        #endregion

        #region Constructor

        public SettingsControl()
        {
            InitializeComponent();

            InitializeChart();
            InitializeTree();
            InitializeEquipment();
            
            lblInfo.Text = string.Format(Resources.Strings.Text_Info, Resources.Strings.Label_LiveRecording);
            lblInfo.Links.Add(Resources.Strings.Text_Info.IndexOf("{0}"), Resources.Strings.Label_LiveRecording.Length, "http://www.zonefivesoftware.com/sporttracks/plugins/?p=liverecording");
        }

        #endregion

        #region Methods

        /// <summary>
        /// Refresh Trainer power curve chart based on selected trainer.
        /// Also updates equation constants in pane below.
        /// </summary>
        internal void RefreshChart()
        {
            // Get selected trainer
            Trainer trainer = GetSelectedTrainer(treeTrainers);
            chartPwrCurve.DataSeries.Clear();

            if (trainer == null) return;

            // 30 mph is max speed = 13.5m/s
            // 40 mph is max speed = 17.89m/s
            double maxSpeed = 17.89;
            double delta = maxSpeed / 20; // 20 data points in chart

            ChartDataSeries series = new ChartDataSeries(chartPwrCurve, chartPwrCurve.YAxis);
            PointF point = new PointF();

            // Create trainer power curve data.  X=speed, Y=Power
            for (double speed = 0; speed < maxSpeed; speed += delta)
            {
                point.X = (float)trainer.ConvertSpeed(speed, GlobalSettings.Custom.Units);
                point.Y = (float)trainer.GetPower(speed);

                series.Points.Add(point.X, point);
            }

            // Add data to chart
            series.LineColor = Color.Red;
            chartPwrCurve.DataSeries.Add(series);
            chartPwrCurve.XAxis.Label = CommonResources.Text.LabelSpeed + " (" + Trainer.SpeedText(GlobalSettings.Custom.Units) + ")";
            chartPwrCurve.AutozoomToData(true);

            // Update equation constants
            //**************************
            // Update Radio buttons
            RefreshDetails();

            // Update equipment list
            //**********************
            treeEquip.RowData = trainer.EquipmentItems;
        }

        private static Trainer GetSelectedTrainer(TreeList tree)
        {
            System.Collections.ArrayList selected = (System.Collections.ArrayList)tree.SelectedItems;

            if (selected == null || selected.Count == 0)
            {
                // Bad data or nothing selected
                return null;
            }

            Trainer trainer = selected[0] as Trainer;

            return trainer;
        }

        internal void ThemeChanged(ITheme visualTheme)
        {
            chartPwrCurve.ThemeChanged(visualTheme);
            treeTrainers.ThemeChanged(visualTheme);
            bnrPowerCurve.ThemeChanged(visualTheme);
            bnrTrainerList.ThemeChanged(visualTheme);
        }

        internal void UICultureChanged(CultureInfo culture)
        {
            bnrPowerCurve.Text = Resources.Strings.Label_PowerCurve;
            bnrTrainerList.Text = Resources.Strings.Label_Trainers;

            chartPwrCurve.XAxis.Label = CommonResources.Text.LabelSpeed + " (" + Trainer.SpeedText(GlobalSettings.Custom.Units) + ")";
            chartPwrCurve.YAxis.Label = CommonResources.Text.LabelPower;

            btnEdit.Text = CommonResources.Text.ActionEdit;
            grpEquation.Text = CommonResources.Text.LabelDetails;
            grpEquip.Text = CommonResources.Text.LabelEquipment;

            lblMake.Text = Resources.Strings.Label_Manufacturer + ":";
            lblModel.Text = Resources.Strings.Label_Model + ":";

            mphMenuItem.Text = Trainer.SpeedText(Trainer.SpeedUnits.mph);
            kmhrMenuItem.Text = Trainer.SpeedText(Trainer.SpeedUnits.kmph);

            UpdateEquation(0, 1, 2, 3, Trainer.SpeedUnits.mph);
        }

        private void InitializeChart()
        {
            chartPwrCurve.XAxis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Speed);
            chartPwrCurve.YAxis.LabelColor = Constants.GetDataColor(Constants.ColorIndex.Power);
            kmhrMenuItem.Tag = Trainer.SpeedUnits.kmph;
            mphMenuItem.Tag = Trainer.SpeedUnits.mph;

            if (GlobalSettings.Custom.Units == Trainer.SpeedUnits.kmph)
            {
                kmhrMenuItem.Checked = true;
            }
            else
            {
                mphMenuItem.Checked = true;
            }
        }

        private void InitializeTree()
        {
            treeTrainers.Columns.Add(new TreeList.Column("Name"));
            treeTrainers.RowData = GlobalSettings.FullTrainerList;
            treeTrainers.MultiSelect = false;

            treeTrainers.RowDataRenderer = new TreeRenderer(treeTrainers);

            btnTrnrAdd.Text = string.Empty;
            btnTrnrAdd.BackgroundImage = Resources.Images.add;
            btnTrnrDel.Text = string.Empty;
            btnTrnrDel.BackgroundImage = Resources.Images.remove;
        }

        private void InitializeEquipment()
        {
            btnAssociateTrnr.Text = string.Empty;
            btnAssociateTrnr.BackgroundImage = Resources.Images.add;
            btnDissociate.Text = string.Empty;
            btnDissociate.BackgroundImage = Resources.Images.remove;

            btnEdit.LeftImage = CommonResources.Images.Edit16;

            // Iniialize combobox
            cboEquipment.Items.Clear();

            foreach (IEquipmentItem item in PluginMain.GetApplication().Logbook.Equipment)
            {
                cboEquipment.Items.Add(item);
            }

            cboEquipment.DisplayMember = "Name";
            cboEquipment.Sorted = true;

            // Initialize Equipment Tree
            treeEquip.Columns.Clear();
            treeEquip.Columns.Add(new TreeList.Column("Name"));
            treeTrainers.MultiSelect = false;
        }

        #endregion

        #region Event Handlers

        private void treeTrainers_SelectedItemsChanged(object sender, EventArgs e)
        {
            RefreshChart();
        }

        private void RefreshDetails()
        {
            Trainer trainer = GetSelectedTrainer(treeTrainers);
            if (trainer == null) return;

            switch (trainer.TrainerType)
            {
                case Trainer.AlgorithmType.A:
                    UpdateEquation(trainer.ConstantA, trainer.ConstantB, trainer.ConstantC, trainer.ConstantD, trainer.AlgoSpeedUnits);

                    txtA.TextChanged -= txtCoefficient_TextChanged;
                    txtB.TextChanged -= txtCoefficient_TextChanged;
                    txtC.TextChanged -= txtCoefficient_TextChanged;
                    txtD.TextChanged -= txtCoefficient_TextChanged;
                    txtMake.TextChanged -= txtMake_TextChanged;
                    txtModel.TextChanged -= txtMake_TextChanged;

                    txtA.Text = trainer.ConstantA.ToString(CultureInfo.CurrentCulture);
                    txtB.Text = trainer.ConstantB.ToString(CultureInfo.CurrentCulture);
                    txtC.Text = trainer.ConstantC.ToString(CultureInfo.CurrentCulture);
                    txtD.Text = trainer.ConstantD.ToString(CultureInfo.CurrentCulture);
                    txtMake.Text = trainer.Manufacturer;
                    txtModel.Text = trainer.Model;

                    txtA.TextChanged += txtCoefficient_TextChanged;
                    txtB.TextChanged += txtCoefficient_TextChanged;
                    txtC.TextChanged += txtCoefficient_TextChanged;
                    txtD.TextChanged += txtCoefficient_TextChanged;
                    txtMake.TextChanged += txtMake_TextChanged;
                    txtModel.TextChanged += txtMake_TextChanged;

                    break;
            }
        }

        private void UpdateEquation(double A, double B, double C, double D, Trainer.SpeedUnits units)
        {
            string speed = CommonResources.Text.LabelSpeed;
            lblEqn.Text = CommonResources.Text.LabelPower + " (" + CommonResources.Text.LabelWatts + ") = " + A.ToString("0.####", CultureInfo.CurrentCulture) + " + " + B.ToString("0.####", CultureInfo.CurrentCulture) + " * " + speed + "(" + Trainer.SpeedText(Trainer.SpeedUnits.mph) + ") + " + C.ToString("0.####", CultureInfo.CurrentCulture) + " * " + speed + "^2 + " + D.ToString("0.####", CultureInfo.CurrentCulture) + " * " + speed + "^3";
        }

        private void btnAssociateTrnr_Click(object sender, EventArgs e)
        {
            // Associate a trainer with a piece of equipment
            IEquipmentItem item = cboEquipment.SelectedItem as IEquipmentItem;
            Trainer trainer = GetSelectedTrainer(treeTrainers);

            if (item == null || trainer == null) return;

            trainer.EquipmentList.Add(item.ReferenceId);

            GlobalSettings.UpdateTrainer(trainer);

            treeEquip.RowData = trainer.EquipmentItems;
        }

        private void btnDissociate_Click(object sender, EventArgs e)
        {
            // Dissociate a trainer from a piece of equipment
            ArrayList items = treeEquip.Selected as ArrayList;
            Trainer trainer = GetSelectedTrainer(treeTrainers);

            if (items == null || trainer == null) return;

            foreach (IEquipmentItem item in items)
            {
                if (trainer.EquipmentList.Contains(item.ReferenceId))
                {
                    trainer.EquipmentList.Remove(item.ReferenceId);
                }
            }

            treeEquip.RowData = trainer.EquipmentItems;
        }

        private void btnTrnrAdd_Click(object sender, EventArgs e)
        {
            // Add new trainer
            Trainer trainer = new Trainer();
            GlobalSettings.UpdateTrainer(trainer);

            // Update trainer list
            treeTrainers.RowData = GlobalSettings.FullTrainerList;

            // Enable editing if not already done
            if (!txtA.Enabled)
            {
                btnEdit_Click(sender, e);
            }

            // Select new trainer in list
            treeTrainers.Selected = new List<Trainer> { trainer };
        }

        private void btnTrnrDel_Click(object sender, EventArgs e)
        {
            Trainer trainer = GetSelectedTrainer(treeTrainers);

            if (!GlobalSettings.RemoveTrainer(trainer))
                MessageDialog.Show(Resources.Strings.Text_CannotRemoveDefaultTrainers, Resources.Strings.Label_TrainerPower, MessageBoxButtons.OK);
            else
                treeTrainers.RowData = GlobalSettings.FullTrainerList;
        }

        private void txtCoefficient_TextChanged(object sender, EventArgs e)
        {
            TextBoxBase btn = sender as TextBoxBase;
            Trainer trainer = GetSelectedTrainer(treeTrainers);

            // Make sure we id the button OK
            if (btn != null && trainer != null)
            {
                double value;

                // Parse text value
                string textEntry = btn.Text;
                //if (textEntry.StartsWith(CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator,true,CultureInfo.CurrentCulture)
                //{
                //    textEntry = "0" + textEntry;
                //}

                if (double.TryParse(textEntry, NumberStyles.Float, CultureInfo.CurrentCulture, out value) || textEntry == "-")
                {
                    // Store in appropriate value
                    switch (btn.Tag as string)
                    {
                        case "A":
                            trainer.ConstantA = value;
                            break;
                        case "B":
                            trainer.ConstantB = value;
                            break;
                        case "C":
                            trainer.ConstantC = value;
                            break;
                        case "D":
                            trainer.ConstantD = value;
                            break;
                    }

                    GlobalSettings.UpdateTrainer(trainer);

                }
                else
                {
                    // Could not parse value properly
                    btn.Text = string.Empty;
                }
            }
        }

        private void txtCoefficient_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtA.Text) && !string.IsNullOrEmpty(txtB.Text) && !string.IsNullOrEmpty(txtC.Text) && !string.IsNullOrEmpty(txtD.Text))
                RefreshChart();
        }

        private void txtMake_TextChanged(object sender, EventArgs e)
        {
            TextBoxBase btn = sender as TextBoxBase;
            Trainer trainer = GetSelectedTrainer(treeTrainers);

            // Make sure we id the button OK
            if (btn != null && trainer != null)
            {
                string value = btn.Text;

                // Parse text value
                if (!string.IsNullOrEmpty(value))
                {
                    // Store in appropriate value
                    switch (btn.Tag as string)
                    {
                        case "Make":
                            trainer.Manufacturer = value;
                            break;
                        case "Model":
                            trainer.Model = value;
                            break;
                    }

                    GlobalSettings.UpdateTrainer(trainer);
                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtA.Enabled = !txtA.Enabled;
            txtB.Enabled = txtA.Enabled;
            txtC.Enabled = txtA.Enabled;
            txtD.Enabled = txtA.Enabled;
            txtMake.Enabled = txtA.Enabled;
            txtModel.Enabled = txtA.Enabled;

            RefreshDetails();

            if (!txtA.Enabled)
            {
                btnEdit.Text = CommonResources.Text.ActionEdit;
                btnEdit.LeftImage = CommonResources.Images.Edit16;
            }
            else
            {
                btnEdit.Text = CommonResources.Text.ActionSave;
                btnEdit.LeftImage = CommonResources.Images.Save16;
            }
        }

        #endregion

        private void bnrPowerCurve_MenuClicked(object sender, EventArgs e)
        {
            ActionBanner ChartBanner = sender as ActionBanner;

            powerMenu.Show(this, new Point(ChartBanner.Right - 2, ChartBanner.Bottom), ToolStripDropDownDirection.BelowLeft);
        }

        private void powerMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem selected = sender as ToolStripMenuItem;

            // Set checked items, and store units
            foreach (ToolStripMenuItem item in powerMenu.Items)
            {
                if (item == selected)
                {
                    item.Checked = true;
                    GlobalSettings.Custom.Units = (Trainer.SpeedUnits)item.Tag;
                }
                else
                {
                    item.Checked = false;
                }
            }

            RefreshChart();
        }

        private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel link = sender as LinkLabel;

            // Determine which link was clicked within the LinkLabel.
            link.Links[link.Links.IndexOf(e.Link)].Visited = true;

            // Display the appropriate link based on the value of the 
            // LinkData property of the Link object.
            string target = e.Link.LinkData as string;

            // If the value looks like a URL, navigate to it.
            // Otherwise, display it in a message box.
            if (null != target && target.StartsWith("http://"))
            {
                System.Diagnostics.Process.Start(target);
            }
            else
            {
                MessageBox.Show("Item clicked: " + target);
            }
        }
    }
}
