using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    int score;
    int level = 1;
    bool gamePaused = false;
    public static GameManager inst;
    public AudioSource audioSource; // إضافة متغير لمكون AudioSource
public AudioClip scoreIncreaseClip; 
    public AudioSource audioSource_1; // إضافة متغير لمكون AudioSource
public AudioClip scoreIncreaseClip_1; 

    [SerializeField] Text levelText;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject pausePanel; // Reference to the pause panel
    [SerializeField] Button continueButton; // Reference to the continue button
    [SerializeField] Text pauseMessage; // Reference to the pause message text

    [SerializeField] PlayerMovement playerMovement;
    private float originalSpeed; // Store the original speed

    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
            audioSource.PlayOneShot(scoreIncreaseClip); // تشغيل الصوت عند زيادة الـScore

        if (score == 20)
        {
               audioSource_1.PlayOneShot(scoreIncreaseClip_1); 

            originalSpeed = playerMovement.speed; // Store the current speed
            playerMovement.speed += 8;
            level++;
            levelText.text = "ROUND: " + level;
            SaveGame();
            pauseMessage.text = "Level up!";
            pausePanel.SetActive(true); // Show the pause panel
            Time.timeScale = 0f; // Pause the game
        }
    }

    public void TogglePause()
    {
        gamePaused = !gamePaused;
        if (gamePaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f; // Continue the game
        }
    }

    private void Awake()
    {
        inst = this;
        LoadGame();
    }

    private void Start()
    {
        pausePanel.SetActive(false); // Ensure pause panel is initially hidden
        continueButton.onClick.AddListener(RestartGame);
    }

    private void Update()
    {

    }

    private void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f; // Ensure time scale is set to normal when restarting
    }

    private void SaveGame()
    {
        // Save level and speed in PlayerPrefs
        PlayerPrefs.SetInt("Level", level);
        PlayerPrefs.SetFloat("Speed", playerMovement.speed);
    }

    private void LoadGame()
    {
        // Load level and speed from PlayerPrefs
        level = PlayerPrefs.GetInt("Level", level);
        playerMovement.speed = PlayerPrefs.GetFloat("Speed", playerMovement.speed);
        levelText.text = "ROUND: " + level;
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.DeleteAll(); // Delete all saved data when the game is closed
    }
}
