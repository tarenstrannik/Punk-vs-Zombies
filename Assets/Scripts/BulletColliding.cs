using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletColliding : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Vector3 collisionNormal = contact.normal;
        Quaternion normalRotation = Quaternion.LookRotation(collisionNormal, Vector3.up);
        float newYAngle =  180 - 2*transform.rotation.y+ 2 * normalRotation.eulerAngles.y;

        transform.Rotate(Vector3.up, newYAngle);
    }

}
