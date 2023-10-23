using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MoveComponent
{
    //restriction params
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Vector3 topRight;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        float rotationInput = Input.GetAxis("Rotation");

        Vector3 globalForward = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * Vector3.forward;
        Vector3 globalRight = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * Vector3.right;

        //transform.Translate(globalForward * speed * speedCoef* Time.deltaTime * verticalInput);
        //transform.Translate(globalRight * speed * speedCoef * Time.deltaTime * horizontalInput);
        // playerRb.AddForce(Vector3.forward * speed * speedCoef * verticalInput);
        //playerRb.AddForce(Vector3.right * speed * speedCoef * horizontalInput);
        personRb.velocity = Vector3.forward * speed * speedCoef * verticalInput + Vector3.right * speed * speedCoef * horizontalInput;
        personAnim.SetFloat("Speed_f", speed * speedCoef * (Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput)));
        personAnim.SetFloat("SpeedCoef_f", speedCoef);
        transform.Rotate(Vector3.up, rotationSpeed * speedCoef * Time.deltaTime * rotationInput);

    }
    private void ConstraintPlayer()
    {
        if (transform.position.x < bottomLeft.x)
        {
            transform.position = new Vector3(bottomLeft.x, transform.position.y, transform.position.z);

        }
        else if (transform.position.x > topRight.x)
        {
            transform.position = new Vector3(topRight.x, transform.position.y, transform.position.z);

        }
        else if (transform.position.z < bottomLeft.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, bottomLeft.z);

        }
        else if (transform.position.z > topRight.z)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, topRight.z);

        }
    }
}
