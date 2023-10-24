using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

public class ScenesToolUtility
{
    [MenuItem("Scenes/BootstrapScene")]
    public static void InitScene() => OpenEditorScene("BootstrapScene");
    
    [MenuItem("Scenes/MenuScene")]
    public static void MenuScene() => OpenEditorScene("MenuScene");

    [MenuItem("Scenes/GameScene")]
    public static void GameScene() => OpenEditorScene("GameScene");

    static void OpenEditorScene(string sceneName)
    {
        if (Application.isPlaying)
            return;

        EditorSceneManager.OpenScene(Application.dataPath + "/Scenes/" + sceneName + ".unity");
    }
}