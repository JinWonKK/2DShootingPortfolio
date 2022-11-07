using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField]
    private MonsterTable monsterTable;
    [SerializeField]
    private Dictionary<string, MonsterClass> monsterData = new Dictionary<string, MonsterClass>();

    [SerializeField]
    private List<Transform> spawnTrans;
    [SerializeField]
    private float spawnDelta;




    [SerializeField]
    private int NextWaveCount = 10;
    [SerializeField]
    private int currentCount = 0;
    [SerializeField]
    private GameObject textwarning;
    [SerializeField]
    private GameObject bossHPbar;

    [SerializeField]
    private List<GameObject> bossObjects;

    int bossCount;

    private void Awake()
    {
        SetDictionary();
        waveCount = 0;
        bossCount = -1;
        StartWave();
    }

    private void SetDictionary()
    {
        for (int i = 0; i < monsterTable.Monster.Count; i++)
        {
            //Debug.Log(i + " " + monsterTable.Monster[i].monsterName);
            monsterData.Add(monsterTable.Monster[i].monsterName, monsterTable.Monster[i]);
        }
    }

    public void StartWave()
    {
        bossHPbar.SetActive(false);
        currentCount = 0;
        StartCoroutine("SpawnEvent");
    }

    private GameObject spawnObj;
    MonsterClass data;
    string stageName;
    int waveCount;

    IEnumerator SpawnEvent()
    {
        waveCount++;
        stageName = "stage_" + waveCount;
        if (!monsterData.TryGetValue(stageName, out data))
        {
            Debug.Log("몬스터 데이터 테이블 참조 실패 " + stageName);
        }

        while (true)
        {
            yield return YieldInstructionCache.WaitForSeconds(spawnDelta);
            currentCount++;
            for (int i = 0; i < 5; i++)
            {
                spawnObj = ObjectPoolManager.Instance.pools[(int)ObjectType.ObjT_Enemy_01].Pop();
                spawnObj.transform.position = spawnTrans[i].position;
                spawnObj.transform.rotation = spawnTrans[i].rotation;
                if (spawnObj.TryGetComponent<EnemyChar>(out EnemyChar enemy))
                {
                    enemy.SetEnemyLevel(data.monsterHP, data.monsterScore);
                }

            }
            if (currentCount == NextWaveCount)
            {
                StopCoroutine("SpawnEvent");
                StartCoroutine("SpawnBoss");
            }
        }
    }

    IEnumerator SpawnBoss()
    {
        bossCount++;
        textwarning.gameObject.SetActive(true);
        yield return YieldInstructionCache.WaitForSeconds(3f);
        textwarning.gameObject.SetActive(false);
        SoundManager.Inst.ChangeBGM(BGM_Type.BGM_BOSS01);
        bossHPbar.SetActive(true);
        bossObjects[bossCount].GetComponent<Boss>().Init(monsterTable.Boss[bossCount].bossName,
                                                         monsterTable.Boss[bossCount].bossHP);
    }
}
