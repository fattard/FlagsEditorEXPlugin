using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{

    static class FlagTypeExtensions
    {
        public static FlagsOrganizer.EventFlagType Parse(this FlagsOrganizer.EventFlagType flagType, string txt)
        {
            switch (txt)
            {
                case "FIELD ITEM":
                    flagType = FlagsOrganizer.EventFlagType.FieldItem;
                    break;

                case "HIDDEN ITEM":
                    flagType = FlagsOrganizer.EventFlagType.HiddenItem;
                    break;

                case "SPECIAL ITEM":
                    flagType = FlagsOrganizer.EventFlagType.SpecialItem;
                    break;

                case "TRAINER BATTLE":
                    flagType = FlagsOrganizer.EventFlagType.TrainerBattle;
                    break;

                case "STATIC BATTLE":
                case "STATIONARY_BATTLE":
                    flagType = FlagsOrganizer.EventFlagType.StaticBattle;
                    break;

                case "IN-GAME TRADE":
                    flagType = FlagsOrganizer.EventFlagType.InGameTrade;
                    break;

                case "GIFT":
                case "ITEM GIFT":
                    flagType = FlagsOrganizer.EventFlagType.ItemGift;
                    break;

                case "PKMN GIFT":
                    flagType = FlagsOrganizer.EventFlagType.PkmnGift;
                    break;

                case "EVENT":
                    flagType = FlagsOrganizer.EventFlagType.GeneralEvent;
                    break;

                case "SIDE EVENT":
                    flagType = FlagsOrganizer.EventFlagType.SideEvent;
                    break;

                case "STORY EVENT":
                    flagType = FlagsOrganizer.EventFlagType.StoryEvent;
                    break;

                case "BERRY TREE":
                    flagType = FlagsOrganizer.EventFlagType.BerryTree;
                    break;

                case "FLY SPOT":
                    flagType = FlagsOrganizer.EventFlagType.FlySpot;
                    break;

                case "COLLECTABLE":
                    flagType = FlagsOrganizer.EventFlagType.Collectable;
                    break;

                case "_UNUSED":
                    flagType = FlagsOrganizer.EventFlagType._Unused;
                    break;

                case "_SEPARATOR":
                    flagType = FlagsOrganizer.EventFlagType._Separator;
                    break;

                default:
                    flagType = FlagsOrganizer.EventFlagType._Unknown;
                    break;
            }

            return flagType;
        }

        public static string AsText(this FlagsOrganizer.EventFlagType flagType)
        {
            string flagTypeTxt = "";

            switch (flagType)
            {
                case FlagsOrganizer.EventFlagType.FieldItem:
                    flagTypeTxt = "FIELD ITEM";
                    break;

                case FlagsOrganizer.EventFlagType.HiddenItem:
                    flagTypeTxt = "HIDDEN ITEM";
                    break;

                case FlagsOrganizer.EventFlagType.SpecialItem:
                    flagTypeTxt = "SPECIAL ITEM";
                    break;

                case FlagsOrganizer.EventFlagType.TrainerBattle:
                    flagTypeTxt = "TRAINER BATTLE";
                    break;

                case FlagsOrganizer.EventFlagType.StaticBattle:
                    flagTypeTxt = "STATIC BATTLE";
                    break;

                case FlagsOrganizer.EventFlagType.InGameTrade:
                    flagTypeTxt = "IN-GAME TRADE";
                    break;

                case FlagsOrganizer.EventFlagType.ItemGift:
                    flagTypeTxt = "ITEM GIFT";
                    break;

                case FlagsOrganizer.EventFlagType.PkmnGift:
                    flagTypeTxt = "PKMN GIFT";
                    break;

                case FlagsOrganizer.EventFlagType.GeneralEvent:
                    flagTypeTxt = "EVENT";
                    break;

                case FlagsOrganizer.EventFlagType.SideEvent:
                    flagTypeTxt = "SIDE EVENT";
                    break;

                case FlagsOrganizer.EventFlagType.StoryEvent:
                    flagTypeTxt = "STORY EVENT";
                    break;

                case FlagsOrganizer.EventFlagType.BerryTree:
                    flagTypeTxt = "BERRY TREE";
                    break;

                case FlagsOrganizer.EventFlagType.FlySpot:
                    flagTypeTxt = "FLY SPOT";
                    break;

                case FlagsOrganizer.EventFlagType.Collectable:
                    flagTypeTxt = "COLLECTABLE";
                    break;

                case FlagsOrganizer.EventFlagType._Unused:
                    flagTypeTxt = "_UNUSED";
                    break;

                default:
                    flagTypeTxt = "";
                    break;
            }

            return flagTypeTxt;
        }
    }



    class DummyOrgFlags : FlagsOrganizer
    {
        protected override void InitFlagsData(SaveFile savFile, string resData)
        {
            m_savFile = savFile;
            bool[] savEventFlags = (m_savFile as IEventFlagArray).GetEventFlags();
            m_flagsSetList.Clear();
            var fSet = new FlagsSet(0, "");

            for (int i = 0; i < savEventFlags.Length; ++i)
            {
                fSet.Flags.Add(new FlagDetail((uint)i, source: 0, EventFlagType._Unknown, "", "", "") { IsSet = savEventFlags[i] });
            }

            m_flagsSetList.Add(fSet);
        }

        public override void MarkFlags(EventFlagType flagType) { }

        public override void UnmarkFlags(EventFlagType flagType) { }

        public override bool SupportsEditingFlag(EventFlagType flagType) { return false; }
    }


    class DummyOrgBlockFlags : FlagsOrganizer
    {
        List<SCBlock> m_blockEventFlags;

        protected override void InitFlagsData(SaveFile savFile, string resData)
        {
            m_savFile = savFile;

            m_blockEventFlags = new List<SCBlock>(5000);
            foreach (var b in (m_savFile as ISCBlockArray).AllBlocks)
            {
                // Filter only bool blocks
                if (b.Type == SCTypeCode.Bool1 || b.Type == SCTypeCode.Bool2)
                {
                    m_blockEventFlags.Add(b);
                }
            }

            m_flagsSetList.Clear();
            var fSet = new FlagsSet(0, "");

            for (int i = 0; i < m_blockEventFlags.Count; ++i)
            {
                var b = m_blockEventFlags[i];
                fSet.Flags.Add(new FlagDetail(b.Key, source: 0, EventFlagType._Unknown, "", "", "") { IsSet = b.Type == SCTypeCode.Bool2 });
            }

            m_flagsSetList.Add(fSet);
        }

        public override void DumpAllFlags()
        {
            StringBuilder sb = new StringBuilder(512 * 1024);

            var flagsList = m_flagsSetList[0].Flags;
            
            for (int i = 0; i < flagsList.Count; ++i)
            {
                sb.AppendFormat("FLAG_0x{0:X8} {1}\t{2}\r\n", flagsList[i].FlagIdx, flagsList[i].IsSet,
                    flagsList[i].FlagTypeVal == EventFlagType._Unused ? "UNUSED" : flagsList[i].ToString());
            }

            System.IO.File.WriteAllText(string.Format("flags_dump_{0}.txt", m_savFile.Version), sb.ToString());
        }

        public override void MarkFlags(EventFlagType flagType) { }

        public override void UnmarkFlags(EventFlagType flagType) { }

        public override bool SupportsEditingFlag(EventFlagType flagType) { return false; }
    }

}
