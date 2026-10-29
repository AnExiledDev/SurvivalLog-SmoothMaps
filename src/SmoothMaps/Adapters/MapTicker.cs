using System;
using GameCore.HotUpdate;
using GameCore.HotUpdate.Battle.Show;
using GameCore.HotUpdate.ReduxUI;
using Il2CppInterop.Runtime;
using SmoothMaps.Core;
using UnityEngine;

namespace SmoothMaps.Adapters;

/// <summary>Each frame, while a map screen is open, makes the game treat it like its other panels.</summary>
public sealed class MapTicker : MonoBehaviour
{
    private const int MaxLoggedFailures = 3;

    internal static bool IsEnabled = true;
    internal static Action<string> Log = _ => { };
    private static Il2CppSystem.Type? _mapScreen;
    private int _seenVersion = int.MinValue;
    private bool _isMapOpen;
    private MapVisit? _visit;
    private int _failures;

    public MapTicker(IntPtr pointer) : base(pointer)
    {
    }

    private static Il2CppSystem.Type MapScreen => _mapScreen ??= Il2CppType.Of<WebUI_ExploreMap>();

    private void Update()
    {
        try
        {
            Tick();
        }
        catch (Exception e)
        {
            if (++_failures <= MaxLoggedFailures) Log($"map check failed: {e.GetType().Name}: {e.Message}");
            if (_failures >= MaxLoggedFailures) enabled = false;
        }
    }

    private void Tick()
    {
        var version = UIViewLayer.ShowQueueVersion;

        if (version != _seenVersion)
        {
            _seenVersion = version;
            NoteMapOpen(IsMapOpen());
        }

        if (!_isMapOpen) return;

        if (FreezeHold.ShouldHold(IsEnabled, _isMapOpen, ContainerTabHotKey.s_cachedAnyPanelOpen, ContainerTabHotKey.s_cachedQueueVersion, version))
        {
            ContainerTabHotKey.s_cachedQueueVersion = version;
            ContainerTabHotKey.s_cachedAnyPanelOpen = true;
        }

        _visit?.Add(Time.unscaledDeltaTime, IsWorldFrozen(), Application.isFocused);
    }

    private void NoteMapOpen(bool isOpen)
    {
        if (isOpen == _isMapOpen) return;

        _isMapOpen = isOpen;
        if (isOpen)
        {
            _visit = new MapVisit();
            Log($"map opened (freeze the world behind it: {(IsEnabled ? "on" : "off")})");
        }
        else
        {
            Log($"map closed: {_visit?.Summary() ?? "no frames"}");
            _visit = null;
        }
    }

    private static bool IsMapOpen() =>
        BaseSingleton<ReduxUISystem>.IsInstanceCreated && BaseSingleton<ReduxUISystem>.Instance.IsUIActive(MapScreen);

    private static bool IsWorldFrozen() =>
        BaseSingleton<BattleShowWorld>.IsInstanceCreated && BaseSingleton<BattleShowWorld>.Instance._CameraManager is { } camera && camera._isRenderFrozen;
}
