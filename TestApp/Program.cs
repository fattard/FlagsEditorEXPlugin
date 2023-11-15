using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using PKHeX.Core;
using FlagsEditorEXPlugin;
using FlagsEditorEXPlugin.Forms;

namespace FlagsEditorEX_App
{
    static class Program
    {
        static SaveFile? SAV;

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            string[] args = Environment.GetCommandLineArgs();

            if (args.Length > 0)
            {
                foreach (string path in args)
                {
                    var other = FileUtil.GetSupportedFile(path, SAV);
                    if (other is SaveFile s)
                    {
                        s.Metadata.SetExtraInfo(path);
                        SAV = s;
                    }
                }
            }

            if (SAV != null)
            {
                var flagsOrganizer = FlagsOrganizer.OrganizeFlags(SAV, resData: null);

                if (flagsOrganizer == null)
                {
                    throw new FormatException("Unsupported SAV format: " + SAV.Version);
                }

                var form = new MainWin(flagsOrganizer);
                form.KeyDown += new KeyEventHandler(Form_KeyDown);
                form.KeyPreview = true;
                Application.Run(form);
            }
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                var organizer = FlagsOrganizer.OrganizeFlags(SAV, resData: null);
                organizer.DumpAllFlags();
            }
        }
    }
}
