using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveComponent
{

    private GameObject player;
    
    private EnemyController enemyController;

    [SerializeField] private float gravityCoef = 0.2f;
    [SerializeField] private float animSpeedCoef = 0.1f;
    [SerializeField] private float randomMovementPhase = 2f;
    private float curRandomMovementTimer=0f;
    public Vector3 randDirection;
    void Start()
    {        
        enemyController = GetComponent<EnemyController>();
        

        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (!player.GetComponent<PlayerController>().IsDead && !enemyController.IsDead)
        {
            Vector3 playerDirection = (player.transform.position - transform.position).normalized;
            playerDirection = new Vector3(playerDirection.x, 0,playerDirection.z);
            Quaternion targetRotation = Quaternion.LookRotation(playerDirection);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime* speedCoef);
            personRb.AddForce(playerDirection * speed* speedCoef);
            personAnim.SetFloat("Speed_f", speed * speedCoef* animSpeedCoef);
            personAnim.SetFloat("SpeedCoef_f", speedCoef);
        }
        else if(player.GetComponent<PlayerController>().IsDead)
        {
            RandomMovement();
        }

    }
    void FixedUpdate()
    {
        personRb.AddForce(Physics.gravity * gravityCoef, ForceMode.Acceleration);
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
            personRb.AddForce(randDirection * speed * speedSlow);
            personAnim.SetFloat("Speed_f", speed * speedSlow * animSpeedCoef);
            personAnim.SetFloat("SpeedCoef_f", speedCoef);

        
    }
    
}
