namespace FlagsEditorEXPlugin.Forms
{
    partial class DailyHiddenItemsEditorSV
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
            filterByItemChk = new CheckBox();
            restoreBtn = new Button();
            setAllBtn = new Button();
            cancelBtn = new Button();
            saveBtn = new Button();
            dataGridView = new DataGridView();
            blockSourceCombo = new ComboBox();
            selectBlockLabel = new Label();
            itemIIndexSelectionCombo = new ComboBox();
            dgv_ref = new DataGridViewTextBoxColumn();
            dgv_location = new DataGridViewTextBoxColumn();
            dgv_item = new DataGridViewComboBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // filterByItemChk
            // 
            filterByItemChk.AutoSize = true;
            filterByItemChk.Location = new Point(543, 477);
            filterByItemChk.Margin = new Padding(4, 3, 4, 3);
            filterByItemChk.Name = "filterByItemChk";
            filterByItemChk.Size = new Size(95, 19);
            filterByItemChk.TabIndex = 23;
            filterByItemChk.Text = "Filter by item";
            filterByItemChk.UseVisualStyleBackColor = true;
            filterByItemChk.CheckedChanged += FilterByItemChk_CheckedChanged;
            // 
            // restoreBtn
            // 
            restoreBtn.Location = new Point(198, 503);
            restoreBtn.Margin = new Padding(4, 3, 4, 3);
            restoreBtn.Name = "restoreBtn";
            restoreBtn.Size = new Size(110, 27);
            restoreBtn.TabIndex = 20;
            restoreBtn.Text = "Restore State";
            restoreBtn.UseVisualStyleBackColor = true;
            restoreBtn.Click += RestoreBtn_Click;
            // 
            // setAllBtn
            // 
            setAllBtn.Location = new Point(403, 503);
            setAllBtn.Margin = new Padding(4, 3, 4, 3);
            setAllBtn.Name = "setAllBtn";
            setAllBtn.Size = new Size(110, 27);
            setAllBtn.TabIndex = 18;
            setAllBtn.Text = "Set All to";
            setAllBtn.UseVisualStyleBackColor = true;
            setAllBtn.Click += SetAllBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(836, 503);
            cancelBtn.Margin = new Padding(4, 3, 4, 3);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(110, 27);
            cancelBtn.TabIndex = 17;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += CancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(836, 470);
            saveBtn.Margin = new Padding(4, 3, 4, 3);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(110, 27);
            saveBtn.TabIndex = 16;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += SaveBtn_Click;
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { dgv_ref, dgv_location, dgv_item });
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.Location = new Point(13, 46);
            dataGridView.Margin = new Padding(4, 3, 4, 3);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(933, 415);
            dataGridView.TabIndex = 15;
            // 
            // blockSourceCombo
            // 
            blockSourceCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            blockSourceCombo.FormattingEnabled = true;
            blockSourceCombo.Location = new Point(301, 12);
            blockSourceCombo.Name = "blockSourceCombo";
            blockSourceCombo.Size = new Size(277, 23);
            blockSourceCombo.TabIndex = 25;
            blockSourceCombo.SelectedIndexChanged += BlockSourceCombo_SelectedIndexChanged;
            // 
            // selectBlockLabel
            // 
            selectBlockLabel.AutoSize = true;
            selectBlockLabel.Location = new Point(187, 15);
            selectBlockLabel.Name = "selectBlockLabel";
            selectBlockLabel.Size = new Size(108, 15);
            selectBlockLabel.TabIndex = 26;
            selectBlockLabel.Text = "Select block source";
            selectBlockLabel.TextAlign = ContentAlignment.TopRight;
            // 
            // itemIIndexSelectionCombo
            // 
            itemIIndexSelectionCombo.DropDownStyle = ComboBoxStyle.DropDownList;
            itemIIndexSelectionCombo.FormattingEnabled = true;
            itemIIndexSelectionCombo.Location = new Point(543, 503);
            itemIIndexSelectionCombo.Name = "itemIIndexSelectionCombo";
            itemIIndexSelectionCombo.Size = new Size(250, 23);
            itemIIndexSelectionCombo.TabIndex = 27;
            itemIIndexSelectionCombo.SelectedIndexChanged += ItemIIndexSelectionCombo_SelectedIndexChanged;
            // 
            // dgv_ref
            // 
            dgv_ref.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_ref.HeaderText = "Ref";
            dgv_ref.Name = "dgv_ref";
            dgv_ref.ReadOnly = true;
            dgv_ref.Width = 49;
            // 
            // dgv_location
            // 
            dgv_location.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_location.HeaderText = "Location";
            dgv_location.Name = "dgv_location";
            dgv_location.ReadOnly = true;
            dgv_location.Width = 78;
            // 
            // dgv_item
            // 
            dgv_item.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_item.HeaderText = "Hidden Item";
            dgv_item.Items.AddRange(new object[] { "Custom" });
            dgv_item.MaxDropDownItems = 100;
            dgv_item.Name = "dgv_item";
            dgv_item.Width = 79;
            // 
            // DailyHiddenItemsEditorSV
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 540);
            Controls.Add(itemIIndexSelectionCombo);
            Controls.Add(selectBlockLabel);
            Controls.Add(blockSourceCombo);
            Controls.Add(filterByItemChk);
            Controls.Add(restoreBtn);
            Controls.Add(setAllBtn);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(dataGridView);
            MaximumSize = new Size(977, 579);
            MinimumSize = new Size(977, 579);
            Name = "DailyHiddenItemsEditorSV";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Daily Hidden Items Editor";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private CheckBox filterByItemChk;
        private Button restoreBtn;
        private Button setAllBtn;
        private Button cancelBtn;
        private Button saveBtn;
        private DataGridView dataGridView;
        private ComboBox blockSourceCombo;
        private Label selectBlockLabel;
        private ComboBox itemIIndexSelectionCombo;
        private DataGridViewTextBoxColumn dgv_ref;
        private DataGridViewTextBoxColumn dgv_location;
        private DataGridViewComboBoxColumn dgv_item;
    }
}