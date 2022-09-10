using UnityEngine;
using UnityEngine.SceneManagement;

public class _SceneManager : MonoBehaviour
{
    public void ChangeToThisScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void _QuitApp()
    {
        Application.Quit();
    }
}
