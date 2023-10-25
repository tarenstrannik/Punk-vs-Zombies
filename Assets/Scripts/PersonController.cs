using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    private MoveComponent moveComponent;

    [SerializeField] protected float maxPersonHealth = 10f;

    private float personHealth;
    public float PersonHealth { 
        get
        {
            return personHealth;
        }
        protected set
        {
            personHealth = Mathf.Clamp(value, -0.5f, maxPersonHealth); ;
        } 
    }
    public bool IsDead { get; protected set; } = false;

    // Start is called before the first frame update
    protected virtual void ReceiveDamage(float damage)
    {
        PersonHealth -= damage;
        if (PersonHealth <= 0 && !IsDead)
        {
            IsDead = true;
            moveComponent.DieAnim();
        }
    }
    protected virtual void Awake()
    {
        personHealth = maxPersonHealth;
    }
    protected virtual void Start()
    {
        moveComponent = GetComponent<MoveComponent>();
    }
    protected virtual void Update()
    {
       
    }

    public virtual void Revive()
    {
        PersonHealth = maxPersonHealth;
        IsDead = false;
       
    }
}
