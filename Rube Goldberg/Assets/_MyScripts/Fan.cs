using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] private float force = 5000f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball.Self.rb.AddForce(transform.up * Time.deltaTime * force, ForceMode.Acceleration);
        }
    }
}
