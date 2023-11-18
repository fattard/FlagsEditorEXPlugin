using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

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
            StaticBattle,
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
                            ValidValues.Add(ParseDecOrHexSigned(t[..sep]), t[(sep + 1)..]);
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
            }

            public WorkDetail(ulong workIdx, EventFlagType flagType, string locationName, string detailMsg, string internalName)
            {
                WorkIdx = workIdx;
                FlagTypeVal = flagType;
                LocationName = locationName;
                DetailMsg = detailMsg;
                InternalName = internalName;
                ValidValues = new Dictionary<long, string>(4);
                Value = 0;
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

        public class SpecialEditableEventInfo
        {
            public int Index { get; private set; }
            public string Name { get; private set; }
            public bool IsAvailable { get; set; }

            public SpecialEditableEventInfo(int index, string name, bool isAvailable = true)
            {
                Index = index;
                Name = name;
                IsAvailable = isAvailable;
            }
        }


        protected SaveFile? m_savFile;

        protected List<FlagsGroup> m_flagsGroupsList = new List<FlagsGroup>(10);
        protected List<WorkDetail> m_eventWorkList = new List<WorkDetail>(4096);

        public List<FlagsGroup> FlagsGroups => m_flagsGroupsList;
        public List<WorkDetail> EventWorkList => m_eventWorkList;

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
                        flagDetail.SourceIdx = sourceIdx;
                        flagsGroup.Flags.Add(flagDetail);
                    }

                    s = reader.ReadLine();

                } while (s is not null);

                m_flagsGroupsList.Add(flagsGroup);
            }
        }


        protected virtual void AssembleWorkList<T>(string workList_res, T[] eventWorkValues) where T: unmanaged
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

        public virtual void DumpAllFlags()
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
                }
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile!.Version), sb.ToString());
        }

        public virtual SpecialEditableEventInfo[] GetSpecialEditableEvents() { return Array.Empty<SpecialEditableEventInfo>(); }

        public virtual void ProcessSpecialEventEdit(SpecialEditableEventInfo eventInfo) { }

        public abstract void BulkMarkFlags(EventFlagType flagType);
        public abstract void BulkUnmarkFlags(EventFlagType flagType);
        public abstract bool SupportsBulkEditingFlags(EventFlagType flagType);
        public abstract void SyncEditedFlags(int sourceIdx);
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

        protected static string ReadResFile(string resName)
        {
            string? contentTxt = null;

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            // Try outside file first
            var offResPath = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(assembly.Location)!, resName);
            if (!System.IO.File.Exists(offResPath))
            {
                resName = assembly.GetManifestResourceNames().Single(str => str.EndsWith(resName));

                using (var stream = assembly.GetManifestResourceStream(resName))
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
            FlagsOrganizer? flagsOrganizer = null;

            switch (savFile.Version)
            {
                case GameVersion.Any:
                case GameVersion.RBY:
                case GameVersion.StadiumJ:
                case GameVersion.Stadium:
                case GameVersion.Stadium2:
                case GameVersion.RSBOX:
                case GameVersion.COLO:
                case GameVersion.XD:
                case GameVersion.CXD:
                case GameVersion.BATREV:
                case GameVersion.ORASDEMO:
                case GameVersion.GO:
                case GameVersion.Unknown:
                case GameVersion.Invalid:
                    break; // unsupported format

                case GameVersion.RD:
                case GameVersion.GN:
                case GameVersion.RB:
                    flagsOrganizer = new FlagsGen1RB();
                    break;

                case GameVersion.YW:
                    flagsOrganizer = new FlagsGen1Y();
                    break;

                case GameVersion.GD:
                case GameVersion.SI:
                case GameVersion.GS:
                    flagsOrganizer = new FlagsGen2GS();
                    break;

                case GameVersion.C:
                    flagsOrganizer = new FlagsGen2C();
                    break;

                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.RS:
                    flagsOrganizer = new FlagsGen3RS();
                    break;

                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.FRLG:
                    flagsOrganizer = new FlagsGen3FRLG();
                    break;

                case GameVersion.E:
                    flagsOrganizer = new FlagsGen3E();
                    break;

                case GameVersion.D:
                case GameVersion.P:
                case GameVersion.DP:
                    flagsOrganizer = new FlagsGen4DP();
                    break;

                case GameVersion.Pt:
                    flagsOrganizer = new FlagsGen4Pt();
                    break;

                case GameVersion.HG:
                case GameVersion.SS:
                case GameVersion.HGSS:
                    flagsOrganizer = new FlagsGen4HGSS();
                    break;

                case GameVersion.B:
                case GameVersion.W:
                case GameVersion.BW:
                    flagsOrganizer = new FlagsGen5BW();
                    break;

                case GameVersion.B2:
                case GameVersion.W2:
                case GameVersion.B2W2:
                    flagsOrganizer = new FlagsGen5B2W2();
                    break;

                case GameVersion.X:
                case GameVersion.Y:
                case GameVersion.XY:
                    flagsOrganizer = new FlagsGen6XY();
                    break;

                case GameVersion.OR:
                case GameVersion.AS:
                case GameVersion.ORAS:
                    flagsOrganizer = new FlagsGen6ORAS();
                    break;

                case GameVersion.SN:
                case GameVersion.MN:
                case GameVersion.SM:
                    flagsOrganizer = new FlagsGen7SM();
                    break;

                case GameVersion.US:
                case GameVersion.UM:
                case GameVersion.USUM:
                    flagsOrganizer = new FlagsGen7USUM();
                    break;

                case GameVersion.GP:
                case GameVersion.GE:
                case GameVersion.GG:
                    flagsOrganizer = new FlagsGen7bGPGE();
                    break;

                case GameVersion.BD:
                case GameVersion.SP:
                case GameVersion.BDSP:
                    flagsOrganizer = new FlagsGen8bsBDSP();
                    break;

                case GameVersion.SW:
                case GameVersion.SH:
                case GameVersion.SWSH:
                    flagsOrganizer = new FlagsGen8SWSH();
                    break;

                case GameVersion.SL:
                case GameVersion.VL:
                case GameVersion.SV:
                    flagsOrganizer = new FlagsGen9SV();
                    break;


                case GameVersion.PLA:
                    flagsOrganizer = new FlagsGen8LA();
                    break;

                default:
                    break;
            }

            if (flagsOrganizer is null)
            {
                throw new FormatException($"Unsupported SAV format: {savFile.Version}");
            }

            flagsOrganizer.InitFlagsData(savFile, resData);

            return flagsOrganizer;
        }

    }

}
