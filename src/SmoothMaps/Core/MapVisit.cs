using System;

namespace SmoothMaps.Core;

/// <summary>Frame timings for one stay on a map screen, written to the log when the map closes.</summary>
public sealed class MapVisit
{
    private int _frames;
    private int _frozenFrames;
    private int _focusedFrames;
    private double _seconds;
    private double _slowestSeconds;

    public void Add(double deltaSeconds, bool isWorldFrozen, bool isFocused)
    {
        if (deltaSeconds <= 0) return;

        _frames++;
        _seconds += deltaSeconds;
        _slowestSeconds = Math.Max(_slowestSeconds, deltaSeconds);
        if (isWorldFrozen) _frozenFrames++;
        if (isFocused) _focusedFrames++;
    }

    public string Summary()
    {
        if (_frames == 0) return "no frames";

        return $"{_seconds:F0} s, {_frames / _seconds:F0} fps average, slowest frame {_slowestSeconds * 1000:F0} ms, " +
               $"world frozen {Percent(_frozenFrames)}% of frames, game focused {Percent(_focusedFrames)}% of frames";
    }

    private int Percent(int count) => (int)Math.Round(100.0 * count / _frames);
}
