namespace FlagsEditorEXPlugin
{
    internal class FlagsGen8SWSH : FlagsOrganizer
    {
        static string? s_flagsList_res = null;

        const int Src_EventFlags = 0;
        const int Src_HiddenItemsFlags = 1;

        readonly uint[] HiddenItemsBlockKeys =
        [
            0x6148F6AC, // Core
            0xE479EE37, // Extended 1
            0xE579EFCA, // Extended 2
        ];

        protected override void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;

#if DEBUG
            // Force refresh
            s_flagsList_res = null;
#endif

            s_flagsList_res = resData ?? s_flagsList_res ?? ReadFlagsResFile("flags_gen8swsh");

            int idxEventFlagsSection = s_flagsList_res.IndexOf("//\tEvent Flags");
            int idxHiddenItemsFlagsSection = s_flagsList_res.IndexOf("//\tHidden Items Flags");
            int idxEventWorkSection = s_flagsList_res.IndexOf("//\tEvent Work");

            AssembleList(s_flagsList_res[idxEventFlagsSection..], Src_EventFlags, "Event Flags", []);
            AssembleList(s_flagsList_res[idxHiddenItemsFlagsSection..], Src_HiddenItemsFlags, "Hidden Items Flags", []);
            AssembleWorkList(s_flagsList_res[idxEventWorkSection..], Array.Empty<uint>());
        }

        protected override void AssembleList(string flagsList_res, int sourceIdx, string sourceName, bool[] flagValues)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                FlagsGroup flagsGroup = new FlagsGroup(sourceIdx, sourceName);
                Dictionary<ulong, byte[]>? hiddenItemsBlocks = null;

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

                        switch (sourceIdx)
                        {
                            case Src_EventFlags:
                                {

                                    var flagDetail = new FlagDetail(s);
                                    if (savEventBlocks.HasBlock((uint)flagDetail.FlagIdx))
                                    {
                                        flagDetail.IsSet = (savEventBlocks.GetBlockSafe((uint)flagDetail.FlagIdx).Type == SCTypeCode.Bool2);
                                        flagDetail.OriginalState = flagDetail.IsSet;
                                        flagDetail.SourceIdx = sourceIdx;
                                        flagsGroup.Flags.Add(flagDetail);
                                    }
                                }
                                break;

                            case Src_HiddenItemsFlags:
                                {
                                    if (hiddenItemsBlocks == null)
                                    {
                                        hiddenItemsBlocks = GetHiddenItemBlocksCopy();
                                    }

                                    // Hidden Item data
                                    // [0] - state 0: active
                                    //             1: active (respawned)
                                    //             2: obtained (will recycle)
                                    //             3: obtained (permanently)
                                    // [1] - respawn ratio (random)
                                    // [2] - item slot (random)
                                    // [3] - always zero (padding?)

                                    var flagDetail = new FlagDetail(s);
                                    byte value = 0;
                                    if (flagDetail.FlagIdx < 512)
                                        value = hiddenItemsBlocks[HiddenItemsBlockKeys[0]][flagDetail.FlagIdx * 4];
                                    else if (flagDetail.FlagIdx < 1024 && hiddenItemsBlocks.Count > 1)
                                        value = hiddenItemsBlocks[HiddenItemsBlockKeys[1]][(flagDetail.FlagIdx - 512) * 4];
                                    else if (flagDetail.FlagIdx < 1536 && hiddenItemsBlocks.Count > 2)
                                        value = hiddenItemsBlocks[HiddenItemsBlockKeys[2]][(flagDetail.FlagIdx - 1024) * 4];
                                    else
                                        break;


                                    flagDetail.IsSet = value >= 2;
                                    flagDetail.OriginalState = flagDetail.IsSet;
                                    flagDetail.SourceIdx = sourceIdx;
                                    flagsGroup.Flags.Add(flagDetail);
                                }
                                break;
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);

                m_flagsGroupsList.Add(flagsGroup);

#if DEBUG
                if (sourceIdx == Src_HiddenItemsFlags)
                {
                    DumpHiddenItemsData("kHiddenItems_status.txt", hiddenItemsBlocks!, HiddenItemsBlockKeys, m_flagsGroupsList[Src_HiddenItemsFlags]);
                }
#endif
            }

        }

        protected override void AssembleWorkList<T>(string workList_res, T[] eventWorkValues)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            using (System.IO.StringReader reader = new System.IO.StringReader(workList_res))
            {
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
                        if (savEventBlocks.HasBlock((uint)workDetail.WorkIdx))
                        {
                            workDetail.Value = Convert.ToInt64(savEventBlocks.GetBlockSafe((uint)workDetail.WorkIdx).GetValue());
                            m_eventWorkList.Add(workDetail);
                        }
                    }

                    s = reader.ReadLine();

                } while (s != null);
            }
        }

#if DEBUG
        static void DumpHiddenItemsData(string filePath, Dictionary<ulong, byte[]> hiddenItemsBlocks, uint[] hiddenItemsBlocksKeys, FlagsGroup hiddenItemsFlagsGroup)
        {
            StringBuilder sb = new StringBuilder(512 * 1024);
            foreach (var f in hiddenItemsFlagsGroup.Flags)
            {
                if (f.FlagTypeVal == EventFlagType.HiddenItem)
                {
                    byte v1, v2, v3 = 0;

                    if (f.FlagIdx < 512)
                    {
                        v1 = hiddenItemsBlocks[hiddenItemsBlocksKeys[0]][f.FlagIdx * 4];
                        v2 = hiddenItemsBlocks[hiddenItemsBlocksKeys[0]][(f.FlagIdx * 4) + 1];
                        v3 = hiddenItemsBlocks[hiddenItemsBlocksKeys[0]][(f.FlagIdx * 4) + 2];
                    }
                    else if (f.FlagIdx < 1024 && hiddenItemsBlocks.Count > 1)
                    {
                        v1 = hiddenItemsBlocks[hiddenItemsBlocksKeys[1]][(f.FlagIdx - 512) * 4];
                        v2 = hiddenItemsBlocks[hiddenItemsBlocksKeys[1]][((f.FlagIdx - 512) * 4) + 1];
                        v3 = hiddenItemsBlocks[hiddenItemsBlocksKeys[1]][((f.FlagIdx - 512) * 4) + 2];
                    }
                    else if (f.FlagIdx < 1536 && hiddenItemsBlocks.Count > 2)
                    {
                        v1 = hiddenItemsBlocks[hiddenItemsBlocksKeys[2]][(f.FlagIdx - 1024) * 4];
                        v2 = hiddenItemsBlocks[hiddenItemsBlocksKeys[2]][((f.FlagIdx - 1024) * 4) + 1];
                        v3 = hiddenItemsBlocks[hiddenItemsBlocksKeys[2]][((f.FlagIdx - 1024) * 4) + 2];
                    }
                    else
                        continue;

                    sb.AppendFormat("0x{0:X4}\t{1}\t=>\t{2:D2} {3:D2} {4:D2}\t{5}\r\n", f.FlagIdx, f.IsSet, v1, v2, v3, f.ToString());
                }
            }

            System.IO.File.WriteAllText($"{filePath}", sb.ToString());
        }
#endif

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
                //var blocks = ((ISCBlockArray)m_savFile!).Accessor;

                switch (flagType)
                {
                    case EventFlagType.HiddenItem:
                        {
                            foreach (var f in m_flagsGroupsList[Src_HiddenItemsFlags].Flags)
                            {
                                if (f.FlagTypeVal == flagType)
                                {
                                    f.IsSet = value;
                                }
                            }

                            SyncEditedFlags(m_flagsGroupsList[Src_HiddenItemsFlags]);
                        }
                        break;

                    case EventFlagType.FieldItem:
                    case EventFlagType.TrainerBattle:
                        {
                            foreach (var f in m_flagsGroupsList[Src_EventFlags].Flags)
                            {
                                if (f.FlagTypeVal == flagType)
                                {
                                    f.IsSet = value;
                                }
                            }

                            SyncEditedFlags(m_flagsGroupsList[Src_EventFlags]);
                        }
                        break;
                }
            }
        }

        public override void SyncEditedFlags(FlagsGroup fGroup)
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            switch (fGroup.SourceIdx)
            {
                case Src_EventFlags:
                    foreach (var f in fGroup.Flags)
                    {
                        savEventBlocks.GetBlockSafe((uint)f.FlagIdx).ChangeBooleanType(f.IsSet ? SCTypeCode.Bool2 : SCTypeCode.Bool1);
                    }
                    break;

                case Src_HiddenItemsFlags:
                    {
                        var bdata1 = savEventBlocks.GetBlockSafe(0x6148F6AC).Data;
                        var bdata2 = savEventBlocks.GetBlockSafe(0xE479EE37).Data;
                        var bdata3 = savEventBlocks.GetBlockSafe(0xE579EFCA).Data;


                        using (var ms1 = new System.IO.MemoryStream(bdata1))
                        using (var ms2 = new System.IO.MemoryStream(bdata2))
                        using (var ms3 = new System.IO.MemoryStream(bdata3))
                        {
                            using (var writer1 = new System.IO.BinaryWriter(ms1))
                            using (var writer2 = new System.IO.BinaryWriter(ms2))
                            using (var writer3 = new System.IO.BinaryWriter(ms3))
                            {
                                foreach (var f in fGroup.Flags)
                                {
                                    if (f.FlagIdx < 512 && ms1.Position < ms1.Length)
                                    {
                                        if (f.IsSet != f.OriginalState)
                                        {
                                            writer1.Write(f.IsSet ? (byte)2 : (byte)0);
                                            writer1.Write(f.IsSet ? (byte)0 : (byte)100);
                                            writer1.Write(f.IsSet ? (byte)0 : (byte)10);
                                            ms1.Position += 1;
                                        }
                                        else // Skip if unmodified
                                        {
                                            ms1.Position += 4;
                                        }
                                    }
                                    else if (f.FlagIdx < 1024 && ms2.Position < ms2.Length)
                                    {
                                        if (f.IsSet != f.OriginalState)
                                        {
                                            writer2.Write(f.IsSet ? (byte)2 : (byte)0);
                                            writer2.Write(f.IsSet ? (byte)0 : (byte)100);
                                            writer2.Write(f.IsSet ? (byte)0 : (byte)10);
                                            ms2.Position += 1;
                                        }
                                        else // Skip if unmodified
                                        {
                                            ms2.Position += 4;
                                        }
                                    }
                                    else if (f.FlagIdx < 1536 && ms3.Position < ms3.Length)
                                    {
                                        if (f.IsSet != f.OriginalState)
                                        {
                                            writer3.Write(f.IsSet ? (byte)2 : (byte)0);
                                            writer3.Write(f.IsSet ? (byte)0 : (byte)100);
                                            writer3.Write(f.IsSet ? (byte)0 : (byte)10);
                                            ms3.Position += 1;
                                        }
                                        else // Skip if unmodified
                                        {
                                            ms3.Position += 4;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    break;
            }
        }

        public override void SyncEditedEventWork()
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            foreach (var w in m_eventWorkList)
            {
                savEventBlocks.GetBlockSafe((uint)w.WorkIdx).SetValue((uint)w.Value);
            }
        }

        public Dictionary<ulong, byte[]> GetHiddenItemBlocksCopy()
        {
            var savEventBlocks = ((ISCBlockArray)m_savFile!).Accessor;

            Dictionary<ulong, byte[]> tBlocks = new Dictionary<ulong, byte[]>(HiddenItemsBlockKeys.Length);

            for (int k = 0; k < HiddenItemsBlockKeys.Length; k++)
            {
                // Skip DLC1 on older saves
                if (k > 0 && ((SAV8SWSH)m_savFile).SaveRevision < 1)
                {
                    continue;
                }

                // Skip DLC2 on older saves
                if (k > 1 && ((SAV8SWSH)m_savFile).SaveRevision < 2)
                {
                    continue;
                }

                var data = savEventBlocks.GetBlockSafe(HiddenItemsBlockKeys[k]).Data;
                if (data.Length > 0)
                {
                    byte[] dataCopy = new byte[data.Length];
                    Array.Copy(data, dataCopy, data.Length);

                    tBlocks.Add(HiddenItemsBlockKeys[k], dataCopy);
                }
            }

            return tBlocks;
        }
    }
}
