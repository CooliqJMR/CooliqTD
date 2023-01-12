using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] int hitPoints = 10;
    [SerializeField] ParticleSystem hitParticles;
    [SerializeField] ParticleSystem destroyParticles;
    [SerializeField] AudioClip damageAudioFX;
    [SerializeField] AudioClip deathAudioFX;
    Text scoreText;
    private int currentScore;

    AudioSource audioSorce;



    private void Start()
    {
        scoreText = GameObject.Find("Score").GetComponent<Text>();
        audioSorce = GetComponent<AudioSource>();
    }
    private void OnParticleCollision(GameObject other)
    {
        Hit();
        if (hitPoints <= 0)
        {
            DieEnemy(destroyParticles, true);
        }
    }

    public void DieEnemy(ParticleSystem fx, bool addScore)
    {
        if (addScore)
        {
            currentScore = int.Parse(scoreText.text);
            currentScore++;
            scoreText.text = currentScore.ToString();
        }

        var destroyFX = Instantiate(fx, transform.position, Quaternion.identity);
        AudioSource.PlayClipAtPoint(deathAudioFX, Camera.main.transform.position);
        destroyFX.Play();
        Destroy(gameObject);
    }

    private void Hit()
    {
        audioSorce.PlayOneShot(damageAudioFX);
        hitParticles.Play();
        hitPoints = hitPoints - 1;
    }
}
