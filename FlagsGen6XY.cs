using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{
    internal class FlagsGen6XY : FlagsOrganizer
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
                s_flagsList_res = ReadResFile("flags_gen6xy.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), 0, "Event Flags", (m_savFile as IEventFlagArray).GetEventFlags());
            AssembleWorkList(s_flagsList_res.Substring(idxEventWorkSection), (m_savFile as IEventWorkArray<ushort>).GetAllEventWork());
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
                var flagHelper = (m_savFile as IEventFlagArray);

                foreach (var f in m_flagsGroupsList[0].Flags)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        f.IsSet = value;
                        flagHelper.SetEventFlag((int)f.FlagIdx, value);
                    }
                }
            }
        }

        public override void SyncEditedFlags(int sourceIdx)
        {
            var flagHelper = (m_savFile as IEventFlagArray);

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
                    }

                    break;
                }
            }
        }

        public override void SyncEditedEventWork()
        {
            var eventWorkHelper = (m_savFile as IEventWorkArray<ushort>);

            foreach (var w in m_eventWorkList)
            {
                eventWorkHelper.SetWork((int)w.WorkIdx, (ushort)w.Value);
            }
        }
    }
}
