using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    public float powerupStrength = 1f;
    private void OnDisable()
    {
        GameManager.Instance.CheckPowerups();
    }
}
