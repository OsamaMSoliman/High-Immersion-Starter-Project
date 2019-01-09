using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public static Goal Self;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball") && Star.StarCounter == Star.stars.Count && Ball.Self.IsThrowable)
        {
            Ball.Self.TouchedGoal = true;
            StartCoroutine(GameManager.Self.LoadNextGame());
        }
    }

    internal static void SetNewPosition(Vector3 vector3)
    {
        print("Goal Reset");
        Self.transform.position = vector3;
    }


}
