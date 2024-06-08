using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorMenuScript : MonoBehaviour
{

    [MenuItem("Game Menu/Main Menu", priority = 1)]
    private static void SetMainMenu()
    {
        LoadScene(0);
    }
    [MenuItem("Game Menu/Level 1", priority = 2)]
    private static void SetLevel1()
    {
        LoadScene(1);     
    }

    [MenuItem("Game Menu/Level 2", priority = 3)]
    private static void SetLevel2()
    {
        LoadScene(2);
    }
    [MenuItem("Game Menu/Level 3", priority = 4)]
    private static void SetLevel3()
    {
        LoadScene(3);
    }
    [MenuItem("Game Menu/Level 4", priority = 5)]
    private static void SetLevel4()
    {
        LoadScene(4);
    }
    [MenuItem("Game Menu/Level 5", priority = 6)]
    private static void SetLevel5()
    {
        LoadScene(5);
    }
    [MenuItem("Game Menu/Level 6", priority = 7)]
    private static void SetLevel6()
    {
        LoadScene(6);

    }
    [MenuItem("Game Menu/Level 7", priority = 8)]
    private static void SetLevel7()
    {
        LoadScene(7);

    }
    [MenuItem("Game Menu/Level 8", priority = 9)]
    private static void SetLevel8()
    {
        LoadScene(8);

    }
    [MenuItem("Game Menu/Level 9", priority = 10)]
    private static void SetLevel9()
    {
        LoadScene(9);

    }
    [MenuItem("Game Menu/Level 10",priority=11)]
    private static void SetLevel10()
    {
        LoadScene(10);

    }

    private static void LoadScene(int SceneIndex)
    {
        SceneManager.LoadScene(SceneIndex);
    }
}
