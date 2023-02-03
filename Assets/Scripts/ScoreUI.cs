using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text scoreText;
    [SerializeField] private Tank player;

    private void Update()
    {
        scoreText.text = "SCORE: " + player.score; 
    }
}
