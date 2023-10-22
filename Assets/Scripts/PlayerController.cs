using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudioSource;

    public GameObject bulletPrefab;
    public GameObject shootingPoint;

    //movement params
    public float speed = 10f;
    public float rotationSpeed = 10f;

    private float speedCoef = 1;
    public float slowSpeedCoef = 0.5f;
    public float normalSpeedCoef = 1;
    public float playerHealth = 10f;

    //restriction params
    public Vector3 bottomLeft;
    public Vector3 topRight;

    public bool isPlayerDead = false;

    public AudioClip shootAudio;
    public AudioClip damageAudio;
    public AudioClip healingAudio;
    public AudioClip deathAudio;


    void Start()
    {

        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponentInChildren<Animator>();
        playerAudioSource = GetComponent<AudioSource>();
        playerAnim.SetBool("Static_b", true);
        playerAnim.SetInteger("DeathType_int", 1);
    }

    // Update is called once per frame
    void Update()
    {

        if (!isPlayerDead)
        {
            PlayerShoot();
            PlayerMovement();
            ConstraintPlayer();
        }

        if (playerHealth <= 0 && !isPlayerDead)
        {

            GameOver();
        }


    }
    private void GameOver()
    {
        isPlayerDead = true;
        playerAnim.SetBool("Death_b", true);
        playerAudioSource.PlayOneShot(deathAudio);
    }
    private void SlowDown()
    {
        speedCoef = slowSpeedCoef;
    }
    private void SpeedUp()
    {
        speedCoef = normalSpeedCoef;
    }
    private void ReceiveDamage(float damage)
    {
        if (!isPlayerDead)
        {
            playerHealth -= damage;
            if (damage > 0)
            {
                playerAudioSource.PlayOneShot(damageAudio);
            }
            else
            {
                playerAudioSource.PlayOneShot(healingAudio);
            }
        }
    }
    private void PlayerShoot()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            playerAudioSource.PlayOneShot(shootAudio);
            Instantiate(bulletPrefab, shootingPoint.transform.position, shootingPoint.transform.rotation);
        }
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
        playerRb.velocity = Vector3.forward * speed * speedCoef * verticalInput + Vector3.right * speed * speedCoef * horizontalInput;
        playerAnim.SetFloat("Speed_f", speed * speedCoef * (Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput)));
        playerAnim.SetFloat("SpeedCoef_f", speedCoef);
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
