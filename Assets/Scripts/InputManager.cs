using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class InputManager : MonoBehaviour
{
    Controls control;

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

    float defaultHoldThreshold = 1f;
    public static InputManager Instance;

    [HideInInspector]
    public UnityEvent OnButtonDown;

    [HideInInspector]
    public UnityEvent OnButtonUp;
    //[HideInInspector]
    //public UnityEvent DoubleClick; don't use doube click for now
    [HideInInspector]
    public UnityEvent<float, float> OnHoldValue; //first value is hold time from 0 to 1, second is in seconds from start fo hold
    [HideInInspector]
    public UnityEvent OnHoldThreshold; //when hold threshold is reached

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += ResetOnLoadScene;


    }




    PlayerControls playerControls;

    void Start()
    {
        control = new Controls();
        control.Enable();
        control.oneButton.Action.performed += ButtonDown;
        control.oneButton.Action.canceled += ButtonRelease;



        //  playerControls = GetComponent<PlayerControls>();
        //if (playerControls == null)
        //    Debug.LogError("playerControls is null");
        //control.oneButton.Enable();
        // control.oneButton.Action.performed += playerControls.ButtonPerformed;
        //control.oneButton.Action.canceled += playerControls.ButtonCanceled;

    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= ResetOnLoadScene;
    }


    bool isPressed;
    bool holdInvoked;
    float time = 0;


    void ButtonDown(InputAction.CallbackContext obj) {

        Debug.Log($"on button down");

        
        if (!isPressed)
        {
            OnButtonDown.Invoke();
            isPressed = true;
            time = 0;
        }
       
    }

    void ButtonRelease(InputAction.CallbackContext obj)
    {

        Debug.Log($"on button release");


        if (isPressed)
        {
            isPressed = false;
            holdInvoked = false;
            OnButtonUp.Invoke();
            time = 0;
        }

    }

    private void Update()
    {
        if (isPressed)
        {
            
            time += Time.deltaTime;
            OnHoldValue.Invoke(Mathf.Min(1f, time / defaultHoldThreshold), time); ;
            if (time >= defaultHoldThreshold && !holdInvoked)
            {
                holdInvoked = true;
                OnHoldThreshold.Invoke();
            }
            
        }
    }

    private void ResetOnLoadScene(Scene scene, LoadSceneMode mode)
    {
        if (isPressed)
        {
            control.oneButton.Action.Reset();
            isPressed = false;
            holdInvoked = false;
            time = 0;
        }
    }


}
