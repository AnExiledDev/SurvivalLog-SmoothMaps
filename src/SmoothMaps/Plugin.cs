using BepInEx;
using BepInEx.Unity.IL2CPP;
using SmoothMaps.Adapters;

namespace SmoothMaps;

[BepInPlugin(Guid, "Smooth Maps", Version)]
public sealed class Plugin : BasePlugin
{
    private const string Guid = "com.dev.survivallog.smoothmaps";
    private const string Version = "0.1.0";

    public override void Load()
    {
        var isFreezing = Config.Bind("Maps", "FreezeWorldBehindMaps", true,
            "When true, the world behind the explore map and the contact map is drawn at a lower rate, the same as behind the bag, the stove and the game's other screens. This keeps the map smooth on a busy graphics card. Set it to false to draw the world at full rate behind the maps again.\n" +
            "设为 true 时，探索地图和联络地图背后的世界会以较低的频率绘制，和背包、灶台等游戏其他界面背后一样。这样在显卡负载较高时地图也能保持流畅。设为 false 则恢复为在地图背后全速绘制世界。");

        MapTicker.IsEnabled = isFreezing.Value;
        MapTicker.Log = message => Log.LogInfo(message);
        AddComponent<MapTicker>();
        Log.LogInfo($"Smooth Maps {Version} started (FreezeWorldBehindMaps = {isFreezing.Value})");
    }
}
