using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{
    internal class FlagsGen8bsBDSP : FlagsOrganizer
    {
        static string s_flagsList_res = null;

        BattleTrainerStatus8b m_battleTrainerStatus;
        FlagWork8b m_flagWork;

        const int Src_EventFlags = 0;
        const int Src_SysFlags = 1;
        const int Src_TrainerFlags = 2;

        protected override void InitFlagsData(SaveFile savFile)
        {
            m_savFile = savFile;
            m_battleTrainerStatus = (m_savFile as SAV8BS).BattleTrainer;
            m_flagWork = (m_savFile as SAV8BS).FlagWork;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            if (s_flagsList_res == null)
            {
                s_flagsList_res = ReadResFile("flags_gen8bsbdsp.txt");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxSysFlagsSection = s_flagsList_res.IndexOf("//\tSys Flags");
            int idxTrainerFlagsSection = s_flagsList_res.IndexOf("//\tTrainer Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            var sysFlagsVals = new bool[m_flagWork.CountSystem];
            for (int i = 0; i < sysFlagsVals.Length; i++)
            {
                sysFlagsVals[i] = m_flagWork.GetSystemFlag(i);
            }

            var battleTrainerVals = new bool[707];
            for (int i = 0; i < battleTrainerVals.Length; i++)
            {
                battleTrainerVals[i] = m_battleTrainerStatus.GetIsWin(i);
            }

            bool[] eventFlags = (m_savFile as IEventFlagArray).GetEventFlags();

            AssembleList(s_flagsList_res.Substring(idxEventFlagsSection), Src_EventFlags, eventFlags);
            AssembleList(s_flagsList_res.Substring(idxSysFlagsSection), Src_SysFlags, sysFlagsVals);
            AssembleList(s_flagsList_res.Substring(idxTrainerFlagsSection), Src_TrainerFlags, battleTrainerVals);

            AssembleWorkList<int>(s_flagsList_res.Substring(idxEventWorkSection));
        }

        public override bool SupportsEditingFlag(EventFlagType flagType)
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

        public override void MarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: true);
        }

        public override void UnmarkFlags(EventFlagType flagType)
        {
            ChangeFlagsVal(flagType, value: false);
        }

        void ChangeFlagsVal(EventFlagType flagType, bool value)
        {
            if (SupportsEditingFlag(flagType))
            {
                foreach (var f in m_eventFlagsList)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        int idx = (int)f.FlagIdx;

                        f.IsSet = value;

                        switch (f.SourceIdx)
                        {
                            case Src_EventFlags:
                                m_flagWork.SetFlag(idx, value);
                                break;

                            case Src_SysFlags:
                                m_flagWork.SetSystemFlag(idx, value);
                                break;

                            case Src_TrainerFlags:
                                m_battleTrainerStatus.SetIsWin(idx, value);
                                break;
                        }
                    }
                }
            }
        }
    }
}
