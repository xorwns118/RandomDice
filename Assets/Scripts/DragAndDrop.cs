using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 defaultPos;
    public Transform parentPanel;
    private static List<Dice> activeTargets = new(); // ��ĥ �� �ִ� ���
    private static List<Dice> nonTargets = new(); // ��ĥ �� ���� ���

    // �巡�� ����
    public void OnBeginDrag(PointerEventData eventData)
    {
        Dice currentDice = GetComponentInChildren<Dice>();
        defaultPos = transform.position;

        currentDice.transform.parent.SetAsLastSibling();
        currentDice.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
        // ����� ��Ӱ� �ϰ�, ���� �ֻ����� Ȱ��ȭ
        HighlightMergeTargets(currentDice);

        // �巡�� ��, ����ĳ��Ʈ Ÿ�� ���� (�ٸ� UI ��� ���� ����)
        this.GetComponentInChildren<Image>().raycastTarget = false;
    }

    // �巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(eventData.position);
        currentPos.z = 0;
        transform.position = currentPos;
    }

    // �巡�� ����
    public void OnEndDrag(PointerEventData eventData)
    {
        Dice currentDice = GetComponentInChildren<Dice>();
        Dice mergeTarget = GetMergeTarget(currentDice);

        if (mergeTarget != null)
        {
            // ���� �ֻ����� ������
            RandomSpawnManager.instance.MergeDice(mergeTarget, currentDice);
            transform.position = defaultPos;
        }
        else
        {
            // ��ĥ �ֻ��� ���� ��, ���� �ڸ��� ����
            transform.position = defaultPos;
        }

        // ��� ��� ����, ����ĳ��Ʈ �ٽ� Ȱ��ȭ
        ResetMergeTargets();
        this.GetComponentInChildren<Image>().raycastTarget = true;

        currentDice.transform.localScale = new Vector3(1f, 1f, 1f);
    }

    // ���� �̸��� �ֻ����� Ȱ��ȭ, ��� ��Ӱ� ó��
    private void HighlightMergeTargets(Dice currentDice)
    {
        activeTargets.Clear();
        nonTargets.Clear();
        Dice[] allDices = parentPanel.transform.GetComponentsInChildren<Dice>();

        foreach (Dice dice in allDices)
        {
            Image diceImage = dice.GetComponent<Image>();

            if (dice != currentDice && dice.diceName == currentDice.diceName && dice.currentValue == currentDice.currentValue)
            {
                activeTargets.Add(dice);
                diceImage.color = new Color(1f, 1f, 1f, 1f); // ��� ǥ��
                continue;
            }

            nonTargets.Add(dice);

            if(dice == currentDice)
            {
                diceImage.color = new Color(1f, 1f, 1f, 1f);
                continue;
            }

            diceImage.color = new Color(1f, 1f, 1f, 0.5f); // ������ ǥ��
        }
    }

    // ���� �̸��� �ֻ��� �� ���� ����� �� ã��
    private Dice GetMergeTarget(Dice currentDice)
    {
        Dice closest = null;
        float minDistance = float.MaxValue;

        foreach (Dice dice in activeTargets)
        {
            float distance = Vector3.Distance(dice.transform.parent.TransformPoint(dice.transform.localPosition), 
                                              currentDice.transform.parent.TransformPoint(currentDice.transform.localPosition));

            if (distance < minDistance)
            {
                minDistance = distance;
                closest = dice;
            }
        }

        if(90f <= minDistance && minDistance < 90.00005f)
        {
            return closest;
        }
        return null;
    }


    // ��� ���� ������� ����
    private void ResetMergeTargets()
    {
        foreach (Dice dice in nonTargets)
        {
            dice.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);
        }
        nonTargets.Clear();
    }
}