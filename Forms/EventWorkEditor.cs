namespace FlagsEditorEXPlugin.Forms
{
    public partial class EventWorkEditor : Form
    {
        readonly List<FlagsOrganizer.WorkDetail> m_eventWorkList;
        readonly List<FlagsOrganizer.WorkDetail> m_editableEventWorkList;
        readonly FlagsOrganizer m_organizer;
        bool isSyncingCells;

        public EventWorkEditor(FlagsOrganizer flagsOrganizer)
        {
            m_organizer = flagsOrganizer;
            m_eventWorkList = m_organizer.EventWorkList;
            m_editableEventWorkList = new List<FlagsOrganizer.WorkDetail>(m_eventWorkList.Count);

            InitializeComponent();
            LocalizedStrings.LocalizeForm(this);

            dataGridView.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            dataGridView.CellValueChanged += DataGridView_CellValueChanged;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            RestoreData();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_editableEventWorkList.Count; i++)
            {
                m_eventWorkList[i].Value = m_editableEventWorkList[i].Value;
            }

            m_organizer.SyncEditedEventWork();

            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e) => Close();

        private void RestoreBtn_Click(object sender, EventArgs e) => RestoreData();

        private void FilterUnusedChk_CheckedChanged(object sender, EventArgs e) => RefreshDataGrid();

        private void FilterBySearchChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!filterBySearchChk.Checked || (filterBySearchChk.Checked && !string.IsNullOrWhiteSpace(searchTermBox.Text)))
            {
                RefreshDataGrid();
            }
            else
            {
                filterBySearchChk.Checked = false;
            }
        }

        private void SearchTermBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!filterBySearchChk.Checked)
                {
                    filterBySearchChk.Checked = true;
                }
                // Force event if already checked
                else
                {
                    FilterBySearchChk_CheckedChanged(sender, new EventArgs());
                }
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
                var idx = ((ulong?)(cells[0].Value)).Value;

                var w = m_editableEventWorkList.Find(element => element.WorkIdx == idx);

                isSyncingCells = true;

                if (e.ColumnIndex == 4) // Combobox
                {
                    var strVal = (string)cells[4].Value;
                    w!.Value = w!.ValidValues.ContainsValue(strVal) ? w.ValidValues.FirstOrDefault(x => x.Value == strVal).Key : 0;
                    cells[5].Value = w.Value;
                }
                else if (e.ColumnIndex == 5) // Text
                {
                    if (long.TryParse(cells[5].Value as string, out long v))
                    {
                        w!.Value = v;
                    }
                    else
                    {
                        cells[5].Value = w!.Value;
                    }
                }

                cells[4].Value = w!.ValidValues.TryGetValue(w!.Value, out string? value) ? value : "";

                isSyncingCells = false;
            }
        }

        private void RestoreData()
        {
            m_editableEventWorkList.Clear();

            foreach (var w in m_eventWorkList)
            {
                m_editableEventWorkList.Add(new FlagsOrganizer.WorkDetail(w));
            }

            RefreshDataGrid();
        }


        private void RefreshDataGrid()
        {
            this.SuspendLayout();

            bool skipUnused = filterUnusedChk.Checked;
            bool filterBySearch = filterBySearchChk.Checked && !string.IsNullOrWhiteSpace(searchTermBox.Text);

            List<DataGridViewRow> rowsToAdd = [];

            bool isNegatedSearch = searchTermBox.Text.StartsWith("^");
            string searchTerm = searchTermBox.Text.ToUpperInvariant().Replace("^", "");
            ulong? searchIdx;
            try
            {
                searchIdx = FlagsOrganizer.ParseDecOrHex(searchTerm);
            }
            catch (Exception)
            {
                searchIdx = null;
            }

            foreach (var w in m_editableEventWorkList)
            {
                if (skipUnused && w.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused)
                {
                    continue;
                }

                if (filterBySearch)
                {
                    if (!isNegatedSearch && ((!searchIdx.HasValue || (searchIdx.Value != w.WorkIdx && searchIdx.Value != (ulong)w.Value)) && !w.ToString().Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }
                    else if (isNegatedSearch && ((searchIdx.HasValue && (searchIdx.Value == w.WorkIdx || searchIdx.Value == (ulong)w.Value)) || w.ToString().Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }
                }

                var validValuesList = new List<string>(w.ValidValues.Values);
                validValuesList.Insert(0, "");

                var curRow = new DataGridViewRow();
                curRow.CreateCells(dataGridView);
                curRow.Cells[0].Value = w.WorkIdx;
                curRow.Cells[1].Value = w.InternalName;
                curRow.Cells[2].Value = w.LocationName;
                curRow.Cells[3].Value = w.DetailMsg;
                curRow.Cells[4] = new DataGridViewComboBoxCell() { DataSource = validValuesList, Value = w.ValidValues.TryGetValue(w.Value, out string? value) ? value : "" };
                curRow.Cells[5].Value = w.Value;

                // Disable if no known valid values available
                if (w.ValidValues.Count == 0)
                {
                    ((DataGridViewComboBoxCell)curRow.Cells[4]).DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    curRow.Cells[4].ReadOnly = true;
                }

                rowsToAdd.Add(curRow);
            }

            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            dataGridView.Rows.AddRange([.. rowsToAdd]);

            this.ResumeLayout(false);
        }
    }
}
