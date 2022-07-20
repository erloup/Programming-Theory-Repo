using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int nbEnemies;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI zoomText;
    public TextMeshProUGUI cooldownText;
    public PlayerController player;
    private float time;
    private bool isOver = false;
    // ENCAPSULATION
    public int NbEnemies
    {
        get => nbEnemies;
        set
        {
            nbEnemies = value;
            if (nbEnemies < 0) nbEnemies = 0;
            if (nbEnemies == 0) Victory();
        }
    }

    private void LateUpdate()
    {
        UpdateATH();
        if(!isOver) time += Time.deltaTime;
    }

    public void GameOver()
    {
        isOver = true;
        MainManager.Instance.isGameOver = true;
        SceneManager.LoadScene(2);
    }

    public void Victory()
    {
        isOver = true;
        MainManager.Instance.timesList.Add(new MainManager.PlayerTime() { name = MainManager.Instance.name, time = time });
        SceneManager.LoadScene(2);
    }
    // ABSTRACTION
    private void UpdateATH()
    {
        zoomText.text = "x" + Math.Round(player.Zoom, 1);
        healthText.text = "PV : " + Mathf.RoundToInt(player.Health);
        int temps = Mathf.RoundToInt(time);
        int secondes = temps % 60;
        int minutes = (temps - secondes) / 60;
        timeText.text = "Time : " + minutes + " ; " + secondes;
        if (player.IsCooldown)
        {
            cooldownText.gameObject.SetActive(true);
            cooldownText.text = "-" + Math.Round(player.Cooldown - player.ActualCooldown, 1);
        }
        else cooldownText.gameObject.SetActive(false);
    }
}
