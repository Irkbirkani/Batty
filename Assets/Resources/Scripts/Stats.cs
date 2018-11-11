using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stats {

    private static int moves = 0, level;
    private static float time;
    public static int Moves
    {
        get { return moves; }
        set { moves = value; }
    }

    public static int Level
    {
        get { return level; }
        set { level = value; }
    }

    public static float Time
    {
        get { return time; }
        set { time = value; }
    }
    
    public static string FormatTime()
    {
        TimeSpan t = TimeSpan.FromSeconds(time);
        return string.Format("Time: {0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
    }
}
