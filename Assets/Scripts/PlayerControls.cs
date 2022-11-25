using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControls : MonoBehaviour
{
    enum playerState
    { 
    wait,
    rotate,
    charge,
    shot,
    airborn
    }
    [SerializeField]
    private Cannon cannon;
    private Rigidbody rb;
    private playerState state;
    private float charge = 0;
    [SerializeField]
    private Slider chargeSlider;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
            Debug.LogError("rigidbody is null");
        rb.isKinematic = false;
        state = playerState.airborn;


        InputManager.Instance.OnButtonDown.AddListener(ButtonPerformed);
        InputManager.Instance.OnButtonUp.AddListener(ButtonCanceled);
        InputManager.Instance.OnHoldValue.AddListener(ButtonHeld);
    }

    private void OnDestroy()
    {
        InputManager.Instance.OnButtonDown.RemoveListener(ButtonPerformed);
        InputManager.Instance.OnButtonUp.RemoveListener(ButtonCanceled);
        InputManager.Instance.OnHoldValue.RemoveListener(ButtonHeld);
    }

    // Update is called once per frame
    void Update()
    {

        SwitchState();


    }

    public void Reset()
    {
        rb.isKinematic = false;
    }
    private void SwitchState()
    {
        switch (state)
        {
            case playerState.wait:
                NextState();
                break;
            case playerState.rotate:
                if (cannon != null)
                {
                    cannon.Move();
                }
           
                break;
            case playerState.charge:
             //   charge = (charge + Time.deltaTime) % 1; this was moved to the hold callback
                chargeSlider.value = charge;
                break;
            case playerState.airborn:
                break;
            default:
                break;
        }
        if (Camera.main.WorldToScreenPoint(transform.position).y < -100) {
            Debug.Log(Camera.main.WorldToScreenPoint(transform.position).y);
        }
    }

  

    public void ButtonPerformed() 
    {
        if (state != playerState.airborn || state != playerState.charge)
        {
            NextState();
            if (state == playerState.charge)
            {
                chargeSlider.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                chargeSlider.gameObject.SetActive(true);
            }
        }
    }
    private void NextState() 
    {
        if (state == playerState.airborn && transform.parent == null)
        {
            return;
        }
        int n = System.Enum.GetNames(typeof(playerState)).Length;
        int cur = ((int)state + 1) % n;
        state = (playerState)cur;
        Debug.Log(state);
    }

    public void ButtonCanceled()
    {
        if (state == playerState.charge)
        {
            rb.isKinematic = false;
            rb.AddForce(transform.up*(5+charge*10),ForceMode.Impulse);
            rb.AddTorque(transform.forward * (1+ charge * 2)*-1, ForceMode.Impulse);
            transform.SetParent(null);
            charge = 0;
            NextState();

        }
    }

    public void ButtonHeld(float thresholdValue, float time)
    {
        charge = time % 1;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (state == playerState.airborn && other.tag == "Cannon")
        {
            Debug.Log("ent");
            rb.isKinematic = true;
            cannon = other.transform.gameObject.GetComponent<Cannon>();
            transform.SetParent(cannon.transform);
            transform.localPosition = Vector3.up*0.5f;
            transform.localRotation = Quaternion.identity;
            NextState();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cannon")
        {
            Debug.Log("exit");

            StartCoroutine(TriggerDelay());

        }
    }
    IEnumerator TriggerDelay()
    {
        yield return new WaitForSeconds(Time.deltaTime*2);
        if (state == playerState.shot)
        {
            NextState();
            chargeSlider.gameObject.SetActive(false);
     }
    }
}
