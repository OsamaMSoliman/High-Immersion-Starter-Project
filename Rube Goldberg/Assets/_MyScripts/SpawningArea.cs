using UnityEngine;

public class SpawningArea : MonoBehaviour
{
    [SerializeField] private int[] radii;

    public Vector3 GetGoalRandomPlace()
    {
        int randomIndex = Random.RandomRange(0, radii.Length);
        Vector2 v2 = Random.insideUnitCircle * radii[randomIndex];
        return transform.GetChild(randomIndex).position + new Vector3(v2.x, 0, v2.y);

    }

    public Vector3 GetStarRandomPlace()
    {
        int randomIndex = Random.RandomRange(0, radii.Length);
        Vector3 v3 = Random.insideUnitSphere * radii[randomIndex];
        v3.y *= v3.y > 0 ? 1 : -1;
        return transform.GetChild(randomIndex).position + v3;
    }

    private void OnDrawGizmosSelected()
    {
        if (transform.childCount != radii.Length)
        {
            int diff = radii.Length - transform.childCount;
            if (diff > 0)
                for (int i = 0; i < diff; i++)
                {
                    GameObject go = new GameObject();
                    go.transform.SetParent(transform);
                }
            else
                for (int i = 0; i > diff; i--)
                {
                    DestroyImmediate(transform.GetChild(0).gameObject);
                }
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform child = transform.GetChild(i);
            child.name = i.ToString();
            Gizmos.DrawWireSphere(child.position, radii[i]);
        }
    }
}
