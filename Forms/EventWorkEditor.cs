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
    public partial class EventWorkEditor : Form
    {
        List<FlagsOrganizer.WorkDetail> m_eventWorkList;
        List<FlagsOrganizer.WorkDetail> m_editableEventWorkList;
        FlagsOrganizer m_organizer;
        bool isSyncingCells;

        public EventWorkEditor(FlagsOrganizer flagsOrganizer)
        {
            m_organizer = flagsOrganizer;
            m_eventWorkList = m_organizer.EventWorkList;
            m_editableEventWorkList = new List<FlagsOrganizer.WorkDetail>(m_eventWorkList.Count);

            InitializeComponent();

            dataGridView.CurrentCellDirtyStateChanged += dataGridView_CurrentCellDirtyStateChanged;
            dataGridView.CellValueChanged += dataGridView_CellValueChanged;

            RestoreData();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < m_editableEventWorkList.Count; i++)
            {
                m_eventWorkList[i].Value = m_editableEventWorkList[i].Value;
            }

            m_organizer.SyncEditedEventWork();

            Close();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void restoreBtn_Click(object sender, EventArgs e)
        {
            RestoreData();
        }

        private void filterUnusedChk_CheckedChanged(object sender, EventArgs e)
        {
            RefreshDataGrid();
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
            if (!isSyncingCells)
            {
                var cells = dataGridView.Rows[e.RowIndex].Cells;
                var idx = (cells[0].Value as UInt64?).Value;
                var w = m_editableEventWorkList.Find(element => (element.WorkIdx == idx));

                isSyncingCells = true;

                if (e.ColumnIndex == 4) // Combobox
                {
                    var strVal = cells[4].Value as string;
                    w.Value = w.ValidValues.ContainsValue(strVal) ? w.ValidValues.FirstOrDefault(x => x.Value == strVal).Key : 0;
                    cells[5].Value = w.Value;
                }
                else if (e.ColumnIndex == 5) // Text
                {
                    if (long.TryParse(cells[5].Value as string, out long v))
                    {
                        w.Value = v;
                    }
                    else
                    {
                        cells[5].Value = w.Value;
                    }
                }

                cells[4].Value = w.ValidValues.ContainsKey(w.Value) ? w.ValidValues[w.Value] : "";

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
            dataGridView.Rows.Clear();
            dataGridView.Refresh();

            GC.Collect();

            bool skipUnused = filterUnusedChk.Checked;

            foreach (var w in m_editableEventWorkList)
            {
                if (w.FlagTypeVal == FlagsOrganizer.EventFlagType._Unused && skipUnused)
                {
                    continue;
                }

                int i = dataGridView.Rows.Add(new object[] { w.WorkIdx, w.InternalName, w.LocationName, w.DetailMsg, "Custom", w.Value });

                var row = dataGridView.Rows[i];

                List<String> validValuesList = new List<string>(w.ValidValues.Values);
                validValuesList.Insert(0, "");

                row.Cells[4] = new DataGridViewComboBoxCell() { DataSource = validValuesList, Value = w.ValidValues.ContainsKey(w.Value) ? w.ValidValues[w.Value] : "" };
                
                // Disable if no known valid values available
                if (w.ValidValues.Count == 0)
                {
                    (row.Cells[4] as DataGridViewComboBoxCell).DisplayStyle = DataGridViewComboBoxDisplayStyle.Nothing;
                    row.Cells[4].ReadOnly = true;
                }
            }
        }
    }
}
