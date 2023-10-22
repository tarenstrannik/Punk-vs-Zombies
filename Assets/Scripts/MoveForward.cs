using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody bulletRb;
    public float speed=50f;
    private float rangeX = 20f;
    private float rangeZ = 20f;
    // Start is called before the first frame update
    void Start()
    {
        bulletRb=GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //bulletRb.AddForce(Vector3.forward * speed * Time.deltaTime);
        if (transform.position.x<-rangeX || transform.position.x > rangeX || transform.position.z < -rangeZ || transform.position.z > rangeZ)
        {
            Destroy(gameObject);
        }
    }
}
