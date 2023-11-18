
namespace FlagsEditorEXPlugin.Forms
{
    partial class EventWorkEditor
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
            filterUnusedChk = new CheckBox();
            restoreBtn = new Button();
            cancelBtn = new Button();
            saveBtn = new Button();
            dataGridView = new DataGridView();
            dgv_ref = new DataGridViewTextBoxColumn();
            dgv_id = new DataGridViewTextBoxColumn();
            dgv_location = new DataGridViewTextBoxColumn();
            dgv_txtDesc = new DataGridViewTextBoxColumn();
            dgv_validValues = new DataGridViewComboBoxColumn();
            dgv_rawValue = new DataGridViewTextBoxColumn();
            searchTermBox = new TextBox();
            filterBySearchChk = new CheckBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // filterUnusedChk
            // 
            filterUnusedChk.AutoSize = true;
            filterUnusedChk.Checked = true;
            filterUnusedChk.CheckState = CheckState.Checked;
            filterUnusedChk.Location = new Point(18, 508);
            filterUnusedChk.Margin = new Padding(4, 3, 4, 3);
            filterUnusedChk.Name = "filterUnusedChk";
            filterUnusedChk.Size = new Size(94, 19);
            filterUnusedChk.TabIndex = 15;
            filterUnusedChk.Text = "Hide Unused";
            filterUnusedChk.UseVisualStyleBackColor = true;
            filterUnusedChk.CheckedChanged += FilterUnusedChk_CheckedChanged;
            // 
            // restoreBtn
            // 
            restoreBtn.Location = new Point(198, 503);
            restoreBtn.Margin = new Padding(4, 3, 4, 3);
            restoreBtn.Name = "restoreBtn";
            restoreBtn.Size = new Size(110, 27);
            restoreBtn.TabIndex = 14;
            restoreBtn.Text = "Restore State";
            restoreBtn.UseVisualStyleBackColor = true;
            restoreBtn.Click += RestoreBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(838, 503);
            cancelBtn.Margin = new Padding(4, 3, 4, 3);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(110, 27);
            cancelBtn.TabIndex = 13;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += CancelBtn_Click;
            // 
            // saveBtn
            // 
            saveBtn.Location = new Point(838, 470);
            saveBtn.Margin = new Padding(4, 3, 4, 3);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(110, 27);
            saveBtn.TabIndex = 12;
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
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { dgv_ref, dgv_id, dgv_location, dgv_txtDesc, dgv_validValues, dgv_rawValue });
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.Location = new Point(14, 14);
            dataGridView.Margin = new Padding(4, 3, 4, 3);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(933, 449);
            dataGridView.TabIndex = 16;
            // 
            // dgv_ref
            // 
            dgv_ref.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_ref.HeaderText = "Ref";
            dgv_ref.Name = "dgv_ref";
            dgv_ref.ReadOnly = true;
            dgv_ref.Width = 49;
            // 
            // dgv_id
            // 
            dgv_id.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_id.HeaderText = "Identifier";
            dgv_id.Name = "dgv_id";
            dgv_id.ReadOnly = true;
            dgv_id.Width = 79;
            // 
            // dgv_location
            // 
            dgv_location.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_location.HeaderText = "Location";
            dgv_location.Name = "dgv_location";
            dgv_location.ReadOnly = true;
            dgv_location.Width = 78;
            // 
            // dgv_txtDesc
            // 
            dgv_txtDesc.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_txtDesc.HeaderText = "Description";
            dgv_txtDesc.Name = "dgv_txtDesc";
            dgv_txtDesc.ReadOnly = true;
            dgv_txtDesc.Width = 92;
            // 
            // dgv_validValues
            // 
            dgv_validValues.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_validValues.HeaderText = "Valid Values";
            dgv_validValues.Items.AddRange(new object[] { "Custom" });
            dgv_validValues.Name = "dgv_validValues";
            dgv_validValues.Width = 74;
            // 
            // dgv_rawValue
            // 
            dgv_rawValue.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dgv_rawValue.HeaderText = "Raw Value";
            dgv_rawValue.Name = "dgv_rawValue";
            // 
            // searchTermBox
            // 
            searchTermBox.Location = new Point(545, 503);
            searchTermBox.Margin = new Padding(4, 3, 4, 3);
            searchTermBox.Name = "searchTermBox";
            searchTermBox.Size = new Size(250, 23);
            searchTermBox.TabIndex = 18;
            searchTermBox.KeyDown += SearchTermBox_KeyDown;
            // 
            // filterBySearchChk
            // 
            filterBySearchChk.AutoSize = true;
            filterBySearchChk.Location = new Point(545, 477);
            filterBySearchChk.Margin = new Padding(4, 3, 4, 3);
            filterBySearchChk.Name = "filterBySearchChk";
            filterBySearchChk.Size = new Size(133, 19);
            filterBySearchChk.TabIndex = 17;
            filterBySearchChk.Text = "Filter by search term";
            filterBySearchChk.UseVisualStyleBackColor = true;
            filterBySearchChk.CheckedChanged += FilterBySearchChk_CheckedChanged;
            // 
            // EventWorkEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 540);
            Controls.Add(searchTermBox);
            Controls.Add(filterBySearchChk);
            Controls.Add(dataGridView);
            Controls.Add(filterUnusedChk);
            Controls.Add(restoreBtn);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Margin = new Padding(4, 3, 4, 3);
            MaximumSize = new Size(977, 579);
            MinimumSize = new Size(977, 579);
            Name = "EventWorkEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Event Work Editor";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.CheckBox filterUnusedChk;
        private System.Windows.Forms.Button restoreBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_location;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_txtDesc;
        private System.Windows.Forms.DataGridViewComboBoxColumn dgv_validValues;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_rawValue;
        private System.Windows.Forms.TextBox searchTermBox;
        private System.Windows.Forms.CheckBox filterBySearchChk;
    }
}