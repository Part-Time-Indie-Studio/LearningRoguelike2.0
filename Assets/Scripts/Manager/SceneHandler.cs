using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : Singleton<SceneHandler>
{
    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
