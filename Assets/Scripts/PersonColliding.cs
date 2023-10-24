using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonColliding : MonoBehaviour
{
    protected MoveComponent moveComponent;
    protected PersonController personController;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        moveComponent=GetComponent<MoveComponent>();
        personController = GetComponent<PersonController>();
        //Debug.Log(personController);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            moveComponent.SendMessage("SlowDown");
        }
    }
    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            moveComponent.SendMessage("SpeedUp");
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet") && !personController.IsDead)
        {
            //ReceiveDamage
            personController.SendMessage("ReceiveDamage", collision.gameObject.GetComponent<BulletController>().bulletDamage);
            collision.gameObject.SetActive(false);
        }
    }
}
