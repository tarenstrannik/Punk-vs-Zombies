using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMove : MoveComponent
{
    //restriction params
    [SerializeField] private Vector3 bottomLeft;
    [SerializeField] private Vector3 topRight;
    [SerializeField] private float shootingTimeout = 2f;
    private Coroutine shootingTimeoutCoroutine;

    private float horizontalInput;
    private float verticalInput;
    private Vector3 m_curDirection = new Vector3(0,0,1);
    private float m_rotationSpeedCoef = 1;
    private void Start()
    {
        
        
    }

    private void PlayerMovement(Vector2 value)
    {
        horizontalInput = value.x;
        verticalInput = value.y;      
    }
    private void PlayerRotation(Vector2 value)
    {
        
        m_rotationSpeedCoef = value.magnitude;
        if (m_rotationSpeedCoef!=0)
            m_curDirection = new Vector3(value.normalized.x,0, value.normalized.y);
        
    }

    private void FixedUpdate()
    {
        //Vector3 globalForward = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * Vector3.forward;
        //Vector3 globalRight = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * Vector3.right;

        //transform.Translate(globalForward * speed * speedCoef* Time.deltaTime * verticalInput);
        //transform.Translate(globalRight * speed * speedCoef * Time.deltaTime * horizontalInput);
        // playerRb.AddForce(Vector3.forward * speed * speedCoef * verticalInput);
        //playerRb.AddForce(Vector3.right * speed * speedCoef * horizontalInput);

        personRb.velocity = Vector3.forward * speed * speedCoef * verticalInput + Vector3.right * speed * speedCoef * horizontalInput;

        //transform.Rotate(Vector3.up, rotationSpeed * speedCoef * Time.deltaTime * rotationInput);
        Quaternion targetRotation = Quaternion.LookRotation(m_curDirection, Vector3.up);
        

        // Интерполируем текущий поворот к целевому повороту с постоянной скоростью
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        ConstraintPlayer();
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
        if (transform.position.z < bottomLeft.z)
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
