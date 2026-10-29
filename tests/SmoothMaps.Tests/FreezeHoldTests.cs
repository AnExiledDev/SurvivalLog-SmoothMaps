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
}
