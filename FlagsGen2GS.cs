using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{
    internal class FlagsGen2GS : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        enum FlagOffsets
        {
            CompletedInGameTradeFlags = 0x24ED
        }

        protected override void InitFlagsData(SaveFile savFile, string resData)
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

            if (resData != null)
            {
                s_flagsList_res = resData;
            }
            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen2gs.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxTradeFlagsSection = s_flagsList_res.IndexOf("//\tTrade Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");


            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), 0, "Event Flags", (m_savFile as IEventFlagArray).GetEventFlags());
            AssembleList(s_flagsList_res.Substring(idxTradeFlagsSection), 1, "Trade Flags", completedInGameTradeFlags);

            AssembleWorkList(s_flagsList_res.Substring(idxEventWorkSection), (m_savFile as IEventWorkArray<byte>).GetAllEventWork());
        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType)
        {
            switch (flagType)
            {
                case EventFlagType.FieldItem:
                case EventFlagType.HiddenItem:
                case EventFlagType.TrainerBattle:
                case EventFlagType.InGameTrade:
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
                var flagHelper = (m_savFile as IEventFlagArray);

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
                                m_savFile.SetFlag((int)FlagOffsets.CompletedInGameTradeFlags + (fIdx >> 3), fIdx & 7, value);
                                break;
                        }
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
    }
}
