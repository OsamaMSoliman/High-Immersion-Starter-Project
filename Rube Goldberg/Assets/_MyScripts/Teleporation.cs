using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Teleporation : MonoBehaviour
{

    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private GameObject placeHolder;
    private int rayLayer;

    // Use this for initialization
    void Start()
    {
        if (lineRenderer == null) Debug.LogError("LineRenderer is not set", gameObject);
        if (placeHolder == null) Debug.LogError("placeHolder is not set", gameObject);
        rayLayer = (1 << LayerMask.NameToLayer("Walkable")) | (1 << LayerMask.NameToLayer("HiddenColliders"));
        GetComponent<CharacterController>().detectCollisions = false;
    }


    void Update()
    {
        if (!OVRInput.Get(OVRInput.NearTouch.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch))
        {
            RaycastHit hit;
            if (Physics.Raycast(lineRenderer.transform.position, lineRenderer.transform.forward, out hit, 10f, rayLayer) && hit.transform.gameObject.layer == LayerMask.NameToLayer("Walkable"))
            {
                lineRenderer.SetPosition(0, lineRenderer.transform.position);
                lineRenderer.SetPosition(1, hit.point);
                placeHolder.SetActive(true);
                placeHolder.transform.position = hit.point;
                if (OVRInput.GetUp(OVRInput.Button.Two, OVRInput.Controller.RTouch))
                    transform.position = placeHolder.transform.position + Vector3.up;
            }
        }
        else
        {
            lineRenderer.SetPosition(0, lineRenderer.transform.position);
            lineRenderer.SetPosition(1, lineRenderer.transform.position);
            placeHolder.SetActive(false);
        }

    }
}
