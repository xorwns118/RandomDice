using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour
{
    public string diceName; // �̸�
    public int currentValue; // ���� ���� ��
    public int currentUpgradeLevel; // ���� ���׷��̵� ����
    public int maxUpgradeLevel; // �ִ� ���׷��̵� ����
    public int upgradeCost; // ���׷��̵� ���
    public int attackRange; // ���� ����
    public int attackTarget; // ���� ��� ��
    public int attackDamage; // ���� ������
    public float attckSpeed; // ���� �ӵ�
    public string description; // ����
}
