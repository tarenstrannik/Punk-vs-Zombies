using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : PersonController
{

    [SerializeField] private float enemyBiteDamage = 1f;
    public float EnemyBiteDamage { 
        get
        {
            return enemyBiteDamage;
        } 
        private set
        {
            enemyBiteDamage = value;
        }
    }
    [SerializeField] private float enemyBiteHeal = 1f;
    public float EnemyBiteHeal
    {
        get
        {
            return enemyBiteHeal;
        }
        private set
        {
            enemyBiteHeal = value;
        }
    }

    [SerializeField] private int scoresForEnemy = 1;
    
    [SerializeField] private float destroyDelay = 1.5f;

    private PlayerController playerController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerController = GetComponent<EnemyMove>().Player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (PersonHealth <= 0 && !IsDead)
        {
            playerController.SendMessage("UpdateScore", scoresForEnemy);
            Invoke("DestroyBody", destroyDelay);
        }
        base.Update();
        
    }
    void DestroyBody()
    {
        gameObject.SetActive(false);
       
    }

}
