namespace FlagsEditorEXPlugin
{
    internal class FlagsGen5B2W2 : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen5b2w2");

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            var evtWork = ((IEventFlagProvider37)m_savFile)!.EventWork;

            AssembleList(s_flagsList_res[idxEventFlagsSection..], 0, "Event Flags", evtWork.GetEventFlags());
            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], evtWork.GetAllEventWork());
        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType) => flagType switch
        {
#if DEBUG
            EventFlagType.FieldItem or
            EventFlagType.HiddenItem or
            EventFlagType.TrainerBattle
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
                var flagHelper = ((IEventFlagProvider37)m_savFile!).EventWork;

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

        public override void SyncEditedFlags(FlagsGroup fGroup)
        {
            var flagHelper = ((IEventFlagProvider37)m_savFile!).EventWork;

            switch (fGroup.SourceIdx)
            {
                case 0: // Event Flags
                    foreach (var f in fGroup.Flags)
                    {
                        flagHelper.SetEventFlag((int)f.FlagIdx, f.IsSet);
                    }
                    break;
            }
        }

        public override void SyncEditedEventWork()
        {
            var eventWorkHelper = ((IEventFlagProvider37)m_savFile!).EventWork;

            foreach (var w in m_eventWorkList)
            {
                eventWorkHelper.SetWork((int)w.WorkIdx, (ushort)w.Value);
            }
        }
    }
}
