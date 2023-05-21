using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ObjectPlacementEditor : EditorWindow
{
    private Terrain terrain;
    private GameObject objectsToPlace;
    private int scaleMin, scaleMax;
    private int numGrassInstances = 100;
    float raycastDistance = 500f;

    [MenuItem("Window/Grass Placement Editor")]
    private static void OpenWindow()
    {
        ObjectPlacementEditor window = EditorWindow.GetWindow<ObjectPlacementEditor>();
        window.Show();
    }

    private void OnGUI()
    {
        terrain = EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true) as Terrain;
        objectsToPlace = EditorGUILayout.ObjectField("ObjectToPlace", objectsToPlace, typeof(GameObject), true) as GameObject;
        scaleMin = EditorGUILayout.IntField("ScaleMin", scaleMin);
        scaleMax = EditorGUILayout.IntField("ScaleMax", scaleMax);

        numGrassInstances = EditorGUILayout.IntField("Number of Grass Instances", numGrassInstances);

        if (GUILayout.Button("Place Objects"))
        {
            if (terrain != null && objectsToPlace != null)
            {
                PlaceObjectsOnTerrain();
            }
            else
            {
                Debug.LogError("Terrain and grass prefab must be assigned!");
            }
        }

        if (GUILayout.Button("Remove Objects"))
        {
            if (terrain != null && objectsToPlace != null)
            {
                RemoveAllObjectsFromTerrain();
            }
            
        }
    }

    private void PlaceObjectsOnTerrain()
    {
        float terrainWidth = terrain.terrainData.size.x;
        float terrainLength = terrain.terrainData.size.z;
        Vector3 terrainPosition = terrain.transform.position;

        GameObject parentGO = null;
        Transform child = terrain.transform.Find(objectsToPlace.name);
        if (child != null)
        {
            parentGO = terrain.transform.Find(objectsToPlace.name).gameObject;
        }
        else
        {
            parentGO = Instantiate(new GameObject("temp"), terrain.transform);
            parentGO.name = objectsToPlace.name;
        }

        // Iterate to place grass instances
        for (int i = 0; i < numGrassInstances; i++)
        {
            // Generate random position within the terrain bounds
            float randomX = Random.Range(0f, terrainWidth);
            float randomZ = Random.Range(0f, terrainLength);

            // Create a raycast from above the terrain to find the appropriate y position
            Ray ray = new Ray(new Vector3(randomX + terrainPosition.x, terrainPosition.y + raycastDistance, randomZ + terrainPosition.z), Vector3.down);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, raycastDistance * 2f))
            {
             //   Debug.Log()
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Ground"))
                    return;
                // Get the hit point and instantiate the grass prefab
                Vector3 grassPosition = hit.point;
                var temp = Instantiate(objectsToPlace, grassPosition, Quaternion.identity, parentGO.transform);
                var randomScale = Random.Range(scaleMin, scaleMax);
                temp.transform.localScale = new Vector3(randomScale, randomScale, randomScale);
                temp.transform.Rotate(Vector3.up, Random.Range(0, 360));
            }
        }
    }

    void RemoveAllObjectsFromTerrain() 
    {
        // Get the Transform component of the parent GameObject
        Transform parentTransform = terrain.transform;

        // Iterate through each child of the parent GameObject
        for (int i = parentTransform.childCount - 1; i >= 0; i--)
        {
            // Destroy the child GameObject
            GameObject childObject = parentTransform.GetChild(i).gameObject;
            DestroyImmediate(childObject);
        }
    }
}
