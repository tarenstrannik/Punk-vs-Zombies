using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MoveComponent
{
    //restriction params
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Vector3 topRight;
    [SerializeField] private float shootingTimeout = 2f;
    private Coroutine shootingTimeoutCoroutine;
    private void Start()
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
    private void Shoot()
    {

        personAnim.SetTrigger("Shoot_t");
        personAnim.SetInteger("WeaponType_int", 1);
        if(shootingTimeoutCoroutine!=null)
        {
            StopCoroutine(shootingTimeoutCoroutine);
        }
        shootingTimeoutCoroutine = StartCoroutine(ShootingTimeoutCor());
    }
    IEnumerator  ShootingTimeoutCor()
    {
        yield return new WaitForSeconds(shootingTimeout);
        personAnim.SetInteger("WeaponType_int", 0);
    }
}
