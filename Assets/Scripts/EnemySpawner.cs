using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float spawnInterval; // ����� �������� ������.
    [SerializeField] EnemyMovement enemyPrefab; // ������ �����. ��������� ������ ��� ������ �� ��� ����� ������ EnemyMovement.
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
