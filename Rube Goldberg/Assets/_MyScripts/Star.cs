using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public static int StarCounter = 0;
    public static List<Star> stars;
    private static Transform starHolder;

    private void Start()
    {
        if (stars == null)
            stars = new List<Star>();
        stars.Add(this);
        if (starHolder == null)
        {
            GameObject go = new GameObject("StarHolder");
            starHolder = go.transform;
        }
        transform.SetParent(starHolder);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && other.GetComponent<Ball>().IsThrowable)
        {
            gameObject.SetActive(false);
            Star.StarCounter++;
        }
    }

    public static void ShowAllStars(bool changeLocation = false)
    {
        StarCounter = 0;
        foreach (Star star in stars)
        {
            star.gameObject.SetActive(true);
            if (changeLocation)
                star.transform.position = GameManager.Self.GetStarRandomPlace();
        }

    }

    public static void AddNewStar()
    {
        GameObject go = Instantiate(stars[0].gameObject);
        stars.Add(go.GetComponent<Star>());
        go.transform.position = GameManager.Self.GetStarRandomPlace();
        go.transform.SetParent(starHolder);
        ShowAllStars(true);
    }
}
