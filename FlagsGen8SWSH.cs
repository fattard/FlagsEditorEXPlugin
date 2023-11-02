using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{
    internal class FlagsGen8SWSH : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        protected override void InitFlagsData(SaveFile savFile, string resData)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (resData != null)
            {
                s_flagsList_res = resData;
            }
            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen8swsh.txt");
            }

            AssembleList(s_flagsList_res, 0, "", null);
        }

        protected override void AssembleList(string flagsList_res, int sourceIdx, string sourceName, bool[] flagValues)
        {
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;
            m_flagsGroupsList.Clear();

            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                var fGroup = new FlagsGroup(sourceIdx, sourceName);
                string s = reader.ReadLine();
                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        var flagDetail = new FlagDetail(s);
                        flagDetail.IsSet = (savEventBlocks.GetBlockSafe((uint)flagDetail.FlagIdx).Type == SCTypeCode.Bool2);
                        fGroup.Flags.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s != null);
                
                m_flagsGroupsList.Add(fGroup);
            }

        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType)
        {
            switch (flagType)
            {
                case EventFlagType.FieldItem:
                case EventFlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                    return true;

                default:
                    return false;
            }
        }

        public override void BulkMarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void BulkUnmarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }

        void ChangeFlagsVal(EventFlagType flagType, bool value)
        {
            if (SupportsBulkEditingFlags(flagType))
            {
                var blocks = (m_savFile as ISCBlockArray).Accessor;

                foreach (var f in m_flagsGroupsList[0].Flags)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        f.IsSet = value;
                        blocks.GetBlockSafe((uint)f.FlagIdx).ChangeBooleanType(value ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
                    }
                }
            }
        }

        public override void SyncEditedFlags(int sourceIdx)
        {

        }

        public override void SyncEditedEventWork()
        {

        }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);

            var flagsList = m_flagsGroupsList[0].Flags;

            for (int i = 0; i < flagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X8} {1}\t{2}\r\n", flagsList[i].FlagIdx, flagsList[i].IsSet,
                    flagsList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : flagsList[i].ToString());
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }
    }
}
