using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalStarPortal : MonoBehaviour
{

    public enum TriggerType
    {
        Star,
        Goal,
        Portal
    }

    [SerializeField] private TriggerType type;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            switch (type)
            {
                case TriggerType.Star: StarFunc(); break;
                case TriggerType.Goal: GoalFunc(); break;
                case TriggerType.Portal: ProtralFunc(other.transform); break;
            }
        }
    }

    private void StarFunc()
    {
        GameManager.StarCounter++;
    }

    private void GoalFunc()
    {
        if (GameManager.StarCounter == GameManager.StarCounterMax)
            GameManager.NextLevel();
        else
            GameManager.ResetLevel();
    }

    private int portalNum;
    private void Start()
    {
        if (type == TriggerType.Portal)
            portalNum = ++GameManager.portalCount;
    }

    private void ProtralFunc(Transform ballTransform)
    {
        int outPortal = portalNum % 2 == 0 ? portalNum - 1 : portalNum + 1;
        //TODO: i'm thinking about using a static list for poratls

    }
}
