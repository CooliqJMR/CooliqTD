using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] ParticleSystem castleDestroyParticles;
    [SerializeField] float _speed;
    [SerializeField] float moveStep;

    EnemyDamage enemyDamage;
    Pathfinder pathfinder;
    Castle castle;

    Vector3 targetPosition;
    void Start()
    {
        castle = FindObjectOfType<Castle>();
        enemyDamage = GetComponent<EnemyDamage>();
        pathfinder = FindObjectOfType<Pathfinder>();
        var path = pathfinder.GetPath();
        StartCoroutine(EnemyMove(path));

    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, moveStep * Time.deltaTime);
    }

    IEnumerator EnemyMove(List<WayPoint> path) // Создаем корутину которая двигает персонажа по клеткам
    {
        foreach (var waypoint in path)
        {
            transform.LookAt(waypoint.transform);
            targetPosition = waypoint.transform.position;
            yield return new WaitForSeconds(_speed);
        }
        castle.Damage();
        enemyDamage.DieEnemy(castleDestroyParticles, false);
    }
}
