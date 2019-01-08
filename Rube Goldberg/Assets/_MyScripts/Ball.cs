using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    Material m;
    Color originalColor;
    Vector3 originalSize;
    Rigidbody rb;
    private void Start()
    {
        m = GetComponent<MeshRenderer>().material;
        originalColor = m.color;
        originalSize = transform.localScale;
        rb = GetComponent<Rigidbody>();
    }

    public bool IsThrowable { get; set; }
    

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Walkable"))
        {
            StartCoroutine(resetBall());
        }
    }

    WaitForSeconds delay = new WaitForSeconds(0.1f);
    [SerializeField] private Transform resetPosition;

    IEnumerator resetBall()
    {
        m.color = Color.red;
        float timescaped = 0;
        while (transform.localScale.magnitude > Vector3.one.magnitude)
        {
            timescaped += 0.1f;
            transform.localScale = Vector3.Lerp(originalSize, Vector3.one, timescaped);
            yield return delay;

        }
        rb.Sleep();
        transform.position = resetPosition.position;
        m.color = originalColor;
        timescaped = 0;
        while (transform.localScale.magnitude < originalSize.magnitude)
        {
            timescaped += 0.1f;
            transform.localScale = Vector3.Lerp(Vector3.one, originalSize, timescaped);
            yield return delay;

        }
    }
}
