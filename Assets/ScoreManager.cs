using UnityEngine;
using TMPro; // Important for TextMeshPro

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreDisplay;

    void Update()
    {
        // This grabs the 'totalScore' from your TargetHit script
        // and displays it on the screen every frame.
        scoreDisplay.text = "Score: " + TargetHit.totalScore.ToString();
    }
}