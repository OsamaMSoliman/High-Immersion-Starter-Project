using System.Collections;
using UnityEngine;

public class TestingTeleportersForward : MonoBehaviour
{
	static WaitForSeconds delay1sec = new WaitForSeconds(.1f);
	private Transform goT;

	private void Start()
	{
		goT = GameObject.CreatePrimitive(PrimitiveType.Sphere).transform;
		goT.position = transform.forward;
		//StartCoroutine(Play());
	}

	private IEnumerator Play()
	{
		goT.position = transform.position;
		while ( true )
		{
			yield return delay1sec;
			if ( goT.position == transform.forward )
				goT.position = transform.position;
			goT.position += transform.forward * 0.1f;
		}
	}

}
