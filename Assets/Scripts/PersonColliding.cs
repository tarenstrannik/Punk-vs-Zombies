using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonColliding : MonoBehaviour
{
    private MoveComponent moveComponent;
    // Start is called before the first frame update
    void Start()
    {
        moveComponent=GetComponent<MoveComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            moveComponent.SendMessage("SlowDown");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            moveComponent.SendMessage("SpeedUp");
        }
    }
}
