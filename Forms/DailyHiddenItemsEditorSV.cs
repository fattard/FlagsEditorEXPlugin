namespace FlagsEditorEXPlugin.Forms
{
    public partial class DailyHiddenItemsEditorSV : Form
    {
        readonly FlagsGen9SV m_organizer;
        readonly Dictionary<ulong, byte[]> m_blocks;
        readonly Dictionary<ulong, byte[]> m_editableBlocks;

        readonly Dictionary<int, ulong> m_indexToKeys;
        int m_curSelectedBlockIdx;
        bool isSyncingCells;

        readonly Dictionary<byte, string> m_itemsIndexesList = new Dictionary<byte, string>()
        {
            { 0x00, "- Collected -" },
            { 0x01, "Item (0x01)" },
            { 0x02, "Item (0x02)" },
            { 0x03, "Item (0x03)" },
            { 0x04, "Item (0x04)" },
            { 0x05, "Item (0x05)" },
            { 0x06, "Item (0x06)" },
            { 0x07, "Item (0x07)" },
            { 0x08, "Item (0x08)" },
            { 0x09, "Item (0x09)" },
            { 0x0A, "Item (0x0A)" },
            { 0x0B, "Item (0x0B)" },
            { 0x0C, "Item (0x0C)" },
            { 0x0D, "Item (0x0D)" },
            { 0x0E, "Item (0x0E)" },
            { 0x0F, "Item (0x0F)" },
            { 0x10, "Item (0x10)" },
            { 0x11, "Item (0x11)" },
            { 0x12, "Item (0x12)" },
            { 0x13, "Item (0x13)" },
            { 0x14, "Item (0x14)" },
            { 0x15, "Item (0x15)" },
            { 0x16, "Item (0x16)" },
            { 0x17, "Item (0x17)" },
            { 0x18, "Item (0x18)" },
            { 0x19, "Item (0x19)" },
            { 0x1A, "Item (0x1A)" },
            { 0x1B, "Item (0x1B)" },
            { 0x1C, "Item (0x1C)" },
            { 0x1D, "Item (0x1D)" },
            { 0x1E, "Item (0x1E)" },
            { 0x1F, "Item (0x1F)" },
            { 0x20, "Item (0x20)" },
            { 0x21, "Item (0x21)" },
            { 0x22, "Item (0x22)" },
            { 0x23, "Item (0x23)" },
            { 0x24, "Item (0x24)" },
            { 0x25, "Item (0x25)" },
            { 0x26, "Item (0x26)" },
            { 0x27, "Item (0x27)" },
            { 0x28, "Item (0x28)" },
            { 0x29, "Item (0x29)" },
            { 0x2A, "Item (0x2A)" },
            { 0x2B, "Item (0x2B)" },
            { 0x2C, "Item (0x2C)" },
            { 0x2D, "Item (0x2D)" },
            { 0x2E, "Item (0x2E)" },
            { 0x2F, "Item (0x2F)" },
            { 0x30, "Item (0x30)" },
            { 0x31, "Item (0x31)" },
            { 0x32, "Item (0x32)" },
            { 0x33, "Item (0x33)" },
            { 0x34, "Item (0x34)" },
            { 0x35, "Item (0x35)" },
            { 0x36, "Item (0x36)" },
            { 0x37, "Item (0x37)" },
            { 0x38, "Item (0x38)" },
            { 0x39, "Item (0x39)" },
            { 0x3A, "Item (0x3A)" },
            { 0x3B, "Item (0x3B)" },
            { 0x3C, "Item (0x3C)" },
            { 0x3D, "Item (0x3D)" },
            { 0x3E, "Item (0x3E)" },
            { 0x3F, "Item (0x3F)" },
            { 0x40, "Item (0x40)" },
            { 0x41, "Item (0x41)" },
            { 0x42, "Item (0x42)" },
            { 0x43, "Item (0x43)" },
            { 0x44, "Item (0x44)" },
            { 0x45, "Item (0x45)" },
            { 0x46, "Item (0x46)" },
            { 0x47, "Item (0x47)" },
            { 0x48, "Item (0x48)" },
            { 0x49, "Item (0x49)" },
            { 0x4A, "Item (0x4A)" },
            { 0x4B, "Item (0x4B)" },
            { 0x4C, "Item (0x4C)" },
            { 0x4D, "Item (0x4D)" },
            { 0x4E, "Item (0x4E)" },
            { 0x4F, "Item (0x4F)" },
            { 0x50, "Item (0x50)" },
            { 0x51, "Item (0x51)" },
            { 0x52, "Item (0x52)" },
            { 0x53, "Item (0x53)" },
            { 0x54, "Item (0x54)" },
            { 0x55, "Item (0x55)" },
            { 0x56, "Item (0x56)" },
            { 0x57, "Item (0x57)" },
            { 0x58, "Item (0x58)" },
            { 0x59, "Item (0x59)" },
            { 0x5A, "Item (0x5A)" },
            { 0x5B, "Item (0x5B)" },
            { 0x5C, "Item (0x5C)" },
            { 0x5D, "Item (0x5D)" },
            { 0x5E, "Item (0x5E)" },
            { 0x5F, "Item (0x5F)" },
            { 0x60, "Item (0x60)" },
            { 0x61, "Item (0x61)" },
            { 0x62, "Item (0x62)" },
            { 0x63, "Item (0x63)" },
            { 0x64, "Item (0x64)" },
            { 0x65, "Item (0x65)" },
            { 0x66, "Item (0x66)" },
            { 0x67, "Item (0x67)" },
            { 0x68, "Item (0x68)" },
            { 0x69, "Item (0x69)" },
            { 0x6A, "Item (0x6A)" },
            { 0x6B, "Item (0x6B)" },
            { 0x6C, "Item (0x6C)" },
            { 0x6D, "Item (0x6D)" },
            { 0x6E, "Item (0x6E)" },
            { 0x6F, "Item (0x6F)" },
            { 0x70, "Item (0x70)" },
            { 0x71, "Item (0x71)" },
            { 0x72, "Item (0x72)" },
            { 0x73, "Item (0x73)" },
            { 0x74, "Item (0x74)" },
            { 0x75, "Item (0x75)" },
            { 0x76, "Item (0x76)" },
            { 0x77, "Item (0x77)" },
            { 0x78, "Item (0x78)" },
            { 0x79, "Item (0x79)" },
            { 0x7A, "Item (0x7A)" },
            { 0x7B, "Item (0x7B)" },
            { 0x7C, "Item (0x7C)" },
            { 0x7D, "Item (0x7D)" },
            { 0x7E, "Item (0x7E)" },
            { 0x7F, "Item (0x7F)" },
            { 0x80, "- Empty (0x80) -" },
            { 0x81, "- Empty (0x81) -" },
            { 0x82, "- Empty (0x82) -" },
            { 0x83, "- Empty (0x83) -" },
            { 0x84, "- Empty (0x84) -" },
            { 0x85, "- Empty (0x85) -" },
            { 0x86, "- Empty (0x86) -" },
            { 0x87, "- Empty (0x87) -" },
            { 0x88, "- Empty (0x88) -" },
            { 0x89, "- Empty (0x89) -" },
            { 0x8A, "- Empty (0x8A) -" },
            { 0x8B, "- Empty (0x8B) -" },
            { 0x8C, "- Empty (0x8C) -" },
            { 0x8D, "- Empty (0x8D) -" },
            { 0x8E, "- Empty (0x8E) -" },
            { 0x8F, "- Empty (0x8F) -" },
            { 0x90, "- Empty (0x90) -" },
            { 0x91, "- Empty (0x91) -" },
            { 0x92, "- Empty (0x92) -" },
            { 0x93, "- Empty (0x93) -" },
            { 0x94, "- Empty (0x94) -" },
            { 0x95, "- Empty (0x95) -" },
            { 0x96, "- Empty (0x96) -" },
            { 0x97, "- Empty (0x97) -" },
            { 0x98, "- Empty (0x98) -" },
            { 0x99, "- Empty (0x99) -" },
            { 0x9A, "- Empty (0x9A) -" },
            { 0x9B, "- Empty (0x9B) -" },
            { 0x9C, "- Empty (0x9C) -" },
            { 0x9D, "- Empty (0x9D) -" },
            { 0x9E, "- Empty (0x9E) -" },
            { 0x9F, "- Empty (0x9F) -" },
            { 0xA0, "- Empty (0xA0) -" },
            { 0xA1, "- Empty (0xA1) -" },
            { 0xA2, "- Empty (0xA2) -" },
            { 0xA3, "- Empty (0xA3) -" },
            { 0xA4, "- Empty (0xA4) -" },
            { 0xA5, "- Empty (0xA5) -" },
            { 0xA6, "- Empty (0xA6) -" },
            { 0xA7, "- Empty (0xA7) -" },
            { 0xA8, "- Empty (0xA8) -" },
            { 0xA9, "- Empty (0xA9) -" },
            { 0xAA, "- Empty (0xAA) -" },
            { 0xAB, "- Empty (0xAB) -" },
            { 0xAC, "- Empty (0xAC) -" },
            { 0xAD, "- Empty (0xAD) -" },
            { 0xAE, "- Empty (0xAE) -" },
            { 0xAF, "- Empty (0xAF) -" },
            { 0xB0, "- Empty (0xB0) -" },
            { 0xB1, "- Empty (0xB1) -" },
            { 0xB2, "- Empty (0xB2) -" },
            { 0xB3, "- Empty (0xB3) -" },
            { 0xB4, "- Empty (0xB4) -" },
            { 0xB5, "- Empty (0xB5) -" },
            { 0xB6, "- Empty (0xB6) -" },
            { 0xB7, "- Empty (0xB7) -" },
            { 0xB8, "- Empty (0xB8) -" },
            { 0xB9, "- Empty (0xB9) -" },
            { 0xBA, "- Empty (0xBA) -" },
            { 0xBB, "- Empty (0xBB) -" },
            { 0xBC, "- Empty (0xBC) -" },
            { 0xBD, "- Empty (0xBD) -" },
            { 0xBE, "- Empty (0xBE) -" },
            { 0xBF, "- Empty (0xBF) -" },
            { 0xC0, "- Empty (0xC0) -" },
            { 0xC1, "- Empty (0xC1) -" },
            { 0xC2, "- Empty (0xC2) -" },
            { 0xC3, "- Empty (0xC3) -" },
            { 0xC4, "- Empty (0xC4) -" },
            { 0xC5, "- Empty (0xC5) -" },
            { 0xC6, "- Empty (0xC6) -" },
            { 0xC7, "- Empty (0xC7) -" },
            { 0xC8, "- Empty (0xC8) -" },
            { 0xC9, "- Empty (0xC9) -" },
            { 0xCA, "- Empty (0xCA) -" },
            { 0xCB, "- Empty (0xCB) -" },
            { 0xCC, "- Empty (0xCC) -" },
            { 0xCD, "- Empty (0xCD) -" },
            { 0xCE, "- Empty (0xCE) -" },
            { 0xCF, "- Empty (0xCF) -" },
            { 0xD0, "- Empty (0xD0) -" },
            { 0xD1, "- Empty (0xD1) -" },
            { 0xD2, "- Empty (0xD2) -" },
            { 0xD3, "- Empty (0xD3) -" },
            { 0xD4, "- Empty (0xD4) -" },
            { 0xD5, "- Empty (0xD5) -" },
            { 0xD6, "- Empty (0xD6) -" },
            { 0xD7, "- Empty (0xD7) -" },
            { 0xD8, "- Empty (0xD8) -" },
            { 0xD9, "- Empty (0xD9) -" },
            { 0xDA, "- Empty (0xDA) -" },
            { 0xDB, "- Empty (0xDB) -" },
            { 0xDC, "- Empty (0xDC) -" },
            { 0xDD, "- Empty (0xDD) -" },
            { 0xDE, "- Empty (0xDE) -" },
            { 0xDF, "- Empty (0xDF) -" },
            { 0xE0, "- Empty (0xE0) -" },
            { 0xE1, "- Empty (0xE1) -" },
            { 0xE2, "- Empty (0xE2) -" },
            { 0xE3, "- Empty (0xE3) -" },
            { 0xE4, "- Empty (0xE4) -" },
            { 0xE5, "- Empty (0xE5) -" },
            { 0xE6, "- Empty (0xE6) -" },
            { 0xE7, "- Empty (0xE7) -" },
            { 0xE8, "- Empty (0xE8) -" },
            { 0xE9, "- Empty (0xE9) -" },
            { 0xEA, "- Empty (0xEA) -" },
            { 0xEB, "- Empty (0xEB) -" },
            { 0xEC, "- Empty (0xEC) -" },
            { 0xED, "- Empty (0xED) -" },
            { 0xEE, "- Empty (0xEE) -" },
            { 0xEF, "- Empty (0xEF) -" },
            { 0xF0, "- Empty (0xF0) -" },
            { 0xF1, "- Empty (0xF1) -" },
            { 0xF2, "- Empty (0xF2) -" },
            { 0xF3, "- Empty (0xF3) -" },
            { 0xF4, "- Empty (0xF4) -" },
            { 0xF5, "- Empty (0xF5) -" },
            { 0xF6, "- Empty (0xF6) -" },
            { 0xF7, "- Empty (0xF7) -" },
            { 0xF8, "- Empty (0xF8) -" },
            { 0xF9, "- Empty (0xF9) -" },
            { 0xFA, "- Empty (0xFA) -" },
            { 0xFB, "- Empty (0xFB) -" },
            { 0xFC, "- Empty (0xFC) -" },
            { 0xFD, "- Empty (0xFD) -" },
            { 0xFE, "- Empty (0xFE) -" },
            { 0xFF, "- Empty (0xFF) -" },
        };


        public DailyHiddenItemsEditorSV(FlagsOrganizer flagsOrganizer)
        {
            m_organizer = (FlagsGen9SV)flagsOrganizer;
            m_blocks = m_organizer.GetHiddenItemBlocksCopy();

            InitializeComponent();
            LocalizedStrings.LocalizeForm(this);

            m_indexToKeys = new Dictionary<int, ulong>(20);
            m_editableBlocks = new Dictionary<ulong, byte[]>(20);
            foreach (var block in m_blocks)
            {
                m_editableBlocks[block.Key] = new byte[block.Value.Length];
                blockSourceCombo.Items.Add(block.Key switch
                {
                    // Paldea
                    0x6DAB304B => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockPaldeaSouth", "South Paldea areas"),
                    0x6EAB31DE => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockPaldeaWest", "West Paldea areas"),
                    0x6FAB3371 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockPaldeaNorth", "North Paldea areas"),
                    0x6CAB2EB8 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockPaldeaEast", "East Paldea areas"),

                    // Area Zero
                    0x9A7A41AB => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockAreaZero1", "Area Zero 1"),
                    0x9B7A433E => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockAreaZero2", "Area Zero 2"),
                    0x9C7A44D1 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockAreaZero3", "Area Zero 3"),

                    // DLC1
                    0x917A3380 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDlC1_1", "Kitakami 1"),
                    0xA07A4B1D => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDLC1_2", "Kitakami 2"),

                    // DLC2
                    0x1281BA58 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDlC2_1", "Terarium 1"),
                    0x1381BBEB => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDlC2_2", "Terarium 2"),
                    0x257F99AA => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDlC2_3", "Terarium 3"),

                    0x1E7F8EA5 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDlC2_AreaZeroDepths", "Area Zero Depths"),

                    _ => $"??? {block.Key}"
                });
                m_indexToKeys[blockSourceCombo.Items.Count - 1] = block.Key;
            }

            m_curSelectedBlockIdx = 0;
            blockSourceCombo.SelectedIndex = 0;
            itemIIndexSelectionCombo.Items.AddRange(m_itemsIndexesList.Values.ToArray());
            itemIIndexSelectionCombo.SelectedIndex = 0;

            dataGridView.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            dataGridView.CellValueChanged += DataGridView_CellValueChanged;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

#if DEBUG
            this.KeyPreview = true;
            this.KeyDown += (object? sender, KeyEventArgs e) =>
            {
                if (e.KeyCode == Keys.D)
                {
                    StringBuilder sb = new StringBuilder(512 * 1024);
                    foreach (var tBlock in m_blocks)
                    {
                        sb.Append($"0x{tBlock.Key:X16}\r\n");

                        for (int i = 1; i < tBlock.Value.Length; i++)
                        {
                            sb.Append($"{i:D04} => 0x{tBlock.Value[i - 1]:X02}\r\n");
                        }

                        sb.Append("\r\n\r\n");
                    }

                    System.IO.File.WriteAllText("_hiddenItemsList.txt", sb.ToString());
                }
            };
#endif

            RestoreData();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            m_organizer.SyncEditedHiddenItems(m_editableBlocks);
            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e) => Close();

        private void SetAllBtn_Click(object sender, EventArgs e)
        {
            byte itemId = m_itemsIndexesList.First(x => x.Value == (string)itemIIndexSelectionCombo.SelectedItem!).Key;

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                var cells = dataGridView.Rows[i].Cells;
                var idx = ((uint?)(cells[0].Value)).Value;

                m_editableBlocks[m_indexToKeys[m_curSelectedBlockIdx]][idx - 1] = itemId;
            }

            RefreshDataGrid();
        }

        private void RestoreBtn_Click(object sender, EventArgs e)
        {
            RestoreData();
        }

        private void BlockSourceCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_curSelectedBlockIdx != blockSourceCombo.SelectedIndex)
            {
                m_curSelectedBlockIdx = blockSourceCombo.SelectedIndex;
                RefreshDataGrid();
            }
        }

        private void FilterByItemChk_CheckedChanged(object sender, EventArgs e) => RefreshDataGrid();

        private void ItemIIndexSelectionCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (filterByItemChk.Checked)
            {
                RefreshDataGrid();
            }
        }

        private void DataGridView_CurrentCellDirtyStateChanged(object? sender, EventArgs e)
        {
            if (dataGridView.IsCurrentCellDirty)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void DataGridView_CellValueChanged(object? sender, DataGridViewCellEventArgs e)
        {
            if (!isSyncingCells)
            {
                var cells = dataGridView.Rows[e.RowIndex].Cells;
                var idx = ((uint?)(cells[0].Value)).Value;
                var strVal = (string)cells[2].Value;
                byte data = m_itemsIndexesList.First(x => x.Value == strVal).Key;
                m_editableBlocks[m_indexToKeys[m_curSelectedBlockIdx]][idx - 1] = data;
            }
        }



        private void RestoreData()
        {
            foreach (var block in m_blocks)
            {
                Array.Copy(block.Value, m_editableBlocks[block.Key], block.Value.Length);
            }

            RefreshDataGrid();
        }

        private void RefreshDataGrid()
        {
            if (!isSyncingCells)
            {
                isSyncingCells = true;
                this.SuspendLayout();

                bool filterByItem = filterByItemChk.Checked;
                byte filteredItemId = m_itemsIndexesList.First(x => x.Value == (string)itemIIndexSelectionCombo.SelectedItem!).Key;
                uint refNum = 1;

                List<DataGridViewRow> rowsToAdd = [];

                foreach (var data in m_editableBlocks[m_indexToKeys[m_curSelectedBlockIdx]])
                {
                    if (filterByItem && data != filteredItemId)
                    {
                        refNum++;
                        continue;
                    }

                    var curRow = new DataGridViewRow();
                    curRow.CreateCells(dataGridView);
                    curRow.Cells[0].Value = refNum++;
                    curRow.Cells[1].Value = string.Empty;
                    curRow.Cells[2] = new DataGridViewComboBoxCell() { DataSource = m_itemsIndexesList.Values.ToList(), Value = m_itemsIndexesList[data] };

                    rowsToAdd.Add(curRow);
                }

                dataGridView.Rows.Clear();
                dataGridView.Refresh();

                dataGridView.Rows.AddRange([.. rowsToAdd]);

                isSyncingCells = false;
                this.ResumeLayout(false);
            }
        }

    }
}
