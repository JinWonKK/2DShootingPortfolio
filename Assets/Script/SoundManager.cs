using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGM_Type
{
    BGM_Normal = 0,
    BGM_BOSS01,
    BGM_BOSS02,
    BGM_BOSS03,
}

public enum sfx_Type
{
    sfx_PlayerFire,
    sfx_EnemyDie,

}

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource bgmAudio;
    [SerializeField]
    private List<AudioClip> bgmList;

    public void ChangeBGM(BGM_Type BGM)
    {
        StartCoroutine(ChangeBGMClip(bgmList[(int)BGM]));
    }

    float current;
    float percent;

    IEnumerator ChangeBGMClip(AudioClip audioClip)
    {
        current = 0;
        percent = 0;
        while(percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1f;
            bgmAudio.volume = Mathf.Lerp(1f, 0f, percent);
            yield return null;
        }

        bgmAudio.clip = audioClip;
        bgmAudio.Play();
        current = 0;
        percent = 0;
        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / 1f;
            bgmAudio.volume = Mathf.Lerp(0f, 1f, percent);
            yield return null;
        }

    }

    private int cursur = 0;

    [SerializeField]
    private List<AudioSource> sfxPlayers;

    [SerializeField]
    private List<AudioClip> sfxList;

    public void PlaySFX(sfx_Type SFX)
    {
        sfxPlayers[cursur].clip = sfxList[(int)SFX];
        sfxPlayers[cursur].Play();

        cursur++;
        if (cursur > 9)
            cursur = 0;
    }

    private static SoundManager instance;
    public static SoundManager Inst
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }
}
