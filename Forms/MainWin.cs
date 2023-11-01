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
    public partial class MainWin : Form
    {
        FlagsOrganizer m_organizer;

        public MainWin(FlagsOrganizer flagsOrganizer)
        {
            m_organizer = flagsOrganizer;

            InitializeComponent();

            foreach (var fSet in m_organizer.FlagsSets)
            {
                flagsSetCombo.Items.Add(fSet.SourceName);
            }

            flagsCategoryCombo.Items.Add("- All -");
            for (FlagsOrganizer.EventFlagType i = (FlagsOrganizer.EventFlagType._Unknown) + 1; i < FlagsOrganizer.EventFlagType._Unused; i++)
            {
                flagsCategoryCombo.Items.Add(i.AsText());
            }


            flagsSetCombo.SelectedIndex = 0;
            flagsCategoryCombo.SelectedIndex = 0;
        }

        private void flagsSetEditBtn_Click(object sender, EventArgs e)
        {
            foreach (var fSet in m_organizer.FlagsSets)
            {
                if (fSet.SourceName == (flagsSetCombo.SelectedItem as string))
                {
                    var filter = FlagsOrganizer.EventFlagType._Unknown;
                    filter = filter.Parse(flagsCategoryCombo.SelectedItem as string);

                    var form = new FlagsEditor(m_organizer, fSet, filter);
                    form.ShowDialog();
                    break;
                }
            }
        }
    }
}
