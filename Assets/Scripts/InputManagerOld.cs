using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class InputManagerOld : MonoBehaviour
{
    Keyboard m_Keyboard;
    Mouse m_Mouse;

    bool isPressed;
    bool isHeldInvoked;

    float holdTime = 1f;
    float doubleClickTimeBetween = 0.1f;
    float doubleFirstClickMaxTime = 0.1f;

    float timer = 0;
    
    bool couldBeDoubleClick = false;

    public static InputManagerOld Instance;


    [HideInInspector]
    public UnityEvent Click;
    [HideInInspector]
    public UnityEvent DoubleClick;
    [HideInInspector]
    public UnityEvent<float, float> HoldValue; //first value is hold time from 0 to 1, second is in seconds from start fo hold
    [HideInInspector]
    public UnityEvent HoldThreshold; //when hold threshold is reached



    [SerializeField]
    bool UseDoubleClick;

    private void Awake()
    {

        if (Instance != null)
        {
            Destroy(gameObject);
        }
        m_Keyboard = Keyboard.current;
        m_Mouse = Mouse.current;
        isPressed = false;
        Instance = this;
        DontDestroyOnLoad(gameObject);



    }

    private void Update()
    {

        var value = m_Keyboard.anyKey.ReadValue() == 1 || m_Mouse.leftButton.ReadValue() == 1 || m_Mouse.rightButton.ReadValue() == 1;


        //If double click handling is not needed, simpler version
        if (!UseDoubleClick) {

            if (!isPressed && value)
            {
                isPressed = true;
                timer = 0;

            }

            else if (isPressed && value)
            {
                timer += Time.deltaTime;
                if (timer > holdTime)
                {
                    if (!isHeldInvoked)
                    {
                        //invoke hold enough threshold
                        Debug.Log($"invoke threshold");
                        HoldThreshold.Invoke();
                        isHeldInvoked = true;
                    }
                }
            //    Debug.Log($"invoke value");
                HoldValue.Invoke(Mathf.Min(1f, timer / holdTime), holdTime);
                //holding for timer time
            }

            else if (isPressed && !value)
            {
                isPressed = false;

                if (!isHeldInvoked)
                {
                    Debug.Log($"invoke click");
                    Click.Invoke();
                }
                timer = 0;
                isHeldInvoked = false;

                HoldValue.Invoke(0, 0);
            }

        }

        else
        {

            if (!isPressed && value)
            {
                isPressed = true;
                timer = 0;

            }

            else if (isPressed && value)
            {
                timer += Time.deltaTime;
                if (timer > holdTime)
                {
                    if (!isHeldInvoked)
                    {
                        //invoke hold enough threshold
                        HoldThreshold.Invoke();
                        isHeldInvoked = true;
                    }
                }
                HoldValue.Invoke(Mathf.Min(1f, timer / holdTime), holdTime);
                //holding for timer time
            }

            else if (isPressed && !value)
            {
                isPressed = false;

                if (!couldBeDoubleClick)
                {
                    //dont double click if click are too long
                    if (timer < doubleFirstClickMaxTime)
                    {
                        couldBeDoubleClick = true;
                        timer = 0;
                        return;
                    }


                }

                if (couldBeDoubleClick)
                {
                    // double click
                    DoubleClick.Invoke();
                    // next click is single click
                    couldBeDoubleClick = false;
                }
                else
                {
                    Click.Invoke();
                }


                timer = 0;
            }

            else if (!isPressed && !value)
            {
                isHeldInvoked = false;
                if (couldBeDoubleClick)
                {
                    timer += Time.deltaTime;

                    if (timer > doubleClickTimeBetween)
                    {
                        couldBeDoubleClick = false;
                        Click.Invoke();
                    }
                }

            }
        }
    }
}
