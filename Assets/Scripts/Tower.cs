using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform towerTop; // Откуда идет выстрел.
    [SerializeField] Transform targetEnemy; // Таргет по кому стреляем.
    [SerializeField] float shootRange; //Дальность стрельбы
    [SerializeField] ParticleSystem bulletParticles; // Стреляем системой частиц.

    public WayPoint baseWayPoint;

    void Update()
    {
        SetTargetEnemy();
        if (targetEnemy)             // Проверяем жив враг или он уже уничтожен.
        {
            towerTop.LookAt(targetEnemy); // Наблюдаем за таргетом.
            Fire();   
        }
        else
        {
            Shoot(false); // Если враг уничтоже, то останавливаем стрельбу.
        }
    }

    /// <summary>
    /// Метод находит ближайшего врага к башне.
    /// </summary>
    private void SetTargetEnemy()
    {
        var sceneEnemies = FindObjectsOfType<EnemyDamage>(); //Получаем все объекты на которых есть скрипт EnemyDamage.
        if (sceneEnemies.Length == 0) { return; } // Проверка есть ли враги на сцене.
        Transform closestEnemy = sceneEnemies[0].transform; // Выбираем первого врага.
        foreach (var test in sceneEnemies) // Сравниваем всех врагов и находим ближайшего.
        {
            closestEnemy = GetClosesEnemy(closestEnemy.transform, test.transform); // Назначаем ближайшего врага.
        }

        targetEnemy = closestEnemy; // Записываем найденого врага в таргет.
    }

    /// <summary>
    /// Метод находит ближайшего врага.
    /// </summary>
    private Transform GetClosesEnemy(Transform enemyA, Transform enemyB)
    {
        var distA = Vector3.Distance(enemyA.position, transform.position); // Находим позицию врага А до башни.
        var distB = Vector3.Distance(enemyB.position, transform.position); // Находим позицию врага Б до башни.

        if (distA < distB) // Сравниваем позиции врага А и Б и возвращаем ближайшую.
        {
            return enemyA;
        }
        return enemyB;
    }

    private void Fire()
    {
        float distanceToEnemy = Vector3.Distance(targetEnemy.position, transform.position); //Находим расстояние от врага до позиции башни.
        if (distanceToEnemy <= shootRange) // Если мы достаем до врага.
        {
            Shoot(true); // То стреляем.
        }
        else
        {
            Shoot(false); // Если нет, то выключаем стрельбу.
        }
    }

    private void Shoot(bool isActive)
    {
        var emission = bulletParticles.emission; // Объявляем модуль Emission системы частиц.
        emission.enabled = isActive; //Выключение или включение стрельбы.
    }
}
