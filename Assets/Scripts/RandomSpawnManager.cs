using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

public class RandomSpawnManager : MonoBehaviour
{
    public static RandomSpawnManager instance;

    public GameObject[] useableDice;
    public Transform[] myDeckTransform;
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

        for (int i = 0; i < myDeckTransform.Length; i++)
        {
            GameObject.Instantiate(useableDice[i], myDeckTransform[i]);
        }
    }

    // �ֻ����� ������ ��ġ�� ��ȯ
    public void SpawnDiceInRandomPoint()
    {
        int randomDice = Random.Range(0, useableDice.Length);
        int randomSpawnPoint = Random.Range(0, empty.Count);

        if (empty.Count == 0)
        {
            Debug.Log("�ֻ����� ��ȯ�� ������ �����ϴ�.");
            return;
        }

        full.Add(empty[randomSpawnPoint]);
        GameObject.Instantiate(useableDice[randomDice], empty[randomSpawnPoint].transform);
        empty.RemoveAt(randomSpawnPoint);
    }

    // �� �ֻ����� ��ġ�� Ÿ�� ��ġ�� ���ο� �ֻ��� ����
    public void MergeDice(Dice target, Dice currentDice)
    {
        int randomDice = Random.Range(0, useableDice.Length);
        int newValue = currentDice.currentValue + 1;

        Destroy(target.gameObject);
        Destroy(currentDice.gameObject);

        empty.Add(currentDice.transform.parent.gameObject);
        full.Remove(target.transform.parent.gameObject);

        GameObject newDice = Instantiate(useableDice[randomDice], target.transform.parent);
        newDice.GetComponent<Dice>().currentValue = newValue;
        newDice.GetComponent<Dice>().SetNewDicePip();
    }
}
