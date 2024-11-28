namespace FlagsEditorEXPlugin.Forms
{
    public partial class FlagsEditor : Form
    {
        readonly List<FlagsOrganizer.FlagDetail> m_flagsList;
        readonly List<FlagsOrganizer.FlagDetail> m_editableFlagsList;
        readonly FlagsOrganizer m_organizer;
        readonly FlagsOrganizer.FlagsGroup m_curFlagsGroup;
        readonly FlagsOrganizer.EventFlagType m_filter;
        int m_totalSet = 0;
        int m_totalUnset = 0;

        public FlagsEditor(FlagsOrganizer flagsOrganizer, FlagsOrganizer.FlagsGroup flagsGroup, FlagsOrganizer.EventFlagType filter)
        {
            m_organizer = flagsOrganizer;
            m_curFlagsGroup = flagsGroup;
            m_filter = filter;
            m_flagsList = flagsGroup.Flags;
            m_editableFlagsList = new List<FlagsOrganizer.FlagDetail>(m_flagsList.Count);

            InitializeComponent();
            LocalizedStrings.LocalizeForm(this);

            this.Text += $" - {m_curFlagsGroup.SourceName}";

            dataGridView.CurrentCellDirtyStateChanged += DataGridView_CurrentCellDirtyStateChanged;
            dataGridView.CellValueChanged += DataGridView_CellValueChanged;
            dataGridView.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;

            RestoreData();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_editableFlagsList.Count; i++)
            {
                m_flagsList[i].IsSet = m_editableFlagsList[i].IsSet;
            }

            m_organizer.SyncEditedFlags(m_curFlagsGroup);

            Close();
        }

        private void CancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetAllBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                var idx = ((ulong?)dataGridView.Rows[i].Cells[1].Value).Value;
                m_editableFlagsList.Find(f => f.FlagIdx == idx)!.IsSet = true;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void UnsetAllBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                var idx = ((ulong?)dataGridView.Rows[i].Cells[1].Value).Value;
                m_editableFlagsList.Find(f => f.FlagIdx == idx)!.IsSet = false;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void RestoreBtn_Click(object sender, EventArgs e)
        {
            RestoreData();
        }

        private void FilterUnusedChk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
            RefreshCounters();
        }

        private void ShowOnlySetChk_CheckedChanged(object sender, EventArgs e)
        {
            if (showOnlySetChk.Checked)
            {
                showOnlyUnsetChk.Checked = false;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void FilterBySearchChk_CheckedChanged(object sender, EventArgs e)
        {
            if (!filterBySearchChk.Checked || (filterBySearchChk.Checked && !string.IsNullOrWhiteSpace(searchTermBox.Text)))
            {
                RefreshDataGrid();
                RefreshCounters();
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

        private void ShowOnlyUnsetChk_CheckedChanged(object sender, EventArgs e)
        {
            if (showOnlyUnsetChk.Checked)
            {
                showOnlySetChk.Checked = false;
            }

            RefreshDataGrid();
            RefreshCounters();
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
            var idx = ((ulong?)dataGridView.Rows[e.RowIndex].Cells[1].Value).Value;
            var toSet = ((bool?)dataGridView.Rows[e.RowIndex].Cells[0].Value).Value;
            m_editableFlagsList.Find(f => f.FlagIdx == idx)!.IsSet = toSet;

            if (toSet)
            {
                m_totalSet++;
                m_totalUnset--;
            }
            else
            {
                m_totalSet--;
                m_totalUnset++;
            }

            RefreshCountersLabels();
        }



        private void RestoreData()
        {
            m_editableFlagsList.Clear();

            foreach (var f in m_flagsList)
            {
                m_editableFlagsList.Add(new FlagsOrganizer.FlagDetail(f.FlagIdx, f.SourceIdx, f.FlagTypeVal, f.LocationName, f.DetailMsg, f.InternalName) { IsSet = f.IsSet });
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void RefreshDataGrid()
        {
            this.SuspendLayout();

            bool skipUnused = filterUnusedChk.Checked;
            bool skipSet = showOnlyUnsetChk.Checked;
            bool skipUnset = showOnlySetChk.Checked;
            bool useCategoryFilter = (m_filter != FlagsOrganizer.EventFlagType._Unknown);
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

            foreach (var f in m_editableFlagsList)
            {
                if (skipUnused && f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused)
                {
                    continue;
                }

                if ((skipSet && f.IsSet) || (skipUnset && !f.IsSet))
                {
                    continue;
                }

                if (useCategoryFilter && f.FlagTypeVal != m_filter)
                {
                    continue;
                }

                if (filterBySearch)
                {
                    if (!isNegatedSearch && ((!searchIdx.HasValue || searchIdx.Value != f.FlagIdx) && !f.ToString().Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }
                    else if (isNegatedSearch && ((searchIdx.HasValue && searchIdx.Value == f.FlagIdx) || f.ToString().Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        continue;
                    }
                }

                var curRow = new DataGridViewRow();
                curRow.CreateCells(dataGridView);
                curRow.Cells[0].Value = f.IsSet;
                curRow.Cells[1].Value = f.FlagIdx;
                curRow.Cells[2].Value = f.InternalName;
                curRow.Cells[3].Value = f.LocationName;
                curRow.Cells[4].Value = f.DetailMsg;

                rowsToAdd.Add(curRow);
            }

            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            dataGridView.Rows.AddRange([.. rowsToAdd]);

            this.ResumeLayout(false);
        }

        private void RefreshCounters()
        {
            int totalRows = dataGridView.Rows.Count;
            int totalSet = 0;
            int totalUnset = 0;

            for (int i = 0; i < dataGridView.RowCount; i++)
            {
                if (((bool?)dataGridView.Rows[i].Cells[0].Value).Value)
                {
                    totalSet++;
                }
                else
                {
                    totalUnset++;
                }
            }

            m_totalSet = totalSet;
            m_totalUnset = totalUnset;

            RefreshCountersLabels();
        }

        private void RefreshCountersLabels()
        {
            int totalRows = dataGridView.Rows.Count;
            int totalSet = m_totalSet;
            int totalUnset = m_totalUnset;

            numSetTxt.Text = string.Format("{0:D4}/{1:D4}", totalSet, totalRows);
            numUnsetTxt.Text = string.Format("{0:D4}/{1:D4}", totalUnset, totalRows);
        }
    }
}
