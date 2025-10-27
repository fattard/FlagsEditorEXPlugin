namespace FlagsEditorEXPlugin
{
    internal class FlagsGen9LZA : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        const int Src_EventFlags = 0;
        const int Src_SysFlags = 1;
        const int Src_EventWork = 2;
        const int Src_MissionsWork = 3;
        const int Src_MableResearchWork = 4;
        const int Src_FieldItemFlags = 5;
        //const int Src_HiddenItemFlags = 6;
        //const int Src_TrainerFlags = 7;

        readonly uint[] HiddenItemsBlockKeys =
        [
            
        ];

        readonly List<FlagDetail> m_unavailableFlagBlocks = [];
        readonly List<WorkDetail> m_unavailableWorkBlocks = [];

        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen9lza");

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxSysFlagsSection = s_flagsList_res.IndexOf("//\tSys Flags");
            int idxFieldItemFlagsSection = s_flagsList_res.IndexOf("//\tField Item Flags");
            //int idxHiddenItemsFlagsSection = s_flagsList_res.IndexOf("//\tHidden Item Flags");
            //int idxTrainerFlagsSection = s_flagsList_res.IndexOf("//\tTrainer Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");
            int idxMissionsWorkSection = s_flagsList_res.IndexOf("//\tMissions Work");
            int idxMableResearchWorkSection = s_flagsList_res.IndexOf("//\tMable Research Work");

            AssembleList(s_flagsList_res[idxEventFlagsSection..], Src_EventFlags, "Event Flags", []);
            AssembleList(s_flagsList_res[idxSysFlagsSection..], Src_SysFlags, "Sys Flags", []);
            AssembleList(s_flagsList_res[idxFieldItemFlagsSection..], Src_FieldItemFlags, "Field Item Flags", []);
            //AssembleList(s_flagsList_res[idxHiddenItemsFlagsSection..], Src_HiddenItemFlags, "Hidden Item Flags", []);
            //AssembleList(s_flagsList_res[idxTrainerFlagsSection..], Src_TrainerFlags, "Regular Trainer Flags", []);

            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], Src_EventWork, "Event Work", Array.Empty<ulong>());
            AssembleWorkList(s_flagsList_res[idxMissionsWorkSection..], Src_MissionsWork, "Missions Work", Array.Empty<ulong>());
            AssembleWorkList(s_flagsList_res[idxMableResearchWorkSection..], Src_MableResearchWork, "Mable Research Work", Array.Empty<ulong>());
        }

        protected override void AssembleList(string flagsList_res, int sourceIdx, string sourceName, bool[] flagValues)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                FlagsGroup flagsGroup = new FlagsGroup(sourceIdx, sourceName);
                Dictionary<ulong, bool>? listOfStatuses = (sourceIdx) switch
                {
                    Src_SysFlags => RetrieveBlockStatuses(savEventBlocks.GetBlockSafe(0xED6F46E7).AsByteArray(), emptyKey: 0xCBF29CE484222645),
                    Src_EventFlags => RetrieveBlockStatuses(savEventBlocks.GetBlockSafe(0x58505C5E).AsByteArray(), emptyKey: 0xCBF29CE484222645),
                    Src_FieldItemFlags => RetrieveBlockStatuses(savEventBlocks.GetBlockSafe(0x2482AD60).AsByteArray(), emptyKey: 0x0000000000000000),
                    _ => []
                };

#if DEBUG
                string dumpName = (sourceIdx) switch
                {
                    Src_SysFlags => "SysFlags",
                    Src_EventFlags => "EventFlags",
                    Src_FieldItemFlags => "FieldItems",
                    _ => ""
                };
                DumpListOfStatuses($"k{dumpName}_status_LZA.txt", listOfStatuses);
#endif

                string? s = reader.ReadLine();

                if (s is null)
                {
                    return;
                }

                // Skip header
                if (s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        // End of section
                        if (s.StartsWith("//"))
                        {
                            break;
                        }

                        var flagDetail = new FlagDetail(s);
                        if (listOfStatuses!.TryGetValue(flagDetail.FlagIdx, out bool value))
                        {
                            flagDetail.IsSet = value;
                            flagDetail.OriginalState = flagDetail.IsSet;
                            flagDetail.SourceIdx = sourceIdx;
                            flagsGroup.Flags.Add(flagDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);

                m_flagsGroupsList.Add(flagsGroup);
            }
        }

        protected void AssembleWorkList<T>(string workList_res, int sourceIdx, string sourceName, T[] eventWorkValues)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            using (System.IO.StringReader reader = new System.IO.StringReader(workList_res))
            {
                Dictionary<ulong, ulong>? listOfValues = (sourceIdx) switch
                {
                    Src_EventWork => RetrieveBlockValues(savEventBlocks.GetBlockSafe(0xFADA7742).AsByteArray(), emptyKey: 0xCBF29CE484222645),
                    Src_MissionsWork => RetrieveBlockValues(savEventBlocks.GetBlockSafe(0xB9B223B9).AsByteArray(), emptyKey: 0xCBF29CE484222645),
                    Src_MableResearchWork => RetrieveBlockValues(savEventBlocks.GetBlockSafe(0x03913534).AsByteArray(), emptyKey: 0xCBF29CE484222645),
                    _ => []
                };

#if DEBUG
                string dumpName = (sourceIdx) switch
                {
                    Src_EventWork => "EventWork",
                    Src_MissionsWork => "MissionsWork",
                    Src_MableResearchWork => "MableResearchWork",
                    _ => ""
                };
                DumpListOfValues($"k{dumpName}_values_LZA.txt", listOfValues);
#endif


                string? s = reader.ReadLine();

                if (s is null)
                {
                    return;
                }

                // Skip header
                if (s.StartsWith("//"))
                {
                    s = reader.ReadLine();
                }

                do
                {
                    if (!string.IsNullOrWhiteSpace(s))
                    {
                        // End of section
                        if (s.StartsWith("//"))
                        {
                            break;
                        }

                        var workDetail = new WorkDetail(s);
                        if (listOfValues.TryGetValue(workDetail.WorkIdx, out ulong value))
                        {
                            workDetail.Value = (long)value;
                            workDetail.SourceIdx = sourceIdx;
                            m_eventWorkList.Add(workDetail);
                        }
                        else
                        {
                            m_unavailableWorkBlocks.Add(workDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }

        Dictionary<ulong, bool> RetrieveBlockStatuses(byte[] aData, ulong emptyKey)
        {
            // Ignore dummy blocks
            if (aData.Length == 0)
            {
                return [];
            }

            var blocksStatus = new Dictionary<ulong, bool>(4000);

            using (var ms = new System.IO.MemoryStream(aData))
            {
                using (var reader = new System.IO.BinaryReader(ms))
                {
                    do
                    {
                        var key = reader.ReadUInt64();

                        if (key == emptyKey)
                        {
                            reader.ReadUInt64();
                            continue;
                        }

                        if (!blocksStatus.ContainsKey(key))
                        {
                            blocksStatus.Add(key, reader.ReadUInt64() == 1);
                        }
                        else
                        {
                            throw new ArgumentException("AHTB collision: 0x" + key.ToString("X8"));
                        }

                    } while (ms.Position < ms.Length);
                }
            }

            return blocksStatus;
        }

        Dictionary<ulong, ulong> RetrieveBlockValues(byte[] aData, ulong emptyKey)
        {
            // Ignore dummy blocks
            if (aData.Length == 0)
            {
                return [];
            }

            var blocksValues = new Dictionary<ulong, ulong>(4000);

            using (var ms = new System.IO.MemoryStream(aData))
            {
                using (var reader = new System.IO.BinaryReader(ms))
                {
                    do
                    {
                        var key = reader.ReadUInt64();

                        if (key == emptyKey)
                        {
                            reader.ReadUInt64();
                            continue;
                        }

                        if (!blocksValues.ContainsKey(key))
                        {
                            blocksValues.Add(key, reader.ReadUInt64());
                        }
                        else
                        {
                            throw new ArgumentException("AHTB collision: 0x" + key.ToString("X8"));
                        }

                    } while (ms.Position < ms.Length);
                }
            }

            return blocksValues;
        }

#if DEBUG
        public override string DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);

            if (m_unavailableFlagBlocks.Count > 0)
            {
                sb.Append($"{"Event Flags"}\r\n");

                for (int i = 0; i < m_unavailableFlagBlocks.Count; ++i)
                {
                    string fmt = m_unavailableFlagBlocks[i].FlagIdx > (ushort.MaxValue) ?
                        m_unavailableFlagBlocks[i].FlagIdx > (uint.MaxValue) ?
                        "FLAG_0x{0:X16} {1}\t{2}\r\n" :
                        "FLAG_0x{0:X8} {1}\t{2}\r\n" :
                        "FLAG_0x{0:X4} {1}\t{2}\r\n";

                    sb.AppendFormat(fmt, m_unavailableFlagBlocks[i].FlagIdx, m_unavailableFlagBlocks[i].IsSet,
                        m_unavailableFlagBlocks[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : m_unavailableFlagBlocks[i].ToString());
                }

                sb.Append("\r\n\r\n");
            }

            if (m_unavailableWorkBlocks.Count > 0)
            {
                sb.Append($"{"Event Work"}\r\n");

                for (int i = 0; i < m_unavailableWorkBlocks.Count; ++i)
                {
                    string fmt = m_unavailableWorkBlocks[i].WorkIdx > (ushort.MaxValue) ?
                        m_unavailableWorkBlocks[i].WorkIdx > (uint.MaxValue) ?
                        "WORK_0x{0:X16} => {1,5}\t{2}\r\n" :
                        "WORK_0x{0:X8} => {1,5}\t{2}\r\n" :
                        "WORK_0x{0:X4} => {1,5}\t{2}\r\n";

                    sb.AppendFormat(fmt, m_unavailableWorkBlocks[i].WorkIdx, m_unavailableWorkBlocks[i].Value,
                        m_unavailableWorkBlocks[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : m_unavailableWorkBlocks[i].ToString());
                }
            }

            System.IO.File.WriteAllText(string.Format("unavailable_flags_dump_LZA.txt", m_savFile!.Version), sb.ToString());

            return base.DumpAllFlags();
        }

        static void DumpListOfStatuses(string filePath, Dictionary<ulong, bool> listOfStatuses)
        {
            StringBuilder sb = new StringBuilder(512 * 1024);
            foreach (var v in listOfStatuses)
            {
                sb.AppendFormat("0x{0:X16}\t{1}\r\n", v.Key, v.Value);
            }

            System.IO.File.WriteAllText($"{filePath}", sb.ToString());
        }

        static void DumpListOfValues(string filePath, Dictionary<ulong, ulong> listOfValues)
        {
            StringBuilder sb = new StringBuilder(512 * 1024);
            foreach (var v in listOfValues)
            {
                sb.AppendFormat("0x{0:X16}\t{1}\r\n", v.Key, v.Value);
            }

            System.IO.File.WriteAllText($"{filePath}", sb.ToString());
        }
#endif

        public override bool SupportsBulkEditingFlags(EventFlagType flagType) => flagType switch
        {
            EventFlagType.FieldItem
            //EventFlagType.HiddenItem or
            //EventFlagType.TrainerBattle or
            //EventFlagType.FlySpot
                => true,
#if DEBUG
            //EventFlagType.ItemGift or
            //EventFlagType.PkmnGift or
            //EventFlagType.StaticEncounter or
            //EventFlagType.InGameTrade or
            //EventFlagType.SideEvent
              //  => true,
#endif
            _ => false
        };

#if DEBUG
        public override EditableEventInfo[] GetSpecialEditableEvents()
        {
            int idx = 0;
            return
            [
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGenLZA.specialEvtBtn_{idx}", "Complete all missions")) { IsAvailable = true },
                new EditableEventInfo(idx++, LocalizedStrings.Find($"SpecialEditsGenLZA.specialEvtBtn_{idx}", "Reset all missions")) { IsAvailable = true },
            ];
        }

        public override void ProcessSpecialEventEdit(EditableEventInfo eventInfo)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            switch (eventInfo.Index)
            {
                case 0: // Complete all missions
                    {
                        foreach (var w in m_eventWorkList)
                        {
                            if (w.SourceIdx == Src_MissionsWork)
                            {
                                w.Value = 255;
                            }
                        }

                        SyncEditedEventWork();
                    }
                    break;

                case 1: // Reset all missions
                    {
                        foreach (var w in m_eventWorkList)
                        {
                            if (w.SourceIdx == Src_MissionsWork)
                            {
                                w.Value = 0;
                            }
                        }

                        SyncEditedEventWork();
                    }
                    break;
            }
        }
#endif

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
                //var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;
                //byte[] bdata;

                switch (flagType)
                {
                    case EventFlagType.FlySpot:
                        {
                            foreach (var f in m_flagsGroupsList[Src_SysFlags].Flags)
                            {
                                if (f.FlagTypeVal == flagType)
                                {
                                    f.IsSet = value;
                                }
                            }

                            SyncEditedFlags(m_flagsGroupsList[Src_SysFlags]);
                        }
                        break;

                    case EventFlagType.FieldItem:
                        {
                            foreach (var f in m_flagsGroupsList[Src_FieldItemFlags].Flags)
                            {
                                if (f.FlagTypeVal == flagType)
                                {
                                    f.IsSet = value;
                                }
                            }

                            SyncEditedFlags(m_flagsGroupsList[Src_FieldItemFlags]);
                        }
                        break;
                }
            }
        }

        public override void SyncEditedFlags(FlagsGroup fGroup)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;
            var bdata = (fGroup.SourceIdx) switch
            {
                Src_SysFlags => savEventBlocks.GetBlockSafe(0xED6F46E7).AsByteArray(),
                Src_EventFlags => savEventBlocks.GetBlockSafe(0x58505C5E).AsByteArray(),
                Src_FieldItemFlags => savEventBlocks.GetBlockSafe(0x2482AD60).AsByteArray(),
                _ => []
            };

            using (var ms = new System.IO.MemoryStream(bdata))
            {
                using (var writer = new System.IO.BinaryWriter(ms))
                {
                    foreach (var f in fGroup.Flags)
                    {
                        if (ms.Position < ms.Length)
                        {
                            writer.Write(f.FlagIdx);
                            writer.Write(f.IsSet ? (ulong)1 : (ulong)0);
                        }
                    }
                }
            }
        }

        public override void SyncEditedEventWork()
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            byte[] bdata_evtWork = savEventBlocks.GetBlockSafe(0xFADA7742).AsByteArray();
            byte[] bdata_missionWork = savEventBlocks.GetBlockSafe(0xB9B223B9).AsByteArray();
            byte[] bdata_mableWork = savEventBlocks.GetBlockSafe(0x03913534).AsByteArray();

            using (var ms_evtWork = new System.IO.MemoryStream(bdata_evtWork))
            using (var ms_missionWork = new System.IO.MemoryStream(bdata_missionWork))
            using (var ms_mableWork = new System.IO.MemoryStream(bdata_mableWork))
            {
                using (var writer_evtWork = new System.IO.BinaryWriter(ms_evtWork))
                using (var writer_missionWork = new System.IO.BinaryWriter(ms_missionWork))
                using (var writer_mableWork = new System.IO.BinaryWriter(ms_mableWork))
                {
                    foreach (var w in m_eventWorkList)
                    {
                        switch (w.SourceIdx)
                        {
                            case Src_EventWork:
                                {
                                    writer_evtWork.Write(w.WorkIdx);
                                    writer_evtWork.Write(w.Value);
                                }
                                break;

                            case Src_MissionsWork:
                                {
                                    writer_missionWork.Write(w.WorkIdx);
                                    writer_missionWork.Write(w.Value);
                                }
                                break;

                            case Src_MableResearchWork:
                                {
                                    writer_mableWork.Write(w.WorkIdx);
                                    writer_mableWork.Write(w.Value);
                                }
                                break;
                        }
                    }
                }
            }
        }
    }
}
