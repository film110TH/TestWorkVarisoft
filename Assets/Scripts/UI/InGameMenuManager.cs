using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class InGameMenuManager : MonoBehaviour
{
    public static InGameMenuManager instance;
    public int Score;
    private void Awake()
    {
        if(instance == null)
            instance = this;

        GameObject _SettingPanel = transform.Find("Canvas/SettingPanel").gameObject;

        transform.Find("Canvas/Main/SettingButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            _SettingPanel.SetActive(true);
            _SettingPanel.transform.Find("BG/HeaderText").GetComponent<TMP_Text>().text = "Pause";
            _SettingPanel.transform.Find("BG/ScoreText").GetComponent<TMP_Text>().text = Score.ToString();
            Time.timeScale = 0;
        });

        _SettingPanel.transform.Find("BG/CloseButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            _SettingPanel.SetActive(false);
            Time.timeScale = 1;
        });
        _SettingPanel.transform.Find("BG/ReStartButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        });
        _SettingPanel.transform.Find("BG/BackToMenuButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        });
    }

    public void addScore(int _score)
    {
        Score += _score;
        transform.Find("Canvas/Main/ScoreText").GetComponent<TMP_Text>().text = Score.ToString();
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        GameObject _SettingPanel = transform.Find("Canvas/SettingPanel").gameObject;
        _SettingPanel.transform.Find("BG/HeaderText").GetComponent<TMP_Text>().text = "GameOver";
        _SettingPanel.transform.Find("BG/ScoreText").GetComponent<TMP_Text>().text = Score.ToString();
        _SettingPanel.SetActive(true);
        SetHihgt();
    }

    public void SetHihgt()
    {
        int hightscore = PlayerPrefs.GetInt("HightScore");

        if (hightscore < Score)
        {
            Debug.Log("HightScore");
            PlayerPrefs.SetInt("HightScore", Score);
            GameObject _SettingPanel = transform.Find("Canvas/SettingPanel").gameObject;
            _SettingPanel.transform.Find("BG/NewHightScoretext").gameObject.SetActive(true);
        }
    }
}
