using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoNextLevel : MonoBehaviour
{
    public void SceneTransition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}