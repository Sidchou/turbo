using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    Controls control;

    PlayerControls playerControls;
    // Start is called before the first frame update
    /*
    void OnEnable()
    {
        control = new Controls();
        playerControls = gameObject.GetComponent<PlayerControls>();
        if (playerControls != null)
            Debug.LogError("playerControls is null");
    }
    */
    void Start()
    {
        control = new Controls();
        playerControls = GetComponent<PlayerControls>();
        if (playerControls == null)
            Debug.LogError("playerControls is null");
        control.oneButton.Enable();
        control.oneButton.Action.performed += playerControls.ButtonPerformed;
        control.oneButton.Action.canceled += playerControls.ButtonCanceled;

    }
}
