using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    public enum WorkingType
    {
        Single = 0,
        Cycle = 1,
        Random = 2
    }

    public enum Direction
    {
        Forward = 0,
        Back = 1,
        Right = 2,
        Left = 3
    }

    public delegate void ControllerExecutor();
}
