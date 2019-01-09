using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class EditorFilter : MonoBehaviour
{

    [MenuItem("Custom/Select All Non-Static")]
    static void FindAllNonStatic()
    {
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        GameObject[] gameObjectArray;
        gameObjectArray = new GameObject[gameObjects.Length];

        int arrayPointer = 0;

        foreach (GameObject gameObject in gameObjects)
        {
            StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(gameObject);

            if ((flags & StaticEditorFlags.LightmapStatic) == 0)
            {
                gameObjectArray[arrayPointer] = gameObject;
                arrayPointer += 1;
            }
        }

        Selection.objects = gameObjectArray;

    }


    [MenuItem("Custom/Select All Static")]
    static void FindAllStatic()
    {
        Object[] gameObjects = FindObjectsOfType(typeof(GameObject));

        GameObject[] gameObjectArray;
        gameObjectArray = new GameObject[gameObjects.Length];

        int arrayPointer = 0;

        foreach (GameObject gameObject in gameObjects)
        {
            StaticEditorFlags flags = GameObjectUtility.GetStaticEditorFlags(gameObject);

            if ((flags & StaticEditorFlags.LightmapStatic) != 0)
            {
                gameObjectArray[arrayPointer] = gameObject;
                arrayPointer += 1;
            }
        }

        Selection.objects = gameObjectArray;

    }


    [MenuItem("Custom/Select All Walkable")]
    static void FindAllWalkable()
    {
        Selection.objects = GameObject.FindGameObjectsWithTag("Walkable"); ;
    }
}
