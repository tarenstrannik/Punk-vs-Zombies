using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class PlayerController : PersonController
{
    

    private AudioSource playerAudioSource;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject shootingPoint;

    

    [SerializeField] private AudioClip shootAudio;
    [SerializeField] private AudioClip damageAudio;
    [SerializeField] private AudioClip healingAudio;
    [SerializeField] private AudioClip deathAudio;

    private PlayerMove playerMove;

    protected override void Start()
    {
        base.Start();
        playerAudioSource = GetComponent<AudioSource>();
        playerMove = GetComponent<PlayerMove>();
    }

    // Update is called once per frame
    protected override void Update()
    {

        if (!IsDead)
        {
            PlayerShoot();
            playerMove.SendMessage("PlayerMovement");
            playerMove.SendMessage("ConstraintPlayer");
        }

        if (PersonHealth <= 0 && !IsDead)
        {
            GameOver();
        }


    }
    private void GameOver()
    {
        playerAudioSource.PlayOneShot(deathAudio);
    }
    private void ReceiveDamage(float damage)
    {
        if (!IsDead)
        {
            PersonHealth -= damage;
            if (damage > 0)
            {
                playerAudioSource.PlayOneShot(damageAudio);
            }
            else
            {
                playerAudioSource.PlayOneShot(healingAudio);
            }
        }
    }
    private void PlayerShoot()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            playerAudioSource.PlayOneShot(shootAudio);
            Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        }
    }
   
}
