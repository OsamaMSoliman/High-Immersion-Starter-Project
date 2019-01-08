using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallThrowableArea : MonoBehaviour
{

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Ball>().IsThrowable = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            other.GetComponent<Ball>().IsThrowable = true;
        }
    }
}
