using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlagsEditorEXPlugin.Forms
{
    public partial class FlagsEditor : Form
    {
        List<FlagsOrganizer.FlagDetail> m_flagsList;
        List<FlagsOrganizer.FlagDetail> m_editableFlagsList;
        FlagsOrganizer m_organizer;
        FlagsOrganizer.FlagsGroup m_curFlagsGroup;
        FlagsOrganizer.EventFlagType m_filter;


        public FlagsEditor(FlagsOrganizer flagsOrganizer, FlagsOrganizer.FlagsGroup flagsGroup, FlagsOrganizer.EventFlagType filter)
        {
            m_organizer = flagsOrganizer;
            m_curFlagsGroup = flagsGroup;
            m_filter = filter;
            m_flagsList = flagsGroup.Flags;
            m_editableFlagsList = new List<FlagsOrganizer.FlagDetail>(m_flagsList.Count);

            InitializeComponent();

            this.Text = $"Flags Editor - {m_curFlagsGroup.SourceName}";

            dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;

            RestoreData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_editableFlagsList.Count; i++)
            {
                m_flagsList[i].IsSet = m_editableFlagsList[i].IsSet;
            }

            m_organizer.SyncEditedFlags(m_curFlagsGroup.SourceIdx);

            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setAllBtn_Click(object sender, EventArgs e)
        {
            bool skipUnused = filterUnusedChk.Checked;

            foreach (var f in m_editableFlagsList)
            {
                if (f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused && skipUnused)
                {
                    continue;
                }

                f.IsSet = true;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void unsetAllBtn_Click(object sender, EventArgs e)
        {
            bool skipUnused = filterUnusedChk.Checked;

            foreach (var f in m_editableFlagsList)
            {
                if (f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused && skipUnused)
                {
                    continue;
                }

                f.IsSet = false;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void restoreBtn_Click(object sender, EventArgs e)
        {
            RestoreData();
        }

        private void filterUnusedChk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
            RefreshCounters();
        }

        private void showOnlySetChk_CheckedChanged(object sender, EventArgs e)
        {
            if (showOnlySetChk.Checked)
            {
                showOnlyUnsetChk.Checked = false;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void showOnlyUnsetChk_CheckedChanged(object sender, EventArgs e)
        {
            if (showOnlyUnsetChk.Checked)
            {
                showOnlySetChk.Checked = false;
            }

            RefreshDataGrid();
            RefreshCounters();
        }

        private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView.IsCurrentCellDirty)
            {
                dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void dataGridView_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            int idx = (int)(dataGridView.Rows[e.RowIndex].Cells[1].Value as UInt64?).Value;
            m_editableFlagsList[idx].IsSet = (dataGridView.Rows[e.RowIndex].Cells[0].Value as Boolean?).Value;
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
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            GC.Collect();

            bool skipUnused = filterUnusedChk.Checked;
            bool skipSet = showOnlyUnsetChk.Checked;
            bool skipUnset = showOnlySetChk.Checked;

            foreach (var f in m_editableFlagsList)
            {
                if (f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused && skipUnused)
                {
                    continue;
                }

                if (f.IsSet && skipSet)
                {
                    continue;
                }
                if (!f.IsSet && skipUnset)
                {
                    continue;
                }

                if (m_filter != FlagsOrganizer.EventFlagType._Unknown && f.FlagTypeVal != m_filter)
                {
                    continue;
                }

                dataGridView.Rows.Add(new object[] { f.IsSet, f.FlagIdx, f.InternalName, f.LocationName, f.DetailMsg });
            }
        }

        private void RefreshCounters()
        {
            int totalRows = dataGridView.Rows.Count;
            int totalSet = 0;
            int totalUnset = 0;

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                if ((dataGridView.Rows[i].Cells[0].Value as Boolean?).Value)
                {
                    totalSet++;
                }
                else
                {
                    totalUnset++;
                }
            }

            numSetTxt.Text = string.Format("{0:D4}/{1:D4}", totalSet, totalRows);
            numUnsetTxt.Text = string.Format("{0:D4}/{1:D4}", totalUnset, totalRows);
        }

    }
}
