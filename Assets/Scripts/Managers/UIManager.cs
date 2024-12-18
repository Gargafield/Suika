using System;
using System.Linq;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    
    public GameObject ScoreLabel;
    public GameObject HighScoreLabel;
    public GameObject GameOverLabel;
    public GameObject DisplayPanel;
    public GameObject FruitDisplayPrefab;
    
    public float Speed = 2;
    public float Amplitude = 0.5f;
    public bool IsGameOver = false;

    public void Start() {
        GameManager.Instance.OnHighScoreChanged += OnHighScoreChanged;
        GameManager.Instance.OnScoreChanged += OnScoreChanged;
        OnHighScoreChanged(GameManager.Instance.HighScore);
        OnScoreChanged(GameManager.Instance.Score);
        
        foreach (Fruit fruit in FruitManager.Instance.FruitCollection.Fruits.AsEnumerable().Reverse()) {
            GameObject fruitObject = Instantiate(FruitDisplayPrefab, DisplayPanel.transform);
            fruitObject.GetComponent<FruitDisplay>().Fruit = fruit;
            fruitObject.SetActive(true);
        }
    }
    
    public void OnHighScoreChanged(int highScore) {
        HighScoreLabel.GetComponent<TMPro.TextMeshProUGUI>().text = highScore.ToString();
    }
    
    public void OnScoreChanged(int score) {
        ScoreLabel.transform.localScale = Vector3.one * 1.5f;
        ScoreLabel.GetComponent<TMPro.TextMeshProUGUI>().text = score.ToString();
    }
    
    public async Awaitable GameOver() {
        IsGameOver = true;
        await Awaitable.WaitForSecondsAsync(1);
        IsGameOver = false;
        await Awaitable.WaitForSecondsAsync(1);
    }
    
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    
    public void Update() {
        ScoreLabel.GetComponent<RectTransform>().rotation = Quaternion.Euler(90, 0, Mathf.Rad2Deg * Mathf.Sin(Time.time * Speed) * Amplitude);
        ScoreLabel.transform.localScale = Vector3.Lerp(ScoreLabel.transform.localScale, Vector3.one, 0.1f);
        HighScoreLabel.GetComponent<RectTransform>().rotation = Quaternion.Euler(90, 0, Mathf.Rad2Deg * Mathf.Sin(Time.time * Speed) * Amplitude);
        
        RectTransform gameOverRect = GameOverLabel.GetComponent<RectTransform>();
        if (IsGameOver) {
            GameOverLabel.transform.localScale = Vector3.Lerp(GameOverLabel.transform.localScale, Vector3.one, 0.1f);
            gameOverRect.anchoredPosition = Vector3.Lerp(gameOverRect.anchoredPosition, Vector3.zero, 0.01f);
        } else {
            GameOverLabel.transform.localScale = Vector3.Lerp(GameOverLabel.transform.localScale, Vector3.zero, 0.1f);
            gameOverRect.anchoredPosition = Vector3.Lerp(gameOverRect.anchoredPosition, new Vector3(0, 700), 0.01f);
        }
    }
}
