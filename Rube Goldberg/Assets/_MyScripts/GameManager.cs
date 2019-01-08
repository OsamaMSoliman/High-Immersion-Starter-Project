using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Self;
    private void Awake()
    {
        if (Self == null)
            Self = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public static int StarCounter { get; set; }
    public static int StarCounterMax { get; set; }
    public static bool canPlayBall { get; set; }
    public static int portalCount { get; set; }

    internal static void ResetLevel()
    {
        throw new NotImplementedException();
    }

    internal static void NextLevel()
    {
        ResetLevel();
    }
}
