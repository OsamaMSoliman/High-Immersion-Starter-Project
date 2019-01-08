using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RubeGoldBergScriptableObject")]
public class RubeGoldBergScriptableObj : ScriptableObject
{
    public bool isTrigger;
    public bool isKinematic;
    public bool useGravity;
}