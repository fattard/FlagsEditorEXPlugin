namespace FlagsEditorEXPlugin
{
    internal class FlagsGen2C : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        enum FlagOffsets_INTL
        {
            VariableSprites = 0x23BC,
            StatusFlags = 0x23DA,
            StatusFlags2 = 0x23DB,
            MomSavingMoney = 0x23E2,
            JohtoBadges = 0x23E5,
            KantoBadges = 0x23E6,
            PokegearFlags = 0x24E5,
            TradeFlags = 0x24EE,
            FarfetchdPosition = 0x24F2,
            EventFlags = 0x2600,
            CelebiEvent = 0x2781,
            BikeFlags = 0x2783,
            DailyFlags1 = 0x27AC,
            DailyFlags2 = 0x27AD,
            SwarmFlags = 0x27AE,
            FruitTreeFlags = 0x27B5,
            UnusedTwoDayTimerOn = 0x27C7,
            DailyRematchFlags = 0x27DA,
            DailyPhoneItemFlags = 0x27DE,
            DailyPhoneTimeOfDayFlags = 0x27E2,
            LuckyNumberShowFlag = 0x282B,
            VisitedSpawns = 0x2833,
            UnlockedUnowns = 0x2A81,
            DayCareMan = 0x2A83,
            DayCareLady = 0x2ABA,
            GSBallFlag = 0x3E3C,
            PlayerGender = 0x3E3D,
            GSBallFlagBackup = 0x3E44,
        }

        int VariableSpritesOffset;
        int StatusFlagsOffset;
        int StatusFlags2Offset;
        int MomSavingMoneyOffset;
        int JohtoBadgesOffset;
        int KantoBadgesOffset;
        int PokegearFlagsOffset;
        int TradeFlagsOffset;
        int FarfetchdPositionOffset;
        int EventFlagsOffset;
        int CelebiEventOffset;
        int BikeFlagsOffset;
        int DailyFlags1Offset;
        int DailyFlags2Offset;
        int SwarmFlagsOffset;
        int BerryTreeFlagsOffset;
        int UnusedTwoDayTimerOnOffset;
        int DailyRematchFlagsOffset;
        int DailyPhoneItemFlagsOffset;
        int DailyPhoneTimeOfDayFlagsOffset;
        int LuckyNumberShowFlagOffset;
        int VisitedSpawnsOffset;
        int UnlockedUnownsOffset;
        int DayCareManOffset;
        int DayCareLadyOffset;
        int GSBallFlagOffset;
        int PlayerGenderOffset;
        int GSBallFlagBackupOffset;

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
                VariableSpritesOffset = (int)FlagOffsets_INTL.VariableSprites;
                StatusFlagsOffset = (int)FlagOffsets_INTL.StatusFlags;
                StatusFlags2Offset = (int)FlagOffsets_INTL.StatusFlags2;
                MomSavingMoneyOffset = (int)FlagOffsets_INTL.MomSavingMoney;
                JohtoBadgesOffset = (int)FlagOffsets_INTL.JohtoBadges;
                KantoBadgesOffset = (int)FlagOffsets_INTL.KantoBadges;
                PokegearFlagsOffset = (int)FlagOffsets_INTL.PokegearFlags;
                TradeFlagsOffset = (int)FlagOffsets_INTL.TradeFlags;
                FarfetchdPositionOffset = (int)FlagOffsets_INTL.FarfetchdPosition;
                EventFlagsOffset = (int)FlagOffsets_INTL.EventFlags;
                CelebiEventOffset = (int)FlagOffsets_INTL.CelebiEvent;
                BikeFlagsOffset = (int)FlagOffsets_INTL.BikeFlags;
                DailyFlags1Offset = (int)FlagOffsets_INTL.DailyFlags1;
                DailyFlags2Offset = (int)FlagOffsets_INTL.DailyFlags2;
                SwarmFlagsOffset = (int)FlagOffsets_INTL.SwarmFlags;
                BerryTreeFlagsOffset = (int)FlagOffsets_INTL.FruitTreeFlags;
                UnusedTwoDayTimerOnOffset = (int)FlagOffsets_INTL.UnusedTwoDayTimerOn;
                DailyRematchFlagsOffset = (int)FlagOffsets_INTL.DailyRematchFlags;
                DailyPhoneItemFlagsOffset = (int)FlagOffsets_INTL.DailyPhoneItemFlags;
                DailyPhoneTimeOfDayFlagsOffset = (int)FlagOffsets_INTL.DailyPhoneTimeOfDayFlags;
                LuckyNumberShowFlagOffset = (int)FlagOffsets_INTL.LuckyNumberShowFlag;
                VisitedSpawnsOffset = (int)FlagOffsets_INTL.VisitedSpawns;
                UnlockedUnownsOffset = (int)FlagOffsets_INTL.UnlockedUnowns;
                DayCareManOffset = (int)FlagOffsets_INTL.DayCareMan;
                DayCareLadyOffset = (int)FlagOffsets_INTL.DayCareLady;
                GSBallFlagOffset = (int)FlagOffsets_INTL.GSBallFlag;
                PlayerGenderOffset = (int)FlagOffsets_INTL.PlayerGender;
                GSBallFlagBackupOffset = (int)FlagOffsets_INTL.GSBallFlagBackup;
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

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen2c");

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
                { idx++, (StatusFlagsOffset, 7) }, // STATUSFLAGS_MAIN_MENU_MOBILE_CHOICES_F

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
                { idx++, (DailyFlags1Offset, 2) }, // DAILYFLAGS1_FISH_SWARM_F
                { idx++, (DailyFlags1Offset, 3) }, // DAILYFLAGS1_TIME_CAPSULE_F
                { idx++, (DailyFlags1Offset, 4) }, // DAILYFLAGS1_ALL_FRUIT_TREES_F
                { idx++, (DailyFlags1Offset, 5) }, // DAILYFLAGS1_GOT_SHUCKIE_TODAY_F
                { idx++, (DailyFlags1Offset, 6) }, // DAILYFLAGS1_GOLDENROD_UNDERGROUND_BARGAIN_F
                { idx++, (DailyFlags1Offset, 7) }, // DAILYFLAGS1_TRAINER_HOUSE_F

                { idx++, (DailyFlags2Offset, 0) }, // DAILYFLAGS2_MT_MOON_SQUARE_CLEFAIRY_F
                { idx++, (DailyFlags2Offset, 1) }, // DAILYFLAGS2_UNION_CAVE_LAPRAS_F
                { idx++, (DailyFlags2Offset, 2) }, // DAILYFLAGS2_GOLDENROD_UNDERGROUND_GOT_HAIRCUT_F
                { idx++, (DailyFlags2Offset, 3) }, // DAILYFLAGS2_GOLDENROD_DEPT_STORE_TM27_RETURN_F
                { idx++, (DailyFlags2Offset, 4) }, // DAILYFLAGS2_DAISYS_GROOMING_F
                { idx++, (DailyFlags2Offset, 5) }, // DAILYFLAGS2_INDIGO_PLATEAU_RIVAL_FIGHT_F
                { idx++, (DailyFlags2Offset, 6) }, // DAILYFLAGS2_MOVE_TUTOR_F
                { idx++, (DailyFlags2Offset, 7) }, // DAILYFLAGS2_BUENAS_PASSWORD_F

                { idx++, (SwarmFlagsOffset, 0) }, // SWARMFLAGS_BUENAS_PASSWORD_F
                { idx++, (SwarmFlagsOffset, 1) }, // SWARMFLAGS_GOLDENROD_DEPT_STORE_SALE_F

                { idx++, (0, 7) }, // GAME_TIMER_MOBILE_F

                { idx++, (PlayerGenderOffset, 0) }, // PLAYERGENDER_FEMALE_F

                { idx++, (CelebiEventOffset, 2) }, // CELEBIEVENT_FOREST_IS_RESTLESS_F

                { idx++, (DailyRematchFlagsOffset, 0) },  // jack
                { idx++, (DailyRematchFlagsOffset, 1) },  // huey
                { idx++, (DailyRematchFlagsOffset, 2) },  // gaven
                { idx++, (DailyRematchFlagsOffset, 3) },  // beth
                { idx++, (DailyRematchFlagsOffset, 4) },  // jose
                { idx++, (DailyRematchFlagsOffset, 5) },  // reena
                { idx++, (DailyRematchFlagsOffset, 6) },  // joey
                { idx++, (DailyRematchFlagsOffset, 7) },  // wade
                { idx++, (DailyRematchFlagsOffset, 8) },  // ralph
                { idx++, (DailyRematchFlagsOffset, 9) },  // liz
                { idx++, (DailyRematchFlagsOffset, 10) }, // anthony
                { idx++, (DailyRematchFlagsOffset, 11) }, // todd
                { idx++, (DailyRematchFlagsOffset, 12) }, // gina
                { idx++, (DailyRematchFlagsOffset, 13) }, // arnie
                { idx++, (DailyRematchFlagsOffset, 14) }, // alan
                { idx++, (DailyRematchFlagsOffset, 15) }, // dana
                { idx++, (DailyRematchFlagsOffset, 16) }, // chad
                { idx++, (DailyRematchFlagsOffset, 17) }, // tully
                { idx++, (DailyRematchFlagsOffset, 18) }, // brent
                { idx++, (DailyRematchFlagsOffset, 19) }, // tiffany
                { idx++, (DailyRematchFlagsOffset, 20) }, // vance
                { idx++, (DailyRematchFlagsOffset, 21) }, // wilton
                { idx++, (DailyRematchFlagsOffset, 22) }, // parry
                { idx++, (DailyRematchFlagsOffset, 23) }, // erin

                { idx++, (DailyPhoneItemFlagsOffset, 0) }, // beverly has nugget
                { idx++, (DailyPhoneItemFlagsOffset, 1) }, // jose has star piece
                { idx++, (DailyPhoneItemFlagsOffset, 2) }, // wade has item
                { idx++, (DailyPhoneItemFlagsOffset, 3) }, // gina has leaf stone
                { idx++, (DailyPhoneItemFlagsOffset, 4) }, // alan has fire stone
                { idx++, (DailyPhoneItemFlagsOffset, 5) }, // liz has thunderstone
                { idx++, (DailyPhoneItemFlagsOffset, 6) }, // derek has nugget
                { idx++, (DailyPhoneItemFlagsOffset, 7) }, // tully has water stone
                { idx++, (DailyPhoneItemFlagsOffset, 8) }, // tiffany has pink bow
                { idx++, (DailyPhoneItemFlagsOffset, 9) }, // wilton has item

                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 0) },  // jack
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 1) },  // huey
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 2) },  // gaven
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 3) },  // beth
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 4) },  // jose
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 5) },  // reena
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 6) },  // joey
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 7) },  // wade
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 8) },  // ralph
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 9) },  // liz
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 10) }, // anthony
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 11) }, // todd
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 12) }, // gina
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 13) }, // arnie
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 14) }, // alan
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 15) }, // dana
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 16) }, // chad
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 17) }, // tully
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 18) }, // brent
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 19) }, // tiffany
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 20) }, // vance
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 21) }, // wilton
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 22) }, // parry
                { idx++, (DailyPhoneTimeOfDayFlagsOffset, 23) }, // erin

                { idx++, (0, 2) }, // PLAYERSPRITESETUP_FEMALE_TO_MALE_F

                { idx++, (SwarmFlagsOffset, 2) }, // SWARMFLAGS_DUNSPARCE_SWARM_F
                { idx++, (SwarmFlagsOffset, 3) }, // SWARMFLAGS_YANMA_SWARM_F
            };
        }

        bool GetSysFlag(int idx)
        {
            var (offset, flagIdx) = m_sysFlagsTbl[idx];
            return m_savFile!.GetFlag(offset + (flagIdx >> 3), flagIdx & 7);
        }

        void SetSysFlag(int idx, bool value)
        {
            var (offset, flagIdx) = m_sysFlagsTbl[idx];
            m_savFile!.SetFlag(offset + (flagIdx >> 3), flagIdx & 7, value);
        }

        public override bool SupportsBulkEditingFlags(EventFlagType flagType) => flagType switch
        {
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
            _ => false
        };

        public override EditableEventInfo[] GetSpecialEditableEvents()
        {
            int idx = 0;
            return new EditableEventInfo[]
            {
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Reset Slowpoke Well events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Reset Lake of Rage events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Reset Radio Tower events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Reset S.S. Aqua first trip events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Reset Legendary Beasts events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Unblock Mt. Silver access")),

                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Unblock Tin Tower stairs")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen2.specialEvtBtn_{idx}", "Reset Celebi event")),
            };
        }

        public override void ProcessSpecialEventEdit(EditableEventInfo eventInfo)
        {
            int idx;
            var flagHelper = (IEventFlagArray)m_savFile!;
            var eventWorkHelper = (IEventWorkArray<byte>)m_savFile!;

            switch (eventInfo.Index)
            {
                case 0: // Slowpoke Well / Ilex Forest
                    {
                        int[] evt_ids =
                        {
                            0x2B, // EVENT_CLEARED_SLOWPOKE_WELL
                            0x6FB, // EVENT_SLOWPOKE_WELL_SLOWPOKES
                            0x6FC, // EVENT_SLOWPOKE_WELL_ROCKETS
                            0x740, // EVENT_SLOWPOKE_WELL_KURT

                            0x29, // EVENT_HERDED_FARFETCHD
                            0x10, // EVENT_GOT_HM01_CUT
                        };

                        foreach (var evt in evt_ids)
                        {
                            flagHelper.SetEventFlag(evt, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[evt].IsSet = false;
                        }

                        m_savFile!.Data[FarfetchdPositionOffset] = 1;
                    }
                    break;

                case 1: // Lake of Rage / Team Rocket HQ
                    {
                        int[] evt_ids =
                        {
                            0x751, // EVENT_LAKE_OF_RAGE_RED_GYARADOS

                            0x22, // EVENT_CLEARED_ROCKET_HIDEOUT
                            0x4C, // EVENT_LANCE_HEALED_YOU_IN_TEAM_ROCKET_BASE
                            0x60, // EVENT_DECIDED_TO_HELP_LANCE
                            0x2E2, // EVENT_UNCOVERED_STAIRCASE_IN_MAHOGANY_MART
                            0x2E3, // EVENT_TURNED_OFF_SECURITY_CAMERAS
                            0x2E4, // EVENT_SECURITY_CAMERA_1
                            0x2E5, // EVENT_SECURITY_CAMERA_2
                            0x2E6, // EVENT_SECURITY_CAMERA_3
                            0x2E7, // EVENT_SECURITY_CAMERA_4
                            0x2E8, // EVENT_SECURITY_CAMERA_5
                            0x2E9, // EVENT_EXPLODING_TRAP_1
                            0x2EA, // EVENT_EXPLODING_TRAP_2
                            0x2EB, // EVENT_EXPLODING_TRAP_3
                            0x2EC, // EVENT_EXPLODING_TRAP_4
                            0x2ED, // EVENT_EXPLODING_TRAP_5
                            0x2EE, // EVENT_EXPLODING_TRAP_6
                            0x2EF, // EVENT_EXPLODING_TRAP_7
                            0x2F0, // EVENT_EXPLODING_TRAP_8
                            0x2F1, // EVENT_EXPLODING_TRAP_9
                            0x2F2, // EVENT_EXPLODING_TRAP_10
                            0x2F3, // EVENT_EXPLODING_TRAP_11
                            0x2F4, // EVENT_EXPLODING_TRAP_12
                            0x2F5, // EVENT_EXPLODING_TRAP_13
                            0x2F6, // EVENT_EXPLODING_TRAP_14
                            0x2F7, // EVENT_EXPLODING_TRAP_15
                            0x2F8, // EVENT_EXPLODING_TRAP_16
                            0x2F9, // EVENT_EXPLODING_TRAP_17
                            0x2FA, // EVENT_EXPLODING_TRAP_18
                            0x2FB, // EVENT_EXPLODING_TRAP_19
                            0x2FC, // EVENT_EXPLODING_TRAP_20
                            0x2FD, // EVENT_EXPLODING_TRAP_21
                            0x2FE, // EVENT_EXPLODING_TRAP_22
                            0x2FF, // EVENT_LEARNED_HAIL_GIOVANNI
                            0x300, // EVENT_OPENED_DOOR_TO_ROCKET_HIDEOUT_TRANSMITTER
                            0x301, // EVENT_LEARNED_SLOWPOKETAIL
                            0x302, // EVENT_LEARNED_RATICATE_TAIL
                            0x303, // EVENT_OPENED_DOOR_TO_GIOVANNIS_OFFICE
                            0x6D6, // EVENT_TEAM_ROCKET_BASE_B2F_LANCE
                            0x6D7, // EVENT_TEAM_ROCKET_BASE_B3F_LANCE_PASSWORDS
                            0x6DA, // EVENT_TEAM_ROCKET_BASE_POPULATION
                            0x6DB, // EVENT_TEAM_ROCKET_BASE_B3F_EXECUTIVE
                            0x6DC, // EVENT_ROUTE_43_GATE_ROCKETS
                            0x6E0, // EVENT_TEAM_ROCKET_BASE_B2F_ELECTRODE_1
                            0x6E1, // EVENT_TEAM_ROCKET_BASE_B2F_ELECTRODE_2
                            0x6E2, // EVENT_TEAM_ROCKET_BASE_B2F_ELECTRODE_3
                        };

                        foreach (var evt in evt_ids)
                        {
                            flagHelper.SetEventFlag(evt, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[evt].IsSet = false;
                        }

                        idx = 0x0E; // ENGINE_ROCKET_SIGNAL_ON_CH20
                        SetSysFlag(idx, true);
                        m_flagsGroupsList[Src_SysFlags].Flags[idx].IsSet = true;

                        idx = 0x17; // ENGINE_ROCKETS_IN_MAHOGANY
                        SetSysFlag(idx, true);
                        m_flagsGroupsList[Src_SysFlags].Flags[idx].IsSet = true;

                        idx = 0x41; // wTeamRocketBaseB2FSceneID
                        eventWorkHelper.SetWork(idx, 0);

                        idx = 0x42; // wTeamRocketBaseB3FSceneID
                        eventWorkHelper.SetWork(idx, 0);
                    }
                    break;

                case 2: // Radio Tower takeover events
                    {
                        int[] evt_ids =
                        {
                            0x21, // EVENT_CLEARED_RADIO_TOWER
                            0x25, // EVENT_USED_THE_CARD_KEY_IN_THE_RADIO_TOWER
                            0x49, // EVENT_USED_BASEMENT_KEY
                            0x4A, // EVENT_RECEIVED_CARD_KEY
                            0x6CC, // EVENT_GOLDENROD_CITY_ROCKET_SCOUT
                            0x6CD, // EVENT_GOLDENROD_CITY_ROCKET_TAKEOVER
                            0x6CE, // EVENT_RADIO_TOWER_ROCKET_TAKEOVER
                        };

                        foreach (var evt in evt_ids)
                        {
                            flagHelper.SetEventFlag(evt, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[evt].IsSet = false;
                        }

                        idx = 0x6CF; // EVENT_GOLDENROD_CITY_CIVILIANS
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x6D0; // EVENT_RADIO_TOWER_CIVILIANS_AFTER
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x307; // EVENT_GOLDENROD_UNDERGROUND_WAREHOUSE_BLOCKED_OFF
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x13; // ENGINE_ROCKETS_IN_RADIO_TOWER
                        SetSysFlag(idx, true);
                        m_flagsGroupsList[Src_SysFlags].Flags[idx].IsSet = true;

                        idx = 0x37; // wRadioTower5FSceneID
                        eventWorkHelper.SetWork(idx, 0);
                    }
                    break;

                case 3: // S.S. Aqua first trip
                    {
                        int[] evt_ids =
                        {
                            0x30, // EVENT_FAST_SHIP_FIRST_TIME
                            0x31, // EVENT_FAST_SHIP_HAS_ARRIVED
                            0x32, // EVENT_FAST_SHIP_FOUND_GIRL
                            0x33, // EVENT_FAST_SHIP_LAZY_SAILOR
                            0x34, // EVENT_FAST_SHIP_INFORMED_ABOUT_LAZY_SAILOR
                            0x71, // EVENT_GOT_METAL_COAT_FROM_GRANDPA_ON_SS_AQUA
                            0x730, // EVENT_FAST_SHIP_CABINS_SE_SSE_GENTLEMAN
                            0x732, // EVENT_FAST_SHIP_CABINS_SE_SSE_CAPTAINS_CABIN_TWIN_2
                            0x739, // EVENT_FAST_SHIP_PASSENGERS_FIRST_TRIP
                        };

                        foreach (var evt in evt_ids)
                        {
                            flagHelper.SetEventFlag(evt, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[evt].IsSet = false;
                        }

                        idx = 0x73A; // EVENT_FAST_SHIP_PASSENGERS_EASTBOUND
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x73B; // EVENT_FAST_SHIP_PASSENGERS_WESTBOUND
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x4A; // wFastShip1FSceneID
                        eventWorkHelper.SetWork(idx, 2);

                        idx = 0x4B; // wFastShipB1FSceneID
                        eventWorkHelper.SetWork(idx, 0);
                    }
                    break;

                case 4: // Legendary Beasts events
                    {
                        int[] evt_ids =
                        {
                            0x7B, // EVENT_RELEASED_THE_BEASTS
                            0x332, // EVENT_HOLE_IN_BURNED_TOWER
                            0x6C5, // EVENT_RIVAL_BURNED_TOWER
                            0x74B, // EVENT_BURNED_TOWER_B1F_BEASTS_2
                            0x765, // EVENT_BURNED_TOWER_1F_EUSINE
                            0x7AB, // EVENT_WISE_TRIOS_ROOM_WISE_TRIO_1
                            0x334, // EVENT_KOJI_ALLOWS_YOU_PASSAGE_TO_TIN_TOWER
                            0x335, // EVENT_FOUGHT_SUICUNE
                        };

                        foreach (var evt in evt_ids)
                        {
                            flagHelper.SetEventFlag(evt, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[evt].IsSet = false;
                        }

                        idx = 0x766; // EVENT_RANG_CLEAR_BELL_1
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x767; // EVENT_RANG_CLEAR_BELL_2
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x7AC; // EVENT_WISE_TRIOS_ROOM_WISE_TRIO_2
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x7B6; // EVENT_TIN_TOWER_1F_WISE_TRIO_1
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;

                        idx = 0x25; // wEcruteakTinTowerEntranceSceneID
                        eventWorkHelper.SetWork(idx, 0);

                        idx = 0x26; // wWiseTriosRoomSceneID
                        eventWorkHelper.SetWork(idx, 0);

                        idx = 0x34; // wTinTower1FSceneID
                        eventWorkHelper.SetWork(idx, 0);

                        idx = 0x35; // wBurnedTower1FSceneID
                        eventWorkHelper.SetWork(idx, 0);

                        idx = 0x36; // wBurnedTowerB1FSceneID
                        eventWorkHelper.SetWork(idx, 0);
                    }
                    break;

                case 5: // Unblock Mt. Silver access
                    {
                        idx = 0x74F; // EVENT_OPENED_MT_SILVER
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;
                    }
                    break;

                case 6: // Unblock Tin Tower Stairs
                    {
                        idx = 0x336; // EVENT_GOT_RAINBOW_WING
                        flagHelper.SetEventFlag(idx, true);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = true;
                    }
                    break;

                case 7: // Celebi Event
                    {
                        const byte gsBallAvailable = 0xb; // GS_BALL_AVAILABLE

                        // sGSBallFlag
                        m_savFile!.Data[GSBallFlagOffset] = gsBallAvailable;
                        // sGSBallFlagBackup
                        m_savFile!.Data[GSBallFlagBackupOffset] = gsBallAvailable;

                        idx = 0x340; // EVENT_GOT_GS_BALL_FROM_GOLDENROD_POKEMON_CENTER
                        flagHelper.SetEventFlag(idx, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
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
                switch (flagType)
                {
                    case EventFlagType.FieldItem:
                        BulkEdit_FieldItem(value);
                        break;

                    case EventFlagType.HiddenItem:
                        BulkEdit_HiddenItems(value);
                        break;

                    case EventFlagType.TrainerBattle:
                        BulkEdit_Trainers(value);
                        break;

                    case EventFlagType.StaticEncounter:
                        BulkEdit_StaticEncounter(value);
                        break;

                    case EventFlagType.InGameTrade:
                        BulkEdit_Trades(value);
                        break;

                    case EventFlagType.ItemGift:
                        BulkEdit_ItemGift(value);
                        break;

                    case EventFlagType.PkmnGift:
                        BulkEdit_PkmnGift(value);
                        break;

                    case EventFlagType.FlySpot:
                        BulkEdit_FlySpot(value);
                        break;

                    case EventFlagType.BerryTree:
                        BulkEdit_BerryTrees(value);
                        break;

                    case EventFlagType.Collectable:
                        BulkEdit_Collectables(value);
                        break;

                }
            }
        }

        void BulkEdit_FieldItem(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_EventFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.FieldItem)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);
        }

        void BulkEdit_HiddenItems(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_EventFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.HiddenItem)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);
        }

        void BulkEdit_Trainers(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_SysFlags],
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.TrainerBattle)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup);
            }

            // Fix Rival event flags
            {
                var flagHelper = (IEventFlagArray)m_savFile!;
                var eventWorkHelper = (IEventWorkArray<byte>)m_savFile!;

                int idx = 0x18; // wCherrygroveCitySceneID
                m_eventWorkList[idx].Value = value ? 0 : 1;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);

                idx = 0x1E; // wAzaleaTownSceneID
                m_eventWorkList[idx].Value = value ? 0 : 1;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);

                idx = 0x35; // wBurnedTower1FSceneID
                m_eventWorkList[idx].Value = value ? 2 : 1;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);
                idx = 0x6C5; // EVENT_RIVAL_BURNED_TOWER
                flagHelper.SetEventFlag(idx, value);
                m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                idx = 0x43; // wGoldenrodUndergroundSwitchRoomEntrancesSceneID
                m_eventWorkList[idx].Value = value ? 1 : 0;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);

                idx = 0x45; // wVictoryRoadSceneID
                m_eventWorkList[idx].Value = value ? 1 : 0;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);

                idx = 0x32; // wMountMoonSceneID
                m_eventWorkList[idx].Value = value ? 1 : 0;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);
                idx = 0x77A; // EVENT_MT_MOON_RIVAL
                flagHelper.SetEventFlag(idx, value);
                m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;
            }

            // Fix Team Rocket thief at Route 24
            if (m_flagsGroupsList[Src_EventFlags].Flags[0xCB].IsSet) // EVENT_MET_ROCKET_GRUNT_AT_CERULEAN_GYM
            {
                var flagHelper = (IEventFlagArray)m_savFile!;

                int idx = 0x76C; // EVENT_ROUTE_24_ROCKET
                flagHelper.SetEventFlag(idx, value);
                m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;
            }
        }

        void BulkEdit_StaticEncounter(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_SysFlags],
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.StaticEncounter)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup);
            }

            // Fix Sudowoodo variablesprite
            {
                // variablesprite SPRITE_WEIRD_TREE = value ? SPRITE_TWIN : SPRITE_SUDOWOODO
                m_savFile!.Data[VariableSpritesOffset + 4] = value ? (byte)0x26 : (byte)0x52;
            }

            // Fix Suicune encounter
            {
                var eventWorkHelper = (IEventWorkArray<byte>)m_savFile!;

                int idx = 0x34; // wTinTower1FSceneID
                m_eventWorkList[idx].Value = value ? 1 : 0;
                eventWorkHelper.SetWork(idx, (byte)m_eventWorkList[idx].Value);
            }
        }

        void BulkEdit_Trades(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_TradeFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.InGameTrade)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);
        }

        void BulkEdit_ItemGift(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_SysFlags],
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.ItemGift)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup);
            }

            // Fix some flags so it can be gifted again
            {
                var flagHelper = (IEventFlagArray)m_savFile!;
                int idx;

                if (!value)
                {
                    if (m_flagsGroupsList[Src_EventFlags].Flags[0x29].IsSet) // EVENT_HERDED_FARFETCHD
                    {
                        idx = 0x6F4; // EVENT_ILEX_FOREST_CHARCOAL_MASTER
                        flagHelper.SetEventFlag(idx, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }

                    if (m_flagsGroupsList[Src_EventFlags].Flags[0x22].IsSet) // EVENT_CLEARED_ROCKET_HIDEOUT
                    {
                        idx = 0x6E2; // EVENT_TEAM_ROCKET_BASE_B2F_ELECTRODE_3
                        flagHelper.SetEventFlag(idx, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }

                    idx = 0x558; // EVENT_BEAT_COOLTRAINERM_KEVIN
                    flagHelper.SetEventFlag(idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                }

                int[] npc_ids =
                {
                    0x7D, // ENGINE_BEVERLY_HAS_NUGGET
                    0x7E, // ENGINE_JOSE_HAS_STAR_PIECE
                    0x7F, // ENGINE_WADE_HAS_ITEM (default: Berry)
                    0x80, // ENGINE_GINA_HAS_LEAF_STONE
                    0x81, // ENGINE_ALAN_HAS_FIRE_STONE
                    0x82, // ENGINE_DANA_HAS_THUNDERSTONE
                    0x83, // ENGINE_DEREK_HAS_NUGGET
                    0x84, // ENGINE_TULLY_HAS_WATER_STONE
                    0x85, // ENGINE_TIFFANY_HAS_PINK_BOW
                    0x86, // ENGINE_WILTON_HAS_ITEM (default: Ultra Ball)
                };

                foreach (var npc_idx in npc_ids)
                {
                    SetSysFlag(npc_idx, !value);
                    m_flagsGroupsList[Src_SysFlags].Flags[npc_idx].IsSet = !value;
                }

                npc_ids = new int[]
                {
                    0x337, // EVENT_HUEY_PROTEIN
                    0x338, // EVENT_JOEY_HP_UP
                    0x339, // EVENT_VANCE_CARBOS
                    0x33A, // EVENT_PARRY_IRON
                    0x33B, // EVENT_ERIN_CALCIUM
                };

                foreach (var npc_idx in npc_ids)
                {
                    flagHelper.SetEventFlag(npc_idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[npc_idx].IsSet = !value;
                }
            }
        }

        void BulkEdit_PkmnGift(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_EventFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.PkmnGift)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);

            // Fix some flags so it can be gifted again
            {
                var flagHelper = (IEventFlagArray)m_savFile!;

                if (!value)
                {
                    int idx = 0x700; // EVENT_ELMS_AIDE_IN_VIOLET_POKEMON_CENTER
                    flagHelper.SetEventFlag(idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x46; // EVENT_MANIA_TOOK_SHUCKIE_OR_LET_YOU_KEEP_HIM
                    flagHelper.SetEventFlag(idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x51; // EVENT_GAVE_KENYA
                    flagHelper.SetEventFlag(idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x52; // EVENT_GOT_HP_UP_FROM_RANDY
                    flagHelper.SetEventFlag(idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0xC1; // EVENT_ANSWERED_DRAGON_MASTER_QUIZ_WRONG
                    flagHelper.SetEventFlag(idx, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                }
            }
        }

        void BulkEdit_FlySpot(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_SysFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.FlySpot)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);
        }

        void BulkEdit_BerryTrees(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_BerryTreeFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.BerryTree)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);
        }

        void BulkEdit_Collectables(bool value)
        {
            var fGroup = m_flagsGroupsList[Src_EventFlags];

            foreach (var f in fGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.Collectable)
                {
                    f.IsSet = value;
                }
            }

            SyncEditedFlags(fGroup);
        }

        public override void SyncEditedFlags(FlagsGroup fGroup)
        {
            var flagHelper = (IEventFlagArray)m_savFile!;

            switch (fGroup.SourceIdx)
            {
                case Src_EventFlags:
                    foreach (var f in fGroup.Flags)
                    {
                        flagHelper.SetEventFlag((int)f.FlagIdx, f.IsSet);
                    }
                    break;

                case Src_SysFlags:
                    foreach (var f in fGroup.Flags)
                    {
                        SetSysFlag((int)f.FlagIdx, f.IsSet);
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
