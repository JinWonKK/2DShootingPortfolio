using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadingMgr : MonoBehaviour
{
    [SerializeField]
    private Image loadingBar;
    [SerializeField]
    private TextMeshProUGUI tipText;

    private void Awake()
    {
        int rTip = Random.Range(1,3);
        if (rTip == 1) { tipText.text = "���͸� óġ�ϸ� Ȯ�������� �������� ���� �� �ֽ��ϴ�."; }
        else if (rTip == 2) { tipText.text = "SpaceBar�� ������ Boom �������� ����մϴ�. "; }
        else { tipText.text = "���׿��� �¾Ƶ� ü���� �Ҹ�˴ϴ�. �����ϼ���!!"; }
        loadingBar.fillAmount = 0f;
        StartCoroutine("LoadAsyncScene");
    }

    IEnumerator LoadAsyncScene()
    {
        yield return YieldInstructionCache.WaitForSeconds(2f);
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync(PlayerPrefs.GetString(SAVE_TYPE.SAVE_Scene.ToString()));
        asyncScene.allowSceneActivation = false;

        float timeC = 0f;

        while(!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;

            if(asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1f, timeC);
                if (loadingBar.fillAmount >= 0.999f)
                    asyncScene.allowSceneActivation = true;
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if (loadingBar.fillAmount >= asyncScene.progress)
                    timeC = 0f;
            }
        }
    }
}
