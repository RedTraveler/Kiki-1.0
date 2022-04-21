using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameEnded = false;
    public float Delay = 0.5f;
    public GameObject completeLevelUI;
    public static GameManager instance;
    public int CurrentLevel;

    void Awake()
    {

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

        public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
        CurrentLevel++;

        PlayerPrefs.SetInt("CurrentLevel", CurrentLevel);

        Debug.Log(CurrentLevel);
    }

    public void EndGame()
    {
        if (gameEnded == false)
        {
            gameEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart", Delay);
        }
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
