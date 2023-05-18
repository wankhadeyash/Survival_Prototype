using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GrassPlacementEditor : EditorWindow
{

    private Terrain terrain;
    public GameObject grassPrefab;
    private int detailIndex;
    private float density;

    [MenuItem("Window/Grass Placement")]
    private static void ShowWindow() 
    {
        GrassPlacementEditor window = GetWindow<GrassPlacementEditor>();
        window.titleContent = new GUIContent("Grass Placement");
        window.Show();
    }

    private void OnGUI()
    {
        EditorGUILayout.LabelField("Grass Placement", EditorStyles.boldLabel);
        terrain = EditorGUILayout.ObjectField("Terrain", terrain, typeof(Terrain), true) as Terrain;
        grassPrefab = EditorGUILayout.ObjectField("Grass Prefab", grassPrefab, typeof(GameObject), false) as GameObject;

        detailIndex = EditorGUILayout.IntField("Detail Index", detailIndex);
        density = EditorGUILayout.FloatField("Density", density);
        if (GUILayout.Button("Place Grass"))
        {
            PlaceGrass();
        }

    }

    private void PlaceGrass()
    {
        if(terrain == null)
        {
            Debug.LogError("Terrain reference is null.");
            return;
        }

        TerrainData terrainData = terrain.terrainData;
        int detailCount = terrainData.detailPrototypes.Length;
        Debug.Log(detailCount);
        if (detailIndex >= detailCount) 
        {
            Debug.LogError("Invalid detail index.");
            return;

        }
        int[,] detailMap = new int[terrainData.detailWidth, terrainData.detailHeight];
        float densityMultiplier = terrainData.detailPrototypes[detailIndex].minWidth * density;

        Debug.Log("asd" + terrainData.detailHeight);
        for (int i = 0; i < terrainData.detailWidth; i += 10)
        {
            for (int j = 0; j < terrainData.detailHeight; j+=10)
            {

                detailMap[i, j] = UnityEngine.Random.value < density ? 1 : 0;

                if (detailMap[i, j] == 1)
                {
                    // Instantiate and position the grass prefab
                    GameObject prefab = Instantiate(grassPrefab);
                    Vector3 position = terrain.transform.position + new Vector3(i, terrain.SampleHeight(new Vector3(i, 0, j)), j);
                    position.x = Mathf.Clamp(position.x, terrain.transform.position.x, terrain.transform.position.x + terrainData.size.x);
                    position.z = Mathf.Clamp(position.z, terrain.transform.position.z, terrain.transform.position.z + terrainData.size.z);
                    prefab.transform.position = position;
                    prefab.transform.parent = terrain.transform;
                }
            }
        }

        terrainData.SetDetailLayer(0, 0, detailIndex, detailMap);
        EditorUtility.SetDirty(terrain);

    }
}
