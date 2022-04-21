using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelComplete : MonoBehaviour
{
    public int currentLevel;

    public void Loading()
    {
        SceneManager.LoadScene("Loading");
    }

    public void LoadNextScene()
    {
        currentLevel = PlayerPrefs.GetInt("CurrentLevel");
        SceneManager.LoadScene(currentLevel+1);
    }
}
