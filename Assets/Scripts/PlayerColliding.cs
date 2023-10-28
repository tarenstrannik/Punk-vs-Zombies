using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerColliding : PersonColliding
{
    private PlayerController playerController;
    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        //playerController=GetComponent<PlayerController>();
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
            other.gameObject.SetActive(false);
            personController.SendMessage("ReceiveDamage", -other.gameObject.GetComponent<PowerupController>().powerupStrength);
            GameManager.Instance.CheckPowerups();
            //player recieved powerup
        }
       

    }


}
