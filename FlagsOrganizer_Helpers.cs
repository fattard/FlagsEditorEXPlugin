namespace FlagsEditorEXPlugin
{

    static class EventFlagTypeExtensions
    {
        public static FlagsOrganizer.EventFlagType Parse(this FlagsOrganizer.EventFlagType _, string txt) => txt switch
        {
            "FIELD ITEM" => FlagsOrganizer.EventFlagType.FieldItem,
            "HIDDEN ITEM" => FlagsOrganizer.EventFlagType.HiddenItem,
            "SPECIAL ITEM" => FlagsOrganizer.EventFlagType.SpecialItem,
            "TRAINER BATTLE" => FlagsOrganizer.EventFlagType.TrainerBattle,
            "STATIC ENCOUNTER" => FlagsOrganizer.EventFlagType.StaticEncounter,
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
            FlagsOrganizer.EventFlagType.StaticEncounter => "STATIC ENCOUNTER",
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

        public static string AsLocalizedText(this FlagsOrganizer.EventFlagType flagType)
        {
            return LocalizedStrings.Find($"EventFlagType.{flagType}", flagType.ToString());
        }

        public static byte[] AsByteArray(this PKHeX.Core.SCBlock block)
        {
            if (System.Runtime.InteropServices.MemoryMarshal.TryGetArray(block.Raw, out ArraySegment<byte> segment))
            {
                return segment.Array!;
            }
            else
            {
                throw new System.InvalidOperationException("Memory is not backed by an array.");
            }
        }
    }
}
