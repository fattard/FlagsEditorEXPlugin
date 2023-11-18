
namespace FlagsEditorEXPlugin.Forms
{
    partial class FlagsEditor
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
            dataGridView = new DataGridView();
            dgv_val = new DataGridViewCheckBoxColumn();
            dgv_ref = new DataGridViewTextBoxColumn();
            dgv_id = new DataGridViewTextBoxColumn();
            dgv_location = new DataGridViewTextBoxColumn();
            dgv_txtDesc = new DataGridViewTextBoxColumn();
            saveBtn = new Button();
            cancelBtn = new Button();
            setAllBtn = new Button();
            unsetAllBtn = new Button();
            restoreBtn = new Button();
            totalSetLabel = new Label();
            totalUnsetLabel = new Label();
            numUnsetTxt = new Label();
            numSetTxt = new Label();
            filterUnusedChk = new CheckBox();
            showOnlySetChk = new CheckBox();
            showOnlyUnsetChk = new CheckBox();
            filterBySearchChk = new CheckBox();
            searchTermBox = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.AllowUserToResizeColumns = false;
            dataGridView.AllowUserToResizeRows = false;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { dgv_val, dgv_ref, dgv_id, dgv_location, dgv_txtDesc });
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.Location = new Point(14, 14);
            dataGridView.Margin = new Padding(4, 3, 4, 3);
            dataGridView.MultiSelect = false;
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(933, 412);
            dataGridView.TabIndex = 0;
            // 
            // dgv_val
            // 
            dgv_val.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgv_val.HeaderText = "Value";
            dgv_val.Name = "dgv_val";
            dgv_val.Width = 41;
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
            // saveBtn
            // 
            saveBtn.Location = new Point(838, 440);
            saveBtn.Margin = new Padding(4, 3, 4, 3);
            saveBtn.Name = "saveBtn";
            saveBtn.Size = new Size(110, 27);
            saveBtn.TabIndex = 1;
            saveBtn.Text = "Save";
            saveBtn.UseVisualStyleBackColor = true;
            saveBtn.Click += SaveBtn_Click;
            // 
            // cancelBtn
            // 
            cancelBtn.Location = new Point(838, 473);
            cancelBtn.Margin = new Padding(4, 3, 4, 3);
            cancelBtn.Name = "cancelBtn";
            cancelBtn.Size = new Size(110, 27);
            cancelBtn.TabIndex = 2;
            cancelBtn.Text = "Cancel";
            cancelBtn.UseVisualStyleBackColor = true;
            cancelBtn.Click += CancelBtn_Click;
            // 
            // setAllBtn
            // 
            setAllBtn.Location = new Point(198, 438);
            setAllBtn.Margin = new Padding(4, 3, 4, 3);
            setAllBtn.Name = "setAllBtn";
            setAllBtn.Size = new Size(110, 27);
            setAllBtn.TabIndex = 3;
            setAllBtn.Text = "Set All";
            setAllBtn.UseVisualStyleBackColor = true;
            setAllBtn.Click += SetAllBtn_Click;
            // 
            // unsetAllBtn
            // 
            unsetAllBtn.Location = new Point(198, 472);
            unsetAllBtn.Margin = new Padding(4, 3, 4, 3);
            unsetAllBtn.Name = "unsetAllBtn";
            unsetAllBtn.Size = new Size(110, 27);
            unsetAllBtn.TabIndex = 4;
            unsetAllBtn.Text = "Unset All";
            unsetAllBtn.UseVisualStyleBackColor = true;
            unsetAllBtn.Click += UnsetAllBtn_Click;
            // 
            // restoreBtn
            // 
            restoreBtn.Location = new Point(198, 505);
            restoreBtn.Margin = new Padding(4, 3, 4, 3);
            restoreBtn.Name = "restoreBtn";
            restoreBtn.Size = new Size(110, 27);
            restoreBtn.TabIndex = 5;
            restoreBtn.Text = "Restore State";
            restoreBtn.UseVisualStyleBackColor = true;
            restoreBtn.Click += RestoreBtn_Click;
            // 
            // totalSetLabel
            // 
            totalSetLabel.AutoSize = true;
            totalSetLabel.Location = new Point(14, 444);
            totalSetLabel.Margin = new Padding(4, 0, 4, 0);
            totalSetLabel.Name = "totalSetLabel";
            totalSetLabel.Size = new Size(54, 15);
            totalSetLabel.TabIndex = 6;
            totalSetLabel.Text = "Total Set:";
            // 
            // totalUnsetLabel
            // 
            totalUnsetLabel.AutoSize = true;
            totalUnsetLabel.Location = new Point(14, 478);
            totalUnsetLabel.Margin = new Padding(4, 0, 4, 0);
            totalUnsetLabel.Name = "totalUnsetLabel";
            totalUnsetLabel.Size = new Size(68, 15);
            totalUnsetLabel.TabIndex = 7;
            totalUnsetLabel.Text = "Total Unset:";
            // 
            // numUnsetTxt
            // 
            numUnsetTxt.AutoSize = true;
            numUnsetTxt.Location = new Point(135, 479);
            numUnsetTxt.Margin = new Padding(4, 0, 4, 0);
            numUnsetTxt.Name = "numUnsetTxt";
            numUnsetTxt.Size = new Size(48, 15);
            numUnsetTxt.TabIndex = 8;
            numUnsetTxt.Text = "000/000";
            // 
            // numSetTxt
            // 
            numSetTxt.AutoSize = true;
            numSetTxt.Location = new Point(135, 444);
            numSetTxt.Margin = new Padding(4, 0, 4, 0);
            numSetTxt.Name = "numSetTxt";
            numSetTxt.Size = new Size(48, 15);
            numSetTxt.TabIndex = 9;
            numSetTxt.Text = "000/000";
            // 
            // filterUnusedChk
            // 
            filterUnusedChk.AutoSize = true;
            filterUnusedChk.Checked = true;
            filterUnusedChk.CheckState = CheckState.Checked;
            filterUnusedChk.Location = new Point(18, 510);
            filterUnusedChk.Margin = new Padding(4, 3, 4, 3);
            filterUnusedChk.Name = "filterUnusedChk";
            filterUnusedChk.Size = new Size(94, 19);
            filterUnusedChk.TabIndex = 10;
            filterUnusedChk.Text = "Hide Unused";
            filterUnusedChk.UseVisualStyleBackColor = true;
            filterUnusedChk.CheckedChanged += FilterUnusedChk_CheckedChanged;
            // 
            // showOnlySetChk
            // 
            showOnlySetChk.AutoSize = true;
            showOnlySetChk.Location = new Point(316, 444);
            showOnlySetChk.Margin = new Padding(4, 3, 4, 3);
            showOnlySetChk.Name = "showOnlySetChk";
            showOnlySetChk.Size = new Size(99, 19);
            showOnlySetChk.TabIndex = 11;
            showOnlySetChk.Text = "Show only set";
            showOnlySetChk.UseVisualStyleBackColor = true;
            showOnlySetChk.CheckedChanged += ShowOnlySetChk_CheckedChanged;
            // 
            // showOnlyUnsetChk
            // 
            showOnlyUnsetChk.AutoSize = true;
            showOnlyUnsetChk.Location = new Point(316, 478);
            showOnlyUnsetChk.Margin = new Padding(4, 3, 4, 3);
            showOnlyUnsetChk.Name = "showOnlyUnsetChk";
            showOnlyUnsetChk.Size = new Size(113, 19);
            showOnlyUnsetChk.TabIndex = 12;
            showOnlyUnsetChk.Text = "Show only unset";
            showOnlyUnsetChk.UseVisualStyleBackColor = true;
            showOnlyUnsetChk.CheckedChanged += ShowOnlyUnsetChk_CheckedChanged;
            // 
            // filterBySearchChk
            // 
            filterBySearchChk.AutoSize = true;
            filterBySearchChk.Location = new Point(545, 443);
            filterBySearchChk.Margin = new Padding(4, 3, 4, 3);
            filterBySearchChk.Name = "filterBySearchChk";
            filterBySearchChk.Size = new Size(133, 19);
            filterBySearchChk.TabIndex = 13;
            filterBySearchChk.Text = "Filter by search term";
            filterBySearchChk.UseVisualStyleBackColor = true;
            filterBySearchChk.CheckedChanged += FilterBySearchChk_CheckedChanged;
            // 
            // searchTermBox
            // 
            searchTermBox.Location = new Point(545, 470);
            searchTermBox.Margin = new Padding(4, 3, 4, 3);
            searchTermBox.Name = "searchTermBox";
            searchTermBox.Size = new Size(250, 23);
            searchTermBox.TabIndex = 14;
            searchTermBox.KeyDown += SearchTermBox_KeyDown;
            // 
            // FlagsEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(961, 540);
            Controls.Add(searchTermBox);
            Controls.Add(filterBySearchChk);
            Controls.Add(showOnlyUnsetChk);
            Controls.Add(showOnlySetChk);
            Controls.Add(filterUnusedChk);
            Controls.Add(numSetTxt);
            Controls.Add(numUnsetTxt);
            Controls.Add(totalUnsetLabel);
            Controls.Add(totalSetLabel);
            Controls.Add(restoreBtn);
            Controls.Add(unsetAllBtn);
            Controls.Add(setAllBtn);
            Controls.Add(cancelBtn);
            Controls.Add(saveBtn);
            Controls.Add(dataGridView);
            Margin = new Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MaximumSize = new Size(977, 579);
            MinimumSize = new Size(977, 579);
            Name = "FlagsEditor";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Flags Editor";
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button cancelBtn;
        private System.Windows.Forms.Button setAllBtn;
        private System.Windows.Forms.Button unsetAllBtn;
        private System.Windows.Forms.Button restoreBtn;
        private System.Windows.Forms.Label totalSetLabel;
        private System.Windows.Forms.Label totalUnsetLabel;
        private System.Windows.Forms.Label numUnsetTxt;
        private System.Windows.Forms.Label numSetTxt;
        private System.Windows.Forms.CheckBox filterUnusedChk;
        private System.Windows.Forms.CheckBox showOnlySetChk;
        private System.Windows.Forms.CheckBox showOnlyUnsetChk;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgv_val;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_location;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_txtDesc;
        private System.Windows.Forms.CheckBox filterBySearchChk;
        private System.Windows.Forms.TextBox searchTermBox;
    }
}