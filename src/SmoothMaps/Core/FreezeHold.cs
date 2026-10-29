namespace SmoothMaps.Core;

/// <summary>
/// The game stops drawing the world at full rate while one of its panels is open, but its list of panels leaves out
/// the map screens. It caches "is a panel open" and only works it out again when its UI queue version changes, so
/// writing true with the current version makes a map count as a panel until the next screen opens or closes.
/// </summary>
public static class FreezeHold
{
    public static bool ShouldHold(bool isEnabled, bool isMapOpen, bool cachedPanelOpen, int cachedVersion, int queueVersion) =>
        isEnabled && isMapOpen && !(cachedPanelOpen && cachedVersion == queueVersion);
}
