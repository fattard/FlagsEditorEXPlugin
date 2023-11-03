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
            miscEventsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.GeneralEvent);
            flySpotsChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.FlySpot);
            berryTreesChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.BerryTree);
            collectablesChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.Collectable);

            #endregion Bulk Edit Tab
        }

        private void rawFlagsEditBtn_Click(object sender, EventArgs e)
        {
            foreach (var fGroup in m_organizer.FlagsGroups)
            {
                if (fGroup.SourceName == (flagsGroupCombo.SelectedItem as string))
                {
                    var filter = FlagsOrganizer.EventFlagType._Unknown;
                    filter = filter.Parse(flagsCategoryCombo.SelectedItem as string);

                    var form = new FlagsEditor(m_organizer, fGroup, filter);
                    form.ShowDialog();
                    break;
                }
            }
        }

        private void eventWorkEditBtn_Click(object sender, EventArgs e)
        {
            var form = new EventWorkEditor(m_organizer);
            form.ShowDialog();
        }

        private void markFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.HiddenItem);

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

            if (miscEventsChk.Checked)
                m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.GeneralEvent);

            if (flySpotsChk.Checked)
                m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.FlySpot);

            if (berryTreesChk.Checked)
                m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.BerryTree);

            if (collectablesChk.Checked)
                m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.Collectable);

            var result = MessageBox.Show("Operation done", "Bulk Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void unmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.HiddenItem);

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

            if (miscEventsChk.Checked)
                m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.GeneralEvent);

            if (flySpotsChk.Checked)
                m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.FlySpot);

            if (berryTreesChk.Checked)
                m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.BerryTree);

            if (collectablesChk.Checked)
                m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.Collectable);

            var result = MessageBox.Show("Operation done", "Bulk Edit", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
