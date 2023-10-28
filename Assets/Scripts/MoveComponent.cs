using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComponent : MonoBehaviour
{
    protected float speedCoef = 1;
    [SerializeField] protected float speedSlowCoef = 0.5f;
    [SerializeField] protected float speedNormalCoef = 1f;

    [SerializeField] protected float velocityConst = 0.5f;

    protected float animCoef = 1;
    [SerializeField] protected float animNormalSpeedCoef = 0.1f;
    [SerializeField] protected float animSlowSpeedCoef = 0.1f;
    
    protected Animator personAnim;
    protected Rigidbody personRb;

    [SerializeField] protected float speed = 5f;
    [SerializeField] protected float rotationSpeed = 20f;

    [SerializeField] private float minAnimationSpeed = 0.3f;
    protected virtual void LateUpdate()
    {
        AnimateMovement();
    }
   
    void OnEnable()
    {
        personAnim = GetComponentInChildren<Animator>();
        personAnim.SetBool("Static_b", true);
        personAnim.SetInteger("DeathType_int", 1);

        personRb = GetComponent<Rigidbody>();
        animCoef = animNormalSpeedCoef;
        speedCoef = speedNormalCoef;
    }


    private void SlowDown()
    {
        animCoef = animSlowSpeedCoef;
        speedCoef = speedSlowCoef;
    }
    private void SpeedUp()
    {
        animCoef = animNormalSpeedCoef;
        speedCoef = speedNormalCoef;
    }

    public void DieAnim()
    {
        personAnim.SetFloat("Speed_f", 0);
        personAnim.SetBool("Death_b", true);

    }

    protected virtual void AnimateMovement()
    {
        
        personAnim.SetFloat("Speed_f", personRb.velocity.magnitude * animCoef);
        //Debug.Log(speed * speedCoef * animSpeedCoef);
        float curAnimationSpeed = personRb.velocity.magnitude / velocityConst > minAnimationSpeed ? personRb.velocity.magnitude / velocityConst : minAnimationSpeed;
        personAnim.SetFloat("SpeedCoef_f", curAnimationSpeed);

       
    }
}
