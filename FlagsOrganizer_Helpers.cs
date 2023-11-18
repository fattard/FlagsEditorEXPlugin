using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PKHeX.Core;

namespace FlagsEditorEXPlugin
{

    static class EventFlagTypeExtensions
    {
        public static FlagsOrganizer.EventFlagType Parse(this FlagsOrganizer.EventFlagType flagType, string txt) => txt switch
        {
            "FIELD ITEM" => FlagsOrganizer.EventFlagType.FieldItem,
            "HIDDEN ITEM" => FlagsOrganizer.EventFlagType.HiddenItem,
            "SPECIAL ITEM" => FlagsOrganizer.EventFlagType.SpecialItem,
            "TRAINER BATTLE" => FlagsOrganizer.EventFlagType.TrainerBattle,
            "STATIC BATTLE" => FlagsOrganizer.EventFlagType.StaticBattle,
            "IN-GAME TRADE" => FlagsOrganizer.EventFlagType.InGameTrade,
            "ITEM GIFT" => FlagsOrganizer.EventFlagType.ItemGift,
            "PKMN GIFT" => FlagsOrganizer.EventFlagType.PkmnGift,
            "EVENT" => FlagsOrganizer.EventFlagType.GeneralEvent,
            "SIDE EVENT" => FlagsOrganizer.EventFlagType.SideEvent,
            "STORY EVENT" => FlagsOrganizer.EventFlagType.StoryEvent,
            "BERRY TREE" => FlagsOrganizer.EventFlagType.BerryTree,
            "FLY SPOT" => FlagsOrganizer.EventFlagType.FlySpot,
            "COLLECTABLE" => FlagsOrganizer.EventFlagType.Collectable,
            "_UNUSED" => FlagsOrganizer.EventFlagType._Unused,
            "_SEPARATOR" => FlagsOrganizer.EventFlagType._Separator,
            _ => FlagsOrganizer.EventFlagType._Unknown,
        };

        public static string AsText(this FlagsOrganizer.EventFlagType flagType) => flagType switch
        {
            FlagsOrganizer.EventFlagType.FieldItem => "FIELD ITEM",
            FlagsOrganizer.EventFlagType.HiddenItem => "HIDDEN ITEM",
            FlagsOrganizer.EventFlagType.SpecialItem => "SPECIAL ITEM",
            FlagsOrganizer.EventFlagType.TrainerBattle => "TRAINER BATTLE",
            FlagsOrganizer.EventFlagType.StaticBattle => "STATIC BATTLE",
            FlagsOrganizer.EventFlagType.InGameTrade => "IN-GAME TRADE",
            FlagsOrganizer.EventFlagType.ItemGift => "ITEM GIFT",
            FlagsOrganizer.EventFlagType.PkmnGift => "PKMN GIFT",
            FlagsOrganizer.EventFlagType.GeneralEvent => "EVENT",
            FlagsOrganizer.EventFlagType.SideEvent => "SIDE EVENT",
            FlagsOrganizer.EventFlagType.StoryEvent => "STORY EVENT",
            FlagsOrganizer.EventFlagType.BerryTree => "BERRY TREE",
            FlagsOrganizer.EventFlagType.FlySpot => "FLY SPOT",
            FlagsOrganizer.EventFlagType.Collectable => "COLLECTABLE",
            FlagsOrganizer.EventFlagType._Unused => "_UNUSED",
            _ => "",
        };
    }
}
