using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    protected float speedCoef = 1;
    [SerializeField] protected float speedSlow = 0.5f;
    [SerializeField] protected float speedNormal = 1f;

    protected Animator personAnim;
    protected Rigidbody personRb;

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float rotationSpeed = 20f;
    // Start is called before the first frame update
    void Awake()
    {
        personAnim = GetComponentInChildren<Animator>();
        personAnim.SetBool("Static_b", true);
        personAnim.SetInteger("DeathType_int", 1);

        personRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        personAnim.SetFloat("Speed_f", 0);
        personAnim.SetBool("Death_b", true);
    }
}
