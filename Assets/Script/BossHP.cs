using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHP : MonoBehaviour
{
    [SerializeField]
    private string bossName;

    [SerializeField]
    private int MaxHP;
    [SerializeField]
    private int currentHP;

    private bool isAlive;
    private SpriteRenderer sr;

    private GameObject obj;

    [SerializeField]
    private Image imgFill;
    [SerializeField]
    private TextMeshProUGUI bossNameText;


    private Boss boss;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void InitState(string name, int newHP)
    {
        currentHP = MaxHP = newHP;
        sr.color = Color.white;
        isAlive = true;
        bossNameText.text = name;
    }

    public void TakeDamage(int damage)
    {
        if (isAlive)
        {
            currentHP -= damage;
            StopCoroutine("HitColor");
            StartCoroutine("HitColor");
            imgFill.fillAmount = Mathf.Clamp((float)currentHP / MaxHP, 0f, 1f);

            if(currentHP == MaxHP/2)
            {
                boss.ChangeState(BossState.BS_Phase02);
            }

            if (currentHP <= 0)
            {
                OnDie();
            }
        }
    }

    [SerializeField]
    EnemySpawnManager enemySpawn;

    private void OnDie()
    {
        DropItem();
        isAlive = false;
        gameObject.SetActive(false);
        transform.position = new Vector3(0f, 7f, 0f);
        enemySpawn.StartWave();
    }

    private IEnumerator HitColor()
    {
        sr.color = Color.red;
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        if (gameObject.activeSelf)
            sr.color = Color.white;
    }

    private void DropItem()
    {
        for (int i = 0; i < 5; i++)
        {
            obj = ObjectPoolManager.Instance.pools[(int)ObjectType.Objt_Item_01].Pop();
            obj.transform.position = transform.position;
            obj.transform.rotation = Quaternion.identity;
        }
    }
}
