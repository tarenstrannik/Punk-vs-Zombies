using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliding : PersonColliding
{
    private PlayerController playerController;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        playerController=GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            playerController.SendMessage("ReceiveDamage", -other.gameObject.GetComponent<PowerupController>().powerupStrength);
            //player recieved powerup
        }
       

    }


}
