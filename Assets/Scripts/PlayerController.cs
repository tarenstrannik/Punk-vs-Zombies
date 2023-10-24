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
    [SerializeField] private Canvas uiDisplayObject;
    private UIDisplay uiDisplay;
    private PlayerMove playerMove;

    private int score = 0;

    private bool isGameOver = false;
    protected override void Awake()
    {
        base.Awake();
        
    }
    protected override void Start()
    {
        base.Start();
        playerAudioSource = GetComponent<AudioSource>();
        playerMove = GetComponent<PlayerMove>();
        ObjectPooler.SharedInstance.PushObjectToPool(bulletPrefab, 0);
        uiDisplay= uiDisplayObject.GetComponent<UIDisplay>();
        uiDisplay.SendMessage("UpdateHealth", (int)PersonHealth);
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!IsDead)
        {
            PlayerShoot();
            playerMove.SendMessage("PlayerMovement");
            playerMove.SendMessage("ConstraintPlayer");
        }

        if (IsDead && !isGameOver)
        {
            GameOver();
        }


    }
    private void GameOver()
    {
        isGameOver = true;
        playerAudioSource.PlayOneShot(deathAudio);
    }
    protected override void ReceiveDamage(float damage)
    {
        float prevHealth = PersonHealth;
        base.ReceiveDamage(damage);
        if (!IsDead)
        {
            if (damage > 0)
            {
                playerAudioSource.PlayOneShot(damageAudio);
            }
            else
            {
                playerAudioSource.PlayOneShot(healingAudio);
            }
            if((int)prevHealth!=(int)PersonHealth)
            uiDisplay.SendMessage("UpdateHealth", (int)PersonHealth);
        }
    }
    private void PlayerShoot()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            playerAudioSource.PlayOneShot(shootAudio);

            GameObject pooledBullet = ObjectPooler.SharedInstance.GetPooledObject(bulletPrefab);
            if (pooledBullet != null)
            {
                pooledBullet.SetActive(true); // activate it

                pooledBullet.transform.position = shootingPoint.transform.position;
                pooledBullet.transform.rotation = shootingPoint.transform.rotation;
            }
           
        }
    }
    private void UpdateScore(int addScore)
    {
        score += addScore;
        uiDisplay.SendMessage("UpdateScore", score);
    }



}
