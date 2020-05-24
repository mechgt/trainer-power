namespace TrainerPower.Settings
{
    partial class SettingsControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.chartPwrCurve = new ZoneFiveSoftware.Common.Visuals.Chart.ChartBase();
            this.bnrPowerCurve = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.bnrTrainerList = new ZoneFiveSoftware.Common.Visuals.ActionBanner();
            this.btnTrnrAdd = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnTrnrDel = new ZoneFiveSoftware.Common.Visuals.Button();
            this.treeTrainers = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.grpEquation = new System.Windows.Forms.GroupBox();
            this.lblD = new System.Windows.Forms.Label();
            this.lblC = new System.Windows.Forms.Label();
            this.lblB = new System.Windows.Forms.Label();
            this.btnEdit = new ZoneFiveSoftware.Common.Visuals.Button();
            this.txtMake = new System.Windows.Forms.TextBox();
            this.txtModel = new System.Windows.Forms.TextBox();
            this.lblMake = new System.Windows.Forms.Label();
            this.txtD = new System.Windows.Forms.TextBox();
            this.lblModel = new System.Windows.Forms.Label();
            this.lblA = new System.Windows.Forms.Label();
            this.txtC = new System.Windows.Forms.TextBox();
            this.txtB = new System.Windows.Forms.TextBox();
            this.txtA = new System.Windows.Forms.TextBox();
            this.lblEqn = new System.Windows.Forms.Label();
            this.cboEquipment = new System.Windows.Forms.ComboBox();
            this.treeEquip = new ZoneFiveSoftware.Common.Visuals.TreeList();
            this.btnAssociateTrnr = new ZoneFiveSoftware.Common.Visuals.Button();
            this.btnDissociate = new ZoneFiveSoftware.Common.Visuals.Button();
            this.grpEquip = new System.Windows.Forms.GroupBox();
            this.powerMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mphMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kmhrMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblInfo = new System.Windows.Forms.LinkLabel();
            this.bnrTrainerList.SuspendLayout();
            this.grpEquation.SuspendLayout();
            this.grpEquip.SuspendLayout();
            this.powerMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // chartPwrCurve
            // 
            this.chartPwrCurve.BackColor = System.Drawing.Color.Transparent;
            this.chartPwrCurve.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.chartPwrCurve.Location = new System.Drawing.Point(209, 33);
            this.chartPwrCurve.Name = "chartPwrCurve";
            this.chartPwrCurve.Padding = new System.Windows.Forms.Padding(5);
            this.chartPwrCurve.Size = new System.Drawing.Size(306, 267);
            this.chartPwrCurve.TabIndex = 0;
            this.chartPwrCurve.TabStop = false;
            // 
            // bnrPowerCurve
            // 
            this.bnrPowerCurve.BackColor = System.Drawing.Color.Transparent;
            this.bnrPowerCurve.HasMenuButton = true;
            this.bnrPowerCurve.Location = new System.Drawing.Point(209, 3);
            this.bnrPowerCurve.Name = "bnrPowerCurve";
            this.bnrPowerCurve.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrPowerCurve.Size = new System.Drawing.Size(306, 24);
            this.bnrPowerCurve.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrPowerCurve.TabIndex = 1;
            this.bnrPowerCurve.Text = "Power Curve";
            this.bnrPowerCurve.UseStyleFont = true;
            this.bnrPowerCurve.MenuClicked += new System.EventHandler(this.bnrPowerCurve_MenuClicked);
            // 
            // bnrTrainerList
            // 
            this.bnrTrainerList.BackColor = System.Drawing.Color.Transparent;
            this.bnrTrainerList.Controls.Add(this.btnTrnrAdd);
            this.bnrTrainerList.Controls.Add(this.btnTrnrDel);
            this.bnrTrainerList.HasMenuButton = false;
            this.bnrTrainerList.Location = new System.Drawing.Point(3, 3);
            this.bnrTrainerList.Name = "bnrTrainerList";
            this.bnrTrainerList.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.bnrTrainerList.Size = new System.Drawing.Size(200, 24);
            this.bnrTrainerList.Style = ZoneFiveSoftware.Common.Visuals.ActionBanner.BannerStyle.Header2;
            this.bnrTrainerList.TabIndex = 1;
            this.bnrTrainerList.Text = "Trainer List";
            this.bnrTrainerList.UseStyleFont = true;
            // 
            // btnTrnrAdd
            // 
            this.btnTrnrAdd.BackColor = System.Drawing.Color.Transparent;
            this.btnTrnrAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTrnrAdd.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnTrnrAdd.CenterImage = null;
            this.btnTrnrAdd.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTrnrAdd.HyperlinkStyle = false;
            this.btnTrnrAdd.ImageMargin = 2;
            this.btnTrnrAdd.LeftImage = null;
            this.btnTrnrAdd.Location = new System.Drawing.Point(173, 0);
            this.btnTrnrAdd.Name = "btnTrnrAdd";
            this.btnTrnrAdd.PushStyle = true;
            this.btnTrnrAdd.RightImage = null;
            this.btnTrnrAdd.Size = new System.Drawing.Size(24, 24);
            this.btnTrnrAdd.TabIndex = 2;
            this.btnTrnrAdd.Text = "+";
            this.btnTrnrAdd.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnTrnrAdd.TextLeftMargin = 2;
            this.btnTrnrAdd.TextRightMargin = 2;
            this.btnTrnrAdd.Click += new System.EventHandler(this.btnTrnrAdd_Click);
            // 
            // btnTrnrDel
            // 
            this.btnTrnrDel.BackColor = System.Drawing.Color.Transparent;
            this.btnTrnrDel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnTrnrDel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnTrnrDel.CenterImage = null;
            this.btnTrnrDel.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnTrnrDel.HyperlinkStyle = false;
            this.btnTrnrDel.ImageMargin = 2;
            this.btnTrnrDel.LeftImage = null;
            this.btnTrnrDel.Location = new System.Drawing.Point(143, 0);
            this.btnTrnrDel.Name = "btnTrnrDel";
            this.btnTrnrDel.PushStyle = true;
            this.btnTrnrDel.RightImage = null;
            this.btnTrnrDel.Size = new System.Drawing.Size(24, 24);
            this.btnTrnrDel.TabIndex = 1;
            this.btnTrnrDel.Text = "-";
            this.btnTrnrDel.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnTrnrDel.TextLeftMargin = 2;
            this.btnTrnrDel.TextRightMargin = 2;
            this.btnTrnrDel.Click += new System.EventHandler(this.btnTrnrDel_Click);
            // 
            // treeTrainers
            // 
            this.treeTrainers.BackColor = System.Drawing.Color.Transparent;
            this.treeTrainers.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeTrainers.CheckBoxes = false;
            this.treeTrainers.DefaultIndent = 15;
            this.treeTrainers.DefaultRowHeight = -1;
            this.treeTrainers.HeaderRowHeight = 21;
            this.treeTrainers.Location = new System.Drawing.Point(3, 33);
            this.treeTrainers.MultiSelect = false;
            this.treeTrainers.Name = "treeTrainers";
            this.treeTrainers.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.None;
            this.treeTrainers.NumLockedColumns = 0;
            this.treeTrainers.RowAlternatingColors = true;
            this.treeTrainers.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.treeTrainers.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeTrainers.RowHotlightMouse = true;
            this.treeTrainers.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeTrainers.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeTrainers.RowSeparatorLines = true;
            this.treeTrainers.ShowLines = false;
            this.treeTrainers.ShowPlusMinus = false;
            this.treeTrainers.Size = new System.Drawing.Size(200, 267);
            this.treeTrainers.TabIndex = 3;
            this.treeTrainers.SelectedItemsChanged += new System.EventHandler(this.treeTrainers_SelectedItemsChanged);
            // 
            // grpEquation
            // 
            this.grpEquation.Controls.Add(this.lblD);
            this.grpEquation.Controls.Add(this.lblC);
            this.grpEquation.Controls.Add(this.lblB);
            this.grpEquation.Controls.Add(this.btnEdit);
            this.grpEquation.Controls.Add(this.txtMake);
            this.grpEquation.Controls.Add(this.txtModel);
            this.grpEquation.Controls.Add(this.lblMake);
            this.grpEquation.Controls.Add(this.txtD);
            this.grpEquation.Controls.Add(this.lblModel);
            this.grpEquation.Controls.Add(this.lblA);
            this.grpEquation.Controls.Add(this.txtC);
            this.grpEquation.Controls.Add(this.txtB);
            this.grpEquation.Controls.Add(this.txtA);
            this.grpEquation.Controls.Add(this.lblEqn);
            this.grpEquation.Location = new System.Drawing.Point(3, 306);
            this.grpEquation.Name = "grpEquation";
            this.grpEquation.Size = new System.Drawing.Size(512, 129);
            this.grpEquation.TabIndex = 3;
            this.grpEquation.TabStop = false;
            this.grpEquation.Text = "Details";
            // 
            // lblD
            // 
            this.lblD.AutoSize = true;
            this.lblD.Location = new System.Drawing.Point(366, 60);
            this.lblD.Name = "lblD";
            this.lblD.Size = new System.Drawing.Size(16, 13);
            this.lblD.TabIndex = 5;
            this.lblD.Text = "3:";
            // 
            // lblC
            // 
            this.lblC.AutoSize = true;
            this.lblC.Location = new System.Drawing.Point(249, 60);
            this.lblC.Name = "lblC";
            this.lblC.Size = new System.Drawing.Size(16, 13);
            this.lblC.TabIndex = 5;
            this.lblC.Text = "2:";
            // 
            // lblB
            // 
            this.lblB.AutoSize = true;
            this.lblB.Location = new System.Drawing.Point(132, 60);
            this.lblB.Name = "lblB";
            this.lblB.Size = new System.Drawing.Size(16, 13);
            this.lblB.TabIndex = 5;
            this.lblB.Text = "1:";
            // 
            // btnEdit
            // 
            this.btnEdit.BackColor = System.Drawing.Color.Transparent;
            this.btnEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnEdit.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnEdit.CenterImage = null;
            this.btnEdit.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnEdit.HyperlinkStyle = false;
            this.btnEdit.ImageMargin = 2;
            this.btnEdit.LeftImage = null;
            this.btnEdit.Location = new System.Drawing.Point(17, 90);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.PushStyle = true;
            this.btnEdit.RightImage = null;
            this.btnEdit.Size = new System.Drawing.Size(80, 24);
            this.btnEdit.TabIndex = 10;
            this.btnEdit.Text = "     Edit";
            this.btnEdit.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnEdit.TextLeftMargin = 2;
            this.btnEdit.TextRightMargin = 2;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // txtMake
            // 
            this.txtMake.Enabled = false;
            this.txtMake.Location = new System.Drawing.Point(135, 102);
            this.txtMake.Name = "txtMake";
            this.txtMake.Size = new System.Drawing.Size(130, 20);
            this.txtMake.TabIndex = 8;
            this.txtMake.Tag = "Make";
            this.txtMake.TextChanged += new System.EventHandler(this.txtMake_TextChanged);
            // 
            // txtModel
            // 
            this.txtModel.Enabled = false;
            this.txtModel.Location = new System.Drawing.Point(272, 102);
            this.txtModel.Name = "txtModel";
            this.txtModel.Size = new System.Drawing.Size(205, 20);
            this.txtModel.TabIndex = 9;
            this.txtModel.Tag = "Model";
            this.txtModel.TextChanged += new System.EventHandler(this.txtMake_TextChanged);
            // 
            // lblMake
            // 
            this.lblMake.AutoSize = true;
            this.lblMake.Location = new System.Drawing.Point(132, 86);
            this.lblMake.Name = "lblMake";
            this.lblMake.Size = new System.Drawing.Size(37, 13);
            this.lblMake.TabIndex = 5;
            this.lblMake.Text = "Make:";
            // 
            // txtD
            // 
            this.txtD.Enabled = false;
            this.txtD.Location = new System.Drawing.Point(389, 57);
            this.txtD.Name = "txtD";
            this.txtD.Size = new System.Drawing.Size(88, 20);
            this.txtD.TabIndex = 7;
            this.txtD.Tag = "D";
            this.txtD.TextChanged += new System.EventHandler(this.txtCoefficient_TextChanged);
            this.txtD.Leave += new System.EventHandler(this.txtCoefficient_Leave);
            // 
            // lblModel
            // 
            this.lblModel.AutoSize = true;
            this.lblModel.Location = new System.Drawing.Point(269, 86);
            this.lblModel.Name = "lblModel";
            this.lblModel.Size = new System.Drawing.Size(39, 13);
            this.lblModel.TabIndex = 5;
            this.lblModel.Text = "Model:";
            // 
            // lblA
            // 
            this.lblA.AutoSize = true;
            this.lblA.Location = new System.Drawing.Point(15, 60);
            this.lblA.Name = "lblA";
            this.lblA.Size = new System.Drawing.Size(16, 13);
            this.lblA.TabIndex = 5;
            this.lblA.Text = "0:";
            // 
            // txtC
            // 
            this.txtC.Enabled = false;
            this.txtC.Location = new System.Drawing.Point(272, 57);
            this.txtC.Name = "txtC";
            this.txtC.Size = new System.Drawing.Size(88, 20);
            this.txtC.TabIndex = 6;
            this.txtC.Tag = "C";
            this.txtC.TextChanged += new System.EventHandler(this.txtCoefficient_TextChanged);
            this.txtC.Leave += new System.EventHandler(this.txtCoefficient_Leave);
            // 
            // txtB
            // 
            this.txtB.Enabled = false;
            this.txtB.Location = new System.Drawing.Point(155, 57);
            this.txtB.Name = "txtB";
            this.txtB.Size = new System.Drawing.Size(88, 20);
            this.txtB.TabIndex = 5;
            this.txtB.Tag = "B";
            this.txtB.TextChanged += new System.EventHandler(this.txtCoefficient_TextChanged);
            this.txtB.Leave += new System.EventHandler(this.txtCoefficient_Leave);
            // 
            // txtA
            // 
            this.txtA.Enabled = false;
            this.txtA.Location = new System.Drawing.Point(38, 57);
            this.txtA.Name = "txtA";
            this.txtA.Size = new System.Drawing.Size(88, 20);
            this.txtA.TabIndex = 4;
            this.txtA.Tag = "A";
            this.txtA.TextChanged += new System.EventHandler(this.txtCoefficient_TextChanged);
            this.txtA.Leave += new System.EventHandler(this.txtCoefficient_Leave);
            // 
            // lblEqn
            // 
            this.lblEqn.AutoSize = true;
            this.lblEqn.Location = new System.Drawing.Point(14, 26);
            this.lblEqn.Margin = new System.Windows.Forms.Padding(0);
            this.lblEqn.Name = "lblEqn";
            this.lblEqn.Size = new System.Drawing.Size(289, 13);
            this.lblEqn.TabIndex = 2;
            this.lblEqn.Text = "Power (watts) = A + B * speed + C * speed^2 + D * speed^3";
            // 
            // cboEquipment
            // 
            this.cboEquipment.FormattingEnabled = true;
            this.cboEquipment.Location = new System.Drawing.Point(17, 49);
            this.cboEquipment.Name = "cboEquipment";
            this.cboEquipment.Size = new System.Drawing.Size(200, 21);
            this.cboEquipment.TabIndex = 11;
            // 
            // treeEquip
            // 
            this.treeEquip.BackColor = System.Drawing.Color.Transparent;
            this.treeEquip.Border = ZoneFiveSoftware.Common.Visuals.ControlBorder.Style.SmallRoundShadow;
            this.treeEquip.CheckBoxes = false;
            this.treeEquip.DefaultIndent = 15;
            this.treeEquip.DefaultRowHeight = -1;
            this.treeEquip.HeaderRowHeight = 21;
            this.treeEquip.Location = new System.Drawing.Point(223, 19);
            this.treeEquip.MultiSelect = false;
            this.treeEquip.Name = "treeEquip";
            this.treeEquip.NumHeaderRows = ZoneFiveSoftware.Common.Visuals.TreeList.HeaderRows.Auto;
            this.treeEquip.NumLockedColumns = 0;
            this.treeEquip.RowAlternatingColors = true;
            this.treeEquip.RowHotlightColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(10)))), ((int)(((byte)(36)))), ((int)(((byte)(106)))));
            this.treeEquip.RowHotlightColorText = System.Drawing.SystemColors.HighlightText;
            this.treeEquip.RowHotlightMouse = true;
            this.treeEquip.RowSelectedColor = System.Drawing.SystemColors.Highlight;
            this.treeEquip.RowSelectedColorText = System.Drawing.SystemColors.HighlightText;
            this.treeEquip.RowSeparatorLines = true;
            this.treeEquip.ShowLines = false;
            this.treeEquip.ShowPlusMinus = false;
            this.treeEquip.Size = new System.Drawing.Size(283, 107);
            this.treeEquip.TabIndex = 5;
            this.treeEquip.TabStop = false;
            // 
            // btnAssociateTrnr
            // 
            this.btnAssociateTrnr.BackColor = System.Drawing.Color.Transparent;
            this.btnAssociateTrnr.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAssociateTrnr.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnAssociateTrnr.CenterImage = null;
            this.btnAssociateTrnr.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnAssociateTrnr.HyperlinkStyle = false;
            this.btnAssociateTrnr.ImageMargin = 2;
            this.btnAssociateTrnr.LeftImage = null;
            this.btnAssociateTrnr.Location = new System.Drawing.Point(193, 19);
            this.btnAssociateTrnr.Name = "btnAssociateTrnr";
            this.btnAssociateTrnr.PushStyle = true;
            this.btnAssociateTrnr.RightImage = null;
            this.btnAssociateTrnr.Size = new System.Drawing.Size(24, 24);
            this.btnAssociateTrnr.TabIndex = 13;
            this.btnAssociateTrnr.Text = "+";
            this.btnAssociateTrnr.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnAssociateTrnr.TextLeftMargin = 2;
            this.btnAssociateTrnr.TextRightMargin = 2;
            this.btnAssociateTrnr.Click += new System.EventHandler(this.btnAssociateTrnr_Click);
            // 
            // btnDissociate
            // 
            this.btnDissociate.BackColor = System.Drawing.Color.Transparent;
            this.btnDissociate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDissociate.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(40)))), ((int)(((byte)(50)))), ((int)(((byte)(120)))));
            this.btnDissociate.CenterImage = null;
            this.btnDissociate.DialogResult = System.Windows.Forms.DialogResult.None;
            this.btnDissociate.HyperlinkStyle = false;
            this.btnDissociate.ImageMargin = 2;
            this.btnDissociate.LeftImage = null;
            this.btnDissociate.Location = new System.Drawing.Point(163, 19);
            this.btnDissociate.Name = "btnDissociate";
            this.btnDissociate.PushStyle = true;
            this.btnDissociate.RightImage = null;
            this.btnDissociate.Size = new System.Drawing.Size(24, 24);
            this.btnDissociate.TabIndex = 12;
            this.btnDissociate.Text = "-";
            this.btnDissociate.TextAlign = System.Drawing.StringAlignment.Center;
            this.btnDissociate.TextLeftMargin = 2;
            this.btnDissociate.TextRightMargin = 2;
            this.btnDissociate.Click += new System.EventHandler(this.btnDissociate_Click);
            // 
            // grpEquip
            // 
            this.grpEquip.Controls.Add(this.cboEquipment);
            this.grpEquip.Controls.Add(this.btnDissociate);
            this.grpEquip.Controls.Add(this.treeEquip);
            this.grpEquip.Controls.Add(this.btnAssociateTrnr);
            this.grpEquip.Location = new System.Drawing.Point(3, 461);
            this.grpEquip.Name = "grpEquip";
            this.grpEquip.Size = new System.Drawing.Size(512, 135);
            this.grpEquip.TabIndex = 7;
            this.grpEquip.TabStop = false;
            this.grpEquip.Text = "Equipment";
            // 
            // powerMenu
            // 
            this.powerMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mphMenuItem,
            this.kmhrMenuItem});
            this.powerMenu.Name = "powerMenu";
            this.powerMenu.Size = new System.Drawing.Size(108, 48);
            // 
            // mphMenuItem
            // 
            this.mphMenuItem.Name = "mphMenuItem";
            this.mphMenuItem.Size = new System.Drawing.Size(107, 22);
            this.mphMenuItem.Text = "mph";
            this.mphMenuItem.Click += new System.EventHandler(this.powerMenuItem_Click);
            // 
            // kmhrMenuItem
            // 
            this.kmhrMenuItem.Name = "kmhrMenuItem";
            this.kmhrMenuItem.Size = new System.Drawing.Size(107, 22);
            this.kmhrMenuItem.Text = "km/hr";
            this.kmhrMenuItem.Click += new System.EventHandler(this.powerMenuItem_Click);
            // 
            // lblInfo
            // 
            this.lblInfo.Location = new System.Drawing.Point(0, 599);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(515, 35);
            this.lblInfo.TabIndex = 9;
            this.lblInfo.TabStop = true;
            this.lblInfo.Text = "Trainer Power plugin can provide power info to Live Recording and HRV plugin for " +
    "live display while using a trainer.\r\n";
            this.lblInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
            // 
            // SettingsControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.grpEquip);
            this.Controls.Add(this.grpEquation);
            this.Controls.Add(this.treeTrainers);
            this.Controls.Add(this.bnrTrainerList);
            this.Controls.Add(this.bnrPowerCurve);
            this.Controls.Add(this.chartPwrCurve);
            this.Name = "SettingsControl";
            this.Size = new System.Drawing.Size(520, 687);
            this.bnrTrainerList.ResumeLayout(false);
            this.grpEquation.ResumeLayout(false);
            this.grpEquation.PerformLayout();
            this.grpEquip.ResumeLayout(false);
            this.powerMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ZoneFiveSoftware.Common.Visuals.Chart.ChartBase chartPwrCurve;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrPowerCurve;
        private ZoneFiveSoftware.Common.Visuals.ActionBanner bnrTrainerList;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeTrainers;
        private System.Windows.Forms.GroupBox grpEquation;
        private System.Windows.Forms.Label lblEqn;
        private System.Windows.Forms.ComboBox cboEquipment;
        private ZoneFiveSoftware.Common.Visuals.TreeList treeEquip;
        private ZoneFiveSoftware.Common.Visuals.Button btnAssociateTrnr;
        private ZoneFiveSoftware.Common.Visuals.Button btnDissociate;
        private System.Windows.Forms.GroupBox grpEquip;
        private System.Windows.Forms.Label lblC;
        private System.Windows.Forms.Label lblB;
        private System.Windows.Forms.Label lblA;
        private System.Windows.Forms.TextBox txtC;
        private System.Windows.Forms.TextBox txtB;
        private System.Windows.Forms.TextBox txtA;
        private System.Windows.Forms.Label lblD;
        private System.Windows.Forms.TextBox txtD;
        private ZoneFiveSoftware.Common.Visuals.Button btnEdit;
        private System.Windows.Forms.TextBox txtMake;
        private System.Windows.Forms.TextBox txtModel;
        private System.Windows.Forms.Label lblMake;
        private System.Windows.Forms.Label lblModel;
        private ZoneFiveSoftware.Common.Visuals.Button btnTrnrAdd;
        private ZoneFiveSoftware.Common.Visuals.Button btnTrnrDel;
        private System.Windows.Forms.ContextMenuStrip powerMenu;
        private System.Windows.Forms.ToolStripMenuItem mphMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kmhrMenuItem;
        private System.Windows.Forms.LinkLabel lblInfo;
    }
}
