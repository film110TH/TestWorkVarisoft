using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private void Awake()
    {
        GameObject _HightScore = transform.Find("Canvas/HightScorePanel").gameObject;

        transform.Find("Canvas/StartButton").GetComponent<Button>().onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
        transform.Find("Canvas/HightScore").GetComponent<Button>().onClick.AddListener(() =>
        {
            _HightScore.SetActive(true);
            _HightScore.transform.Find("ScoreText").GetComponent<TMP_Text>().text = PlayerPrefs.GetInt("HightScore").ToString();
            _HightScore.transform.Find("CloseButton").GetComponent<Button>().onClick.AddListener(() =>
            {
                _HightScore.SetActive(false);
            });
        });
    }
}
