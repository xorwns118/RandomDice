using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiceInfo : MonoBehaviour
{
    public Text diceName;
    public Image diceImage;
    public Text skillDamage;
    public Text attackTarget;
    public Text attackDamage;
    public Text attackSpeed;
    public Text description;
    public ScrollRect scrollRect;

    public void ClosePannel()
    {
        scrollRect.content.anchoredPosition = new Vector2(0, 0);

        diceName.text = "";
        diceImage.sprite = null;
        skillDamage.text = "";
        attackTarget.text = "";
        attackDamage.text = "";
        attackSpeed.text = "";
        description.text = "";

        this.gameObject.SetActive(false);
    }
}
