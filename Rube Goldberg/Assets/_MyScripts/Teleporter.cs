using UnityEngine;

public class Teleporter : MonoBehaviour
{
	[SerializeField] private Transform myKinsForward;

	private void Start()
	{
		if ( myKinsForward == null )
		{
			int myIndex = transform.GetSiblingIndex();
			Transform parent = transform.parent;
			myKinsForward = parent.GetChild((myIndex + 1) % parent.childCount).GetChild(1); ;
		}

	}

	private void OnCollisionEnter( Collision collision )
	{
		if ( collision.transform.CompareTag("Ball") )
		{
			Ball.Self.transform.position = myKinsForward.position;
		}
	}
}
