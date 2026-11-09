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

    /// <summary>
    /// When the setting is turned off while a map is held, the cached true has to be let go, or the world stays at the
    /// lower rate until the map closes. Marking the cache as from an older queue version makes the game work it out again.
    /// </summary>
    public static bool ShouldRelease(bool isEnabled, bool isHolding) => !isEnabled && isHolding;

    /// <summary>A queue version the game will treat as stale, so it works out "is a panel open" again from its own list.</summary>
    public static int StaleVersion(int queueVersion) => unchecked(queueVersion - 1);
}
