using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public static class SceneNavigationMenu
{
    private const string SCENE_DIRECTORY_PATH = "Assets/Scenes/";
    private const string SCENE_NAVIGATION_MENU = "Scene Navigation/";

    private const string START_MENU_NAME = "CanvasStartMenu";

    static SceneNavigationMenu()
    {
    }

    [MenuItem(SCENE_NAVIGATION_MENU + "Base Scene")] public static void loadBaseScene()
    {
        EditorSceneManager.OpenScene(SCENE_DIRECTORY_PATH + "Persistant.unity");
    }

    [MenuItem(SCENE_NAVIGATION_MENU + "1 Level1")] public static void loadLevel1()
    {
        GameObject.Find(START_MENU_NAME).SetActive(false);

        EditorSceneManager.OpenScene(SCENE_DIRECTORY_PATH + "Levels/Level1.unity", 
            OpenSceneMode.Additive);
    }
}
