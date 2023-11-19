namespace FlagsEditorEXPlugin
{
    internal class FlagsGen1RB : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        enum FlagOffsets_INTL
        {
            BadgeFlags = 0x2602,
            MissableObjectFlags = 0x2852,
            GameProgressFlags = 0x289C,
            ObtainedHiddenItems = 0x299C,
            ObtainedHiddenCoins = 0x29AA,
            FlySpotFlags = 0x29B7,
            RodFlags = 0x29D4,
            LaprasFlag = 0x29DA,
            CompletedInGameTradeFlags = 0x29E3,
            EventFlags = 0x29F3,
        }

        enum FlagOffsets_JAP
        {
            BadgeFlags = 0x25F8,
            MissableObjectFlags = 0x2848,
            GameProgressFlags = 0x2892,
            ObtainedHiddenItems = 0x2992,
            ObtainedHiddenCoins = 0x29A0,
            FlySpotFlags = 0x29AD,
            RodFlags = 0x29CA,
            LaprasFlag = 0x29D0,
            CompletedInGameTradeFlags = 0x29D9,
            EventFlags = 0x29E9,
        }

        int gameVer;

        const int GameVer_JapRedGreen = 35;
        const int GameVer_IntlRedBlue = 36;
        const int GameVer_JapBlue = 37;

        int EventFlagsOffset;
        int BadgeFlagsOffset;
        int MissableObjectFlagsOffset;
        int GameProgressWorkOffset;
        int ObtainedHiddenItemsOffset;
        int ObtainedHiddenCoinsOffset;
        int FlySpotFlagsOffset;
        int RodFlagsOffset;
        int LaprasFlagOffset;
        int CompletedInGameTradeFlagsOffset;

        const int Src_EventFlags = 0;
        const int Src_HideShowFlags = 1;
        const int Src_HiddenItemFlags = 2;
        const int Src_HiddenCoinsFlags = 3;
        const int Src_TradeFlags = 4;
        const int Src_FlySpotFlags = 5;
        const int Src_BadgesFlags = 6;
        const int Src_Misc_wd728 = 7;
        const int Src_Misc_wd72e = 8;


        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;

            var savFile_SAV1 = (SAV1)m_savFile;

            if (savFile_SAV1.Japanese)
            {
                BadgeFlagsOffset = (int)FlagOffsets_JAP.BadgeFlags;
                MissableObjectFlagsOffset = (int)FlagOffsets_JAP.MissableObjectFlags;
                GameProgressWorkOffset = (int)FlagOffsets_JAP.GameProgressFlags;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_JAP.ObtainedHiddenItems;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_JAP.ObtainedHiddenCoins;
                FlySpotFlagsOffset = (int)FlagOffsets_JAP.FlySpotFlags;
                RodFlagsOffset = (int)FlagOffsets_JAP.RodFlags;
                LaprasFlagOffset = (int)FlagOffsets_JAP.LaprasFlag;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_JAP.CompletedInGameTradeFlags;
                EventFlagsOffset = (int)FlagOffsets_JAP.EventFlags;

                switch (m_savFile.Data[0x555])
                {
                    case GameVer_JapBlue:
                        gameVer = GameVer_JapBlue;
                        break;

                    case GameVer_JapRedGreen:
                        gameVer = GameVer_JapRedGreen;
                        break;

                    default:
                        {
                            var dialogResult = System.Windows.Forms.MessageBox.Show("Do your save file comes from Jap Blue version,\ninstead of a Jap Red/Green version?", "Jap Gen1 Save File Detected", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question);
                            if (dialogResult == System.Windows.Forms.DialogResult.Yes)
                            {
                                gameVer = GameVer_JapBlue;
                            }
                            else
                            {
                                gameVer = GameVer_JapRedGreen;
                            }

                        }
                        break;
                }
            }
            else
            {
                BadgeFlagsOffset = (int)FlagOffsets_INTL.BadgeFlags;
                MissableObjectFlagsOffset = (int)FlagOffsets_INTL.MissableObjectFlags;
                GameProgressWorkOffset = (int)FlagOffsets_INTL.GameProgressFlags;
                ObtainedHiddenItemsOffset = (int)FlagOffsets_INTL.ObtainedHiddenItems;
                ObtainedHiddenCoinsOffset = (int)FlagOffsets_INTL.ObtainedHiddenCoins;
                FlySpotFlagsOffset = (int)FlagOffsets_INTL.FlySpotFlags;
                RodFlagsOffset = (int)FlagOffsets_INTL.RodFlags;
                LaprasFlagOffset = (int)FlagOffsets_INTL.LaprasFlag;
                CompletedInGameTradeFlagsOffset = (int)FlagOffsets_INTL.CompletedInGameTradeFlags;
                EventFlagsOffset = (int)FlagOffsets_INTL.EventFlags;

                gameVer = GameVer_IntlRedBlue;
            }

#if DEBUG
            m_savFile.Data[0x555] = (byte)gameVer;
#endif


            // wObtainedBadges
            bool[] result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(BadgeFlagsOffset + (i >> 3), i & 7);
            }
            bool[] badgeFlags = result;

            // wMissableObjectIndex
            result = new bool[32 * 8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(MissableObjectFlagsOffset + (i >> 3), i & 7);
            }
            bool[] missableObjectFlags = result;

            // wObtainedHiddenItemsFlags
            result = new bool[112];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(ObtainedHiddenItemsOffset + (i >> 3), i & 7);
            }
            bool[] obtainedHiddenItemsFlags = result;

            // wObtainedHiddenCoinsFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(ObtainedHiddenCoinsOffset + (i >> 3), i & 7);
            }
            bool[] obtainedHiddenCoinsFlags = result;

            // wTownVisitedFlag
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(FlySpotFlagsOffset + (i >> 3), i & 7);
            }
            bool[] flySpotFlags = result;

            // wCompletedInGameTradeFlags
            result = new bool[16];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(CompletedInGameTradeFlagsOffset + (i >> 3), i & 7);
            }
            bool[] completedInGameTradeFlags = result;

            // wd728
            result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(RodFlagsOffset + (i >> 3), i & 7);
            }
            bool[] miscFlags_wd728 = result;

            // wd72e
            result = new bool[8];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = m_savFile.GetFlag(LaprasFlagOffset + (i >> 3), i & 7);
            }
            bool[] miscFlags_wd72e = result;

            // wEventFlags
            bool[] eventFlags = ((IEventFlagArray)m_savFile!).GetEventFlags();

            // wGameProgressFlags
            var workValues = new byte[0xC8];
            for (int i = 0; i < workValues.Length; i++)
            {
                workValues[i] = m_savFile.Data[GameProgressWorkOffset + i];
            }

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen1rb");

            string? flagsList_res_jp_blue = null;
            if (gameVer == GameVer_JapBlue)
            {
                flagsList_res_jp_blue = ReadFlagsResFile("flags_gen1jbu");
            }

            string? flagsList_res_jp_redgreen = null;
            if (gameVer == GameVer_JapRedGreen)
            {
                flagsList_res_jp_redgreen = ReadFlagsResFile("flags_gen1jgn");
            }

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxHideShowSection = s_flagsList_res.IndexOf("//\tHide-Show Flags");
            int idxFlySpotSection = s_flagsList_res.IndexOf("//\tFly Spot Flags");
            int idxBadgesSection = s_flagsList_res.IndexOf("//\tBadges Flags");
            int idxMisc_wd728_Section = s_flagsList_res.IndexOf("//\tMisc-wd728");
            int idxMisc_wd72e_Section = s_flagsList_res.IndexOf("//\tMisc-wd72e");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            AssembleList(s_flagsList_res[idxEventFlagsSection..], Src_EventFlags, "Event Flags", eventFlags);
            AssembleList(s_flagsList_res[idxHideShowSection..], Src_HideShowFlags, "Hide-Show Flags", missableObjectFlags);

            int idxTradesSection = 0;
            int idxHiddenItemsSection = 0;
            int idxHiddenCoinsSection = 0;

            if (gameVer == GameVer_JapRedGreen)
            {
                idxHiddenItemsSection = flagsList_res_jp_redgreen!.IndexOf("//\tHidden Items Flags");
                idxHiddenCoinsSection = flagsList_res_jp_redgreen!.IndexOf("//\tHidden Coins Flags");
                idxTradesSection = s_flagsList_res.IndexOf("//\tIn-Game Trades Flags");

                AssembleList(flagsList_res_jp_redgreen[idxHiddenItemsSection..], Src_HiddenItemFlags, "Hidden Items Flags", obtainedHiddenItemsFlags);
                AssembleList(flagsList_res_jp_redgreen[idxHiddenCoinsSection..], Src_HiddenCoinsFlags, "Hidden Coins Flags", obtainedHiddenCoinsFlags);
                AssembleList(s_flagsList_res[idxTradesSection..], Src_TradeFlags, "Trade Flags", completedInGameTradeFlags);
            }
            else if (gameVer == GameVer_JapBlue)
            {
                idxHiddenItemsSection = s_flagsList_res.IndexOf("//\tHidden Items Flags");
                idxHiddenCoinsSection = s_flagsList_res.IndexOf("//\tHidden Coins Flags");
                idxTradesSection = flagsList_res_jp_blue!.IndexOf("//\tIn-Game Trades Flags");

                AssembleList(s_flagsList_res[idxHiddenItemsSection..], Src_HiddenItemFlags, "Hidden Items Flags", obtainedHiddenItemsFlags);
                AssembleList(s_flagsList_res[idxHiddenCoinsSection..], Src_HiddenCoinsFlags, "Hidden Coins Flags", obtainedHiddenCoinsFlags);
                AssembleList(flagsList_res_jp_blue[idxTradesSection..], Src_TradeFlags, "Trade Flags", completedInGameTradeFlags);
            }
            else
            {
                idxHiddenItemsSection = s_flagsList_res.IndexOf("//\tHidden Items Flags");
                idxHiddenCoinsSection = s_flagsList_res.IndexOf("//\tHidden Coins Flags");
                idxTradesSection = s_flagsList_res.IndexOf("//\tIn-Game Trades Flags");

                AssembleList(s_flagsList_res[idxHiddenItemsSection..], Src_HiddenItemFlags, "Hidden Items Flags", obtainedHiddenItemsFlags);
                AssembleList(s_flagsList_res[idxHiddenCoinsSection..], Src_HiddenCoinsFlags, "Hidden Coins Flags", obtainedHiddenCoinsFlags);
                AssembleList(s_flagsList_res[idxTradesSection..], Src_TradeFlags, "Trade Flags", completedInGameTradeFlags);
            }

            AssembleList(s_flagsList_res[idxFlySpotSection..], Src_FlySpotFlags, "Fly Spot Flags", flySpotFlags);
            AssembleList(s_flagsList_res[idxBadgesSection..], Src_BadgesFlags, "Badges Flags", badgeFlags);
            AssembleList(s_flagsList_res[idxMisc_wd728_Section..], Src_Misc_wd728, "Misc-wd728 Flags", miscFlags_wd728);
            AssembleList(s_flagsList_res[idxMisc_wd72e_Section..], Src_Misc_wd72e, "Misc-wd72e Flags", miscFlags_wd72e);

            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], workValues);
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
            EventFlagType.FlySpot
                => true,
            _ => false
        };


        public override EditableEventInfo[] GetSpecialEditableEvents()
        {
            int idx = 0;
            return new EditableEventInfo[]
            {
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset Fossils choice")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset Dojo choice")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset Bill events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset S.S. Anne events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset Rocket Hideout events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset Pokémon Tower events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Reset Silph Co. events")),
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGen1.specialEvtBtn_{idx}", "Unblock Cerulean Cave")),
            };
        }

        public override void ProcessSpecialEventEdit(EditableEventInfo eventInfo)
        {
            int idx;

            switch (eventInfo.Index)
            {
                case 0: // Fossils Choice
                    {
                        idx = 0x6D; // HS_MT_MOON_B2F_FOSSIL_1
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;

                        idx = 0x6E; // HS_MT_MOON_B2F_FOSSIL_2
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;

                        idx = 0x57E; // EVENT_GOT_DOME_FOSSIL
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        idx = 0x57F; // EVENT_GOT_HELIX_FOSSIL
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }
                    break;

                case 1: // Dojo Choice
                    {
                        idx = 0x4A; // HS_FIGHTING_DOJO_GIFT_1
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;

                        idx = 0x4B; // HS_FIGHTING_DOJO_GIFT_2
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;

                        idx = 0x350; // EVENT_DEFEATED_FIGHTING_DOJO
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        idx = 0x356; // EVENT_GOT_HITMONLEE
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        idx = 0x357; // EVENT_GOT_HITMONCHAN
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }
                    break;

                case 2: // Bill events
                    {
                        idx = 0x61; // HS_BILL_POKEMON
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;

                        idx = 0x62; // HS_BILL_1
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                        idx = 0x63; // HS_BILL_2
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                        idx = 0x550; // EVENT_MET_BILL
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        // EVENT_USED_CELL_SEPARATOR_ON_BILL .. EVENT_LEFT_BILLS_HOUSE_AFTER_HELPING
                        for (idx = 0x55B; idx <= 0x55F; idx++)
                        {
                            m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                        }
                    }
                    break;

                case 3: // S.S. Anne events
                    {
                        // EVENT_GOT_HM01 .. EVENT_WALKED_OUT_OF_DOCK
                        for (idx = 0x5E0; idx <= 0x5E5; idx++)
                        {
                            m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                        }
                    }
                    break;

                case 4: // Rocket Hideout events
                    {
                        idx = 0x83; // HS_ROCKET_HIDEOUT_B4F_GIOVANNI
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;

                        idx = 0x6A7; // EVENT_BEAT_ROCKET_HIDEOUT_GIOVANNI
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }
                    break;

                case 5: // Pokémon Tower events
                    {
                        // HS_POKEMON_TOWER_7F_ROCKET_1 .. HS_POKEMON_TOWER_7F_MR_FUJI
                        for (idx = 0x40; idx <= 0x43; idx++)
                        {
                            m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                            m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;
                        }

                        idx = 0x44; // HS_MR_FUJIS_HOUSE_MR_FUJI
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                        idx = 0x10F; // EVENT_BEAT_GHOST_MAROWAK
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        // EVENT_BEAT_POKEMONTOWER_7_TRAINER_0 .. EVENT_BEAT_POKEMONTOWER_7_TRAINER_2
                        for (idx = 0x111; idx <= 0x113; idx++)
                        {
                            m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                            m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                        }

                        idx = 0x117; // EVENT_RESCUED_MR_FUJI_2
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        idx = 0x4CF; // EVENT_RESCUED_MR_FUJI
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }
                    break;

                case 6: // Silph Co. events
                    {
                        // HS_SILPH_CO_2F_1 .. HS_SILPH_CO_11F_3
                        for (idx = 0x89; idx <= 0xB9; idx++)
                        {
                            if (m_flagsGroupsList[Src_HideShowFlags].Flags[idx].FlagTypeVal == EventFlagType.GeneralEvent)
                            {
                                m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, false);
                                m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = false;
                            }
                        }

                        idx = 0x4C; // HS_SILPH_CO_1F_RECEPTIONIST
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                        idx = 0x397; // EVENT_SILPH_CO_RECEPTIONIST_AT_DESK
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                        idx = 0x78F; // EVENT_BEAT_SILPH_CO_GIOVANNI
                        m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                        m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;
                    }
                    break;

                case 7: // Unblock Cerulean Cave
                    {
                        idx = 0x08; // HS_CERULEAN_CAVE_GUY
                        m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                        m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;
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

                }
            }
        }

        void BulkEdit_FieldItem(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_HideShowFlags]
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.FieldItem)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup.SourceIdx);
            }
        }

        void BulkEdit_HiddenItems(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_HiddenItemFlags],
                m_flagsGroupsList[Src_HiddenCoinsFlags]
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.HiddenItem)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup.SourceIdx);
            }
        }

        void BulkEdit_Trainers(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_HideShowFlags],
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

                SyncEditedFlags(fGroup.SourceIdx);
            }

            // Fix simple HS flags
            if (!value)
            {
                int[] idxArr = new int[]
                {
                    0x06, // HS_CERULEAN_ROCKET
                    0x32, // HS_VIRIDIAN_GYM_GIOVANNI
                };

                foreach (var idx in idxArr)
                {
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = value;
                }
            }

            // Fix Rival event flags
            {
                int idx = 0x0EE; // EVENT_POKEMON_TOWER_RIVAL_ON_LEFT
                m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                idx = 0x75; // wSSAnne2FCurScript
                m_eventWorkList[idx].Value = value ? 4 : 0;
                m_savFile.Data[GameProgressWorkOffset + idx] = (byte)m_eventWorkList[idx].Value;

                bool hasBoulderBadge = m_flagsGroupsList[Src_BadgesFlags].Flags[0].IsSet;
                bool hasEarthBadge = m_flagsGroupsList[Src_BadgesFlags].Flags[7].IsSet;

                if (!hasBoulderBadge)
                {
                    idx = 0x520; // EVENT_1ST_ROUTE22_RIVAL_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, !value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = !value;

                    idx = 0x525; // EVENT_BEAT_ROUTE22_RIVAL_1ST_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                    idx = 0x22; // HS_ROUTE_22_RIVAL_1
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = value;

                    idx = 0x521; // EVENT_2ND_ROUTE22_RIVAL_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x526; // EVENT_BEAT_ROUTE22_RIVAL_2ND_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x23; // HS_ROUTE_22_RIVAL_2
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                    idx = 0x527; // EVENT_ROUTE22_RIVAL_WANTS_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, !value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = !value;

                    idx = 0x1A; // wRoute22CurScript
                    m_eventWorkList[idx].Value = 0;
                    m_savFile.Data[GameProgressWorkOffset + idx] = (byte)m_eventWorkList[idx].Value;
                }

                else if (hasEarthBadge)
                {
                    idx = 0x520; // EVENT_1ST_ROUTE22_RIVAL_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x525; // EVENT_BEAT_ROUTE22_RIVAL_1ST_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                    idx = 0x22; // HS_ROUTE_22_RIVAL_1
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                    idx = 0x521; // EVENT_2ND_ROUTE22_RIVAL_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, !value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = !value;

                    idx = 0x526; // EVENT_BEAT_ROUTE22_RIVAL_2ND_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                    idx = 0x23; // HS_ROUTE_22_RIVAL_2
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = value;

                    idx = 0x527; // EVENT_ROUTE22_RIVAL_WANTS_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, !value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = !value;

                    idx = 0x1A; // wRoute22CurScript
                    m_eventWorkList[idx].Value = value ? 7 : 0;
                    m_savFile.Data[GameProgressWorkOffset + idx] = (byte)m_eventWorkList[idx].Value;
                }

                else
                {
                    idx = 0x520; // EVENT_1ST_ROUTE22_RIVAL_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x525; // EVENT_BEAT_ROUTE22_RIVAL_1ST_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                    idx = 0x22; // HS_ROUTE_22_RIVAL_1
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                    idx = 0x521; // EVENT_2ND_ROUTE22_RIVAL_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x526; // EVENT_BEAT_ROUTE22_RIVAL_2ND_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x23; // HS_ROUTE_22_RIVAL_2
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, true);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = true;

                    idx = 0x527; // EVENT_ROUTE22_RIVAL_WANTS_BATTLE
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, false);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = false;

                    idx = 0x1A; // wRoute22CurScript
                    m_eventWorkList[idx].Value = 0;
                    m_savFile.Data[GameProgressWorkOffset + idx] = (byte)m_eventWorkList[idx].Value;
                }
            }
        }

        void BulkEdit_StaticEncounter(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_HideShowFlags]
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

                SyncEditedFlags(fGroup.SourceIdx);
            }
        }

        void BulkEdit_Trades(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_TradeFlags]
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.InGameTrade)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup.SourceIdx);
            }
        }

        void BulkEdit_ItemGift(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_HideShowFlags],
                m_flagsGroupsList[Src_Misc_wd728]
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

                SyncEditedFlags(fGroup.SourceIdx);
            }

            // Fix simple HS flags
            if (!value)
            {
                int[] idxArr = new int[]
                {
                    0x06, // HS_CERULEAN_ROCKET
                    0x24, // HS_NUGGET_BRIDGE_GUY
                    0x29, // HS_TOWN_MAP
                    0x32, // HS_VIRIDIAN_GYM_GIOVANNI
                    0x34, // HS_OLD_AMBER
                };

                foreach (var idx in idxArr)
                {
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = value;
                }
            }

            // Fix Bill event flags
            {
                int idx = 0x55F; // EVENT_LEFT_BILLS_HOUSE_AFTER_HELPING
                m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                idx = 0x62; // HS_BILL_1
                m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = value;

                idx = 0x63; // HS_BILL_2
                m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, !value);
                m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = !value;
            }

            // Fix Daisy event flags
            {
                if (m_flagsGroupsList[Src_EventFlags].Flags[0x025].IsSet) // EVENT_GOT_POKEDEX
                {
                    int idx = 0x01A; // EVENT_DAISY_WALKING
                    m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;

                    idx = 0x27; // HS_DAISY_SITTING
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, value);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = value;

                    idx = 0x28; // HS_DAISY_WALKING
                    m_savFile!.SetFlag(MissableObjectFlagsOffset + (idx >> 3), idx & 7, !value);
                    m_flagsGroupsList[Src_HideShowFlags].Flags[idx].IsSet = !value;
                }
            }
        }

        void BulkEdit_PkmnGift(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_EventFlags],
                m_flagsGroupsList[Src_HideShowFlags],
                m_flagsGroupsList[Src_Misc_wd72e]
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.PkmnGift)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup.SourceIdx);
            }

            // Fix Karate Master flag
            //TODO: Fix choice of Hitmonlee / Hitmonchan, only one should be allowed
            {
                int idx = 0x350; // EVENT_DEFEATED_FIGHTING_DOJO
                m_savFile!.SetFlag(EventFlagsOffset + (idx >> 3), idx & 7, value);
                m_flagsGroupsList[Src_EventFlags].Flags[idx].IsSet = value;
            }
        }

        void BulkEdit_FlySpot(bool value)
        {
            var flagGroups = new FlagsGroup[]
            {
                m_flagsGroupsList[Src_FlySpotFlags]
            };

            foreach (var fGroup in flagGroups)
            {
                foreach (var f in fGroup.Flags)
                {
                    if (f.FlagTypeVal == EventFlagType.FlySpot)
                    {
                        f.IsSet = value;
                    }
                }

                SyncEditedFlags(fGroup.SourceIdx);
            }
        }

        public override void SyncEditedFlags(int sourceIdx)
        {
            foreach (var fGroup in m_flagsGroupsList)
            {
                if (fGroup.SourceIdx == sourceIdx)
                {
                    int _offset = 0;

                    switch (fGroup.SourceIdx)
                    {
                        case Src_EventFlags:
                            _offset = EventFlagsOffset;
                            break;

                        case Src_HideShowFlags:
                            _offset = MissableObjectFlagsOffset;
                            break;

                        case Src_HiddenItemFlags:
                            _offset = ObtainedHiddenItemsOffset;
                            break;

                        case Src_HiddenCoinsFlags:
                            _offset = ObtainedHiddenCoinsOffset;
                            break;

                        case Src_TradeFlags:
                            _offset = CompletedInGameTradeFlagsOffset;
                            break;

                        case Src_FlySpotFlags:
                            _offset = FlySpotFlagsOffset;
                            break;

                        case Src_BadgesFlags:
                            _offset = BadgeFlagsOffset;
                            break;

                        case Src_Misc_wd728:
                            _offset = RodFlagsOffset;
                            break;

                        case Src_Misc_wd72e:
                            _offset = LaprasFlagOffset;
                            break;
                    }

                    foreach (var f in fGroup.Flags)
                    {
                        int idx = (int)f.FlagIdx;
                        m_savFile!.SetFlag(_offset + (idx >> 3), idx & 7, f.IsSet);
                    }

                    break;
                }
            }
        }

        public override void SyncEditedEventWork()
        {
            for (int i = 0; i < m_eventWorkList.Count; i++)
            {
                m_savFile!.Data[GameProgressWorkOffset + i] = (byte)m_eventWorkList[i].Value;
            }

            /*var eventWorkHelper = (IEventWorkArray<byte>)m_savFile!;

            foreach (var w in m_eventWorkList)
            {
                eventWorkHelper.SetWork((int)w.WorkIdx, (byte)w.Value);
            }*/
        }
    }
}
