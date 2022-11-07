using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Inst
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
    }

    [SerializeField]
    private List<GameObject> heartImage;
    [SerializeField]
    private TextMeshProUGUI scoreText;
    [SerializeField]
    private TextMeshProUGUI boomText;

    public void ChangeBoomText(int count)
    {
        boomText.text = "X " + count.ToString();
    }

    public void ChangeHeart(bool isHealing, int heartPoint)
    {
        if (isHealing)
            heartImage[heartPoint - 1].SetActive(true);
        else
            heartImage[heartPoint].SetActive(false);
    }

    public int score = 0;
    public void AddScore(int Point)
    {
        score += Point;
        scoreText.text = "SCORE : " + score.ToString();
    }

    public void SaveScore()
    {
        int EXP = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_EXP.ToString());
        int Level = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_Level.ToString());
        int Gold = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_GOLD.ToString());

        Debug.Log("종료 전 데이터 EXP: " + EXP + " Level : " + Level + "Gold : " + Gold);
        EXP += (score / 1000);
        Level += EXP / 300;
        EXP %= 300;
        Gold += (score / 100);

        Debug.Log("종료 후 데이터 EXP: " + EXP + " Level : " + Level + "Gold : " + Gold);
        PlayerPrefs.SetInt(SAVE_TYPE.SAVE_EXP.ToString(), EXP);
        PlayerPrefs.SetInt(SAVE_TYPE.SAVE_Level.ToString(), Level);
        PlayerPrefs.SetInt(SAVE_TYPE.SAVE_GOLD.ToString(), Gold);
    }
}
