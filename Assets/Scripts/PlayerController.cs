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
        uiDisplay.SendMessage("UpdateHealth", Mathf.Ceil(PersonHealth));
        MainManager.Instance.playerController = this;
        GameManager.Instance.playerController = this;
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

        


    }
    private void resetHealthToValue(float value)
    {
        PersonHealth = value;
        uiDisplay.SendMessage("UpdateHealth", Mathf.Ceil(PersonHealth));
    }

    public override void Revive()
    {
        base.Revive();
        uiDisplay.SendMessage("UpdateHealth", Mathf.Ceil(PersonHealth));

    }
    protected override void ReceiveDamage(float damage)
    {
        float prevHealth = PersonHealth;
        base.ReceiveDamage(damage);
        if (Mathf.Ceil(prevHealth) != Mathf.Ceil(PersonHealth))
            uiDisplay.SendMessage("UpdateHealth", Mathf.Ceil(PersonHealth));
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
            
        }
        else if (IsDead && MainManager.Instance.isGameActive)
        {
            playerAudioSource.PlayOneShot(deathAudio);
            GameManager.Instance.GameOver();
        }
    }
    private void PlayerShoot()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            
            playerMove.SendMessage("Shoot");

        }
    }
    private void ShootBullet()
    {
        playerAudioSource.PlayOneShot(shootAudio);

        GameObject pooledBullet = ObjectPooler.SharedInstance.GetPooledObject(bulletPrefab);
        if (pooledBullet != null)
        {
            pooledBullet.SetActive(true); // activate it

            pooledBullet.transform.position = shootingPoint.transform.position;
            
            pooledBullet.transform.rotation = Quaternion.Euler(0f, shootingPoint.transform.rotation.eulerAngles.y, shootingPoint.transform.rotation.eulerAngles.z); 
        }
    }




}
