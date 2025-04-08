using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        if (MonsterSpawnManager_GameMode2.instance.spawnMonsterList != null)
        {
            targetMonster = MonsterSpawnManager_GameMode2.instance.spawnMonsterList[0];
        }
        else
        {
            targetMonster = null;
            isAttack = false;
        }

        if (!isAttack && !isCoroutinePlaying)
        {
            Debug.Log($"{name} 공격 시작");
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

    public IEnumerator DiceAttackCoroutine()
    {
        while(isAttack)
        {
            Debug.Log($"{name} 공격 실행");
            StartCoroutine(PipCoroutine(targetMonster));

            yield return new WaitForSeconds(5f);
        }

        isCoroutinePlaying = false;
    }

    public IEnumerator PipCoroutine(Monster targetMonster)
    {
        Vector3 targetPos = targetMonster.transform.position;
        Transform monsterField = targetMonster.transform.parent;

        GameObject newPip = GameObject.Instantiate(pip, this.transform);
        newPip.transform.SetParent(monsterField);
        newPip.transform.localScale = Vector3.one;

        Vector3 startPos = newPip.transform.position;

        float speed = 20f; // 이동 속도 (유닛/초)
        float distance = Vector3.Distance(startPos, targetPos);
        float duration = distance / speed; // 거리 비례한 도달 시간

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            newPip.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        newPip.transform.position = targetPos;
        targetMonster.TakeDamage(attackDamage);
        Destroy(newPip);
    }
}
