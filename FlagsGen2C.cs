using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{
    internal class FlagsGen2C : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        enum FlagOffsets
        {
            CompletedInGameTradeFlags = 0x24EE
        }

        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;

            // wTradeFlags
            bool[] result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (i >> 3), i & 7);
            }
            bool[] completedInGameTradeFlags = result;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadResFile("flags_gen2c.txt");

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxTradeFlagsSection = s_flagsList_res.IndexOf("//\tTrade Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");


            AssembleList(s_flagsList_res[idxEventFlagsSection..], 0, "Event Flags", ((IEventFlagArray)m_savFile!).GetEventFlags());
            AssembleList(s_flagsList_res[idxTradeFlagsSection..], 1, "Trade Flags", completedInGameTradeFlags);

            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], ((IEventWorkArray<byte>)m_savFile!).GetAllEventWork());
        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType) => flagType switch
        {
#if DEBUG
            EventFlagType.FieldItem or
            EventFlagType.HiddenItem or
            EventFlagType.TrainerBattle or
            EventFlagType.InGameTrade
                => true,
#endif
            _ => false
        };

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
                var flagHelper = (IEventFlagArray)m_savFile!;

                foreach (var f in m_flagsGroupsList[0].Flags)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        int fIdx = (int)f.FlagIdx;

                        f.IsSet = value;

                        switch (f.SourceIdx)
                        {
                            case 0: // EventFlags
                                flagHelper.SetEventFlag(fIdx, value);
                                break;

                            case 1: // TradeFlags
                                m_savFile!.SetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (fIdx >> 3), fIdx & 7, value);
                                break;
                        }
                    }
                }
            }
        }

        public override void SyncEditedFlags(int sourceIdx)
        {
            var flagHelper = (IEventFlagArray)m_savFile!;

            foreach (var fGroup in m_flagsGroupsList)
            {
                if (fGroup.SourceIdx == sourceIdx)
                {
                    switch (fGroup.SourceIdx)
                    {
                        case 0: // Event Flags
                            foreach (var f in fGroup.Flags)
                            {
                                flagHelper.SetEventFlag((int)f.FlagIdx, f.IsSet);
                            }
                            break;

                        case 1: // Trade Flags
                            foreach (var f in fGroup.Flags)
                            {
                                int fIdx = (int)f.FlagIdx;
                                m_savFile!.SetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (fIdx >> 3), fIdx & 7, f.IsSet);
                            }
                            break;
                    }

                    break;
                }
            }
        }

        public override void SyncEditedEventWork()
        {
            var eventWorkHelper = (IEventWorkArray<byte>)m_savFile!;

            foreach (var w in m_eventWorkList)
            {
                eventWorkHelper.SetWork((int)w.WorkIdx, (byte)w.Value);
            }
        }
    }
}
