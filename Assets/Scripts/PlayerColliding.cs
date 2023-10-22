using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliding : MonoBehaviour
{
    private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController=GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            playerController.SendMessage("ReceiveDamage", -other.gameObject.GetComponent<PowerupController>().powerupStrength);
            //player recieved powerup
        }
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            playerController.SendMessage("SlowDown");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            playerController.SendMessage("SpeedUp");
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            //ReceiveDamage
            playerController.SendMessage("ReceiveDamage", collision.gameObject.GetComponent<BulletController>().bulletDamage);
            Destroy(collision.gameObject);
        }

    }

}
