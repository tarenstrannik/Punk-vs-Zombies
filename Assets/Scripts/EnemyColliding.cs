using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliding : PersonColliding
{
    // Start is called before the first frame update

    private EnemyController enemyController;
    private EnemyMove enemyMove;
 

    public float biteDelay = 1f;
    public float curBitingTimer = 0f;
    void Start()
    {
        enemyController=GetComponent<EnemyController>();
        enemyMove=GetComponent<EnemyMove>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && !enemyController.IsDead)
        {
            //ReceiveDamage
            enemyController.SendMessage("ReceiveDamage", collision.gameObject.GetComponent<BulletController>().bulletDamage);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Player") && !enemyController.IsDead)
        {
            //Bite player
            collision.gameObject.SendMessage("ReceiveDamage", enemyController.enemyBiteDamage);
            curBitingTimer = biteDelay;
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && !enemyController.IsDead)
        {
            curBitingTimer -= Time.deltaTime;
            if (curBitingTimer <= 0)
            { 
            //Bite player
                collision.gameObject.SendMessage("ReceiveDamage", enemyController.enemyBiteDamage);
                curBitingTimer = biteDelay;
            }

        }
    }


}
