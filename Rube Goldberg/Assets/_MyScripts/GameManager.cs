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
        fadeDelay = new WaitForSeconds(ovrScreenFade.fadeTime);
    }

    public OVRScreenFade ovrScreenFade;
    public SpawningArea spawningArea;
    private WaitForSeconds fadeDelay;

    public IEnumerator LoadNextGame()
    {
        ovrScreenFade.FadeOut();
        yield return fadeDelay;
        Star.AddNewStar();
        Goal.SetNewPosition(spawningArea.GetGoalRandomPlace());
        Ball.Self.ResetPosition();
        ovrScreenFade.FadeIn();
    }

    public Vector3 GetStarRandomPlace()
    {
        return spawningArea.GetStarRandomPlace();
    }

    public IEnumerator ResetPlayer(Transform player, Vector3 resetPos)
    {
        ovrScreenFade.FadeOut();
        yield return fadeDelay;
        player.position = resetPos;
        Ball.Self.ResetPosition();
        ovrScreenFade.FadeIn();
    }
}
