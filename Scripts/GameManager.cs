using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager singleton;
    public int currentStage = 0;
    public SceneFader sceneFader;
    int currentLevelIndex;
    

    private void Awake()
    {
        if (singleton == null)
            singleton = this;
        else if (singleton != this)
            Destroy(gameObject);

        currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void NextLevel()
    {
        sceneFader.FadeToOut(currentLevelIndex + 1);
    }

    public void RestartLevel()
    {
        sceneFader.FadeToOut(currentLevelIndex);
    }
}
