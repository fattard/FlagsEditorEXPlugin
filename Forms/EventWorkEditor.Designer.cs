
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
            this.filterUnusedChk = new System.Windows.Forms.CheckBox();
            this.restoreBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.saveBtn = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.dgv_ref = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_location = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_txtDesc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgv_validValues = new System.Windows.Forms.DataGridViewComboBoxColumn();
            this.dgv_rawValue = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // filterUnusedChk
            // 
            this.filterUnusedChk.AutoSize = true;
            this.filterUnusedChk.Checked = true;
            this.filterUnusedChk.CheckState = System.Windows.Forms.CheckState.Checked;
            this.filterUnusedChk.Location = new System.Drawing.Point(15, 440);
            this.filterUnusedChk.Name = "filterUnusedChk";
            this.filterUnusedChk.Size = new System.Drawing.Size(88, 17);
            this.filterUnusedChk.TabIndex = 15;
            this.filterUnusedChk.Text = "Hide Unused";
            this.filterUnusedChk.UseVisualStyleBackColor = true;
            this.filterUnusedChk.CheckedChanged += new System.EventHandler(this.filterUnusedChk_CheckedChanged);
            // 
            // restoreBtn
            // 
            this.restoreBtn.Location = new System.Drawing.Point(137, 436);
            this.restoreBtn.Name = "restoreBtn";
            this.restoreBtn.Size = new System.Drawing.Size(94, 23);
            this.restoreBtn.TabIndex = 14;
            this.restoreBtn.Text = "Restore State";
            this.restoreBtn.UseVisualStyleBackColor = true;
            this.restoreBtn.Click += new System.EventHandler(this.restoreBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.Location = new System.Drawing.Point(718, 436);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(94, 23);
            this.cancelBtn.TabIndex = 13;
            this.cancelBtn.Text = "Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            this.cancelBtn.Click += new System.EventHandler(this.cancelBtn_Click);
            // 
            // saveBtn
            // 
            this.saveBtn.Location = new System.Drawing.Point(718, 407);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(94, 23);
            this.saveBtn.TabIndex = 12;
            this.saveBtn.Text = "Save";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
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
            this.dgv_ref,
            this.dgv_id,
            this.dgv_location,
            this.dgv_txtDesc,
            this.dgv_validValues,
            this.dgv_rawValue});
            this.dataGridView.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataGridView.Location = new System.Drawing.Point(12, 12);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowHeadersVisible = false;
            this.dataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView.Size = new System.Drawing.Size(800, 389);
            this.dataGridView.TabIndex = 16;
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
            this.dgv_id.HeaderText = "Identifier";
            this.dgv_id.Name = "dgv_id";
            this.dgv_id.ReadOnly = true;
            this.dgv_id.Width = 72;
            // 
            // dgv_location
            // 
            this.dgv_location.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_location.HeaderText = "Location";
            this.dgv_location.Name = "dgv_location";
            this.dgv_location.ReadOnly = true;
            this.dgv_location.Width = 73;
            // 
            // dgv_txtDesc
            // 
            this.dgv_txtDesc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_txtDesc.HeaderText = "Description";
            this.dgv_txtDesc.Name = "dgv_txtDesc";
            this.dgv_txtDesc.ReadOnly = true;
            this.dgv_txtDesc.Width = 85;
            // 
            // dgv_validValues
            // 
            this.dgv_validValues.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
            this.dgv_validValues.HeaderText = "Valid Values";
            this.dgv_validValues.Items.AddRange(new object[] {
            "Custom"});
            this.dgv_validValues.Name = "dgv_validValues";
            this.dgv_validValues.Width = 71;
            // 
            // dgv_rawValue
            // 
            this.dgv_rawValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
            this.dgv_rawValue.HeaderText = "Raw Value";
            this.dgv_rawValue.Name = "dgv_rawValue";
            // 
            // EventWorkEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(824, 468);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.filterUnusedChk);
            this.Controls.Add(this.restoreBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.saveBtn);
            this.MaximumSize = new System.Drawing.Size(840, 507);
            this.MinimumSize = new System.Drawing.Size(840, 507);
            this.Name = "EventWorkEditor";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Event Work Editor";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
    }
}