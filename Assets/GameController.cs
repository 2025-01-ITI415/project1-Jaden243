// Roll-a-Ball Enhanced: Unity C# Scripts

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Text timerText;
    public Text winText;
    public float startTime = 60f;
    private float countdownTimer;
    private int pickupCount;

    void Start()
    {
        countdownTimer = startTime;
        winText.text = "";
        UpdateTimerText();
        pickupCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
    }

    void Update()
    {
        // Update countdown timer
        countdownTimer -= Time.deltaTime;
        UpdateTimerText();

        // Game over condition
        if (countdownTimer <= 0)
        {
            GameOver("Time's up!");
        }
    }

    void UpdateTimerText()
    {
        timerText.text = "Time: " + Mathf.Max(countdownTimer, 0).ToString("F2") + "s";
    }

    public void AddTime(float amount)
    {
        countdownTimer += amount;
        UpdateTimerText();
    }

    public void PickupCollected()
    {
        pickupCount--;
        if (pickupCount <= 0)
        {
            GameOver("You Win!");
        }
    }

    void GameOver(string message)
    {
        winText.text = message;
        Time.timeScale = 0; // Pause the game
    }
}