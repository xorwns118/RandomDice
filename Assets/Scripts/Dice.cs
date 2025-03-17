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

    // �ֻ��� ���� ��ġ
    readonly Vector3[][] pipsPosition =
    {
        new Vector3[] { new(0, 5, 0) },

        new Vector3[] { new(20, 25, 0), new(-20, -15, 0) },

        new Vector3[] { new(0, 5, 0), new(20, 25, 0), new(-20, -15, 0) },

        new Vector3[] { new(-20, 25, 0), new(20, 25, 0), new(-20, -15, 0),
                        new(20, -15, 0) },

        new Vector3[] { new(0, 5, 0), new(-20, 25, 0), new(20, 25, 0), new(-20, -15, 0),
                        new(20, -15, 0) },

        new Vector3[] { new(-20, 25, 0), new(20, 25, 0), new(-20, 5, 0),
                        new(20, 5, 0), new(-20, -15, 0), new(20, -15, 0) }
    };

    // �ֻ��� ���� ���
    public void Upgrade()
    {
        if(currentValue < 7)
        {
            switch (currentValue)
            {
                case 1:
                    SetPips(1); // 1�� �� -> 0, 1 index 2��
                    break;
                case 2:
                    SetPips(2); // 2�� �� -> 0, 1, 2 index 3��
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

    // ���� ����
    private void SetPips(int value)
    {
        GameObject newPip = GameObject.Instantiate(pip);

        newPip.transform.SetParent(gameObject.transform);

        pips.Add(newPip);

        SetPosition(value);

        currentValue++;
    }

    // �ִ� ������ ���׷��̵� �� ���
    private void MaxUpgrade()
    {
        for(int i = 0; i < pips.Count; i++)
        {
            pips[i].SetActive(false);
        }

        star.SetActive(true);

        currentValue++;
    }

    // �ֻ��� ���� ����
    public void Downgrade()
    {
        switch (currentValue)
        {
            case 7:
                RemoveStar();
                break;
            case 6:
                RemovePip(5); // 6�� �� -> 0, 1, 2, 3, 4 index 5��
                break;
            case 5:
                RemovePip(4); // 5�� �� -> 0, 1, 2, 3 index 4��
                break;
            case 4:
                RemovePip(3);
                break;
            case 3:
                RemovePip(2);
                break;
            case 2:
                RemovePip(1);
                break;
            case 1:
                Destroy(this.gameObject);
                Debug.Log($"destroyed {this.diceName}");
                break;
            default:
                break;
        }
    }

    private void RemoveStar()
    {
        star.SetActive(false);

        for(int i = 0; i < pips.Count; i++)
        {
            pips[i].SetActive(true);
        }

        currentValue--;
    }

    // ���� ����
    private IEnumerator RemovePipCoroutine(int value)
    {
        Destroy(pips[value]);

        // Destroy�� RemoveAt�� ���ÿ� ����Ǵ� ��Ȳ�� �߻����� ��,
        // ������Ʈ�� ���ŵǱ� ���� ����Ʈ���� ������ �õ��ϸ� ������� ������Ʈ�� �����Ѵٴ� 
        // ��� ���� �� �־�, �� �������� ������ Destroy�� �Ϸ�ǵ��� ��
        yield return null; 

        pips.RemoveAt(value);

        SetPosition(value - 1);

        currentValue--;
    }

    private void RemovePip(int value)
    {
        StartCoroutine(RemovePipCoroutine(value));
    }

    private void SetPosition(int value)
    {
        Vector3[] pipsValue = pipsPosition[value];

        for (int i = 0; i < pipsValue.Length; i++)
        {
            pips[i].transform.localPosition = pipsValue[i];
            pips[i].transform.localScale = new(1, 1, 1);
        }
    }
}
