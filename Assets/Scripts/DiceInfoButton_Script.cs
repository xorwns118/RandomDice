using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfoButton_Script : MonoBehaviour
{
    public void OnClickButton()
    {
        GameObject diceInfoGameObject = GameObject.Find("MyDice").transform.Find("Dice Info").gameObject;
        DiceInfo diceInfo = diceInfoGameObject.GetComponentInChildren<DiceInfo>();
        Dice dice = this.GetComponentInChildren<Dice>();

        diceInfo.diceName.text = dice.diceName;
        diceInfo.diceImage.sprite = dice.GetComponent<Image>().sprite;
        diceInfo.description.text = dice.description;
        diceInfo.attackDamage.text = dice.attackDamage.ToString();
        diceInfo.skillDamage.text = dice.skillDamage.ToString();
        diceInfo.attackTarget.text = dice.attackTarget.ToString();
        diceInfo.attackSpeed.text = dice.attackSpeed.ToString();

        diceInfo.gameObject.SetActive(true);
    }
}
