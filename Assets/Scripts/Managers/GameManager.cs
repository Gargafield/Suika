using System;
using UnityEngine;

public enum GameState {
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameState State = GameState.Playing;

    private int _score = 0;
    public int Score {
        get => _score;
        set {
            _score = value;
            OnScoreChanged?.Invoke(value);
        }
    }

    private int _highscore = 0;
    public int HighScore {
        get => _highscore;
        set {
            _highscore = value;
            OnHighScoreChanged?.Invoke(value);
        }
    }
    
    public event Action<int> OnScoreChanged = default!;
    public event Action<int> OnHighScoreChanged = default!;
    
    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    void Start() {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
    }    
    
    public async Awaitable GameOver() {
        if (State == GameState.GameOver) {
            return;
        }
        State = GameState.GameOver;
        HighScore = Math.Max(Score, HighScore);
        
        Score = 0;
        await FruitManager.Instance.Reset();
        
        UIManager.Instance.IsGameOver = true;
        await Awaitable.WaitForSecondsAsync(2);
        UIManager.Instance.IsGameOver = false;
        
        State = GameState.Playing;
    }
    
    public void AddScore(int score) {
        Score += score;
        
        if (Score > HighScore) {
            HighScore = Score;
            PlayerPrefs.SetInt("HighScore", HighScore);
        }
    }
    
    public void SubtractScore(int score) {
        Score -= score;
    }
}