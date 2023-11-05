
namespace FlagsEditorEXPlugin.Forms
{
    partial class MainWin
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rawFlagsEditBtn = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flagsCategoryCombo = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flagsGroupCombo = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.eventWorkEditBtn = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.unmarkFlagsBtn = new System.Windows.Forms.Button();
            this.markFlagsBtn = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flySpotsChk = new System.Windows.Forms.CheckBox();
            this.collectablesChk = new System.Windows.Forms.CheckBox();
            this.pkmnGiftsChk = new System.Windows.Forms.CheckBox();
            this.specialItemsChk = new System.Windows.Forms.CheckBox();
            this.berryTreesChk = new System.Windows.Forms.CheckBox();
            this.sideEventsChk = new System.Windows.Forms.CheckBox();
            this.inGameTradesChk = new System.Windows.Forms.CheckBox();
            this.staticEncounterChk = new System.Windows.Forms.CheckBox();
            this.itemGiftsChk = new System.Windows.Forms.CheckBox();
            this.fieldItemsChk = new System.Windows.Forms.CheckBox();
            this.trainerBattlesChk = new System.Windows.Forms.CheckBox();
            this.hiddenItemsChk = new System.Windows.Forms.CheckBox();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.warnLabel = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Multiline = true;
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(362, 397);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rawFlagsEditBtn);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 40);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(354, 353);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Raw Flags Edit";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rawFlagsEditBtn
            // 
            this.rawFlagsEditBtn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rawFlagsEditBtn.Location = new System.Drawing.Point(87, 205);
            this.rawFlagsEditBtn.Name = "rawFlagsEditBtn";
            this.rawFlagsEditBtn.Size = new System.Drawing.Size(180, 23);
            this.rawFlagsEditBtn.TabIndex = 1;
            this.rawFlagsEditBtn.Text = "Edit...";
            this.rawFlagsEditBtn.UseVisualStyleBackColor = true;
            this.rawFlagsEditBtn.Click += new System.EventHandler(this.rawFlagsEditBtn_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.flagsCategoryCombo);
            this.groupBox2.Location = new System.Drawing.Point(15, 115);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(324, 71);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Flags Category";
            // 
            // flagsCategoryCombo
            // 
            this.flagsCategoryCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsCategoryCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flagsCategoryCombo.FormattingEnabled = true;
            this.flagsCategoryCombo.Location = new System.Drawing.Point(15, 28);
            this.flagsCategoryCombo.Name = "flagsCategoryCombo";
            this.flagsCategoryCombo.Size = new System.Drawing.Size(294, 21);
            this.flagsCategoryCombo.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.flagsGroupCombo);
            this.groupBox1.Location = new System.Drawing.Point(15, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(324, 71);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Flags Group";
            // 
            // flagsGroupCombo
            // 
            this.flagsGroupCombo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.flagsGroupCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.flagsGroupCombo.FormattingEnabled = true;
            this.flagsGroupCombo.Location = new System.Drawing.Point(15, 28);
            this.flagsGroupCombo.Name = "flagsGroupCombo";
            this.flagsGroupCombo.Size = new System.Drawing.Size(294, 21);
            this.flagsGroupCombo.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.eventWorkEditBtn);
            this.tabPage3.Location = new System.Drawing.Point(4, 40);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(354, 353);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Event Work Edit";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // eventWorkEditBtn
            // 
            this.eventWorkEditBtn.Location = new System.Drawing.Point(87, 77);
            this.eventWorkEditBtn.Name = "eventWorkEditBtn";
            this.eventWorkEditBtn.Size = new System.Drawing.Size(180, 23);
            this.eventWorkEditBtn.TabIndex = 0;
            this.eventWorkEditBtn.Text = "Edit...";
            this.eventWorkEditBtn.UseVisualStyleBackColor = true;
            this.eventWorkEditBtn.Click += new System.EventHandler(this.eventWorkEditBtn_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.unmarkFlagsBtn);
            this.tabPage2.Controls.Add(this.markFlagsBtn);
            this.tabPage2.Controls.Add(this.groupBox3);
            this.tabPage2.Location = new System.Drawing.Point(4, 40);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(354, 353);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bulk Flags Edit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // unmarkFlagsBtn
            // 
            this.unmarkFlagsBtn.Location = new System.Drawing.Point(190, 316);
            this.unmarkFlagsBtn.Name = "unmarkFlagsBtn";
            this.unmarkFlagsBtn.Size = new System.Drawing.Size(125, 23);
            this.unmarkFlagsBtn.TabIndex = 14;
            this.unmarkFlagsBtn.Text = "Bulk Unset Selected";
            this.unmarkFlagsBtn.UseVisualStyleBackColor = true;
            this.unmarkFlagsBtn.Click += new System.EventHandler(this.unmarkFlagsBtn_Click);
            // 
            // markFlagsBtn
            // 
            this.markFlagsBtn.Location = new System.Drawing.Point(40, 316);
            this.markFlagsBtn.Name = "markFlagsBtn";
            this.markFlagsBtn.Size = new System.Drawing.Size(125, 23);
            this.markFlagsBtn.TabIndex = 13;
            this.markFlagsBtn.Text = "Bulk Set Selected";
            this.markFlagsBtn.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flySpotsChk);
            this.groupBox3.Controls.Add(this.collectablesChk);
            this.groupBox3.Controls.Add(this.pkmnGiftsChk);
            this.groupBox3.Controls.Add(this.specialItemsChk);
            this.groupBox3.Controls.Add(this.berryTreesChk);
            this.groupBox3.Controls.Add(this.sideEventsChk);
            this.groupBox3.Controls.Add(this.inGameTradesChk);
            this.groupBox3.Controls.Add(this.staticEncounterChk);
            this.groupBox3.Controls.Add(this.itemGiftsChk);
            this.groupBox3.Controls.Add(this.fieldItemsChk);
            this.groupBox3.Controls.Add(this.trainerBattlesChk);
            this.groupBox3.Controls.Add(this.hiddenItemsChk);
            this.groupBox3.Location = new System.Drawing.Point(95, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(170, 288);
            this.groupBox3.TabIndex = 12;
            this.groupBox3.TabStop = false;
            // 
            // flySpotsChk
            // 
            this.flySpotsChk.AutoSize = true;
            this.flySpotsChk.Location = new System.Drawing.Point(6, 220);
            this.flySpotsChk.Name = "flySpotsChk";
            this.flySpotsChk.Size = new System.Drawing.Size(69, 17);
            this.flySpotsChk.TabIndex = 12;
            this.flySpotsChk.Text = "Fly Spots";
            this.flySpotsChk.UseVisualStyleBackColor = true;
            // 
            // collectablesChk
            // 
            this.collectablesChk.AutoSize = true;
            this.collectablesChk.Location = new System.Drawing.Point(6, 266);
            this.collectablesChk.Name = "collectablesChk";
            this.collectablesChk.Size = new System.Drawing.Size(83, 17);
            this.collectablesChk.TabIndex = 11;
            this.collectablesChk.Text = "Collectables";
            this.collectablesChk.UseVisualStyleBackColor = true;
            // 
            // pkmnGiftsChk
            // 
            this.pkmnGiftsChk.AutoSize = true;
            this.pkmnGiftsChk.Location = new System.Drawing.Point(6, 103);
            this.pkmnGiftsChk.Name = "pkmnGiftsChk";
            this.pkmnGiftsChk.Size = new System.Drawing.Size(81, 17);
            this.pkmnGiftsChk.TabIndex = 4;
            this.pkmnGiftsChk.Text = "PKMN Gifts";
            this.pkmnGiftsChk.UseVisualStyleBackColor = true;
            // 
            // specialItemsChk
            // 
            this.specialItemsChk.AutoSize = true;
            this.specialItemsChk.Location = new System.Drawing.Point(6, 56);
            this.specialItemsChk.Name = "specialItemsChk";
            this.specialItemsChk.Size = new System.Drawing.Size(89, 17);
            this.specialItemsChk.TabIndex = 9;
            this.specialItemsChk.Text = "Special Items";
            this.specialItemsChk.UseVisualStyleBackColor = true;
            // 
            // berryTreesChk
            // 
            this.berryTreesChk.AutoSize = true;
            this.berryTreesChk.Location = new System.Drawing.Point(6, 243);
            this.berryTreesChk.Name = "berryTreesChk";
            this.berryTreesChk.Size = new System.Drawing.Size(80, 17);
            this.berryTreesChk.TabIndex = 10;
            this.berryTreesChk.Text = "Berry Trees";
            this.berryTreesChk.UseVisualStyleBackColor = true;
            // 
            // sideEventsChk
            // 
            this.sideEventsChk.AutoSize = true;
            this.sideEventsChk.Location = new System.Drawing.Point(6, 197);
            this.sideEventsChk.Name = "sideEventsChk";
            this.sideEventsChk.Size = new System.Drawing.Size(83, 17);
            this.sideEventsChk.TabIndex = 8;
            this.sideEventsChk.Text = "Side Events";
            this.sideEventsChk.UseVisualStyleBackColor = true;
            // 
            // inGameTradesChk
            // 
            this.inGameTradesChk.AutoSize = true;
            this.inGameTradesChk.Location = new System.Drawing.Point(6, 174);
            this.inGameTradesChk.Name = "inGameTradesChk";
            this.inGameTradesChk.Size = new System.Drawing.Size(102, 17);
            this.inGameTradesChk.TabIndex = 7;
            this.inGameTradesChk.Text = "In-Game Trades";
            this.inGameTradesChk.UseVisualStyleBackColor = true;
            // 
            // staticEncounterChk
            // 
            this.staticEncounterChk.AutoSize = true;
            this.staticEncounterChk.Location = new System.Drawing.Point(6, 150);
            this.staticEncounterChk.Name = "staticEncounterChk";
            this.staticEncounterChk.Size = new System.Drawing.Size(88, 17);
            this.staticEncounterChk.TabIndex = 6;
            this.staticEncounterChk.Text = "Static Battles";
            this.staticEncounterChk.UseVisualStyleBackColor = true;
            // 
            // itemGiftsChk
            // 
            this.itemGiftsChk.AutoSize = true;
            this.itemGiftsChk.Location = new System.Drawing.Point(6, 79);
            this.itemGiftsChk.Name = "itemGiftsChk";
            this.itemGiftsChk.Size = new System.Drawing.Size(70, 17);
            this.itemGiftsChk.TabIndex = 3;
            this.itemGiftsChk.Text = "Item Gifts";
            this.itemGiftsChk.UseVisualStyleBackColor = true;
            // 
            // fieldItemsChk
            // 
            this.fieldItemsChk.AutoSize = true;
            this.fieldItemsChk.Location = new System.Drawing.Point(6, 10);
            this.fieldItemsChk.Name = "fieldItemsChk";
            this.fieldItemsChk.Size = new System.Drawing.Size(76, 17);
            this.fieldItemsChk.TabIndex = 1;
            this.fieldItemsChk.Text = "Field Items";
            this.fieldItemsChk.UseVisualStyleBackColor = true;
            // 
            // trainerBattlesChk
            // 
            this.trainerBattlesChk.AutoSize = true;
            this.trainerBattlesChk.Location = new System.Drawing.Point(6, 126);
            this.trainerBattlesChk.Name = "trainerBattlesChk";
            this.trainerBattlesChk.Size = new System.Drawing.Size(64, 17);
            this.trainerBattlesChk.TabIndex = 5;
            this.trainerBattlesChk.Text = "Trainers";
            this.trainerBattlesChk.UseVisualStyleBackColor = true;
            // 
            // hiddenItemsChk
            // 
            this.hiddenItemsChk.AutoSize = true;
            this.hiddenItemsChk.Location = new System.Drawing.Point(6, 33);
            this.hiddenItemsChk.Name = "hiddenItemsChk";
            this.hiddenItemsChk.Size = new System.Drawing.Size(88, 17);
            this.hiddenItemsChk.TabIndex = 2;
            this.hiddenItemsChk.Text = "Hidden Items";
            this.hiddenItemsChk.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            this.tabPage4.Location = new System.Drawing.Point(4, 40);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(354, 353);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Special Edit";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            this.tabPage5.Location = new System.Drawing.Point(4, 40);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(354, 353);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Misc Edit";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // warnLabel
            // 
            this.warnLabel.AutoSize = true;
            this.warnLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.warnLabel.Location = new System.Drawing.Point(4, 398);
            this.warnLabel.MaximumSize = new System.Drawing.Size(350, 0);
            this.warnLabel.Name = "warnLabel";
            this.warnLabel.Size = new System.Drawing.Size(226, 39);
            this.warnLabel.TabIndex = 1;
            this.warnLabel.Text = "Changing the flags values may cause softlock,\r\ncrashes, and permanent data loss.\r" +
    "\nSave file backups is strongly recommended.";
            // 
            // MainWin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 441);
            this.Controls.Add(this.warnLabel);
            this.Controls.Add(this.tabControl1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(380, 480);
            this.MinimumSize = new System.Drawing.Size(380, 480);
            this.Name = "MainWin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Flags Editor EX";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox flagsCategoryCombo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button rawFlagsEditBtn;
        private System.Windows.Forms.ComboBox flagsGroupCombo;
        private System.Windows.Forms.TabPage tabPage4;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.Label warnLabel;
        private System.Windows.Forms.Button eventWorkEditBtn;
        private System.Windows.Forms.Button unmarkFlagsBtn;
        private System.Windows.Forms.Button markFlagsBtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox flySpotsChk;
        private System.Windows.Forms.CheckBox collectablesChk;
        private System.Windows.Forms.CheckBox pkmnGiftsChk;
        private System.Windows.Forms.CheckBox specialItemsChk;
        private System.Windows.Forms.CheckBox berryTreesChk;
        private System.Windows.Forms.CheckBox sideEventsChk;
        private System.Windows.Forms.CheckBox inGameTradesChk;
        private System.Windows.Forms.CheckBox staticEncounterChk;
        private System.Windows.Forms.CheckBox itemGiftsChk;
        private System.Windows.Forms.CheckBox fieldItemsChk;
        private System.Windows.Forms.CheckBox trainerBattlesChk;
        private System.Windows.Forms.CheckBox hiddenItemsChk;
    }
}