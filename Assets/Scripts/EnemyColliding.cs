using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyColliding : MonoBehaviour
{
    // Start is called before the first frame update

    private EnemyController enemyController;
    private MoveToPlayer enemyMove;
 

    public float biteDelay = 1f;
    public float curBitingTimer = 0f;
    void Start()
    {
        enemyController=GetComponent<EnemyController>();
        enemyMove=GetComponent<MoveToPlayer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            enemyMove.SendMessage("SlowDown");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            enemyMove.SendMessage("SpeedUp");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Bullet") && !enemyController.isDead)
        {
            //ReceiveDamage
            enemyController.SendMessage("ReceiveDamage", collision.gameObject.GetComponent<BulletController>().bulletDamage);
            Destroy(collision.gameObject);
        }
        else if(collision.gameObject.CompareTag("Player") && !enemyController.isDead)
        {
            //Bite player
            collision.gameObject.SendMessage("ReceiveDamage", enemyController.enemyBiteDamage);
            curBitingTimer = biteDelay;
        }
    }
    private void OnCollisionStay(Collision collision)
    {

        if (collision.gameObject.CompareTag("Player") && !enemyController.isDead)
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
