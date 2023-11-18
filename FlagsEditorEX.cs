using PKHeX.Core;
using System;
using System.Windows.Forms;

namespace FlagsEditorEXPlugin
{
    public class FlagsEditorEX : IPlugin
    {
        public string Name => "Flags Editor EX";
        private string NameEditFlags => "Edit flags...";
        private string NameDumpAllFlags => "Dump all Flags";
        public int Priority => 100; // Loading order, lowest is first.
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public ISaveFileProvider SaveFileEditor { get; private set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        private ToolStripMenuItem? ctrl;

        private ToolStripMenuItem? menuEntry_EditFlags;
        private ToolStripMenuItem? menuEntry_DumpAllFlags;

        public void Initialize(params object[] args)
        {
            SaveFileEditor = (ISaveFileProvider)Array.Find(args, z => z is ISaveFileProvider)!;
            var menu = (ToolStrip)Array.Find(args, z => z is ToolStrip)!;
            LoadMenuStrip(menu);
        }

        private void LoadMenuStrip(ToolStrip menuStrip)
        {
            var items = menuStrip.Items;
            var tools = (ToolStripDropDownItem)items.Find("Menu_Tools", false)[0];
            AddPluginControl(tools);
        }

        
        private void AddPluginControl(ToolStripDropDownItem tools)
        {
            ctrl = new ToolStripMenuItem(Name);
            ctrl.Enabled = false;
            tools.DropDownItems.Add(ctrl);

            menuEntry_DumpAllFlags = new ToolStripMenuItem(NameDumpAllFlags);
            menuEntry_DumpAllFlags.Enabled = false;
            menuEntry_DumpAllFlags.Click += DumpAllFlags_UIEvt;
            ctrl.DropDownItems.Add(menuEntry_DumpAllFlags);

            menuEntry_EditFlags = new ToolStripMenuItem(NameEditFlags);
            menuEntry_EditFlags.Enabled = false;
            menuEntry_EditFlags.Click += EditFlags_UIEvt;
            ctrl.DropDownItems.Add(menuEntry_EditFlags);
        }

        private void DumpAllFlags_UIEvt(object? sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.CreateFlagsOrganizer(SaveFileEditor.SAV, resData: null);
            flagsOrganizer.DumpAllFlags();
        }

        private void EditFlags_UIEvt(object? sender, EventArgs e)
        {
            var flagsOrganizer = FlagsOrganizer.CreateFlagsOrganizer(SaveFileEditor.SAV, resData: null);
            var form = new Forms.MainWin(flagsOrganizer);
            form.ShowDialog();
        }

        public void NotifySaveLoaded()
        {
            ctrl!.Enabled = true;
            menuEntry_DumpAllFlags!.Enabled = true;
            menuEntry_EditFlags!.Enabled = true;

            var savData = SaveFileEditor.SAV;

            // Prevent usage if state is not Exportable
            if (!savData.State.Exportable)
            {
                ctrl.Enabled = false;
                return;
            }

            switch (savData.Version)
            {
                case GameVersion.Any:
                case GameVersion.RBY:
                case GameVersion.StadiumJ:
                case GameVersion.Stadium:
                case GameVersion.Stadium2:
                case GameVersion.RSBOX:
                case GameVersion.COLO:
                case GameVersion.XD:
                case GameVersion.CXD:
                case GameVersion.BATREV:
                case GameVersion.ORASDEMO:
                case GameVersion.GO:
                case GameVersion.Unknown:
                case GameVersion.Invalid:
                    ctrl.Enabled = false;
                    break;


                // Check for AS Demo
                case GameVersion.AS:
                    {
                        if (savData is SAV6AODemo)
                        {
                            ctrl.Enabled = false;
                        }
                    }
                    break;


                // Check for SN Demo
                case GameVersion.SN:
                    {
                        var sav7 = (SAV7SM)savData;
                        if (sav7.BoxLayout.BoxesUnlocked == 8 && string.IsNullOrWhiteSpace(sav7.BoxLayout.GetBoxName(10)))
                        {
                            // Can't have a renamed box which is locked - must be Demo
                            ctrl.Enabled = false;
                        }
                    }
                    break;
            }

#if DEBUG
            // Quick dump all flags on load during DEBUG
            if (ctrl.Enabled)
            {
                DumpAllFlags_UIEvt(null, new EventArgs());
            }
#endif
        }

        public bool TryLoadFile(string filePath)
        {
            return false; // no action taken
        }

    }

}
