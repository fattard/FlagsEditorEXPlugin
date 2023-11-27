namespace FlagsEditorEXPlugin
{
    internal class FlagsGen2GS : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        enum FlagOffsets_INTL
        {
            StatusFlags = 0x23D9,
            StatusFlags2 = 0x23DA,
            MomSavingMoney = 0x23E1,
            JohtoBadges = 0x23E4,
            KantoBadges = 0x23E5,
            PokegearFlags = 0x24E4,
            TradeFlags = 0x24ED,
            EventFlags = 0x261F,
            BikeFlags = 0x27A7,
            DailyFlags1 = 0x27D0,
            DailyFlags2 = 0x27D1,
            FruitTreeFlags = 0x27D9,
            UnusedTwoDayTimerOn = 0x27EB,
            LuckyNumberShowFlag = 0x284F,
            VisitedSpawns = 0x2856,
            UnlockedUnowns = 0x2AA6,
            DayCareMan = 0x2AA8,
            DayCareLady = 0x2ADF,
        }

        int StatusFlagsOffset;
        int StatusFlags2Offset;
        int MomSavingMoneyOffset;
        int JohtoBadgesOffset;
        int KantoBadgesOffset;
        int PokegearFlagsOffset;
        int TradeFlagsOffset;
        int EventFlagsOffset;
        int BikeFlagsOffset;
        int DailyFlags1Offset;
        int DailyFlags2Offset;
        int BerryTreeFlagsOffset;
        int UnusedTwoDayTimerOnOffset;
        int LuckyNumberShowFlagOffset;
        int VisitedSpawnsOffset;
        int UnlockedUnownsOffset;
        int DayCareManOffset;
        int DayCareLadyOffset;

        const int Src_EventFlags = 0;
        const int Src_SysFlags = 1;
        const int Src_TradeFlags = 2;
        const int Src_BerryTreeFlags = 3;

        Dictionary<int, (int offset, int flagIdx)> m_sysFlagsTbl = new Dictionary<int, (int offset, int flagIdx)>();

        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;

            var savFile_SAV2 = (SAV2)m_savFile;

            /*if (savFile_SAV2.Japanese)
            {
            }
            else if (savFile_SAV2.Korean)
            {

            }
            else*/
            {
                StatusFlagsOffset = (int)FlagOffsets_INTL.StatusFlags;
                StatusFlags2Offset = (int)FlagOffsets_INTL.StatusFlags2;
                MomSavingMoneyOffset = (int)FlagOffsets_INTL.MomSavingMoney;
                JohtoBadgesOffset = (int)FlagOffsets_INTL.JohtoBadges;
                KantoBadgesOffset = (int)FlagOffsets_INTL.KantoBadges;
                PokegearFlagsOffset = (int)FlagOffsets_INTL.PokegearFlags;
                TradeFlagsOffset = (int)FlagOffsets_INTL.TradeFlags;
                EventFlagsOffset = (int)FlagOffsets_INTL.EventFlags;
                BikeFlagsOffset = (int)FlagOffsets_INTL.BikeFlags;
                DailyFlags1Offset = (int)FlagOffsets_INTL.DailyFlags1;
                DailyFlags2Offset = (int)FlagOffsets_INTL.DailyFlags2;
                BerryTreeFlagsOffset = (int)FlagOffsets_INTL.FruitTreeFlags;
                UnusedTwoDayTimerOnOffset = (int)FlagOffsets_INTL.UnusedTwoDayTimerOn;
                LuckyNumberShowFlagOffset = (int)FlagOffsets_INTL.LuckyNumberShowFlag;
                VisitedSpawnsOffset = (int)FlagOffsets_INTL.VisitedSpawns;
                UnlockedUnownsOffset = (int)FlagOffsets_INTL.UnlockedUnowns;
                DayCareManOffset = (int)FlagOffsets_INTL.DayCareMan;
                DayCareLadyOffset = (int)FlagOffsets_INTL.DayCareLady;
            }

            CreateSysFlagsTbl();

            // wTradeFlags
            bool[] result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(TradeFlagsOffset + (i >> 3), i & 7);
            }
            bool[] completedInGameTradeFlags = result;

            // wFruitTreeFlags
            result = new bool[32];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(BerryTreeFlagsOffset + (i >> 3), i & 7);
            }
            bool[] berryTreesFlags = result;

            // EngineFlags
            result = new bool[m_sysFlagsTbl.Count];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = GetSysFlag(i);
            }
            bool[] sysFlags = result;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen2gs");

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxSysFlagsSection = s_flagsList_res.IndexOf("//\tSys Flags");
            int idxTradeFlagsSection = s_flagsList_res.IndexOf("//\tTrade Flags");
            int idxBerryTreesSection = s_flagsList_res.IndexOf("//\tBerry Trees Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");


            AssembleList(s_flagsList_res[idxEventFlagsSection..], Src_EventFlags, "Event Flags", ((IEventFlagArray)m_savFile!).GetEventFlags());
            AssembleList(s_flagsList_res[idxSysFlagsSection..], Src_SysFlags, "Sys Flags", sysFlags);
            AssembleList(s_flagsList_res[idxTradeFlagsSection..], Src_TradeFlags, "Trade Flags", completedInGameTradeFlags);
            AssembleList(s_flagsList_res[idxBerryTreesSection..], Src_BerryTreeFlags, "Berry Trees Flags", berryTreesFlags);

            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], ((IEventWorkArray<byte>)m_savFile!).GetAllEventWork());
        }

        void CreateSysFlagsTbl()
        {
            // Based on data/events/engine_flags

            int idx = 0;

            m_sysFlagsTbl = new Dictionary<int, (int offset, int flagIdx)>()
            {
                { idx++, (PokegearFlagsOffset, 1) }, // POKEGEAR_RADIO_CARD_F
                { idx++, (PokegearFlagsOffset, 0) }, // POKEGEAR_MAP_CARD_F
                { idx++, (PokegearFlagsOffset, 2) }, // POKEGEAR_PHONE_CARD_F
                { idx++, (PokegearFlagsOffset, 3) }, // POKEGEAR_EXPN_CARD_F
                { idx++, (PokegearFlagsOffset, 7) }, // POKEGEAR_OBTAINED_F

                { idx++, (DayCareManOffset, 6) }, // DAYCAREMAN_HAS_EGG_F
                { idx++, (DayCareManOffset, 0) }, // DAYCAREMAN_HAS_MON_F
                { idx++, (DayCareLadyOffset, 0) }, // DAYCARELADY_HAS_MON_F

                { idx++, (MomSavingMoneyOffset, 0) }, // MOM_SAVING_SOME_MONEY_F
                { idx++, (MomSavingMoneyOffset, 7) }, // MOM_ACTIVE_F

                { idx++, (UnusedTwoDayTimerOnOffset, 0) },

                { idx++, (StatusFlagsOffset, 0) }, // STATUSFLAGS_POKEDEX_F
                { idx++, (StatusFlagsOffset, 1) }, // STATUSFLAGS_UNOWN_DEX_F
                { idx++, (StatusFlagsOffset, 3) }, // STATUSFLAGS_CAUGHT_POKERUS_F
                { idx++, (StatusFlagsOffset, 4) }, // STATUSFLAGS_ROCKET_SIGNAL_F
                { idx++, (StatusFlagsOffset, 6) }, // STATUSFLAGS_HALL_OF_FAME_F

                { idx++, (StatusFlags2Offset, 2) }, // STATUSFLAGS2_BUG_CONTEST_TIMER_F
                { idx++, (StatusFlags2Offset, 1) }, // STATUSFLAGS2_SAFARI_GAME_F
                { idx++, (StatusFlags2Offset, 0) }, // STATUSFLAGS2_ROCKETS_IN_RADIO_TOWER_F
                { idx++, (StatusFlags2Offset, 4) }, // STATUSFLAGS2_BIKE_SHOP_CALL_F
                { idx++, (StatusFlags2Offset, 5) }, // STATUSFLAGS2_UNUSED_5_F
                { idx++, (StatusFlags2Offset, 6) }, // STATUSFLAGS2_REACHED_GOLDENROD_F
                { idx++, (StatusFlags2Offset, 7) }, // STATUSFLAGS2_ROCKETS_IN_MAHOGANY_F

                { idx++, (BikeFlagsOffset, 0) }, // BIKEFLAGS_STRENGTH_ACTIVE_F
                { idx++, (BikeFlagsOffset, 1) }, // BIKEFLAGS_ALWAYS_ON_BIKE_F
                { idx++, (BikeFlagsOffset, 2) }, // BIKEFLAGS_DOWNHILL_F

                { idx++, (JohtoBadgesOffset, 0) }, // ZEPHYRBADGE
                { idx++, (JohtoBadgesOffset, 1) }, // HIVEBADGE
                { idx++, (JohtoBadgesOffset, 2) }, // PLAINBADGE
                { idx++, (JohtoBadgesOffset, 3) }, // FOGBADGE
                { idx++, (JohtoBadgesOffset, 4) }, // MINERALBADGE
                { idx++, (JohtoBadgesOffset, 5) }, // STORMBADGE
                { idx++, (JohtoBadgesOffset, 6) }, // GLACIERBADGE
                { idx++, (JohtoBadgesOffset, 7) }, // RISINGBADGE

                { idx++, (KantoBadgesOffset, 0) }, // BOULDERBADGE
                { idx++, (KantoBadgesOffset, 1) }, // CASCADEBADGE
                { idx++, (KantoBadgesOffset, 2) }, // THUNDERBADGE
                { idx++, (KantoBadgesOffset, 3) }, // RAINBOWBADGE
                { idx++, (KantoBadgesOffset, 4) }, // SOULBADGE
                { idx++, (KantoBadgesOffset, 5) }, // MARSHBADGE
                { idx++, (KantoBadgesOffset, 6) }, // VOLCANOBADGE
                { idx++, (KantoBadgesOffset, 7) }, // EARTHBADGE

                { idx++, (UnlockedUnownsOffset, 0) }, // UNLOCKED_UNOWNS_A_TO_K_F
                { idx++, (UnlockedUnownsOffset, 1) }, // UNLOCKED_UNOWNS_L_TO_R_F
                { idx++, (UnlockedUnownsOffset, 2) }, // UNLOCKED_UNOWNS_S_TO_W_F
                { idx++, (UnlockedUnownsOffset, 3) }, // UNLOCKED_UNOWNS_X_TO_Z_F
                { idx++, (UnlockedUnownsOffset, 4) },
                { idx++, (UnlockedUnownsOffset, 5) },
                { idx++, (UnlockedUnownsOffset, 6) },
                { idx++, (UnlockedUnownsOffset, 7) },

                { idx++, (VisitedSpawnsOffset, 0) },  // SPAWN_HOME
                { idx++, (VisitedSpawnsOffset, 1) },  // SPAWN_DEBUG
                { idx++, (VisitedSpawnsOffset, 2) },  // SPAWN_PALLET
                { idx++, (VisitedSpawnsOffset, 3) },  // SPAWN_VIRIDIAN
                { idx++, (VisitedSpawnsOffset, 4) },  // SPAWN_PEWTER
                { idx++, (VisitedSpawnsOffset, 5) },  // SPAWN_CERULEAN
                { idx++, (VisitedSpawnsOffset, 6) },  // SPAWN_ROCK_TUNNEL
                { idx++, (VisitedSpawnsOffset, 7) },  // SPAWN_VERMILION
                { idx++, (VisitedSpawnsOffset, 8) },  // SPAWN_LAVENDER
                { idx++, (VisitedSpawnsOffset, 9) },  // SPAWN_SAFFRON
                { idx++, (VisitedSpawnsOffset, 10) }, // SPAWN_CELADON
                { idx++, (VisitedSpawnsOffset, 11) }, // SPAWN_FUCHSIA
                { idx++, (VisitedSpawnsOffset, 12) }, // SPAWN_CINNABAR
                { idx++, (VisitedSpawnsOffset, 13) }, // SPAWN_INDIGO
                { idx++, (VisitedSpawnsOffset, 14) }, // SPAWN_NEW_BARK
                { idx++, (VisitedSpawnsOffset, 15) }, // SPAWN_CHERRYGROVE
                { idx++, (VisitedSpawnsOffset, 16) }, // SPAWN_VIOLET
                { idx++, (VisitedSpawnsOffset, 18) }, // SPAWN_AZALEA
                { idx++, (VisitedSpawnsOffset, 19) }, // SPAWN_CIANWOOD
                { idx++, (VisitedSpawnsOffset, 20) }, // SPAWN_GOLDENROD
                { idx++, (VisitedSpawnsOffset, 21) }, // SPAWN_OLIVINE
                { idx++, (VisitedSpawnsOffset, 22) }, // SPAWN_ECRUTEAK
                { idx++, (VisitedSpawnsOffset, 23) }, // SPAWN_MAHOGANY
                { idx++, (VisitedSpawnsOffset, 24) }, // SPAWN_LAKE_OF_RAGE
                { idx++, (VisitedSpawnsOffset, 25) }, // SPAWN_BLACKTHORN
                { idx++, (VisitedSpawnsOffset, 26) }, // SPAWN_MT_SILVER
                { idx++, (VisitedSpawnsOffset, 28) }, // NUM_SPAWNS

                { idx++, (LuckyNumberShowFlagOffset, 0) }, // LUCKYNUMBERSHOW_GAME_OVER_F

                { idx++, (StatusFlags2Offset, 3) }, // STATUSFLAGS2_UNUSED_3_F

                { idx++, (DailyFlags1Offset, 0) }, // DAILYFLAGS1_KURT_MAKING_BALLS_F
                { idx++, (DailyFlags1Offset, 1) }, // DAILYFLAGS1_BUG_CONTEST_F
                { idx++, (DailyFlags1Offset, 2) }, // DAILYFLAGS1_SWARM_F
                { idx++, (DailyFlags1Offset, 3) }, // DAILYFLAGS1_TIME_CAPSULE_F
                { idx++, (DailyFlags1Offset, 4) }, // DAILYFLAGS1_ALL_FRUIT_TREES_F
                { idx++, (DailyFlags1Offset, 5) }, // DAILYFLAGS1_GOT_SHUCKIE_TODAY_F
                { idx++, (DailyFlags1Offset, 6) }, // DAILYFLAGS1_GOLDENROD_UNDERGROUND_BARGAIN_F
                { idx++, (DailyFlags1Offset, 7) }, // DAILYFLAGS1_TRAINER_HOUSE_F

                { idx++, (DailyFlags2Offset, 0) }, // DAILYFLAGS2_MT_MOON_SQUARE_CLEFAIRY_F
                { idx++, (DailyFlags2Offset, 1) }, // DAILYFLAGS2_UNION_CAVE_LAPRAS_F
                { idx++, (DailyFlags1Offset, 2) }, // DAILYFLAGS2_GOLDENROD_UNDERGROUND_GOT_HAIRCUT_F
                { idx++, (DailyFlags1Offset, 3) }, // DAILYFLAGS2_GOLDENROD_DEPT_STORE_TM27_RETURN_F
                { idx++, (DailyFlags1Offset, 4) }, // DAILYFLAGS2_DAISYS_GROOMING_F
                { idx++, (DailyFlags1Offset, 5) }, // DAILYFLAGS2_INDIGO_PLATEAU_RIVAL_FIGHT_F
            };
        }

        bool GetSysFlag(int idx)
        {
            var entry = m_sysFlagsTbl[idx];
            return m_savFile!.GetFlag(entry.offset + (entry.flagIdx >> 3), entry.flagIdx & 7);
        }

        void SetSysFlag(int idx, bool value)
        {
            var entry = m_sysFlagsTbl[idx];
            m_savFile!.SetFlag(entry.offset + (entry.flagIdx >> 3), entry.flagIdx & 7, value);
        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType) => flagType switch
        {
#if DEBUG
            EventFlagType.FieldItem or
            EventFlagType.HiddenItem or
            EventFlagType.TrainerBattle or
            EventFlagType.StaticEncounter or
            EventFlagType.InGameTrade or
            EventFlagType.ItemGift or
            EventFlagType.PkmnGift or
            EventFlagType.FlySpot or
            EventFlagType.BerryTree or
            EventFlagType.Collectable
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

                foreach (var fGroup in m_flagsGroupsList)
                {
                    foreach (var f in fGroup.Flags)
                    {
                        if (f.FlagTypeVal == flagType)
                        {
                            int fIdx = (int)f.FlagIdx;

                            f.IsSet = value;

                            switch (f.SourceIdx)
                            {
                                case Src_EventFlags:
                                    flagHelper.SetEventFlag(fIdx, value);
                                    break;

                                case Src_TradeFlags:
                                    m_savFile!.SetFlag(TradeFlagsOffset + (fIdx >> 3), fIdx & 7, value);
                                    break;

                                case Src_BerryTreeFlags:
                                    m_savFile!.SetFlag(BerryTreeFlagsOffset + (fIdx >> 3), fIdx & 7, value);
                                    break;

                                case Src_SysFlags:
                                    SetSysFlag(fIdx, value);
                                    break;
                            }
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
                        case Src_EventFlags:
                            foreach (var f in fGroup.Flags)
                            {
                                flagHelper.SetEventFlag((int)f.FlagIdx, f.IsSet);
                            }
                            break;

                        case Src_TradeFlags:
                            foreach (var f in fGroup.Flags)
                            {
                                int fIdx = (int)f.FlagIdx;
                                m_savFile!.SetFlag(TradeFlagsOffset + (fIdx >> 3), fIdx & 7, f.IsSet);
                            }
                            break;

                        case Src_BerryTreeFlags:
                            foreach (var f in fGroup.Flags)
                            {
                                int fIdx = (int)f.FlagIdx;
                                m_savFile!.SetFlag(BerryTreeFlagsOffset + (fIdx >> 3), fIdx & 7, f.IsSet);
                            }
                            break;

                        case Src_SysFlags:
                            foreach (var f in fGroup.Flags)
                            {
                                int fIdx = (int)f.FlagIdx;
                                SetSysFlag(fIdx, f.IsSet);
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
