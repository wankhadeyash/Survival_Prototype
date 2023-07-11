using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public class SceneChangerEditor : EditorWindow
{
    private string[] sceneNames; // Array to store scene names
    private int selectedSceneIndex = 0; // Index of the selected scene

    // Specify the folder path to filter scenes
    private string folderPath = "Assets/_Scenes/";

    // Add a menu item to open the Scene Changer
    [MenuItem("SceneChanger/Change Scene")]
    static void Init()
    {
        SceneChangerEditor window = (SceneChangerEditor)EditorWindow.GetWindow(typeof(SceneChangerEditor));
        window.Show();
    }

    private void OnEnable()
    {
        // Get all the scene names in the specified folder
        string[] sceneGUIDs = AssetDatabase.FindAssets("t:Scene", new[] { folderPath });
        sceneNames = new string[sceneGUIDs.Length];

        for (int i = 0; i < sceneGUIDs.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(sceneGUIDs[i]);
            sceneNames[i] = System.IO.Path.GetFileNameWithoutExtension(path);
        }
    }

    private void OnGUI()
    {
        GUILayout.Label("Scene Changer", EditorStyles.boldLabel);

        // Display a dropdown to select the scene
        selectedSceneIndex = EditorGUILayout.Popup("Select Scene", selectedSceneIndex, sceneNames);

        GUILayout.Space(10);

        // Button to load the selected scene
        if (GUILayout.Button("Load Scene"))
        {
            if (selectedSceneIndex >= 0 && selectedSceneIndex < sceneNames.Length)
            {
                string sceneName = sceneNames[selectedSceneIndex];
                if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) // Save current scene if there are unsaved changes
                {
                    EditorSceneManager.OpenScene(folderPath + sceneName + ".unity");
                }
            }
        }
    }
}
