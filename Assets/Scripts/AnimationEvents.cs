using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent=transform.parent.gameObject;

    }

    public void ShootBullet()
    {
        parent.SendMessage("ShootBullet");
    }
}
