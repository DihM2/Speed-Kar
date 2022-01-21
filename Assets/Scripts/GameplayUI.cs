using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI gasText;

    public void ShowScore(int score)
    {
        scoreText.SetText($"Score: {score}");
    }

    public void ShowFuel(float fuel)
    {
        gasText.SetText($"Gas: {fuel.ToString("f0")}%");
    }

}
