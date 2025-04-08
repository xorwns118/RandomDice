using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RandomSpawnManager : MonoBehaviour
{
    public static RandomSpawnManager instance;

    public GameObject[] useableDice;
    public GameObject[] diceSpawnPoints;
    public List<GameObject> empty;
    public List<GameObject> full;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        full = new List<GameObject>();
    }

    // 주사위를 랜덤한 위치에 소환
    public void SpawnDiceInRandomPoint()
    {
        int randomDice = Random.Range(0, useableDice.Length);
        int randomSpawnPoint = Random.Range(0, empty.Count);

        if (empty.Count == 0)
        {
            return;
        }

        full.Add(empty[randomSpawnPoint]);
        GameObject newDice = GameObject.Instantiate(useableDice[randomDice], empty[randomSpawnPoint].transform);
        Dice dice = newDice.GetComponent<Dice>();
        StartCoroutine(dice.SpawnDiceCoroutine());
        empty.RemoveAt(randomSpawnPoint);
    }

    // 두 주사위를 합치고 타겟 위치에 새로운 주사위 스폰
    public void MergeDice(Dice target, Dice currentDice)
    {
        int randomDice = Random.Range(0, useableDice.Length);
        int newValue = currentDice.currentValue + 1;

        Destroy(target.gameObject);
        Destroy(currentDice.gameObject);

        empty.Add(currentDice.transform.parent.gameObject);
        full.Remove(target.transform.parent.gameObject);

        GameObject newDice = Instantiate(useableDice[randomDice], target.transform.parent);
        Dice dice = newDice.GetComponent<Dice>();
        dice.currentValue = newValue;
        DiceUpgradeManager.instance.SetNewDicePip(dice);
        StartCoroutine(dice.SpawnDiceCoroutine());
    }
}
