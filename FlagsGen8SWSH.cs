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

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), 0, "Event Flags", null);
            AssembleWorkList<uint>(s_flagsList_res.Substring(idxEventWorkSection), null);
        }

        protected override void AssembleList(string flagsList_res, int sourceIdx, string sourceName, bool[] flagValues)
        {
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                FlagsGroup flagsGroup = new FlagsGroup(sourceIdx, sourceName);

                string s = reader.ReadLine();

                // Skip header
                if (s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        // End of section
                        if (s.StartsWith("//"))
                        {
                            break;
                        }

                        var flagDetail = new FlagDetail(s);
                        if (savEventBlocks.HasBlock((uint)flagDetail.FlagIdx))
                        {
                            flagDetail.IsSet = (savEventBlocks.GetBlockSafe((uint)flagDetail.FlagIdx).Type == SCTypeCode.Bool2);
                            flagDetail.SourceIdx = sourceIdx;
                            flagsGroup.Flags.Add(flagDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);
                
                m_flagsGroupsList.Add(flagsGroup);
            }

        }

        protected override void AssembleWorkList<T>(string workList_res, T[] eventWorkValues)
        {
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            using (System.IO.StringReader reader = new System.IO.StringReader(workList_res))
            {
                string s = reader.ReadLine();

                // Skip header
                if (s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        // End of section
                        if (s.StartsWith("//"))
                        {
                            break;
                        }

                        var workDetail = new WorkDetail(s);
                        if (savEventBlocks.HasBlock((uint)workDetail.WorkIdx))
                        {
                            workDetail.Value = Convert.ToInt64(savEventBlocks.GetBlockSafe((uint)workDetail.WorkIdx).GetValue());
                            m_eventWorkList.Add(workDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType)
        {
            switch (flagType)
            {
#if DEBUG
                case EventFlagType.FieldItem:
                case EventFlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                    return true;
#endif

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
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            foreach (var fGroup in m_flagsGroupsList)
            {
                if (fGroup.SourceIdx == sourceIdx)
                {
                    switch (fGroup.SourceIdx)
                    {
                        case 0: // Event Flags
                            foreach (var f in fGroup.Flags)
                            {
                                savEventBlocks.GetBlockSafe((uint)f.FlagIdx).ChangeBooleanType(f.IsSet ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
                            }
                            break;
                    }

                    break;
                }
            }
        }

        public override void SyncEditedEventWork()
        {
            var savEventBlocks = (m_savFile as ISCBlockArray).Accessor;

            foreach (var w in m_eventWorkList)
            {
                savEventBlocks.GetBlockSafe((uint)w.WorkIdx).SetValue((uint)w.Value);
            }
        }
    }
}
