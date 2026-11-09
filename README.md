# Smooth Maps

Keeps the explore map and the contact map smooth.

Behind the bag, the stove and most other screens, Survival Log stops drawing the world at full speed, because you can't see it anyway. The maps are left off that list, so the game keeps drawing the whole world behind them at full rate. On a laptop or a busy graphics card that leaves too little for the map itself, and it stutters or lags. Smooth Maps puts the maps on the list.

## Features

- **Smooth maps**: while the explore map or the contact map is open, the world behind it is drawn at a lower rate, the same as behind the game's other screens.
- **Back to normal when it closes**: the world is drawn at full rate again as soon as the map closes.
- **Map timings in the log**: each time a map closes, the log gets one line with how smooth it was.

Smooth Maps adds nothing to your save and does not edit game files. You can remove it at any time.

## Requirements

- Survival Log 1.1.20719. Built for this version. A game update can break it until the mod is updated.
- The [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12), the BepInEx 6 (IL2CPP) build for the game.
- [Exiled's Toolkit](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit) 0.19.0 or newer. Smooth Maps does not load without it. It applies a setting changed on the Mod Manager's Mods screen at once, without a restart.
- Optional: [Mod Manager](https://www.nexusmods.com/survivallog/mods/73). With it, you can change every setting in game on its Mods screen and see what each one does, without opening the config file.

## Install

1. Install the [BepInEx Pack for Survival Log](https://www.nexusmods.com/survivallog/mods/12) (if no other mods were installed before, start the game once so BepInEx finishes setup, then quit).
2. Install Exiled's Toolkit. Download `ExiledToolkit-x.y.z.zip` from [its latest release](https://github.com/AnExiledDev/SurvivalLog-ExiledToolkit/releases/latest) or from [Nexus Mods](https://www.nexusmods.com/survivallog/mods/61), and extract it into the game folder (the folder with `SurvivalLog.exe`).
3. Download `SmoothMaps-x.y.z.zip` from the [latest release](https://github.com/AnExiledDev/SurvivalLog-SmoothMaps/releases/latest).
4. Extract the zip into the game folder (the folder with `SurvivalLog.exe`). It holds a `BepInEx` folder, so the DLL lands in the right place. When asked, merge the folders. The result should be:
   `C:\Program Files (x86)\Steam\steamapps\common\Survival Log\BepInEx\plugins\SmoothMaps.dll`

## Uninstall

Delete `SmoothMaps.dll` from the `BepInEx\plugins` folder. For a clean slate, also delete `BepInEx\config\com.dev.survivallog.smoothmaps.cfg`.

## Configuration

Settings live in `BepInEx\config\com.dev.survivallog.smoothmaps.cfg`, created the first time the game starts with the mod. See [CONFIG.md](CONFIG.md) for every setting. A change takes effect at once.

## Troubleshooting

Built against BepInEx `6.0.0-be.788`.

Check `BepInEx\LogOutput.log` for `Smooth Maps 0.1.2 started`. Each time you open a map, a `map opened` line follows, and when you close it, a `map closed` line shows the average frames per second, the slowest frame, and how much of the time the world behind the map was drawn at the lower rate (`world frozen`). If that share stays near 0%, the mod is not working on your game version.

## Feedback and bug reports

Found a bug, have an idea, or something feels off? Come say so on the Core Directive Discord: https://discord.gg/trTsyxUFqg . When reporting a bug, attach `BepInEx\LogOutput.log`.

To reach me directly, message `exiled.dev` on Discord; you can find me in the [official Survival Log Discord](https://discord.gg/MZGg4gQAUe). I'm also on X as [@AnExiledDev](https://x.com/AnExiledDev).

## Build

This is a BepInEx 6 IL2CPP plugin. It compiles against BepInEx, the game's IL2CPP interop assemblies and Exiled's Toolkit, so a game install with BepInEx set up and started once is required. Those assemblies are not part of this repo. The .NET 8 SDK is required.

```
dotnet build src/SmoothMaps -c Release -p:GameDir="C:\path\to\Survival Log" -p:ToolkitDll="C:\path\to\ExiledToolkit.dll"
```

`GameDir` is the game folder and `ToolkitDll` is a built `ExiledToolkit.dll`. The output DLL is at `src\SmoothMaps\bin\Release\SmoothMaps.dll`.

The unit tests cover the pure rules and need nothing from the game:

```
dotnet test tests/SmoothMaps.Tests
```

## License

MIT. Copyright (c) 2026 AnExiledDev. See [LICENSE](LICENSE).
