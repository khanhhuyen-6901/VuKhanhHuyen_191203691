using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameDefine
{
    public enum ColorType
    {
        green = 0,
        blue,
        red,
        yellow,
        white,
        none = -1
    }

    public enum EnemyState
    {
        findBrick,
        moveToBridge,
    }
}
