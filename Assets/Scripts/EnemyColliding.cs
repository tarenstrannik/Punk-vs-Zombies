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
    protected override void Start()
    {
        base.Start();
        enemyController=GetComponent<EnemyController>();
        enemyMove=GetComponent<EnemyMove>();
    }


    
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if(collision.gameObject.CompareTag("Player") && !enemyController.IsDead)
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
