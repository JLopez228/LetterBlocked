using UnityEngine;
using UnityEngine.SceneManagement;

public class NextTutorial : MonoBehaviour
{
     public void SceneTransition(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
