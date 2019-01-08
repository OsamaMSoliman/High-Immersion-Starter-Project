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

    public static bool canPlayBall { get; set; }
    public static int portalCount { get; set; }

    public OVRScreenFade ovrScreenFade;

    public void LoadNextGame()
    {
        ovrScreenFade.FadeOut();
        //TODO: wait
        Star.AddNewStar();
        //TODO: wait
        ovrScreenFade.FadeIn();

    }
}
