using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Stats {

    private static int moves = 0;

    public static int Moves
    {
        get { return moves; }
        set { moves = value; }
    }

}
