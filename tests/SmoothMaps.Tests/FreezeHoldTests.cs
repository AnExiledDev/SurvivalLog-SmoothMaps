using SmoothMaps.Core;
using Xunit;

namespace SmoothMaps.Tests;

public class FreezeHoldTests
{
    [Fact]
    public void A_map_the_game_does_not_count_as_a_panel_is_held()
    {
        Assert.True(FreezeHold.ShouldHold(isEnabled: true, isMapOpen: true, cachedPanelOpen: false, cachedVersion: 7, queueVersion: 7));
    }

    [Fact]
    public void An_answer_already_held_for_this_queue_version_is_left_alone()
    {
        Assert.False(FreezeHold.ShouldHold(isEnabled: true, isMapOpen: true, cachedPanelOpen: true, cachedVersion: 7, queueVersion: 7));
    }

    // A true left over from an older queue version would be thrown away by the game, which then works out false.
    [Fact]
    public void A_true_from_an_older_queue_version_is_held_again()
    {
        Assert.True(FreezeHold.ShouldHold(isEnabled: true, isMapOpen: true, cachedPanelOpen: true, cachedVersion: 6, queueVersion: 7));
    }

    [Fact]
    public void Nothing_is_held_with_the_map_closed()
    {
        Assert.False(FreezeHold.ShouldHold(isEnabled: true, isMapOpen: false, cachedPanelOpen: false, cachedVersion: 7, queueVersion: 7));
    }

    [Fact]
    public void Nothing_is_held_when_the_setting_is_off()
    {
        Assert.False(FreezeHold.ShouldHold(isEnabled: false, isMapOpen: true, cachedPanelOpen: false, cachedVersion: 7, queueVersion: 7));
    }

    // Turning the setting off on the Mods screen with a map open must let the held true go, or the world stays slow until the map closes.
    [Fact]
    public void A_held_map_is_released_when_the_setting_is_turned_off()
    {
        Assert.True(FreezeHold.ShouldRelease(isEnabled: false, isHolding: true));
    }

    [Fact]
    public void Nothing_is_released_while_the_setting_is_on()
    {
        Assert.False(FreezeHold.ShouldRelease(isEnabled: true, isHolding: true));
    }

    [Fact]
    public void Nothing_is_released_when_nothing_was_held()
    {
        Assert.False(FreezeHold.ShouldRelease(isEnabled: false, isHolding: false));
    }

    // The game only works out "is a panel open" again when the cached version differs from its queue version.
    [Fact]
    public void The_stale_version_differs_from_the_queue_version()
    {
        Assert.NotEqual(7, FreezeHold.StaleVersion(7));
        Assert.NotEqual(int.MinValue, FreezeHold.StaleVersion(int.MinValue));
    }
}
