using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObstacleFactory
{
    public static IObstacleCounter GetCounter(string obstacleType)
    {
        switch (obstacleType)
        {
            case "box":
                return new BoxCounter();
            case "stone":
                return new StoneCounter();
            case "vase_01":
                return new VaseCounter();
            default:
                throw new ArgumentException("Unknown obstacle type");
        }
    }
}


