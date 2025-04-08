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

    // 주사위 눈금 위치 리스트
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

    // 주사위 눈금 위치 세팅
    public void SetPipPosition(int value, List<GameObject> pips)
    {
        Vector3[] pipsValue = pipsPosition[value];

        for (int i = 0; i < pipsValue.Length; i++)
        {
            pips[i].transform.localPosition = pipsValue[i];
            pips[i].transform.localScale = new(1, 1, 1);
        }
    }

    // 소환된 주사위 눈금 세팅
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

    // 눈금 생성
    private void InstantiatePips(Dice dice, int value)
    {
        GameObject newPip = GameObject.Instantiate(dice.pip);

        newPip.transform.SetParent(dice.gameObject.transform);

        dice.pips.Add(newPip);

        SetPipPosition(value, dice.pips);

        dice.currentValue++;
    }

    // 최대 레벨로 업그레이드 될 경우
    private void MaxUpgrade(Dice dice)
    {
        for (int i = 0; i < dice.pips.Count; i++)
        {
            dice.pips[i].SetActive(false);
        }

        dice.star.SetActive(true);
    }

    // 7레벨이 아닐 경우 별 제거
    private void RemoveStar(Dice dice)
    {
        dice.star.SetActive(false);

        for (int i = 0; i < dice.pips.Count; i++)
        {
            dice.pips[i].SetActive(true);
        }

        dice.currentValue--;
    }

    // 눈금 제거
    private void RemovePip(Dice dice, int value)
    {
        StartCoroutine(RemovePipCoroutine(dice, value));
    }

    // 눈금 제거
    private IEnumerator RemovePipCoroutine(Dice dice, int value)
    {
        Destroy(dice.pips[value]);

        // Destroy와 RemoveAt이 동시에 실행되는 상황이 발생했을 때,
        // 오브젝트가 제거되기 전에 리스트에서 삭제를 시도하면 사용중인 오브젝트를 삭제한다는 
        // 경고가 나올 수 있어, 한 프레임을 대기시켜 Destroy가 완료되도록 함
        yield return null;

        dice.pips.RemoveAt(value);

        SetPipPosition(value - 1, dice.pips);

        dice.currentValue--;
    }

    // 주사위 눈금 상승
    public void Upgrade(Dice dice)
    {
        if (dice.currentValue < 7)
        {
            switch (dice.currentValue)
            {
                case 1:
                    InstantiatePips(dice, 1); // 1일 때 -> 0, 1 index 2개
                    break;
                case 2:
                    InstantiatePips(dice, 2); // 2일 때 -> 0, 1, 2 index 3개
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

    // 주사위 눈금 감소
    public void Downgrade(Dice dice)
    {
        switch (dice.currentValue)
        {
            case 7:
                RemoveStar(dice);
                break;
            case 6:
                RemovePip(dice, 5); // 6일 때 -> 0, 1, 2, 3, 4 index 5개
                break;
            case 5:
                RemovePip(dice, 4); // 5일 때 -> 0, 1, 2, 3 index 4개
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
