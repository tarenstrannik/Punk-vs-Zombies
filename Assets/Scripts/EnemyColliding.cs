using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliding : PersonColliding
{
    // Start is called before the first frame update

    private EnemyController enemyController;

 

    public float biteDelay = 1f;
    public float curBitingTimer = 0f;
    protected override void Awake()
    {
        base.Awake();
        enemyController=GetComponent<EnemyController>();

    }


    
    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);
        if(collision.gameObject.CompareTag("Player") && !personController.IsDead)
        {
            Bite(collision.gameObject);
        }
    }
    private void Bite(GameObject biteTarget)
    {
        //Bite player
        biteTarget.SendMessage("ReceiveDamage", enemyController.EnemyBiteDamage);
        personController.SendMessage("ReceiveDamage", -enemyController.EnemyBiteHeal);
        curBitingTimer = biteDelay;
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && !personController.IsDead)
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
