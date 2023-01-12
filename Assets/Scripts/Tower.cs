using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerTop; // ������ ���� �������.
    [SerializeField] Transform targetEnemy; // ������ �� ���� ��������.
    [SerializeField] float shootRange; //��������� ��������
    [SerializeField] ParticleSystem bulletParticles; // �������� �������� ������.

    public WayPoint baseWayPoint;

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)             // ��������� ��� ���� ��� �� ��� ���������.
        {
            towerTop.LookAt(targetEnemy); // ��������� �� ��������.
            Fire();   
        }
        else
        {
            Shoot(false); // ���� ���� ��������, �� ������������� ��������.
        }
    }

    /// <summary>
    /// ����� ������� ���������� ����� � �����.
    /// </summary>
    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>(); //�������� ��� ������� �� ������� ���� ������ EnemyDamage.
        if (sceneEnemies.Length == 0) { return; } // �������� ���� �� ����� �� �����.
        Transform closestEnemy = sceneEnemies[0].transform; // �������� ������� �����.
        foreach (var test in sceneEnemies) // ���������� ���� ������ � ������� ����������.
        {
            closestEnemy = GetClosesEnemy(closestEnemy.transform, test.transform); // ��������� ���������� �����.
        }

        targetEnemy = closestEnemy; // ���������� ��������� ����� � ������.
    }

    /// <summary>
    /// ����� ������� ���������� �����.
    /// </summary>
    private Transform GetClosesEnemy(Transform enemyA, Transform enemyB)
    {
        var distA = Vector3.Distance(enemyA.position, transform.position); // ������� ������� ����� � �� �����.
        var distB = Vector3.Distance(enemyB.position, transform.position); // ������� ������� ����� � �� �����.

        if (distA < distB) // ���������� ������� ����� � � � � ���������� ���������.
        {
            return enemyA;
        }
        return enemyB;
    }

    private void Fire()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, transform.position); //������� ���������� �� ����� �� ������� �����.
        if (distanceToEnemy <= shootRange) // ���� �� ������� �� �����.
        {
            Shoot(true); // �� ��������.
        }
        else
        {
            Shoot(false); // ���� ���, �� ��������� ��������.
        }
    }

    private void Shoot(bool isActive)
    {
        var emission = bulletParticles.emission; // ��������� ������ Emission ������� ������.
        emission.enabled = isActive; //���������� ��� ��������� ��������.
    }
}
