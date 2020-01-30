using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int score = 0;

    void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public int GetScore() { return score; }

    public void AddToScore(int n) { score += n; }

    public void ResetScore() { score = 0; }
}
