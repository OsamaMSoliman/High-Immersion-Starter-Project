using UnityEngine;

public class Teleporter : MonoBehaviour
{
    private Transform myKin;

    private void Start()
    {
        int myIndex = transform.GetSiblingIndex();
        Transform parent = transform.parent;
        myKin = parent.GetChild((myIndex + 1) % parent.childCount);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ball"))
        {
            Ball.Self.transform.position = myKin.forward.normalized * 0.2f;
        }
    }
}
