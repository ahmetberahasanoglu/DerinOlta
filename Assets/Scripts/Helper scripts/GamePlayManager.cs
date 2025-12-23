using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager instance;

    [SerializeField] private TextMeshProUGUI countdownText;
    public int countdownTimer = 60;
[SerializeField] private GameObject pausePanel;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int scoreCount;

    [SerializeField] private UnityEngine.UI.Image scoreFillUI;

    private bool isPaused = false;

    void Awake()
    {
         if (instance != null && instance != this)
    {
        Destroy(gameObject);
        return;
    }

    instance = this;
    }

    void Start()
    {
        scoreCount = 0;                
    scoreFillUI.fillAmount = 0f;    

    DisplayScore(0);
          
    countdownText.text = countdownTimer.ToString();

    StartCoroutine(Countdown());
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevelInstant();
        }

      
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    IEnumerator Countdown()
    {
        while (countdownTimer > 0)
        {
            yield return new WaitForSeconds(1f);

            if (isPaused) continue;

            countdownTimer--;
            countdownText.text = countdownTimer.ToString();

            if (countdownTimer <= 10)
                SoundManager.instance.TimeRunningOut(true);
        }

        SoundManager.instance.GameEnd();
        SoundManager.instance.TimeRunningOut(false);
        StartCoroutine(RestartGame());
    }

    public void DisplayScore(int scoreValue)
    {
         if (!this || !gameObject.activeInHierarchy)
        return;

        scoreCount += scoreValue;
        scoreText.text = "$ " + scoreCount;

        scoreFillUI.fillAmount = (float)scoreCount / 100f;

        if (scoreCount >= 100)
         {
            LevelCompleted();
         }
    }

    void LevelCompleted()
    {
         if (!gameObject.activeInHierarchy)
        return;

    StopAllCoroutines();

    SoundManager.instance.GameEnd();

    int currentIndex = SceneManager.GetActiveScene().buildIndex;
    int lastLevelIndex = 3; 
    int winScreenIndex = 4;

    if (currentIndex < lastLevelIndex)
    {
        SceneManager.LoadScene(currentIndex + 1);
    }
    else
    {
        SceneManager.LoadScene(winScreenIndex);
    }
    }

    void RestartLevelInstant()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene("Level1");
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
            SoundManager.instance.TimeRunningOut(false);
        }
        else
        {
           ResumeGame();
        }
    }

    public void ResumeGame()
{
    pausePanel.SetActive(false);
    Time.timeScale = 1f;
    isPaused = false;
}

public void RestartLevel()
{
    Time.timeScale = 1f;
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}

public void GoToMenu()
{
    Time.timeScale = 1f;
    instance = null;
    Destroy(gameObject);
    SceneManager.LoadScene(0);
}

}
