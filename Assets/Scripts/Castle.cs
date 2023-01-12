using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Castle : MonoBehaviour
{
    [SerializeField] int playerLife = 10;
    [SerializeField] int damageCount = 1;
    [SerializeField] Text textLife;
    [SerializeField] AudioClip damageCastleAudio;

    AudioSource audioSource;

    private void Start()
    {
        textLife.text = playerLife.ToString();
        audioSource = GetComponent<AudioSource>();
    }
    public void Damage()
    {
        audioSource.PlayOneShot(damageCastleAudio);
        playerLife -= damageCount;
        textLife.text = playerLife.ToString();
    }
}
