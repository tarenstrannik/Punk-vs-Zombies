using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonController : MonoBehaviour
{
    [SerializeField] private float personHealth = 10f;
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
    protected virtual void Start()
    {

    }
}
