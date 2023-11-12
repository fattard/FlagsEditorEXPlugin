# FlagsEditorEX(pert) Plugin
This is a [PKHeX](https://github.com/kwsch/PKHeX) plugin for advanced users.
It allows you to edit all sorts of Flags and Event Work data from save files.

## Actions

### Dump all Flags

This action will export the entire flag database with the current flag states (True/False) and event work values, with additional description (when available).  
This action is mainly for researching flag states and diffing previous/current states to discover and document the flag usages, that will be added as human readable information.  
The exported file will contains the name *flags_dump_VERSION.txt* that will be created alongside the PKHeX program path.

### Edit Flags

This action opens the main window of the plugin.  
All sorts of Flags and Event Work data can be edited through several tabs, that will open dedicated editors, and basic filters.  

![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/bdc96090-d506-49b6-9c21-3283a999a0bf)
![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/5d7ccace-e6de-4a54-9a08-f39d2458c1ee)
![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/5aff3825-be2f-4f3d-aee2-489a599eb354)
![image](https://github.com/fattard/FlagsEditorEXPlugin/assets/1159052/640c5696-6939-4ba6-8d7a-23d40264bc68)

After finishing editing, export your save data from the base application.

**Note: The lists may contain unused data, which will be filtered out later, as documentation on the flags progresses.**  
**No safety checks are done at all, so any combination of changes may cause issues like softlocks, crashes and permanent data loss.**
**Save data backups are strongly recommended before going into flags editing.**

## Supported Games
All mainline games are supported (limited descriptions for many of them)

- Red / Blue / Yellow (International and Japanese versions)
- Gold / Silver / Crystal (International versions only)
- Ruby / Sapphire / Emerald / FireRed / LeafGreen
- Diamond / Pearl / Platinum / HeartGold / Soul Silver
- Black / White / Black 2 / White 2
- X / Y / Omega Ruby / Alpha Sapphire
- Sun / Moon / Ultra Sun / Ultra Moon / Let's Go Pikachu / Let's Go Eevee
- Sword / Shield / Brilliant Diamond / Shiny Pearl / Legends: Arceus
- Scarlet / Violet

## Contributing

The event flags are being researched little by little.  
As the flags gets documented and descriptions are created, they will be embedded into the next version of the plugin.

All research work can be checked here

[Event Flags - Research spreadsheet](https://docs.google.com/spreadsheets/d/1PkY3AVafdOEqKiD_TzD4hTDRvf39ad-eI7e4JylyVII/edit?usp=sharing)

To contribute, create a copy of the above, fill the info you researched, and contact back with the information of what needs to be merged.

Priority for community contribution would be the 3DS games.  
B2W2 had some progress right now.
