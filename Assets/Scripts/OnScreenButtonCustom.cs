using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using UnityEngine.InputSystem.Utilities;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.AnimatedValues;
#endif

////TODO: custom icon for OnScreenButton component

public class OnScreenButtonCustom : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public void OnPointerUp(PointerEventData eventData)
        {
            //SendValueToControl(0.0f);
			onButtonRelease.Invoke();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            //SendValueToControl(1.0f);
			onButtonPress.Invoke();
        }
    public UnityEvent onButtonPress = new UnityEvent();
    public UnityEvent onButtonRelease = new UnityEvent();

}
	
	

