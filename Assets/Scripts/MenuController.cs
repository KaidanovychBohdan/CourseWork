using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject playerPrefab;
    public void LoadLevel(int sceneID)
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        SceneManager.LoadScene(sceneID);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Instantiate(playerPrefab);

        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
