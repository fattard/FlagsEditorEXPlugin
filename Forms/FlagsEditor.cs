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
        List<FlagsOrganizer.FlagDetail> m_editabelFlagsList;


        public FlagsEditor(List<FlagsOrganizer.FlagDetail> flagsList, FlagsOrganizer flagsOrganizer)
        {
            m_flagsList = flagsList;
            m_editabelFlagsList = new List<FlagsOrganizer.FlagDetail>(m_flagsList.Count);

            InitializeComponent();

            RestoreData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_editabelFlagsList.Count; i++)
            {
                m_flagsList[i].IsSet = m_editabelFlagsList[i].IsSet;
            }

            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void setAllBtn_Click(object sender, EventArgs e)
        {
            bool skipUnused = filterUnusedChk.Checked;

            foreach (var f in m_editabelFlagsList)
            {
                if (skipUnused && f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused)
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

            foreach (var f in m_editabelFlagsList)
            {
                if (skipUnused && f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused)
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



        private void RestoreData()
        {
            m_editabelFlagsList.Clear();

            foreach (var f in m_flagsList)
            {
                m_editabelFlagsList.Add(new FlagsOrganizer.FlagDetail(f.FlagIdx, f.SourceIdx, f.FlagTypeVal, f.LocationName, f.DetailMsg, f.InternalName) { IsSet = f.IsSet });
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

            foreach (var f in m_editabelFlagsList)
            {
                if (skipUnused && f.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused)
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

                dataGridView.Rows.Add(new object[] { f.IsSet, f.FlagIdx, f.InternalName, $"{f.LocationName} - {f.DetailMsg}"});
            }
        }


        private void RefreshCounters()
        {
            int totalRows = dataGridView.Rows.Count;
            int totalSet = 0;
            int totalUnset = 0;

            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                var a = (dataGridView.Rows[i].Cells[0].Value as Boolean?);

                if (a.Value)
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
