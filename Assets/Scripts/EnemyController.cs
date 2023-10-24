using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : PersonController
{

    public float enemyBiteDamage = 1f;

    public float destroyDelay = 1.5f;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start(); 
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (PersonHealth <= 0 && !IsDead)
        {
            Invoke("DestroyBody", destroyDelay);
        }
        base.Update();
        
    }
    void DestroyBody()
    {
        gameObject.SetActive(false);
       
    }

}
