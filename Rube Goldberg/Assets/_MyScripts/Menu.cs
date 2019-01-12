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
		pool = new Dictionary<string , Queue<Transform>>();
		foreach ( Transform item in transform )
		{
			item.gameObject.SetActive(false);
			pool[item.name] = new Queue<Transform>();
		}
	}


	void Update()
	{
		#region TestingWithoutVR
		//if ( Input.GetKeyDown(KeyCode.W) )
		//{
		//	transform.GetChild(currentIndex).gameObject.SetActive(false);
		//	canSwipe = false;
		//	currentIndex++;
		//	currentIndex %= transform.childCount;
		//	transform.GetChild(currentIndex).gameObject.SetActive(true);
		//}
		//else if ( Input.GetKeyDown(KeyCode.S) )
		//{
		//	transform.GetChild(currentIndex).gameObject.SetActive(false);
		//	canSwipe = false;
		//	currentIndex--;
		//	if ( currentIndex < 0 ) currentIndex = transform.childCount - 1;
		//	transform.GetChild(currentIndex).gameObject.SetActive(true);
		//}
		//else if ( Input.GetMouseButtonDown(0) )
		//{
		//	RaycastHit hit;
		//	if ( Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition) , out hit , 100f) )
		//	{

		//		OnTriggerExit(hit.collider);
		//	}
		//}
		//return;
		#endregion

		bool b = Mathf.Abs(Vector3.Angle(transform.right , Vector3.up)) < 25;
		transform.GetChild(currentIndex).gameObject.SetActive(b);
		if ( !canSwipe && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y == 0f )
		{
			canSwipe = true;
		}
		else if ( canSwipe && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y > 0.5f )
		{
			transform.GetChild(currentIndex).gameObject.SetActive(false);
			canSwipe = false;
			currentIndex++;
			currentIndex %= transform.childCount;
		}
		else if ( canSwipe && OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y < -0.5f )
		{
			transform.GetChild(currentIndex).gameObject.SetActive(false);
			canSwipe = false;
			currentIndex--;
			if ( currentIndex < 0 ) currentIndex = transform.childCount - 1;
		}
	}

	private void OnTriggerExit( Collider other )
	{
		Transform grabbed = other.transform;
		if ( grabbed.CompareTag("RubeGoldbergObj") && grabbed.IsChildOf(transform) )
		{
			GetFromPoolToMenu(grabbed);
			AddToPool(grabbed);

			if ( grabbed.name.Contains("Teleporter") )
			{
				grabbed.GetComponent<Collider>().enabled = false;
				foreach ( Transform child in grabbed )
					child.GetComponent<Collider>().enabled = true;
			}

		}
	}

	private Dictionary<string , Queue<Transform>> pool;

	public void GetFromPoolToMenu( Transform target )
	{
		// get the name (key)
		string key = target.name;


		if ( pool[key].Count == 0 || pool[key].Peek().gameObject.activeInHierarchy )
		{
			// create new 
			var newT = Instantiate(target.gameObject , transform.position , Quaternion.identity , transform).transform;
			newT.SetSiblingIndex(target.GetSiblingIndex());
			newT.name = target.name;
		}
		else
		{
			// get from pool
			Transform pooledTrans = pool[key].Dequeue();
			pooledTrans.position = transform.position;
			pooledTrans.rotation = Quaternion.identity;
			pooledTrans.SetParent(transform);
			pooledTrans.SetSiblingIndex(target.GetSiblingIndex());
			pooledTrans.gameObject.SetActive(true);
		}
	}

	private void AddToPool( Transform grabbed )
	{
		grabbed.SetParent(null);
		grabbed.localScale *= 2;
		pool[grabbed.name].Enqueue(grabbed);
	}

	public void ResetPool()
	{
		foreach ( var entry in pool )
		{
			foreach ( var item in entry.Value )
			{
				item.localScale /= 2;
				item.gameObject.SetActive(false);
				if ( item.name.Contains("Teleporter") )
				{
					item.GetComponent<Collider>().enabled = true;
					foreach ( Transform child in item )
						child.GetComponent<Collider>().enabled = false;
				}
			}
		}

	}
}
