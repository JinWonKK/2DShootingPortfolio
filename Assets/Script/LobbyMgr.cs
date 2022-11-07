using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public enum MenuType
{
    MenuType_Enchant = 1,
    MenuType_Option = 5,
}

public class LobbyMgr : MonoBehaviour
{
    [SerializeField]
    private FadeInOut fadeScr;

    [SerializeField]
    private TextMeshProUGUI pLevelText;
    [SerializeField]
    private TextMeshProUGUI goldText;
    [SerializeField]
    private Image expBar;

    private void Awake()
    {
        InitLobbyScene();
    }

    [SerializeField]
    private List<SkillButton> skillButtonList;

    private void InitLobbyScene()
    {
        pLevelText.text = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_Level.ToString()).ToString();
        goldText.text = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_GOLD.ToString()).ToString();
        expBar.fillAmount = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_EXP.ToString()) / 300f;

        for(int i = 0; i < skillButtonList.Count; i++)
        {
            Skill_Type skill = (Skill_Type)i;
            if (0 < PlayerPrefs.GetInt(skill.ToString()))
            {
                skillButtonList[i].InitBtn(true, 9);
            }
            else
                skillButtonList[i].InitBtn(false, 9);
        }
        SFX_ValueChange(PlayerPrefs.GetFloat(SAVE_TYPE.SAVE_SFX.ToString()));
        BGM_ValueChange(PlayerPrefs.GetFloat(SAVE_TYPE.SAVE_BGM.ToString()));
    }

    


    public void FadeInOut()
    {
        fadeScr.Fade_InOut(false, 3.0f);
        Invoke("StartGame", 3.0f);
    }

    public void StartGame()
    {
        PlayerPrefs.SetString(SAVE_TYPE.SAVE_Scene.ToString(), SCENE_NAME.PlayScene.ToString());
        SceneManager.LoadScene(SCENE_NAME.LoadingScene.ToString());
    }

    private int activeMenu = 0;

    public int ActivaMenu
    {
        set
        {
            if (value < 1 || value > 5)
            {
                Debug.Log("Error 1001 : activeMenu °ª ¿À·ù");
                activeMenu = 0;
            }
            else
                activeMenu = value;
        }
    }

    [SerializeField]
    private List<GameObject> popupObjList;

    public void OnClickBtn(int i)
    {
        if(i == activeMenu)
        {
            LeanTween.moveLocalY(popupObjList[activeMenu], -1500f, 0.5f);
            activeMenu = 0;
        }
        else
        {
            if(activeMenu != 0)
            {
                LeanTween.moveLocalY(popupObjList[activeMenu], -1500f, 0.5f);
            }
            activeMenu = i;
            LeanTween.moveLocalY(popupObjList[activeMenu], 0f, 0.5f);
        }
    }

    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private TextMeshProUGUI sfx_Text;

    [SerializeField]
    private Slider sfx_Slider;

    private float valueF;

    public void SFX_ValueChange(float value)
    {
        PlayerPrefs.SetFloat(SAVE_TYPE.SAVE_SFX.ToString(), value);
        ChangeVolume(sfx_Text, sfx_Slider, SAVE_TYPE.SAVE_SFX, value);
    }

    void ChangeVolume(TextMeshProUGUI text, Slider slider, SAVE_TYPE type, float newVolume)
    {
        text.text = newVolume.ToString("N2");
        slider.value = newVolume;

        valueF = newVolume * 30f - 30f;
        if (valueF < -29f)
            valueF = -80f;
        audioMixer.SetFloat(type.ToString(), valueF);
    }

    [SerializeField]
    private TextMeshProUGUI bgm_Text;

    [SerializeField]
    private Slider bgm_Slider;

    public void BGM_ValueChange(float value)
    {
        PlayerPrefs.SetFloat(SAVE_TYPE.SAVE_BGM.ToString(), value);
        ChangeVolume(bgm_Text, bgm_Slider, SAVE_TYPE.SAVE_BGM, value);
    }
}
