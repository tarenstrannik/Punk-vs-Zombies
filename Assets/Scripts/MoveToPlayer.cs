using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    public float speed=5f;
    public float rotationSpeed = 20f;

    public float speedCoef = 1;
    public float speedSlow = 0.5f;
    public float speedNormal = 1f;
    private Rigidbody enemyRb;
    private GameObject player;
    private Animator enemyAnim;
    private EnemyController enemyController;

    public float gravityCoef = 0.2f;
    public float animSpeedCoef = 0.1f;
    public float randomMovementPhase = 2f;
    private float curRandomMovementTimer=0f;
    public Vector3 randDirection;
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        enemyAnim = GetComponentInChildren<Animator>();
        enemyController = GetComponent<EnemyController>();
        enemyAnim.SetBool("Static_b", true);
        enemyAnim.SetInteger("DeathType_int", 1);

        player = GameObject.Find("Player");


    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerController>().isPlayerDead && !enemyController.isDead)
        {
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(playerDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime* speedCoef);
            enemyRb.AddForce(playerDirection * speed* speedCoef);
            enemyAnim.SetFloat("Speed_f", speed * speedCoef* animSpeedCoef);
            enemyAnim.SetFloat("SpeedCoef_f", speedCoef);
        }
        else if(player.GetComponent<PlayerController>().isPlayerDead)
        {
            RandomMovement();
        }

    }
    void FixedUpdate()
    {
        enemyRb.AddForce(Physics.gravity * gravityCoef, ForceMode.Acceleration);
        }
    void RandomMovement()
    {
        if (curRandomMovementTimer <= 0f)
        {
            curRandomMovementTimer = randomMovementPhase;
            randDirection = new Vector3(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)).normalized;
        }
        else
        {
            curRandomMovementTimer -= Time.deltaTime;

        }
            Quaternion targetRotation = Quaternion.LookRotation(randDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * speedCoef);
            enemyRb.AddForce(randDirection * speed * speedSlow);
            enemyAnim.SetFloat("Speed_f", speed * speedSlow * animSpeedCoef);
            enemyAnim.SetFloat("SpeedCoef_f", speedCoef);

        
    }
    private void SlowDown()
    {
        speedCoef = speedSlow;
    }
    private void SpeedUp()
    {
        speedCoef = speedNormal;
    }

    public void DieAnim()
    {
        enemyAnim.SetFloat("Speed_f", 0);
        enemyAnim.SetBool("Death_b", true);
    }
}
