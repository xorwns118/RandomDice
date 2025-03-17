using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    public string diceName;
    public int currentValue;
    public int currentUpgradeLevel;
    public int maxUpgradeLevel;
    public int upgradeCost;
    public int skillDamage;
    public int attackTarget;
    public int attackDamage;
    public float attackSpeed;
    public string description;
    public GameObject pip; // 1 ~ 6
    public GameObject star; // 7
    public List<GameObject> pips; // 1 ~ 7

    readonly Vector3[][] pipsPosition =
    {
        new Vector3[] { new Vector3(0, 5, 0) },

        new Vector3[] { new Vector3(20, 25, 0), new Vector3(-20, -15, 0) },

        new Vector3[] { new Vector3(0, 5, 0), new Vector3(20, 25, 0), new Vector3(-20, -15, 0) },

        new Vector3[] { new Vector3(-20, 25, 0), new Vector3(20, 25, 0), new Vector3(-20, -15, 0),
                        new Vector3(20, -15, 0) },

        new Vector3[] { new Vector3(0, 5, 0), new Vector3(-20, 25, 0), new Vector3(20, 25, 0), new Vector3(-20, -15, 0),
                        new Vector3(20, -15, 0) },

        new Vector3[] { new Vector3(-20, 25, 0), new Vector3(20, 25, 0), new Vector3(-20, 5, 0),
                        new Vector3(20, 5, 0), new Vector3(-20, -15, 0), new Vector3(20, -15, 0) }
    };

    public void Upgrade()
    {
        if(currentValue < 7)
        {
            switch (currentValue)
            {
                case 1:
                    SetPips(1);
                    break;
                case 2:
                    SetPips(2);
                    break;
                case 3:
                    SetPips(3);
                    break;
                case 4:
                    SetPips(4);
                    break;
                case 5:
                    SetPips(5);
                    break;
                case 6:
                    MaxUpgrade();
                    break;
                default:
                    break;
            }
        }
        else
        {
            Debug.Log("Max Value");
        }
    }

    private void SetPips(int value)
    {
        GameObject newPip = GameObject.Instantiate(pip);

        newPip.transform.SetParent(gameObject.transform);

        pips.Add(newPip);

        Vector3[] pipsValue = pipsPosition[value];

        for(int i = 0; i < pipsValue.Length; i++)
        {
            pips[i].transform.localPosition = pipsValue[i];
            pips[i].transform.localScale = new Vector3(1, 1, 1);
        }

        currentValue++;
    }

    private void MaxUpgrade()
    {
        for(int i = 0; i < pips.Count; i++)
        {
            pips[i].SetActive(false);
        }

        star.SetActive(true);

        currentValue++;
    }

    public void Downgrade()
    {
        if (currentValue > 1)
        {
            switch (currentValue)
            {
                case 6:
                    break;
                case 5:
                    break;
                case 4:
                    break;
                case 3:
                    break;
                case 2:
                    break;
                case 1:
                    break;
                default:
                    break;
            }
        }
        else
        {
            Destroy(pips[^1]);
            pips.Remove(pips[^1]);

            Destroy(gameObject);
        }
    }

    private void RemovePip(int value)
    {
        Destroy(pips[^1]);
        pips.Remove(pips[^1]);

        Vector3[] pipsValue = pipsPosition[value];

        for (int i = 0; i < pipsValue.Length; i++)
        {
            pips[i].transform.localPosition = pipsValue[i];
            pips[i].transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
