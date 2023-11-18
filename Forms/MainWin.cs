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
        readonly FlagsOrganizer m_organizer;

        public MainWin(FlagsOrganizer flagsOrganizer)
        {
            m_organizer = flagsOrganizer;

            InitializeComponent();

            #region Raw Flags Edit Tab
            
            foreach (var fGroup in m_organizer.FlagsGroups)
            {
                flagsGroupCombo.Items.Add(fGroup.SourceName);
            }

            flagsCategoryCombo.Items.Add("- All -");
            for (FlagsOrganizer.EventFlagType i = (FlagsOrganizer.EventFlagType._Unknown) + 1; i < FlagsOrganizer.EventFlagType._Unused; i++)
            {
                flagsCategoryCombo.Items.Add(i.AsText());
            }


            flagsGroupCombo.SelectedIndex = 0;
            flagsCategoryCombo.SelectedIndex = 0;

            #endregion Raw Flags Edit Tab


            #region Bulk Edit Tab

            fieldItemsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.FieldItem);
            hiddenItemsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.HiddenItem);
            itemGiftsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.ItemGift);
            pkmnGiftsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.PkmnGift);
            trainerBattlesChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.TrainerBattle);
            staticEncounterChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.StaticBattle);
            inGameTradesChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.InGameTrade);
            sideEventsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.SideEvent);
            specialItemsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.GeneralEvent);
            flySpotsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.FlySpot);
            berryTreesChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.BerryTree);
            collectablesChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.Collectable);

            bool anyChecked =
                collectablesChk.Enabled ||
                berryTreesChk.Enabled ||
                flySpotsChk.Enabled ||
                specialItemsChk.Enabled ||
                sideEventsChk.Enabled ||
                inGameTradesChk.Enabled ||
                staticEncounterChk.Enabled ||
                trainerBattlesChk.Enabled ||
                pkmnGiftsChk.Enabled ||
                itemGiftsChk.Enabled ||
                hiddenItemsChk.Enabled ||
                fieldItemsChk.Enabled;

            markFlagsBtn.Enabled = unmarkFlagsBtn.Enabled = anyChecked;

            #endregion Bulk Edit Tab

            #region Special Edit Tab

            var specialEditableEvents = m_organizer.GetSpecialEditableEvents();
            for (int i = 0; i < specialEditableEvents.Length; i++)
            {
                var evt = specialEditableEvents[i];

                var newBtn = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                    Location = new System.Drawing.Point(i < 16 ? 2 : (200 + 8), 2 + ((i % 16) * 27)),
                    Name = "specialEvtBtn_" + evt.Index,
                    Size = new System.Drawing.Size(200, 23),
                    //newBtn.TabIndex = 1;
                    Text = evt.Name,
                    UseVisualStyleBackColor = true
                };
                newBtn.Click += (object? sender, EventArgs e) =>
                {
                    m_organizer.ProcessSpecialEventEdit(evt);
                };

                tabPage4.Controls.Add(newBtn);
            }
            if (specialEditableEvents.Length == 0)
            {
                tabControl1.Controls.Remove(tabPage4);
            }

            #endregion Special Edit Tab

            #region Misc Edit Tab
            //TODO:
            tabControl1.Controls.Remove(tabPage5);
            #endregion Misc Edit Tab
        }

        private void RawFlagsEditBtn_Click(object sender, EventArgs e)
        {
            foreach (var fGroup in m_organizer.FlagsGroups)
            {
                if (fGroup.SourceName == ((string)flagsGroupCombo.SelectedItem))
                {
                    var filter = FlagsOrganizer.EventFlagType._Unknown;
                    filter = filter.Parse((string)flagsCategoryCombo.SelectedItem);

                    var form = new FlagsEditor(m_organizer, fGroup, filter);
                    form.ShowDialog();
                    break;
                }
            }
        }

        private void EventWorkEditBtn_Click(object sender, EventArgs e)
        {
            var form = new EventWorkEditor(m_organizer);
            form.ShowDialog();
        }

        private void MarkFlagsBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("This operation cannot be undone.\nAre you sure?", "Bulk Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (fieldItemsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.FieldItem);

                if (hiddenItemsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.HiddenItem);

                if (specialItemsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.SpecialItem);

                if (itemGiftsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.ItemGift);

                if (pkmnGiftsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.PkmnGift);

                if (trainerBattlesChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.TrainerBattle);

                if (staticEncounterChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.StaticBattle);

                if (inGameTradesChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.InGameTrade);

                if (sideEventsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.SideEvent);

                if (flySpotsChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.FlySpot);

                if (berryTreesChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.BerryTree);

                if (collectablesChk.Checked)
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.Collectable);

                MessageBox.Show("Operation done", "Bulk Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UnmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("This operation cannot be undone.\nAre you sure?", "Bulk Edit", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                if (fieldItemsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.FieldItem);

                if (hiddenItemsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.HiddenItem);

                if (specialItemsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.SpecialItem);

                if (itemGiftsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.ItemGift);

                if (pkmnGiftsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.PkmnGift);

                if (trainerBattlesChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.TrainerBattle);

                if (staticEncounterChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.StaticBattle);

                if (inGameTradesChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.InGameTrade);

                if (sideEventsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.SideEvent);

                if (flySpotsChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.FlySpot);

                if (berryTreesChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.BerryTree);

                if (collectablesChk.Checked)
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.Collectable);

                MessageBox.Show("Operation done", "Bulk Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
