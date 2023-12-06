using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCustomInput : MonoBehaviour
{
    [SerializeField] private OnScreenStickCustom m_movementStick;

    [SerializeField] private OnScreenStickCustom m_rotationStick;

    [SerializeField] private OnScreenButtonCustom m_shootButton;
    [SerializeField] private float m_onScreenRotationSpeedCoef=2;

    public void Start()
    {
        m_movementStick.onJoystickMove.AddListener(OnMoveStickMove);
        m_movementStick.onJoystickRelease.AddListener(OnMoveStickRelease);

        m_rotationStick.onJoystickMove.AddListener(OnRotationStickMove);
        m_rotationStick.onJoystickRelease.AddListener(OnRotationStickRelease);

        m_shootButton.onButtonPress.AddListener(OnShoot);
    }

    public void OnMoveStickMove(Vector2 direction)
    {
        SendMessage("OnMove", direction);
    }

    public void OnMoveStickRelease()
    {
        SendMessage("OnMove", Vector2.zero);
        
    }

    public void OnRotationStickMove(Vector2 direction)
    {
        SendMessage("OnRotate", direction* m_onScreenRotationSpeedCoef);
    }

    public void OnRotationStickRelease()
    {
        SendMessage("OnRotate", Vector2.zero);
    }

    public void ResetSticks()
    {
        
        m_movementStick.ResetStickPos();
        m_rotationStick.ResetStickPos();
    }
    public void OnShoot()
    {
        SendMessage("OnFire");
    }
}
