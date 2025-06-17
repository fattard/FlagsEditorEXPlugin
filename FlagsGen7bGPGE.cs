namespace FlagsEditorEXPlugin
{
    internal class FlagsGen7bGPGE : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        EventWork7b? m_eventWorkData;

        bool[] GetEventFlags(EventWork7b source)
        {
            var result = new bool[source.CountFlag];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = source.GetFlag(i);
            }
            return result;
        }

        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;
            m_eventWorkData = ((SAV7b)m_savFile).EventWork;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen7blgpe");

            var workValues = new int[m_eventWorkData.CountWork];
            for (int i = 0; i < workValues.Length; i++)
            {
                workValues[i] = m_eventWorkData.GetWork(i);
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            AssembleList(s_flagsList_res[idxEventFlagsSection..], 0, "Event Flags", GetEventFlags(m_eventWorkData));
            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], workValues);
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

        public override EditableEventInfo[] GetSpecialEditableEvents()
        {
            int idx = 0;
            return
            [
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGenLGPE.specialEvtBtn_{idx}", "Bypass Fuchsia Gym requirement")),
            ];
        }

        public override void ProcessSpecialEventEdit(EditableEventInfo eventInfo)
        {
            int idx;

            switch (eventInfo.Index)
            {
                case 0: // Bypass Fuchsia Gym 50 species caught requirement
                    {
                        idx = 0x5D3; // t008gym01_tr0012_poison
                        m_eventWorkData!.SetFlag(idx, false);
                        m_flagsGroupsList[0].Flags[idx].IsSet = false;

                        idx = 0x0B0; // WK_SCENE_MAIN_M_KYO
                        m_eventWorkData!.SetWork(idx, 20);
                        m_eventWorkList[idx].Value = 20;
                    }
                    break;
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
                foreach (var f in m_flagsGroupsList[0].Flags)
                {
                    if (f.FlagTypeVal == flagType)
                    {
                        f.IsSet = value;
                        m_eventWorkData!.SetFlag((int)f.FlagIdx, value);
                    }
                }
            }
        }

        public override void SyncEditedFlags(FlagsGroup fGroup)
        {
            switch (fGroup.SourceIdx)
            {
                case 0: // Event Flags
                    foreach (var f in fGroup.Flags)
                    {
                        m_eventWorkData!.SetFlag((int)f.FlagIdx, f.IsSet);
                    }
                    break;
            }
        }

        public override void SyncEditedEventWork()
        {
            foreach (var w in m_eventWorkList)
            {
                m_eventWorkData!.SetWork((int)w.WorkIdx, (int)w.Value);
            }
        }
    }
}
