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
            personHealth = value;
        } 
    }
    public bool IsDead { get; protected set; } = false;

    // Start is called before the first frame update
    private void ReceiveDamage(float damage)
    {
        PersonHealth -= damage;
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
        if (PersonHealth <= 0 && !IsDead)
        {
            IsDead = true;
            moveComponent.DieAnim();
        }
    }

    public void DefaultHealth()
    {
        personHealth = maxPersonHealth;
    }
}
