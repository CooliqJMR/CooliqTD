using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval; // Время создания врагов.
    [SerializeField] EnemyMovement enemyPrefab; // Префаб врага. Добавляем только тех врагов на ком висит скрипт EnemyMovement.
    [SerializeField] AudioClip enemySpawnSoundFX;
    void Start()
    {
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            GetComponent<AudioSource>().PlayOneShot(enemySpawnSoundFX);
            var newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            newEnemy.transform.parent = transform;
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
