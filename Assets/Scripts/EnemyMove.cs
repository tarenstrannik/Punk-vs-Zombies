using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MoveComponent
{

    public GameObject Player { get; private set; }
    
    private EnemyController enemyController;

    [SerializeField] private float gravityCoef = 0.2f;
    [SerializeField] private float speedForce = 4f;

    [SerializeField] private float randomMovementPhase = 2f;
    private float curRandomMovementTimer=0f;
    public Vector3 randDirection;

    
    void Start()
    {        
        enemyController = GetComponent<EnemyController>();


        Player = GameObject.Find("Player");

    }

    // Update is called once per frame

    void FixedUpdate()
    {
        if (Time.timeScale > 0f)
        personRb.AddForce(Physics.gravity * gravityCoef, ForceMode.Acceleration);

        if (!Player.GetComponent<PlayerController>().IsDead && !enemyController.IsDead && Time.timeScale > 0f)
        {
            Vector3 playerDirection = (Player.transform.position - transform.position).normalized;
            MovementAtDirection(playerDirection);
        }
        else if (Player.GetComponent<PlayerController>().IsDead)
        {
            RandomMovement();
        }

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
        MovementAtDirection(randDirection);



    }
    private void MovementAtDirection(Vector3 direction)
    {
        direction = new Vector3(direction.x, 0, direction.z);
        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * speedCoef);
        personRb.AddForce(direction * speedForce * speedCoef);

        AnimateMovement();
    }

    protected override void AnimateMovement()
    {
        base.AnimateMovement();
        personAnim.SetFloat("Speed_f", speedForce * speedCoef * animCoef);
        
    }
}
