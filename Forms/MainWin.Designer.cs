
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
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            rawFlagsEditBtn = new Button();
            groupBox2 = new GroupBox();
            flagsCategoryCombo = new ComboBox();
            groupBox1 = new GroupBox();
            flagsGroupCombo = new ComboBox();
            tabPage3 = new TabPage();
            eventWorkEditBtn = new Button();
            tabPage2 = new TabPage();
            unmarkFlagsBtn = new Button();
            markFlagsBtn = new Button();
            groupBox3 = new GroupBox();
            flySpotsChk = new CheckBox();
            collectablesChk = new CheckBox();
            pkmnGiftsChk = new CheckBox();
            specialItemsChk = new CheckBox();
            berryTreesChk = new CheckBox();
            sideEventsChk = new CheckBox();
            inGameTradesChk = new CheckBox();
            staticEncounterChk = new CheckBox();
            itemGiftsChk = new CheckBox();
            fieldItemsChk = new CheckBox();
            trainerBattlesChk = new CheckBox();
            hiddenItemsChk = new CheckBox();
            tabPage4 = new TabPage();
            tabPage5 = new TabPage();
            warnLabel = new Label();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage3.SuspendLayout();
            tabPage2.SuspendLayout();
            groupBox3.SuspendLayout();
            SuspendLayout();
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage4);
            tabControl1.Controls.Add(tabPage5);
            tabControl1.Location = new Point(2, 0);
            tabControl1.Margin = new Padding(4, 3, 4, 3);
            tabControl1.Multiline = true;
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(522, 458);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(rawFlagsEditBtn);
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Margin = new Padding(4, 3, 4, 3);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(4, 3, 4, 3);
            tabPage1.Size = new Size(514, 430);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Raw Flags Edit";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // rawFlagsEditBtn
            // 
            rawFlagsEditBtn.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rawFlagsEditBtn.Location = new Point(151, 237);
            rawFlagsEditBtn.Margin = new Padding(4, 3, 4, 3);
            rawFlagsEditBtn.Name = "rawFlagsEditBtn";
            rawFlagsEditBtn.Size = new Size(210, 27);
            rawFlagsEditBtn.TabIndex = 1;
            rawFlagsEditBtn.Text = "Edit...";
            rawFlagsEditBtn.UseVisualStyleBackColor = true;
            rawFlagsEditBtn.Click += RawFlagsEditBtn_Click;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(flagsCategoryCombo);
            groupBox2.Location = new Point(67, 133);
            groupBox2.Margin = new Padding(4, 3, 4, 3);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(4, 3, 4, 3);
            groupBox2.Size = new Size(378, 82);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Flags Category";
            // 
            // flagsCategoryCombo
            // 
            flagsCategoryCombo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flagsCategoryCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            flagsCategoryCombo.FormattingEnabled = true;
            flagsCategoryCombo.Location = new Point(18, 32);
            flagsCategoryCombo.Margin = new Padding(4, 3, 4, 3);
            flagsCategoryCombo.Name = "flagsCategoryCombo";
            flagsCategoryCombo.Size = new Size(342, 23);
            flagsCategoryCombo.TabIndex = 0;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(flagsGroupCombo);
            groupBox1.Location = new Point(67, 29);
            groupBox1.Margin = new Padding(4, 3, 4, 3);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(4, 3, 4, 3);
            groupBox1.Size = new Size(378, 82);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Flags Group";
            // 
            // flagsGroupCombo
            // 
            flagsGroupCombo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            flagsGroupCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            flagsGroupCombo.FormattingEnabled = true;
            flagsGroupCombo.Location = new Point(18, 32);
            flagsGroupCombo.Margin = new Padding(4, 3, 4, 3);
            flagsGroupCombo.Name = "flagsGroupCombo";
            flagsGroupCombo.Size = new Size(342, 23);
            flagsGroupCombo.TabIndex = 0;
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(eventWorkEditBtn);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Margin = new Padding(4, 3, 4, 3);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(4, 3, 4, 3);
            tabPage3.Size = new Size(514, 430);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Event Work Edit";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // eventWorkEditBtn
            // 
            eventWorkEditBtn.Location = new Point(151, 89);
            eventWorkEditBtn.Margin = new Padding(4, 3, 4, 3);
            eventWorkEditBtn.Name = "eventWorkEditBtn";
            eventWorkEditBtn.Size = new Size(210, 27);
            eventWorkEditBtn.TabIndex = 0;
            eventWorkEditBtn.Text = "Edit...";
            eventWorkEditBtn.UseVisualStyleBackColor = true;
            eventWorkEditBtn.Click += EventWorkEditBtn_Click;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(unmarkFlagsBtn);
            tabPage2.Controls.Add(markFlagsBtn);
            tabPage2.Controls.Add(groupBox3);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Margin = new Padding(4, 3, 4, 3);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(4, 3, 4, 3);
            tabPage2.Size = new Size(514, 430);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Bulk Flags Edit";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // unmarkFlagsBtn
            // 
            unmarkFlagsBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            unmarkFlagsBtn.Font = new Font("Segoe UI", 8F);
            unmarkFlagsBtn.Location = new Point(262, 365);
            unmarkFlagsBtn.Margin = new Padding(4, 3, 4, 3);
            unmarkFlagsBtn.Name = "unmarkFlagsBtn";
            unmarkFlagsBtn.Size = new Size(240, 27);
            unmarkFlagsBtn.TabIndex = 14;
            unmarkFlagsBtn.Text = "Bulk Unset Selected";
            unmarkFlagsBtn.UseVisualStyleBackColor = true;
            unmarkFlagsBtn.Click += UnmarkFlagsBtn_Click;
            // 
            // markFlagsBtn
            // 
            markFlagsBtn.Font = new Font("Segoe UI", 8F);
            markFlagsBtn.Location = new Point(11, 365);
            markFlagsBtn.Margin = new Padding(4, 3, 4, 3);
            markFlagsBtn.Name = "markFlagsBtn";
            markFlagsBtn.Size = new Size(240, 27);
            markFlagsBtn.TabIndex = 13;
            markFlagsBtn.Text = "Bulk Set Selected";
            markFlagsBtn.UseVisualStyleBackColor = true;
            markFlagsBtn.Click += MarkFlagsBtn_Click;
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(flySpotsChk);
            groupBox3.Controls.Add(collectablesChk);
            groupBox3.Controls.Add(pkmnGiftsChk);
            groupBox3.Controls.Add(specialItemsChk);
            groupBox3.Controls.Add(berryTreesChk);
            groupBox3.Controls.Add(sideEventsChk);
            groupBox3.Controls.Add(inGameTradesChk);
            groupBox3.Controls.Add(staticEncounterChk);
            groupBox3.Controls.Add(itemGiftsChk);
            groupBox3.Controls.Add(fieldItemsChk);
            groupBox3.Controls.Add(trainerBattlesChk);
            groupBox3.Controls.Add(hiddenItemsChk);
            groupBox3.Location = new Point(157, 10);
            groupBox3.Margin = new Padding(4, 3, 4, 3);
            groupBox3.Name = "groupBox3";
            groupBox3.Padding = new Padding(4, 3, 4, 3);
            groupBox3.Size = new Size(198, 332);
            groupBox3.TabIndex = 12;
            groupBox3.TabStop = false;
            // 
            // flySpotsChk
            // 
            flySpotsChk.AutoSize = true;
            flySpotsChk.Location = new Point(7, 254);
            flySpotsChk.Margin = new Padding(4, 3, 4, 3);
            flySpotsChk.Name = "flySpotsChk";
            flySpotsChk.Size = new Size(73, 19);
            flySpotsChk.TabIndex = 12;
            flySpotsChk.Text = "Fly Spots";
            flySpotsChk.UseVisualStyleBackColor = true;
            // 
            // collectablesChk
            // 
            collectablesChk.AutoSize = true;
            collectablesChk.Location = new Point(7, 307);
            collectablesChk.Margin = new Padding(4, 3, 4, 3);
            collectablesChk.Name = "collectablesChk";
            collectablesChk.Size = new Size(90, 19);
            collectablesChk.TabIndex = 11;
            collectablesChk.Text = "Collectables";
            collectablesChk.UseVisualStyleBackColor = true;
            // 
            // pkmnGiftsChk
            // 
            pkmnGiftsChk.AutoSize = true;
            pkmnGiftsChk.Location = new Point(7, 119);
            pkmnGiftsChk.Margin = new Padding(4, 3, 4, 3);
            pkmnGiftsChk.Name = "pkmnGiftsChk";
            pkmnGiftsChk.Size = new Size(87, 19);
            pkmnGiftsChk.TabIndex = 4;
            pkmnGiftsChk.Text = "PKMN Gifts";
            pkmnGiftsChk.UseVisualStyleBackColor = true;
            // 
            // specialItemsChk
            // 
            specialItemsChk.AutoSize = true;
            specialItemsChk.Location = new Point(7, 65);
            specialItemsChk.Margin = new Padding(4, 3, 4, 3);
            specialItemsChk.Name = "specialItemsChk";
            specialItemsChk.Size = new Size(95, 19);
            specialItemsChk.TabIndex = 9;
            specialItemsChk.Text = "Special Items";
            specialItemsChk.UseVisualStyleBackColor = true;
            // 
            // berryTreesChk
            // 
            berryTreesChk.AutoSize = true;
            berryTreesChk.Location = new Point(7, 280);
            berryTreesChk.Margin = new Padding(4, 3, 4, 3);
            berryTreesChk.Name = "berryTreesChk";
            berryTreesChk.Size = new Size(83, 19);
            berryTreesChk.TabIndex = 10;
            berryTreesChk.Text = "Berry Trees";
            berryTreesChk.UseVisualStyleBackColor = true;
            // 
            // sideEventsChk
            // 
            sideEventsChk.AutoSize = true;
            sideEventsChk.Location = new Point(7, 227);
            sideEventsChk.Margin = new Padding(4, 3, 4, 3);
            sideEventsChk.Name = "sideEventsChk";
            sideEventsChk.Size = new Size(85, 19);
            sideEventsChk.TabIndex = 8;
            sideEventsChk.Text = "Side Events";
            sideEventsChk.UseVisualStyleBackColor = true;
            // 
            // inGameTradesChk
            // 
            inGameTradesChk.AutoSize = true;
            inGameTradesChk.Location = new Point(7, 201);
            inGameTradesChk.Margin = new Padding(4, 3, 4, 3);
            inGameTradesChk.Name = "inGameTradesChk";
            inGameTradesChk.Size = new Size(109, 19);
            inGameTradesChk.TabIndex = 7;
            inGameTradesChk.Text = "In-Game Trades";
            inGameTradesChk.UseVisualStyleBackColor = true;
            // 
            // staticEncounterChk
            // 
            staticEncounterChk.AutoSize = true;
            staticEncounterChk.Location = new Point(7, 173);
            staticEncounterChk.Margin = new Padding(4, 3, 4, 3);
            staticEncounterChk.Name = "staticEncounterChk";
            staticEncounterChk.Size = new Size(93, 19);
            staticEncounterChk.TabIndex = 6;
            staticEncounterChk.Text = "Static Battles";
            staticEncounterChk.UseVisualStyleBackColor = true;
            // 
            // itemGiftsChk
            // 
            itemGiftsChk.AutoSize = true;
            itemGiftsChk.Location = new Point(7, 91);
            itemGiftsChk.Margin = new Padding(4, 3, 4, 3);
            itemGiftsChk.Name = "itemGiftsChk";
            itemGiftsChk.Size = new Size(77, 19);
            itemGiftsChk.TabIndex = 3;
            itemGiftsChk.Text = "Item Gifts";
            itemGiftsChk.UseVisualStyleBackColor = true;
            // 
            // fieldItemsChk
            // 
            fieldItemsChk.AutoSize = true;
            fieldItemsChk.Location = new Point(7, 12);
            fieldItemsChk.Margin = new Padding(4, 3, 4, 3);
            fieldItemsChk.Name = "fieldItemsChk";
            fieldItemsChk.Size = new Size(83, 19);
            fieldItemsChk.TabIndex = 1;
            fieldItemsChk.Text = "Field Items";
            fieldItemsChk.UseVisualStyleBackColor = true;
            // 
            // trainerBattlesChk
            // 
            trainerBattlesChk.AutoSize = true;
            trainerBattlesChk.Location = new Point(7, 145);
            trainerBattlesChk.Margin = new Padding(4, 3, 4, 3);
            trainerBattlesChk.Name = "trainerBattlesChk";
            trainerBattlesChk.Size = new Size(67, 19);
            trainerBattlesChk.TabIndex = 5;
            trainerBattlesChk.Text = "Trainers";
            trainerBattlesChk.UseVisualStyleBackColor = true;
            // 
            // hiddenItemsChk
            // 
            hiddenItemsChk.AutoSize = true;
            hiddenItemsChk.Location = new Point(7, 38);
            hiddenItemsChk.Margin = new Padding(4, 3, 4, 3);
            hiddenItemsChk.Name = "hiddenItemsChk";
            hiddenItemsChk.Size = new Size(97, 19);
            hiddenItemsChk.TabIndex = 2;
            hiddenItemsChk.Text = "Hidden Items";
            hiddenItemsChk.UseVisualStyleBackColor = true;
            // 
            // tabPage4
            // 
            tabPage4.Location = new Point(4, 24);
            tabPage4.Margin = new Padding(4, 3, 4, 3);
            tabPage4.Name = "tabPage4";
            tabPage4.Padding = new Padding(4, 3, 4, 3);
            tabPage4.Size = new Size(514, 430);
            tabPage4.TabIndex = 3;
            tabPage4.Text = "Special Edit";
            tabPage4.UseVisualStyleBackColor = true;
            // 
            // tabPage5
            // 
            tabPage5.Location = new Point(4, 24);
            tabPage5.Margin = new Padding(4, 3, 4, 3);
            tabPage5.Name = "tabPage5";
            tabPage5.Padding = new Padding(4, 3, 4, 3);
            tabPage5.Size = new Size(514, 430);
            tabPage5.TabIndex = 4;
            tabPage5.Text = "Misc Edit";
            tabPage5.UseVisualStyleBackColor = true;
            // 
            // warnLabel
            // 
            warnLabel.AutoSize = true;
            warnLabel.ForeColor = Color.DarkRed;
            warnLabel.Location = new Point(5, 459);
            warnLabel.Margin = new Padding(4, 0, 4, 0);
            warnLabel.MaximumSize = new Size(408, 0);
            warnLabel.Name = "warnLabel";
            warnLabel.Size = new Size(250, 45);
            warnLabel.TabIndex = 1;
            warnLabel.Text = "Changing the flags values may cause softlock,\r\ncrashes, and permanent data loss.\r\nSave file backups is strongly recommended.";
            // 
            // MainWin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(525, 509);
            Controls.Add(warnLabel);
            Controls.Add(tabControl1);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(541, 548);
            MinimumSize = new Size(541, 548);
            Name = "MainWin";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Flags Editor EX";
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            groupBox2.ResumeLayout(false);
            groupBox1.ResumeLayout(false);
            tabPage3.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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