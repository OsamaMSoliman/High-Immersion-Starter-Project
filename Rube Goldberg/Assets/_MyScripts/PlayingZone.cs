using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingZone : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(GameManager.Self.ResetPlayer(other.transform, transform.position));
        }
        else if (other.CompareTag("Ball"))
        {
            Ball.Self.ResetPosition();
        }
    }
}
