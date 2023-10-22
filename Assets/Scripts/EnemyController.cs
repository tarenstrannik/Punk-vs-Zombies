using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float enemyHealth = 3f;

    public float enemyBiteDamage = 1f;

    private MoveToPlayer moveToPlayer;
    public bool isDead=false;
    public float destroyDelay = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        moveToPlayer=GetComponent<MoveToPlayer>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyHealth<=0)
        {
            isDead = true;
            moveToPlayer.DieAnim();
            Invoke("DestroyBody", destroyDelay);
        }
    }
    void DestroyBody()
    {
        Destroy(gameObject);
    }
    private void ReceiveDamage(float damage)
    {
        enemyHealth -= damage;
    }
}
