using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static MainManager;

public class HightScoreUIHandler : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI scoreText;
    public RectTransform allScoreContent;
    public TextMeshProUGUI scoreTextPrelab;

    // Start is called before the first frame update
    void Start()
    {
        SetText();
    }

    private void SetText()
    {
        bool isGameOver = MainManager.Instance.isGameOver;
        if (isGameOver)
        {
            titleText.text = "Game Over";
            titleText.color = Color.red;

            scoreText.gameObject.SetActive(false);
        }
        else
        {
            titleText.text = "Victory";
            titleText.color = Color.green;

            scoreText.gameObject.SetActive(true);
            MainManager.PlayerTime score = MainManager.Instance.timesList.Last();
            scoreText.text = score.name + " - " + FormatTime(score.time);
        }

        int heigth = -25;
        foreach (PlayerTime score in MainManager.Instance.timesList.OrderBy(x => x.time))
        {
            TextMeshProUGUI textScore = Instantiate(scoreTextPrelab);
            textScore.text = score.name + " - " + FormatTime(score.time);
            textScore.transform.position = new Vector3(110, heigth);
            textScore.transform.SetParent(allScoreContent, false);
            heigth -= 25;
        }
        allScoreContent.sizeDelta = new Vector2(allScoreContent.sizeDelta.x, -heigth * 2);
    }

    private string FormatTime(float time)
    {
        int temps = Mathf.RoundToInt(time);
        int secondes = temps % 60;
        int minutes = (temps - secondes) / 60;
        return minutes + " ; " + secondes;
    }

    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
