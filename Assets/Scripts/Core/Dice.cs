using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class Dice : MonoBehaviour
{
    [Header("Current Dice Info")]
    public GameObject pip; // 1 ~ 6
    public GameObject star; // 7
    public List<GameObject> pips; // 1 ~ 7
    public Monster targetMonster;
    
    [Header("Dice Info")]
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

    public RectTransform monsterField;

    bool isAttack = false;
    bool isCoroutinePlaying = false;

    private void Update()
    {
        if (MonsterSpawnManager_GameMode2.instance.spawnMonsterList.Count != 0)
        {
            targetMonster = MonsterSpawnManager_GameMode2.instance.spawnMonsterList[0];
        }
        else
        {
            targetMonster = null;
            isAttack = false;
        }

        if (targetMonster != null && !isAttack && !isCoroutinePlaying)
        {
            isAttack = true;
            isCoroutinePlaying = true;
            StartCoroutine(DiceAttackCoroutine());
        }
    }

    public IEnumerator SpawnDiceCoroutine()
    {
        float t = 0f;
        float duration = 0.1f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        transform.localScale = Vector3.one;
    }

    // ���� �ڷ�ƾ
    public IEnumerator DiceAttackCoroutine()
    {
        while(isAttack)
        {
            if(targetMonster == null)
            {
                isAttack = false;
                isCoroutinePlaying = false;
                yield break;
            }

            StartCoroutine(MovePipCoroutine(targetMonster));

            // ���� �ӵ���ŭ ���
            yield return new WaitForSeconds(attackSpeed);
        }

        isAttack = false;
        isCoroutinePlaying = false;
    }

    // ���͸� �ǰ��ϴ� pip �̵� �ڷ�ƾ
    public IEnumerator MovePipCoroutine(Monster targetMonster)
    {
        GameObject newPip = GameObject.Instantiate(pip, this.transform);
        Vector3 startPos = newPip.transform.position;

        float speed = 10f; // �̵� �ӵ� (�������� ����)
        float distance = Vector3.Distance(startPos, targetMonster.transform.position);
        float duration = distance / speed; // �Ÿ� ����� ���� �ð�

        float t = 0f;

        while (t < 1f)
        {
            if(targetMonster == null)
            {
                // Ÿ�� ���Ͱ� ������� ������Ʈ �ı� �� �ڷ�ƾ ����
                Destroy(newPip);
                yield break;
            }

            t += Time.deltaTime / duration;
            newPip.transform.position = Vector3.Lerp(startPos, targetMonster.transform.position, t);

            yield return null;
        }

        if (targetMonster != null)
        {
            targetMonster.TakeDamage(attackDamage);
        }

        Destroy(newPip);
    }
}