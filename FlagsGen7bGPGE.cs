using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{
    internal class FlagsGen7bGPGE : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        EventWork7b m_eventWorkData;

        protected override void InitFlagsData(SaveFile savFile, string resData)
        {
            m_savFile = savFile;
            m_eventWorkData = (m_savFile as SAV7b).EventWork;

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
                s_flagsList_res = ReadResFile("flags_gen7blgpe.txt");
            }

            AssembleList(s_flagsList_res, 0, "Event Flags", (m_savFile as IEventFlagArray).GetEventFlags());

            // AssembleWorkList<int>
            m_eventWorkList.Clear();
            for (uint i = 0; i < m_eventWorkData.CountWork; i++)
            {
                var workDetail = new WorkDetail(i, EventFlagType._Unknown, "");
                workDetail.Value = Convert.ToInt64(m_eventWorkData.GetWork((int)workDetail.WorkIdx));
                m_eventWorkList.Add(workDetail);
            }
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

                foreach (var f in m_flagsSetList[0].Flags)
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

        }
    }
}
