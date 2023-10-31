using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;


namespace FlagsEditorEXPlugin.Forms
{
    public partial class SelectedFlagsEditor : Form
    {
        FlagsOrganizer m_flagsOrganizer;

        public SelectedFlagsEditor(FlagsOrganizer flagsOrganizer)
        {
            m_flagsOrganizer = flagsOrganizer;

            InitializeComponent();

            fieldItemsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.FieldItem);
            hiddenItemsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.HiddenItem);
            itemGiftsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.ItemGift);
            pkmnGiftsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.PkmnGift);
            trainerBattlesChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.TrainerBattle);
            staticEncounterChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.StaticBattle);
            inGameTradesChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.InGameTrade);
            sideEventsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.SideEvent);
            miscEventsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.GeneralEvent);
            flySpotsChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.FlySpot);
            berryTreesChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.BerryTree);
            collectablesChk.Enabled = m_flagsOrganizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.Collectable);
        }

        private void markFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.HiddenItem);

            if (itemGiftsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.ItemGift);

            if (pkmnGiftsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.PkmnGift);
            
            if (trainerBattlesChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.TrainerBattle);

            if (staticEncounterChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.StaticBattle);

            if (inGameTradesChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.InGameTrade);

            if (sideEventsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.SideEvent);

            if (miscEventsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.GeneralEvent);

            if (flySpotsChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.FlySpot);

            if (berryTreesChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.BerryTree);

            if (collectablesChk.Checked)
                m_flagsOrganizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.Collectable);

            Close();
        }

        private void unmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            if (fieldItemsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.FieldItem);

            if (hiddenItemsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.HiddenItem);

            if (itemGiftsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.ItemGift);

            if (pkmnGiftsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.PkmnGift);

            if (trainerBattlesChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.TrainerBattle);

            if (staticEncounterChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.StaticBattle);

            if (inGameTradesChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.InGameTrade);

            if (sideEventsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.SideEvent);

            if (miscEventsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.GeneralEvent);

            if (flySpotsChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.FlySpot);

            if (berryTreesChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.BerryTree);

            if (collectablesChk.Checked)
                m_flagsOrganizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.Collectable);

            Close();
        }
    }
}
