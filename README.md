# FlagsEditorEX(pert) Plugin
This is a [PKHeX](https://github.com/kwsch/PKHeX) plugin for advanced users.
It allows you to edit all sorts of Flags and Event Work data from save files.

## Setup Instructions
- Ensure you have the latest PKHeX version from [here](https://projectpokemon.org/pkhex/)
- Download the plugin from the latest release [here](https://github.com/fattard/FlagsEditorEXPlugin/releases/latest).
- Extract and unblock them in Windows' Properties Menu.
- Put them in the *plugins* folder that is in the same folder as the PKHeX program path.
- If the *plugins* folder does not exist, create a new one, all lowercase letters.
- Check for more instructions examples if you have trouble (specifically steps 4 and 5): [Manually Installing PKHeX Plugins](https://github.com/architdate/PKHeX-Plugins/wiki/Installing-PKHeX-Plugins#manual-installation-or-installing-older-releases).

## Actions

**Note: The lists may contain unused data, which will be filtered out later, as documentation on the flags progresses.**  

### Edit Flags

This action opens the main window of the plugin.  
All sorts of Flags and Event Work data can be edited through several tabs, that will open dedicated editors, and basic filters.  

![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/bdc96090-d506-49b6-9c21-3283a999a0bf)
![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/5d7ccace-e6de-4a54-9a08-f39d2458c1ee)
![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/5aff3825-be2f-4f3d-aee2-489a599eb354)
![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/640c5696-6939-4ba6-8d7a-23d40264bc68)

After finishing editing, export your save data from the base application.

**No safety checks are done at all, so any combination of changes may cause issues like softlocks, crashes and permanent data loss.**
**Save data backups are strongly recommended before going into flags editing.**

### Dump all Flags

This action will export the entire flag database with the current flag states (True/False) and event work values, with additional description (when available).  
This action is mainly for researching flag states and diffing previous/current states to discover and document the flag usages, that will be added as human readable information.  

## Supported Games
All mainline games are supported (limited descriptions for many of them)

- Red / Blue / Yellow (International and Japanese versions)
- Gold / Silver / Crystal (International, Japanese and Korean versions)
- Ruby / Sapphire / Emerald / FireRed / LeafGreen
- Diamond / Pearl / Platinum / HeartGold / Soul Silver
- Black / White / Black 2 / White 2
- X / Y / Omega Ruby / Alpha Sapphire
- Sun / Moon / Ultra Sun / Ultra Moon / Let's Go Pikachu / Let's Go Eevee
- Sword / Shield / Brilliant Diamond / Shiny Pearl / Legends: Arceus
- Scarlet / Violet

## Contributing

### Localized content

The UI localization files follows the same format as the PKHeX localization resources, with a key=value pair by the '=' character.  
The files are located at the [_localization_](/localization) folder.

It detects the same language as the main PKHeX application is currently using.

I've included an additional language file for pt-BR language, altough not supported, that I use to make room for labels in the UI, as this language is as bad in UI space constraints as Spanish or German.  
It could also be used as a given example on how the localization for the UI works.

The following table has the languages that are open to contribution
| Key | Language            | Contributors   |
|-----|---------------------|----------------|
| de  | German              |                |
| en  | English             | Me             |
| es  | Spanish             |                |
| fr  | French              |                |
| it  | Italian             |                |
| ja  | Japanese            |                |
| ko  | Korean              |                |
| zh  | Simplified Chinese  |pplloufh and wubinwww|
| zh2 | Traditional Chinese |                |

The flags resources database can also be localized, but it is not recommended at this moment due to constantly changes to those resources.

Those files are simple _tsv_ text files located at  [_flagslist_](/flagslist) folder.

The header of the files, with some examples:
| Raw Idx | Event Type | Location     | Complement | Text Description                             | Valid Values                    | Identifier              |
|---------|------------|--------------|------------|----------------------------------------------|---------------------------------|-------------------------|
| 0x0008  | ITEM GIFT  | Violet City  | Gym        | Received TM31 (Mud Slap) from Leader Falkner |                                 | EVENT_GOT_TM31_MUD_SLAP |
| 0x0034  |            | Victory Road | 1F         | Rival state                                  | 0:Will battle you,1:Disappeared | wVictoryRoadSceneID     |

The following columns should **NOT** be modified, as they are part of internal logic
- Raw Idx
- Event Type
- Identifier

The localizable columns are:
- Location (the major location for this event flag, like a town name, city, dungeon)
- Complement (some useful complement like floor number, or name of a place like a house of someone)
- Text description (the description of the purpose of the flag)
- Valid Values (only used for Event Work flags, they are key:value pair by the character ':' and each entry is separated by ',')

### New discovered flags

The event flags are being researched little by little.  
As the flags gets documented and descriptions are created, they will be embedded into the next version of the plugin.

All research work can be checked here

[Event Flags - Research spreadsheet](https://docs.google.com/spreadsheets/d/1PkY3AVafdOEqKiD_TzD4hTDRvf39ad-eI7e4JylyVII/copy)

To contribute, create a copy of the above, fill the info you researched, and contact back with the information of what needs to be merged.

Priority for community contribution would be the 3DS games.  
Switch games had some progress right now.

## Credits

[Kurt](https://github.com/kwsch) for [PKHeX](https://github.com/kwsch/PKHeX) and [pkNX](https://github.com/kwsch/pkNX)  
[Matt](https://github.com/sora10pls) for a lot of research over event flags and datamining  
[Pret](https://github.com/Pret) and all the disassemblies  
All the people in [PPOrg](https://projectpokemon.org) that have contributed to event flags research
