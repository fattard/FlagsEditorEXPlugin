using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FlagsEditorEXPlugin.Forms
{
    public partial class DailyHiddenItemsEditorSV : Form
    {
        readonly FlagsGen9SV m_organizer;
        readonly Dictionary<ulong, byte[]> m_blocks;
        Dictionary<ulong, byte[]> m_editableBlocks;

        Dictionary<int, ulong> m_indexToKeys;
        int m_curSelectedBlockIdx;
        bool isSyncingCells;

        Dictionary<byte, string> m_itemsIndexesList = new Dictionary<byte, string>()
        {
            { 0x80, "- Empty -" },
            { 0, "- Collected -" },
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
                    0x6CAB2EB8 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockPaldeaEast", "East Paldea Coverings"),

                    // Area Zero
                    0x9A7A41AB => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockAreaZero1", "Area Zero 1"),
                    0x9B7A433E => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockAreaZero2", "Area Zero 2"),
                    0x9C7A44D1 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockAreaZero3", "Area Zero 3"),

                    // DLC1
                    0x917A3380 => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDlC1_1", "Kitakami 1"),
                    0xA07A4B1D => LocalizedStrings.Find("DailyHiddenItemsEditorSV.blockDLC1_2", "Kitakami 2"),

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
                var strVal = (string)cells[3].Value;
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

                dataGridView.Rows.Clear();
                dataGridView.Refresh();

                GC.Collect();

                bool filterByItem = filterByItemChk.Checked;
                byte filteredItemId = m_itemsIndexesList.First(x => x.Value == (string)itemIIndexSelectionCombo.SelectedItem!).Key;
                uint refNum = 1;

                foreach (var data in m_editableBlocks[m_indexToKeys[m_curSelectedBlockIdx]])
                {
                    if (filterByItem && data != filteredItemId)
                    {
                        refNum++;
                        continue;
                    }

                    int i = dataGridView.Rows.Add(new object[] { refNum++, "", "", "Custom" });

                    bool exists = m_itemsIndexesList.TryGetValue(data, out string? item_name);

                    if (!exists)
                    {
                        item_name = $"0x{data.ToString("X2")}";
                        m_itemsIndexesList.Add(data, item_name);
                    }

                    dataGridView.Rows[i].Cells[3] = new DataGridViewComboBoxCell() { DataSource = m_itemsIndexesList.Values.ToList(), Value = item_name };
                }

                {
                    int oldIdx = itemIIndexSelectionCombo.SelectedIndex;
                    itemIIndexSelectionCombo.Items.Clear();
                    itemIIndexSelectionCombo.Items.AddRange(m_itemsIndexesList.Values.ToArray());
                    itemIIndexSelectionCombo.SelectedIndex = oldIdx;
                }

                isSyncingCells = false;
                this.ResumeLayout(false);
            }
        }

    }
}
