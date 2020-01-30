using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    void Start() { scoreText.text = FindObjectOfType<GameSession>().GetScore().ToString(); }
    void Update() { scoreText.text = FindObjectOfType<GameSession>().GetScore().ToString(); }
}
