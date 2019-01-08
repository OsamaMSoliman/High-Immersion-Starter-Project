using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private int currentIndex;
    private bool canSwipe = true;
    MeshRenderer mr;

    // Use this for initialization
    void Start()
    {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(currentIndex).gameObject.SetActive(Mathf.Abs(Vector3.Angle(transform.right, Vector3.up)) < 25);
        if (!canSwipe && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y == 0f)
        {
            canSwipe = true;
        }
        else if (canSwipe && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y > 0.5f)
        {
            transform.GetChild(currentIndex).gameObject.SetActive(false);
            canSwipe = false;
            currentIndex++;
            currentIndex %= transform.childCount;
        }
        else if (canSwipe && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y < -0.5f)
        {
            transform.GetChild(currentIndex).gameObject.SetActive(false);
            canSwipe = false;
            currentIndex--;
            if (currentIndex < 0) currentIndex = transform.childCount - 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Transform t = other.transform;
        if (t.CompareTag("RubeGoldbergObj") && t.IsChildOf(transform))
        {
            var newT = Instantiate(other.gameObject, transform).transform;
            newT.SetSiblingIndex(t.GetSiblingIndex());
            newT.position = transform.position;
            newT.rotation = Quaternion.identity;
            t.SetParent(null);
            t.localScale *= 2;
        }
    }
}
