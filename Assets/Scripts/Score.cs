using UnityEngine;
using TMPro;

public class Score : SingletonMonoBehaviour<Score>
{
  const string HIGH_SCORE_KEY = "highScore";

  [SerializeField]
  TextMeshProUGUI scoreText = default;

  [SerializeField]
  TextMeshProUGUI highScoreText = default;

  int score;

  int highScore;

  void Start()
  {
    Initialize();
  }

  void Update()
  {
    highScore = Mathf.Max(highScore, score);
    scoreText.text = score.ToString();
    highScoreText.text = $"HighScore : {highScore}";
  }

  public void Initialize()
  {
    score = 0;
    highScore = PlayerPrefs.GetInt(HIGH_SCORE_KEY, 0);
  }

  public void AddPoint(int point)
  {
    score = score + point;
  }

  public void Save()
  {
    PlayerPrefs.SetInt(HIGH_SCORE_KEY, highScore);
    PlayerPrefs.Save();
  }
}
