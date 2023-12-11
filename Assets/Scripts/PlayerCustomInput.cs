using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCustomInput : MonoBehaviour
{
    [SerializeField] private GameObject m_movementStickObject;
    private VirtualJoystick m_joystickMove;
    private OnScreenStickCustom m_movementStick;
    [SerializeField] private GameObject m_rotationStickObject;
    private OnScreenStickCustom m_rotationStick;
    private VirtualJoystick m_joystickRotate;

    [SerializeField] private OnScreenButtonCustom m_shootButtonLeft;
    [SerializeField] private OnScreenButtonCustom m_shootButtonRight;
    //[SerializeField] private float m_onScreenRotationSpeedCoef=2;

    private void Awake()
    {
        m_movementStick = m_movementStickObject.GetComponentInChildren<OnScreenStickCustom>();
        m_rotationStick = m_rotationStickObject.GetComponentInChildren<OnScreenStickCustom>();
        m_joystickMove = m_movementStickObject.GetComponent<VirtualJoystick>();
        m_joystickRotate = m_rotationStickObject.GetComponent<VirtualJoystick>();
    }
    public void Start()
    {
        m_movementStick.onJoystickMove.AddListener(OnMoveStickMove);
        m_movementStick.onJoystickRelease.AddListener(OnMoveStickRelease);

        m_rotationStick.onJoystickMove.AddListener(OnRotationStickMove);
        m_rotationStick.onJoystickRelease.AddListener(OnRotationStickRelease);

        m_shootButtonLeft.onButtonPress.AddListener(OnShoot);
        m_shootButtonRight.onButtonPress.AddListener(OnShoot);
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
        SendMessage("OnRotate", direction);
    }

    public void OnRotationStickRelease()
    {
        //SendMessage("OnRotate", Vector2.zero);
    }

    public void ResetSticks()
    {

        m_joystickMove.OnPointerUpZero();
        m_joystickRotate.OnPointerUpZero();
    }
    public void OnShoot()
    {
        SendMessage("OnFire");
    }
}
