using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public static int StarCounter = 0;
    public static List<Star> stars;
    private void Start()
    {
        if (stars == null)
            stars = new List<Star>();
        stars.Add(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball") && other.GetComponent<Ball>().IsThrowable)
        {
            gameObject.SetActive(false);
            StarCounter++;
            if (StarCounter == stars.Count)
                GameManager.Self.LoadNextGame();
        }
    }

    public static void ShowAllStars()
    {
        StarCounter = 0;
        foreach (Star star in stars)
            star.gameObject.SetActive(true);
    }

    public static void AddNewStar()
    {
        GameObject go = Instantiate(stars[0].gameObject);
        stars.Add(go.GetComponent<Star>());
        //TODO: change the position
        ShowAllStars();
    }
}
