using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public string monsterName;
    public float moveSpeed;
    public int hp;

    private RectTransform rectTransform;
    private bool isMovingY = true;
    private bool isMovingX = false;

    private float targetY = 665f;
    private float targetX = 860f;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        StartCoroutine(SpawnMonsterCoroutine());
    }

    private void Update()
    {
        if(hp <= 0)
        {
            Destroy(this.gameObject);
        }

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
                InGameManager.instance.OnGameOver();
                Destroy(this.gameObject);
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
}
