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
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
