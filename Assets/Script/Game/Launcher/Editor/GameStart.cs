using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{

    [MenuItem("Tools/��Ϸ��ʼ")]
    static void MenuItemPrint()
    {
        Debug.Log("��ʼ��Ϸ");
       // UnityEditor.EditorApplication.isPlaying = true;
        UnityEditor.EditorApplication.EnterPlaymode();
        if (UnityEditor.EditorApplication.isPlaying)
        {
            SceneManager.LoadScene("Scences/GameEntry", LoadSceneMode.Additive);
        }
        
    }
}
