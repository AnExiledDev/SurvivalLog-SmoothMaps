using SmoothMaps.Core;
using Xunit;

namespace SmoothMaps.Tests;

public class MapVisitTests
{
    [Fact]
    public void The_summary_reports_average_rate_slowest_frame_and_shares()
    {
        var visit = new MapVisit();
        visit.Add(0.5, isWorldFrozen: true, isFocused: true);
        visit.Add(0.25, isWorldFrozen: true, isFocused: false);
        visit.Add(0.25, isWorldFrozen: false, isFocused: false);
        visit.Add(1.0, isWorldFrozen: false, isFocused: true);

        Assert.Equal("2 s, 2 fps average, slowest frame 1000 ms, world frozen 50% of frames, game focused 50% of frames", visit.Summary());
    }

    [Fact]
    public void A_zero_length_frame_is_not_counted()
    {
        var visit = new MapVisit();
        visit.Add(0, isWorldFrozen: true, isFocused: true);
        visit.Add(0.1, isWorldFrozen: false, isFocused: false);

        Assert.Equal("0 s, 10 fps average, slowest frame 100 ms, world frozen 0% of frames, game focused 0% of frames", visit.Summary());
    }

    [Fact]
    public void A_visit_with_no_frames_says_so()
    {
        Assert.Equal("no frames", new MapVisit().Summary());
    }
}
