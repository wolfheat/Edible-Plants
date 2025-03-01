using UnityEditor;
using UnityEngine;

public class StartMenu : MonoBehaviour
{


    public void OnQuit()
    {
        Debug.Log("ON QUIT");
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

}
