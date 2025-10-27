namespace FlagsEditorEXPlugin
{

    public abstract class FlagsOrganizer
    {
        public enum EventFlagType
        {
            _Unknown,

            FieldItem,
            HiddenItem,
            SpecialItem,
            TrainerBattle,
            StaticEncounter,
            InGameTrade,
            ItemGift,
            PkmnGift,
            GeneralEvent,
            SideEvent,
            StoryEvent,
            FlySpot,
            BerryTree,
            Collectable,

            _Unused,
            _Separator,
        }


        public class FlagDetail
        {
            public int SourceIdx { get; set; }
            public ulong FlagIdx { get; private set; }
            public EventFlagType FlagTypeVal { get; private set; }
            public string FlagTypeTxt => FlagTypeVal.AsText();
            public string LocationName { get; private set; }
            public string DetailMsg { get; private set; }
            public string InternalName { get; private set; }
            public bool IsSet { get; set; }
            public bool OriginalState { get; set; }


            public FlagDetail(string detailEntry)
            {
                string[] info = detailEntry.Split('\t');

                if (info.Length < 7)
                {
                    throw new ArgumentException("Argument detailEntry format is not valid");
                }
                FlagIdx = ParseDecOrHex(info[0]);
                FlagTypeVal = FlagTypeVal.Parse(info[1]);
                LocationName = info[2];
                if (!string.IsNullOrWhiteSpace(info[3]))
                {
                    LocationName += " " + info[3];
                }
                DetailMsg = info[4];
                InternalName = info[6];
                IsSet = false;
                OriginalState = false;
                SourceIdx = 0;
            }

            public FlagDetail(ulong flagIdx, int source, EventFlagType flagType, string locationName, string detailMsg, string internalName)
            {
                FlagIdx = flagIdx;
                FlagTypeVal = flagType;
                LocationName = locationName;
                DetailMsg = detailMsg;
                InternalName = internalName;
                IsSet = false;
                OriginalState = false;
                SourceIdx = source;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(128);
                const string sep = " - ";
                if (!string.IsNullOrEmpty(InternalName))
                {
                    sb.Append(InternalName);
                }
                if (!string.IsNullOrEmpty(FlagTypeTxt))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(FlagTypeTxt);
                }
                if (!string.IsNullOrEmpty(LocationName))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(LocationName);
                }
                if (!string.IsNullOrEmpty(DetailMsg))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(DetailMsg);
                }

                return sb.ToString();
            }
        }


        public class FlagsGroup
        {
            public int SourceIdx { get; private set; }
            public string SourceName { get; private set; }
            public List<FlagDetail> Flags { get; private set; }


            public FlagsGroup(int sourceIdx, string sourceName)
            {
                SourceIdx = sourceIdx;
                SourceName = sourceName;
                Flags = new List<FlagDetail>(4096);
            }
        }


        public class WorkDetail
        {
            public int SourceIdx { get; set; }
            public ulong WorkIdx { get; private set; }
            public EventFlagType FlagTypeVal { get; private set; }
            public string FlagTypeTxt => FlagTypeVal.AsText();
            public string LocationName { get; private set; }
            public string DetailMsg { get; private set; }
            public string InternalName { get; private set; }
            public Dictionary<long, string> ValidValues { get; private set; }
            public long Value { get; set; }


            public WorkDetail(string detailEntry)
            {
                string[] info = detailEntry.Split('\t');

                if (info.Length < 7)
                {
                    throw new ArgumentException("Argument detailEntry format is not valid");
                }
                WorkIdx = ParseDecOrHex(info[0]);
                FlagTypeVal = FlagTypeVal.Parse(info[1]);
                LocationName = info[2];
                if (!string.IsNullOrWhiteSpace(info[3]))
                {
                    LocationName += " " + info[3];
                }
                DetailMsg = info[4];
                InternalName = info[6];
                Value = 0;
                SourceIdx = 0;

                ValidValues = new Dictionary<long, string>(4);
                if (!string.IsNullOrWhiteSpace(info[5]))
                {
                    // x:y tuples separated by ,
                    var possibleTuples = info[5].Split(',');
                    foreach (var t in possibleTuples)
                    {
                        int sep = t.IndexOf(':');
                        if (sep > 0)
                        {
                            ValidValues.Add(ParseDecOrHexSigned(t[..sep].Trim()), t[(sep + 1)..].Trim());
                        }
                    }
                }
            }

            public WorkDetail(WorkDetail workDetail)
            {
                WorkIdx = workDetail.WorkIdx;
                FlagTypeVal = workDetail.FlagTypeVal;
                LocationName = workDetail.LocationName;
                DetailMsg = workDetail.DetailMsg;
                InternalName = workDetail.InternalName;
                ValidValues = workDetail.ValidValues;
                Value = workDetail.Value;
                SourceIdx = workDetail.SourceIdx;
            }

            public WorkDetail(ulong workIdx, int source, EventFlagType flagType, string locationName, string detailMsg, string internalName)
            {
                WorkIdx = workIdx;
                FlagTypeVal = flagType;
                LocationName = locationName;
                DetailMsg = detailMsg;
                InternalName = internalName;
                ValidValues = new Dictionary<long, string>(4);
                Value = 0;
                SourceIdx = source;
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder(128);
                const string sep = " - ";
                if (!string.IsNullOrEmpty(InternalName))
                {
                    sb.Append(InternalName);
                }
                if (!string.IsNullOrEmpty(FlagTypeTxt))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(FlagTypeTxt);
                }
                if (!string.IsNullOrEmpty(LocationName))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(LocationName);
                }
                if (!string.IsNullOrEmpty(DetailMsg))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(sep);
                    }
                    sb.Append(DetailMsg);
                }
                if (ValidValues.Count > 0 && ValidValues.TryGetValue(Value, out string? value))
                {
                    sb.Append($" => {value}");
                }

                return sb.ToString();
            }
        }

        public class EditableEventInfo
        {
            public int Index { get; private set; }
            public string Label { get; private set; }
            public Type? EditorClassType { get; private set; }
            public bool IsAvailable { get; set; }

            public EditableEventInfo(int index, string label, bool isAvailable = true)
            {
                Index = index;
                Label = label;
                IsAvailable = isAvailable;
            }

            public EditableEventInfo(int index, string label, Type editorClassType, bool isAvailable = true)
            {
                Index = index;
                Label = label;
                EditorClassType = editorClassType;
                IsAvailable = isAvailable;
            }
        }


        protected SaveFile? m_savFile;

        protected List<FlagsGroup> m_flagsGroupsList = new List<FlagsGroup>(10);
        protected List<WorkDetail> m_eventWorkList = new List<WorkDetail>(4096);

        public List<FlagsGroup> FlagsGroups => m_flagsGroupsList;
        public List<WorkDetail> EventWorkList => m_eventWorkList;
        public SaveFile? SaveFile => m_savFile;

        protected virtual void InitFlagsData(SaveFile savFile, string? resData)
        {
            m_savFile = savFile;
            m_flagsGroupsList.Clear();
            m_eventWorkList.Clear();
        }

        protected virtual void AssembleList(string flagsList_res, int sourceIdx, string sourceName, bool[] flagValues)
        {
            using (System.IO.StringReader reader = new System.IO.StringReader(flagsList_res))
            {
                FlagsGroup flagsGroup = new FlagsGroup(sourceIdx, sourceName);

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
                        flagDetail.IsSet = flagValues[flagDetail.FlagIdx];
                        flagDetail.OriginalState = flagDetail.IsSet;
                        flagDetail.SourceIdx = sourceIdx;
                        flagsGroup.Flags.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s is not null);

                m_flagsGroupsList.Add(flagsGroup);
            }
        }


        protected virtual void AssembleWorkList<T>(string workList_res, T[] eventWorkValues) where T : unmanaged
        {
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
                        workDetail.Value = Convert.ToInt64(eventWorkValues[workDetail.WorkIdx]);
                        m_eventWorkList.Add(workDetail);
                    }

                    s = reader.ReadLine();

                } while (s is not null);
            }
        }



        #region Actions

        public virtual string DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);

            foreach (var fGroup in m_flagsGroupsList)
            {
                var flagsList = fGroup.Flags;
                sb.Append($"{fGroup.SourceName}\r\n");

                for (int i = 0; i < flagsList.Count; ++i)
                {
                    string fmt = flagsList[i].FlagIdx > (ushort.MaxValue) ?
                        flagsList[i].FlagIdx > (uint.MaxValue) ?
                        "FLAG_0x{0:X16} {1}\t{2}\r\n" :
                        "FLAG_0x{0:X8} {1}\t{2}\r\n" :
                        "FLAG_0x{0:X4} {1}\t{2}\r\n";

                    sb.AppendFormat(fmt, flagsList[i].FlagIdx, flagsList[i].IsSet,
                        flagsList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : flagsList[i].ToString());

#if DEBUG
                    if (flagsList[i].FlagTypeVal == EventFlagType._Unused && flagsList[i].IsSet)
                    {
                        MessageBox.Show($"[{fGroup.SourceName}] FLAG_0x{flagsList[i].FlagIdx:X4} is set,\nbut it should be unused.",
                            "Unused warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
#endif
                }

                sb.Append("\r\n\r\n");
            }

            if (m_eventWorkList.Count > 0)
            {
                sb.Append($"{"Event Work"}\r\n");

                for (int i = 0; i < m_eventWorkList.Count; ++i)
                {
                    string fmt = m_eventWorkList[i].WorkIdx > (ushort.MaxValue) ?
                        m_eventWorkList[i].WorkIdx > (uint.MaxValue) ?
                        "WORK_0x{0:X16} => {1,5}\t{2}\r\n" :
                        "WORK_0x{0:X8} => {1,5}\t{2}\r\n" :
                        "WORK_0x{0:X4} => {1,5}\t{2}\r\n";

                    sb.AppendFormat(fmt, m_eventWorkList[i].WorkIdx, m_eventWorkList[i].Value,
                        m_eventWorkList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : m_eventWorkList[i].ToString());

#if DEBUG
                    if (m_eventWorkList[i].FlagTypeVal == EventFlagType._Unused && m_eventWorkList[i].Value != 0)
                    {
                        MessageBox.Show($"WORK_0x{m_eventWorkList[i].WorkIdx:X4} has a value of {m_eventWorkList[i].Value},\nbut it should be unused.",
                            "Unused warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
#endif
                }
            }

            return sb.ToString();
        }

        public virtual EditableEventInfo[] GetSpecialEditableEvents() { return []; }

        public virtual EditableEventInfo[] GetMiscEditableEvents() { return []; }

        public virtual void ProcessSpecialEventEdit(EditableEventInfo eventInfo) { }

        public abstract void BulkMarkFlags(EventFlagType flagType);
        public abstract void BulkUnmarkFlags(EventFlagType flagType);
        public abstract bool SupportsBulkEditingFlags(EventFlagType flagType);
        public abstract void SyncEditedFlags(FlagsGroup fGroup);
        public abstract void SyncEditedEventWork();

        #endregion

        public static ulong ParseDecOrHex(string str)
        {
            if (str.StartsWith("0x"))
                return Convert.ToUInt64(str, 16);

            return Convert.ToUInt64(str);
        }

        public static long ParseDecOrHexSigned(string str)
        {
            if (str.StartsWith("0x"))
                return Convert.ToInt64(str, 16);

            return Convert.ToInt64(str);
        }

        protected static string ReadFlagsResFile(string resName, string? langCode = null)
        {
            string? contentTxt = null;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            langCode ??= GameInfo.CurrentLanguage;

            string resFileName = $"{resName}_{langCode}.txt";

            // Try outside file first
            var offResPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location)!, resFileName);
            if (!System.IO.File.Exists(offResPath))
            {
                try
                {
                    resFileName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resFileName));
                }
                catch (InvalidOperationException)
                {
                    // Load default language
                    return ReadFlagsResFile(resName, "en");
                }

                using (var stream = assembly.GetManifestResourceStream(resFileName))
                {
                    using (var reader = new System.IO.StreamReader(stream!))
                    {
                        contentTxt = reader.ReadToEnd();
                    }
                }
            }
            else
            {
                contentTxt = System.IO.File.ReadAllText(offResPath);
            }

            return contentTxt;
        }



        public static FlagsOrganizer CreateFlagsOrganizer(SaveFile savFile, string? resData)
        {
            FlagsOrganizer? flagsOrganizer = savFile.Version switch
            {
                GameVersion.Any or
                GameVersion.RBY or
                GameVersion.StadiumJ or
                GameVersion.Stadium or
                GameVersion.Stadium2 or
                GameVersion.RSBOX or
                GameVersion.COLO or
                GameVersion.XD or
                GameVersion.CXD or
                GameVersion.BATREV or
                GameVersion.ORASDEMO or
                GameVersion.GO or
                GameVersion.Invalid
                    // unsupported format
                    => null,

                GameVersion.RD or
                GameVersion.GN or
                GameVersion.BU or
                GameVersion.RB
                    => new FlagsGen1RB(),

                GameVersion.YW
                    => new FlagsGen1Y(),

                GameVersion.GD or
                GameVersion.SI or
                GameVersion.GS
                    => new FlagsGen2GS(),

                GameVersion.C
                    => new FlagsGen2C(),

                GameVersion.R or
                GameVersion.S or
                GameVersion.RS
                    => new FlagsGen3RS(),

                GameVersion.FR or
                GameVersion.LG or
                GameVersion.FRLG
                    => new FlagsGen3FRLG(),

                GameVersion.E
                    => new FlagsGen3E(),

                GameVersion.D or
                GameVersion.P or
                GameVersion.DP
                    => new FlagsGen4DP(),

                GameVersion.Pt
                    => new FlagsGen4Pt(),

                GameVersion.HG or
                GameVersion.SS or
                GameVersion.HGSS
                    => new FlagsGen4HGSS(),

                GameVersion.B or
                GameVersion.W or
                GameVersion.BW
                    => new FlagsGen5BW(),

                GameVersion.B2 or
                GameVersion.W2 or
                GameVersion.B2W2
                    => new FlagsGen5B2W2(),

                GameVersion.X or
                GameVersion.Y or
                GameVersion.XY
                    => new FlagsGen6XY(),

                GameVersion.OR or
                GameVersion.AS or
                GameVersion.ORAS
                    => new FlagsGen6ORAS(),

                GameVersion.SN or
                GameVersion.MN or
                GameVersion.SM
                    => new FlagsGen7SM(),

                GameVersion.US or
                GameVersion.UM or
                GameVersion.USUM
                    => new FlagsGen7USUM(),

                GameVersion.GP or
                GameVersion.GE or
                GameVersion.GG
                    => new FlagsGen7bGPGE(),

                GameVersion.SW or
                GameVersion.SH or
                GameVersion.SWSH
                    => new FlagsGen8SWSH(),

                GameVersion.BD or
                GameVersion.SP or
                GameVersion.BDSP
                    => new FlagsGen8BDSP(),

                GameVersion.PLA
                    => new FlagsGen8LA(),

                GameVersion.SL or
                GameVersion.VL or
                GameVersion.SV
                    => new FlagsGen9SV(),

                GameVersion.ZA
                    => new FlagsGen9LZA(),

                _ => null
            };

            if (flagsOrganizer is null)
            {
                throw new FormatException($"Unsupported SAV format: {savFile.Version}");
            }

            flagsOrganizer.InitFlagsData(savFile, resData);

            return flagsOrganizer;
        }

    }

}
