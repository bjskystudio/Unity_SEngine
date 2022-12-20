using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    [MenuItem("Tools/游戏开始")]
    static void MenuItemPrint()
    {
        Debug.Log("开始游戏");
       // UnityEditor.EditorApplication.isPlaying = true;
        UnityEditor.EditorApplication.EnterPlaymode();
        if (UnityEditor.EditorApplication.isPlaying)
        {
            SceneManager.LoadScene("Scences/GameEntry", LoadSceneMode.Additive);
        }
        
    }
}
