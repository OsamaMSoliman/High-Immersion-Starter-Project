using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    private int currentIndex;
    private bool canSwipe = true;
    private MeshRenderer mr;

    // Use this for initialization
    void Start()
    {
        pool = new Dictionary<string, List<GameObject>>();
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
            pool[item.name] = new List<GameObject>();
            pool[item.name].Add(item.gameObject);
        }
        mr = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool b = Mathf.Abs(Vector3.Angle(transform.right, Vector3.up)) < 25;
        transform.GetChild(currentIndex).gameObject.SetActive(b);
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

            if (t.name.Contains("Teleporter"))
            {
                t.GetComponent<Collider>().enabled = false;
                foreach (Transform child in t)
                {
                    child.GetComponent<Collider>().enabled = true;
                }
            }

        }
    }

    private Dictionary<string, List<GameObject>> pool;
    public Transform GetPooledObject()
    {
        /*
        var newT = Instantiate(other.gameObject, transform).transform;
        newT.SetSiblingIndex(t.GetSiblingIndex());
        newT.position = transform.position;
        newT.rotation = Quaternion.identity;
        return newT;

        ballPoolNum++;
        if (ballPoolNum > (ballsAmount - 1))
        {
            ballPoolNum = 0;
        }
        //if we’ve run out of objects in the pool too quickly, create a new one
        if (pooledBalls[ballPoolNum].activeInHierarchy)
        {
            //create a new bullet and add it to the bulletList
            GameObject obj = Instantiate(pooledBall);
            pooledBalls.Add(obj);
            ballsAmount++;
            ballPoolNum = ballsAmount - 1;
        }
        //Debug.Log(ballPoolNum);
        return pooledBalls[ballPoolNum];
        */
        return null;
    }

}
