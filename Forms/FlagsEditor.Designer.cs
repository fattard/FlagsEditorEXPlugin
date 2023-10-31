
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
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.saveBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.setAllBtn = new System.Windows.Forms.Button();
            this.unsetAllBtn = new System.Windows.Forms.Button();
            this.restoreBtn = new System.Windows.Forms.Button();
            this.totalSetLabel = new System.Windows.Forms.Label();
            this.totalUnsetLabel = new System.Windows.Forms.Label();
            this.numUnsetTxt = new System.Windows.Forms.Label();
            this.numSetTxt = new System.Windows.Forms.Label();
            this.filterUnusedChk = new System.Windows.Forms.CheckBox();
            this.dgv_val = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgv_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_txtDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.showOnlySetChk = new System.Windows.Forms.CheckBox();
            this.showOnlyUnsetChk = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.ControlDark;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgv_val,
            this.dgv_ref,
            this.dgv_id,
            this.dgv_txtDesc});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(776, 357);
            this.dataGridView.TabIndex = 0;
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(694, 380);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(94, 23);
            this.saveBtn.TabIndex = 1;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(694, 409);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(94, 23);
            this.cancelBtn.TabIndex = 2;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // setAllBtn
            // 
            this.setAllBtn.Location = new System.Drawing.Point(137, 380);
            this.setAllBtn.Name = "setAllBtn";
            this.setAllBtn.Size = new System.Drawing.Size(94, 23);
            this.setAllBtn.TabIndex = 3;
            this.setAllBtn.Text = "Set All";
            this.setAllBtn.UseVisualStyleBackColor = true;
            this.setAllBtn.Click += new System.EventHandler(this.setAllBtn_Click);
            // 
            // unsetAllBtn
            // 
            this.unsetAllBtn.Location = new System.Drawing.Point(137, 409);
            this.unsetAllBtn.Name = "unsetAllBtn";
            this.unsetAllBtn.Size = new System.Drawing.Size(94, 23);
            this.unsetAllBtn.TabIndex = 4;
            this.unsetAllBtn.Text = "Unset All";
            this.unsetAllBtn.UseVisualStyleBackColor = true;
            this.unsetAllBtn.Click += new System.EventHandler(this.unsetAllBtn_Click);
            // 
            // restoreBtn
            // 
            this.restoreBtn.Location = new System.Drawing.Point(137, 438);
            this.restoreBtn.Name = "restoreBtn";
            this.restoreBtn.Size = new System.Drawing.Size(94, 23);
            this.restoreBtn.TabIndex = 5;
            this.restoreBtn.Text = "Restore State";
            this.restoreBtn.UseVisualStyleBackColor = true;
            this.restoreBtn.Click += new System.EventHandler(this.restoreBtn_Click);
            // 
            // totalSetLabel
            // 
            this.totalSetLabel.AutoSize = true;
            this.totalSetLabel.Location = new System.Drawing.Point(12, 385);
            this.totalSetLabel.Name = "totalSetLabel";
            this.totalSetLabel.Size = new System.Drawing.Size(53, 13);
            this.totalSetLabel.TabIndex = 6;
            this.totalSetLabel.Text = "Total Set:";
            // 
            // totalUnsetLabel
            // 
            this.totalUnsetLabel.AutoSize = true;
            this.totalUnsetLabel.Location = new System.Drawing.Point(12, 414);
            this.totalUnsetLabel.Name = "totalUnsetLabel";
            this.totalUnsetLabel.Size = new System.Drawing.Size(65, 13);
            this.totalUnsetLabel.TabIndex = 7;
            this.totalUnsetLabel.Text = "Total Unset:";
            // 
            // numUnsetTxt
            // 
            this.numUnsetTxt.AutoSize = true;
            this.numUnsetTxt.Location = new System.Drawing.Point(83, 415);
            this.numUnsetTxt.Name = "numUnsetTxt";
            this.numUnsetTxt.Size = new System.Drawing.Size(48, 13);
            this.numUnsetTxt.TabIndex = 8;
            this.numUnsetTxt.Text = "000/000";
            // 
            // numSetTxt
            // 
            this.numSetTxt.AutoSize = true;
            this.numSetTxt.Location = new System.Drawing.Point(83, 385);
            this.numSetTxt.Name = "numSetTxt";
            this.numSetTxt.Size = new System.Drawing.Size(48, 13);
            this.numSetTxt.TabIndex = 9;
            this.numSetTxt.Text = "000/000";
            // 
            // filterUnusedChk
            // 
            this.filterUnusedChk.AutoSize = true;
            this.filterUnusedChk.Checked = true;
            this.filterUnusedChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterUnusedChk.Location = new System.Drawing.Point(15, 442);
            this.filterUnusedChk.Name = "filterUnusedChk";
            this.filterUnusedChk.Size = new System.Drawing.Size(88, 17);
            this.filterUnusedChk.TabIndex = 10;
            this.filterUnusedChk.Text = "Hide Unused";
            this.filterUnusedChk.UseVisualStyleBackColor = true;
            this.filterUnusedChk.CheckedChanged += new System.EventHandler(this.filterUnusedChk_CheckedChanged);
            // 
            // dgv_val
            // 
            this.dgv_val.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_val.HeaderText = "Value";
            this.dgv_val.Name = "dgv_val";
            this.dgv_val.Width = 40;
            // 
            // dgv_ref
            // 
            this.dgv_ref.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_ref.HeaderText = "Ref";
            this.dgv_ref.Name = "dgv_ref";
            this.dgv_ref.ReadOnly = true;
            this.dgv_ref.Width = 49;
            // 
            // dgv_id
            // 
            this.dgv_id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_id.HeaderText = "Internal Name";
            this.dgv_id.Name = "dgv_id";
            this.dgv_id.ReadOnly = true;
            this.dgv_id.Width = 98;
            // 
            // dgv_txtDesc
            // 
            this.dgv_txtDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_txtDesc.HeaderText = "Description";
            this.dgv_txtDesc.Name = "dgv_txtDesc";
            this.dgv_txtDesc.ReadOnly = true;
            this.dgv_txtDesc.Width = 85;
            // 
            // showOnlySetChk
            // 
            this.showOnlySetChk.AutoSize = true;
            this.showOnlySetChk.Location = new System.Drawing.Point(238, 385);
            this.showOnlySetChk.Name = "showOnlySetChk";
            this.showOnlySetChk.Size = new System.Drawing.Size(92, 17);
            this.showOnlySetChk.TabIndex = 11;
            this.showOnlySetChk.Text = "Show only set";
            this.showOnlySetChk.UseVisualStyleBackColor = true;
            this.showOnlySetChk.CheckedChanged += new System.EventHandler(this.showOnlySetChk_CheckedChanged);
            // 
            // showOnlyUnsetChk
            // 
            this.showOnlyUnsetChk.AutoSize = true;
            this.showOnlyUnsetChk.Location = new System.Drawing.Point(238, 414);
            this.showOnlyUnsetChk.Name = "showOnlyUnsetChk";
            this.showOnlyUnsetChk.Size = new System.Drawing.Size(104, 17);
            this.showOnlyUnsetChk.TabIndex = 12;
            this.showOnlyUnsetChk.Text = "Show only unset";
            this.showOnlyUnsetChk.UseVisualStyleBackColor = true;
            this.showOnlyUnsetChk.CheckedChanged += new System.EventHandler(this.showOnlyUnsetChk_CheckedChanged);
            // 
            // FlagsEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 468);
            this.Controls.Add(this.showOnlyUnsetChk);
            this.Controls.Add(this.showOnlySetChk);
            this.Controls.Add(this.filterUnusedChk);
            this.Controls.Add(this.numSetTxt);
            this.Controls.Add(this.numUnsetTxt);
            this.Controls.Add(this.totalUnsetLabel);
            this.Controls.Add(this.totalSetLabel);
            this.Controls.Add(this.restoreBtn);
            this.Controls.Add(this.unsetAllBtn);
            this.Controls.Add(this.setAllBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.dataGridView);
            this.MaximizeBox = false;
            this.Name = "FlagsEditor";
            this.Text = "Flags Editor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgv_val;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_ref;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_id;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgv_txtDesc;
        private System.Windows.Forms.CheckBox showOnlySetChk;
        private System.Windows.Forms.CheckBox showOnlyUnsetChk;
    }
}