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
            Bite(collision.gameObject);
        }
    }
    private void Bite(GameObject biteTarget)
    {
        //Bite player
        biteTarget.SendMessage("ReceiveDamage", enemyController.EnemyBiteDamage);
        enemyController.SendMessage("ReceiveDamage", -enemyController.EnemyBiteHeal);
        curBitingTimer = biteDelay;
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && !enemyController.IsDead)
        {
            curBitingTimer -= Time.deltaTime;
            if (curBitingTimer <= 0)
            {
                //Bite player
                Bite(collision.gameObject);
            }

        }
    }


}
