using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ToolTip : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI nameText;
    [SerializeField]
    private TextMeshProUGUI balanceText;
    [SerializeField]
    private TextMeshProUGUI priceText;
    [SerializeField]
    private GameObject upgradeBtnObj;


    private SkillButton sButton;
    private int uPrice;
    private int balance;

    public void InitToolTip(SkillButton skillButton, int price)
    {
        sButton = skillButton;

        nameText.text = skillButton.TYPE.ToString(); //스킬 이름
        priceText.text = "/ " + price.ToString(); //필요금액
        uPrice = price;
        balance = PlayerPrefs.GetInt(SAVE_TYPE.SAVE_GOLD.ToString());
        //balance = 9999; // 테스트용
        balanceText.text = balance.ToString();
        if (balance >= uPrice)
            balanceText.color = Color.white;
        else
            balanceText.color = Color.red;

        if (skillButton.STATE == EnchantState.ES_Enable)
            upgradeBtnObj.SetActive(true);
        else
            upgradeBtnObj.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
            gameObject.SetActive(false);
    }

    public void UpgradeBtn()
    {
        if(balance >= uPrice && sButton)
        {
            balance -= uPrice;
            PlayerPrefs.SetInt(SAVE_TYPE.SAVE_GOLD.ToString(), balance);
            sButton.Upgrade();
            balanceText.text = balance.ToString();
        }
    }

}
