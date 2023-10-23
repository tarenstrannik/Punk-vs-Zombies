using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : PersonController
{

    public float enemyBiteDamage = 1f;

    private EnemyMove moveToPlayer;

    public float destroyDelay = 1.5f;
    // Start is called before the first frame update
    protected override void  Start()
    {
        base.Start();
        moveToPlayer=GetComponent<EnemyMove>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(PersonHealth<=0)
        {
            IsDead = true;
            moveToPlayer.DieAnim();
            Invoke("DestroyBody", destroyDelay);
        }
    }
    void DestroyBody()
    {
        Destroy(gameObject);
    }

}
