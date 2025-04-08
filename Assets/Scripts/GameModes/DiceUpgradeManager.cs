using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class DiceUpgradeManager : MonoBehaviour
{
    public static DiceUpgradeManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // �ֻ��� ���� ��ġ ����Ʈ
    public static readonly Vector3[][] pipsPosition =
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

    // �ֻ��� ���� ��ġ ����
    public void SetPipPosition(int value, List<GameObject> pips)
    {
        Vector3[] pipsValue = pipsPosition[value];

        for (int i = 0; i < pipsValue.Length; i++)
        {
            pips[i].transform.localPosition = pipsValue[i];
            pips[i].transform.localScale = new(1, 1, 1);
        }
    }

    // ��ȯ�� �ֻ��� ���� ����
    public void SetNewDicePip(Dice dice)
    {
        if (dice.currentValue == 7)
        {
            MaxUpgrade(dice);
            return;
        }

        for (int i = 0; i < dice.currentValue - 1; i++)
        {
            GameObject newPip = GameObject.Instantiate(dice.pip);
            newPip.transform.SetParent(dice.gameObject.transform);
            dice.pips.Add(newPip);
        }

        SetPipPosition(dice.currentValue - 1, dice.pips);
    }

    // ���� ����
    private void InstantiatePips(Dice dice, int value)
    {
        GameObject newPip = GameObject.Instantiate(dice.pip);

        newPip.transform.SetParent(dice.gameObject.transform);

        dice.pips.Add(newPip);

        SetPipPosition(value, dice.pips);

        dice.currentValue++;
    }

    // �ִ� ������ ���׷��̵� �� ���
    private void MaxUpgrade(Dice dice)
    {
        for (int i = 0; i < dice.pips.Count; i++)
        {
            dice.pips[i].SetActive(false);
        }

        dice.star.SetActive(true);
    }

    // 7������ �ƴ� ��� �� ����
    private void RemoveStar(Dice dice)
    {
        dice.star.SetActive(false);

        for (int i = 0; i < dice.pips.Count; i++)
        {
            dice.pips[i].SetActive(true);
        }

        dice.currentValue--;
    }

    // ���� ����
    private void RemovePip(Dice dice, int value)
    {
        StartCoroutine(RemovePipCoroutine(dice, value));
    }

    // ���� ����
    private IEnumerator RemovePipCoroutine(Dice dice, int value)
    {
        Destroy(dice.pips[value]);

        // Destroy�� RemoveAt�� ���ÿ� ����Ǵ� ��Ȳ�� �߻����� ��,
        // ������Ʈ�� ���ŵǱ� ���� ����Ʈ���� ������ �õ��ϸ� ������� ������Ʈ�� �����Ѵٴ� 
        // ��� ���� �� �־�, �� �������� ������ Destroy�� �Ϸ�ǵ��� ��
        yield return null;

        dice.pips.RemoveAt(value);

        SetPipPosition(value - 1, dice.pips);

        dice.currentValue--;
    }

    // �ֻ��� ���� ���
    public void Upgrade(Dice dice)
    {
        if (dice.currentValue < 7)
        {
            switch (dice.currentValue)
            {
                case 1:
                    InstantiatePips(dice, 1); // 1�� �� -> 0, 1 index 2��
                    break;
                case 2:
                    InstantiatePips(dice, 2); // 2�� �� -> 0, 1, 2 index 3��
                    break;
                case 3:
                    InstantiatePips(dice, 3);
                    break;
                case 4:
                    InstantiatePips(dice, 4);
                    break;
                case 5:
                    InstantiatePips(dice, 5);
                    break;
                case 6:
                    MaxUpgrade(dice);
                    break;
                default:
                    break;
            }
        }
    }

    // �ֻ��� ���� ����
    public void Downgrade(Dice dice)
    {
        switch (dice.currentValue)
        {
            case 7:
                RemoveStar(dice);
                break;
            case 6:
                RemovePip(dice, 5); // 6�� �� -> 0, 1, 2, 3, 4 index 5��
                break;
            case 5:
                RemovePip(dice, 4); // 5�� �� -> 0, 1, 2, 3 index 4��
                break;
            case 4:
                RemovePip(dice, 3);
                break;
            case 3:
                RemovePip(dice, 2);
                break;
            case 2:
                RemovePip(dice, 1);
                break;
            case 1:
                Destroy(dice.gameObject);
                Debug.Log($"destroyed {dice.diceName}");
                break;
            default:
                break;
        }
    }
}
