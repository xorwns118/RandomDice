using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public float moveSpeed;
    public int hp;
    public int cost;

    private RectTransform rectTransform;
    public bool isMovingY = true;
    public bool isMovingX = false;

    private const float targetY = 665f;
    private const float targetX = 860f;

    public bool isDead = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        StartCoroutine(SpawnMonsterCoroutine());
    }

    private void Update()
    {
        if (isMovingY) // X ��ǥ ���� �� ������
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.y += moveSpeed * Time.deltaTime;

            if (pos.y >= targetY)
            {
                pos.y = targetY;
                isMovingY = false;
                isMovingX = true;
            }

            rectTransform.anchoredPosition = pos;
        }
        else if (isMovingX) // Y ��ǥ ���� �� ������
        {
            Vector2 pos = rectTransform.anchoredPosition;
            pos.x += moveSpeed * Time.deltaTime;

            if (pos.x >= targetX)
            {
                /*�׽�Ʈ*/
                Destroy(this.gameObject);
                MonsterSpawnManager_GameMode2.instance.spawnMonsterList.Remove(this);
                /*�׽�Ʈ*/

                InGameManager.instance.OnGameOver();
            }

            rectTransform.anchoredPosition = pos;
        }
    }

    private IEnumerator SpawnMonsterCoroutine()
    {
        float t = 0f;
        float duration = 0.3f; // ���Ͱ� Ŀ���� �� �ɸ��� �� �ð� (��)

        while (t < 1f)
        {
            t += Time.deltaTime / duration;
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
            yield return null;
        }

        transform.localScale = Vector3.one;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;

        if (hp <= 0 && !isDead)
        {
            InGameManager.instance.DestroyedMonster(cost);
            MonsterSpawnManager_GameMode2.instance.spawnMonsterList.Remove(this);
            Destroy(gameObject);
            isDead = true;
        }
    }
}
