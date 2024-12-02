using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public TextMeshProUGUI gameOverText;
    public float gameTime = 60f;
    private bool gameEnded = false;

    void Start()
    {
        if (gameOverText != null)
        {
            gameOverText.text = "";
        }
        else
        {
            Debug.LogError("GameOverText not assigned in the inspector.");
        }

        if (playerHealth == null)
        {
            Debug.LogError("PlayerHealth reference not set in GameManager.");
        }
    }

    void Update()
    {
        if (gameEnded || playerHealth == null)
            return;

        gameTime -= Time.deltaTime;

        if (playerHealth.currentHealth <= 0)
        {
            EndGame("Game Over");
        }
        else if (gameTime <= 0)
        {
            EndGame("You Win!");
        }
    }

    void EndGame(string message)
    {
        gameEnded = true;
        if (gameOverText != null)
        {
            gameOverText.text = message;
        }
        Time.timeScale = 0f;
    }
}
