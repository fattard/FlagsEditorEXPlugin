using FlagsEditorEXPlugin;
using FlagsEditorEXPlugin.Forms;
using PKHeX.Core;

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

            foreach (string path in args)
            {
                var other = FileUtil.GetSupportedFile(path, SAV);
                if (other is SaveFile s)
                {
                    s.Metadata.SetExtraInfo(path);
                    SAV = s;
                }
            }

            if (SAV is not null)
            {
                //LocalizedStrings.Initialize(GameInfo.CurrentLanguage);
                LocalizedStrings.Initialize("br");
                var flagsOrganizer = FlagsOrganizer.CreateFlagsOrganizer(SAV, resData: null);
                var form = new MainWin(flagsOrganizer);
                form.KeyDown += Form_KeyDown;
                form.KeyPreview = true;
                form.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(form);
            }
        }

        private static void Form_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D)
            {
                FlagsOrganizer.CreateFlagsOrganizer(SAV!, resData: null).DumpAllFlags();
            }
        }
    }
}
