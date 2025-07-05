using System.Reflection;

namespace FlagsEditorEXPlugin.Forms
{
    public partial class MainWin : Form
    {
        readonly FlagsOrganizer m_organizer;

        public MainWin(FlagsOrganizer flagsOrganizer)
        {
            m_organizer = flagsOrganizer;

            InitializeComponent();
            LocalizedStrings.LocalizeForm(this);
            this.warnLabel.Text = this.warnLabel.Text.Replace("|", "\r\n");

            #region Raw Flags Edit Tab

            foreach (var fGroup in m_organizer.FlagsGroups)
            {
                flagsGroupCombo.Items.Add(fGroup.SourceName);
            }

            flagsCategoryCombo.Items.Add(LocalizedStrings.Find("MainWin.flagsCategoryComboAll", "- All -"));
            for (FlagsOrganizer.EventFlagType i = (FlagsOrganizer.EventFlagType._Unknown) + 1; i < FlagsOrganizer.EventFlagType._Unused; i++)
            {
                flagsCategoryCombo.Items.Add(i.AsLocalizedText());
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
            staticEncounterChk.Enabled = m_organizer.SupportsBulkEditingFlags(FlagsOrganizer.EventFlagType.StaticEncounter);
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
                    Anchor = AnchorStyles.Top | AnchorStyles.Left,
                    Location = new System.Drawing.Point(i < 16 ? 20 : 274, 2 + ((i % 16) * 37)),
                    Name = "specialEvtBtn_" + evt.Index,
                    Size = new System.Drawing.Size(210, 37),
                    //newBtn.TabIndex = 1;
                    Text = evt.Label,
                    UseVisualStyleBackColor = true,
                    Enabled = evt.IsAvailable
                };
                float newSize = 8.0f;
                newBtn.Font = new Font(newBtn.Font.FontFamily, newSize, newBtn.Font.Style);
                newBtn.Click += (object? sender, EventArgs e) =>
                {
                    var result = MessageBox.Show(LocalizedStrings.Find("MainWin.BulkOperation_msg", "This operation cannot be undone.|Are you sure?").Replace("|", "\r\n"),
                        LocalizedStrings.Find("MainWin.BulkOperation_caption", "Not Undoable Edits"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                    if (result == DialogResult.Yes)
                    {
                        m_organizer.ProcessSpecialEventEdit(evt);

                        MessageBox.Show(LocalizedStrings.Find("MainWin.BulkOperationDone_msg", "Operation done"),
                            LocalizedStrings.Find("MainWin.BulkOperation_caption", "Not Undoable Edits"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };

                tabPage4.Controls.Add(newBtn);
            }
            if (specialEditableEvents.Length == 0)
            {
                tabControl1.Controls.Remove(tabPage4);
            }

            #endregion Special Edit Tab

            #region Misc Edit Tab

            // Add Block Data Editor
            var blockEditorBtn = new Button
            {
                Anchor = AnchorStyles.Top | AnchorStyles.Left,
                Location = new System.Drawing.Point(0 < 16 ? 20 : 274, 2 + ((0 % 16) * 37)),
                Name = "miscEvtBtn_BlockDataEditor",
                Size = new System.Drawing.Size(210, 37),
                //TabIndex = 1;
                Text = LocalizedStrings.Find($"MiscEdits.miscEvtBtn_BlockDataEditor", "Block Data Editor"),
                UseVisualStyleBackColor = true
            };
            {
                float newSize = 8.0f;
                blockEditorBtn.Font = new Font(blockEditorBtn.Font.FontFamily, newSize, blockEditorBtn.Font.Style);
            }
            blockEditorBtn.Click += (object? sender, EventArgs e) =>
            {
                try
                {
                    var pkHexAssembly = Assembly.GetEntryAssembly();
                    var blockEditorFormType = pkHexAssembly!.GetType("PKHeX.WinForms.Controls.SAVEditor");
                    var getAccessorFormMethod = blockEditorFormType!.GetMethod("GetAccessorForm", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);
                    var blockEditorFormInstance = (Form)getAccessorFormMethod!.Invoke(null, [m_organizer.SaveFile])!;
                    blockEditorFormInstance.ShowDialog();
                    blockEditorFormInstance.Dispose();
                }
                catch (Exception)
                {
                }
            };
            tabPage5.Controls.Add(blockEditorBtn);

            var miscEditableEvents = m_organizer.GetMiscEditableEvents();
            for (int i = 0; i < miscEditableEvents.Length; i++)
            {
                var evt = miscEditableEvents[i];
                int btnIdx = i + 1; // Block Editor Button is idx = 0

                var newBtn = new Button
                {
                    Anchor = AnchorStyles.Top | AnchorStyles.Left,
                    Location = new System.Drawing.Point(btnIdx < 16 ? 20 : 274, 2 + ((btnIdx % 16) * 37)),
                    Name = "miscEvtBtn_" + evt.Index,
                    Size = new System.Drawing.Size(210, 37),
                    //TabIndex = 1;
                    Text = evt.Label,
                    UseVisualStyleBackColor = true
                };
                {
                    float newSize = 8.0f;
                    newBtn.Font = new Font(newBtn.Font.FontFamily, newSize, newBtn.Font.Style);
                }
                newBtn.Click += (object? sender, EventArgs e) =>
                {
                    Form newEditorForm = (Form)Activator.CreateInstance(evt.EditorClassType!, m_organizer)!;
                    newEditorForm.ShowDialog();
                    newEditorForm.Dispose();
                };

                tabPage5.Controls.Add(newBtn);
            }

            #endregion Misc Edit Tab
        }

        private void RawFlagsEditBtn_Click(object sender, EventArgs e)
        {
            foreach (var fGroup in m_organizer.FlagsGroups)
            {
                if (fGroup.SourceName == ((string)flagsGroupCombo.SelectedItem!))
                {
                    var filter = (FlagsOrganizer.EventFlagType)flagsCategoryCombo.SelectedIndex;

                    var form = new FlagsEditor(m_organizer, fGroup, filter);
                    form.ShowDialog();
                    form.Dispose();
                    break;
                }
            }
        }

        private void EventWorkEditBtn_Click(object sender, EventArgs e)
        {
            var form = new EventWorkEditor(m_organizer);
            form.ShowDialog();
            form.Dispose();
        }

        private void MarkFlagsBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(LocalizedStrings.Find("MainWin.BulkOperation_msg", "This operation cannot be undone.|Are you sure?").Replace("|", "\r\n"),
                LocalizedStrings.Find("MainWin.BulkOperation_caption", "Not Undoable Edits"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    m_organizer.BulkMarkFlags(FlagsOrganizer.EventFlagType.StaticEncounter);

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

                MessageBox.Show(LocalizedStrings.Find("MainWin.BulkOperationDone_msg", "Operation done"),
                    LocalizedStrings.Find("MainWin.BulkOperation_caption", "Not Undoable Edits"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void UnmarkFlagsBtn_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(LocalizedStrings.Find("MainWin.BulkOperation_msg", "This operation cannot be undone.|Are you sure?").Replace("|", "\r\n"),
                LocalizedStrings.Find("MainWin.BulkOperation_caption", "Not Undoable Edits"), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    m_organizer.BulkUnmarkFlags(FlagsOrganizer.EventFlagType.StaticEncounter);

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

                MessageBox.Show(LocalizedStrings.Find("MainWin.BulkOperationDone_msg", "Operation done"),
                    LocalizedStrings.Find("MainWin.BulkOperation_caption", "Not Undoable Edits"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
