using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Material m;
    Color originalColor;
    Vector3 originalSize;
    public Rigidbody rb;
    private void Start()
    {
        m = GetComponent<MeshRenderer>().material;
        originalColor = m.color;
        originalSize = transform.localScale;
        rb = GetComponent<Rigidbody>();
        ovrGrabbable = GetComponent<OVRGrabbable>();
    }

    private OVRGrabbable ovrGrabbable;
    private bool _isThrowable = true;
    public bool IsThrowable
    {
        get { return _isThrowable; }
        set
        {
            if (ovrGrabbable.isGrabbed)
            {
                _isThrowable = value;
                m.color = _isThrowable ? originalColor : Color.red;
            }
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Walkable") && !TouchedGoal)
        {
            StartCoroutine(resetBall());
            Star.ShowAllStars();
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


    public static Ball Self;
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

    public bool TouchedGoal { get; set; }
    public void ResetPosition()
    {
        transform.position = resetPosition.position;
        TouchedGoal = false;
    }
}
