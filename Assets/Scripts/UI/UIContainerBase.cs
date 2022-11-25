using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIContainerBase : MonoBehaviour
{



    protected UIButtonBase[] uIButtons;
    protected int selectedIndex = 0;

    [SerializeField]
    private bool IsActive;



  
    protected virtual void Start()
    {
        selectedIndex = 0;
        uIButtons = GetComponentsInChildren<UIButtonBase>();
    //   Debug.Log(uIButtons.Length);
        if (uIButtons.Length > 0 && IsActive)
        {
            Activate();

           
        }
       
    }



    private void OnDestroy()
    {
        if (uIButtons.Length > 0)
        {
            Deactivate();
        }
      
    }

    protected void Activate()
    {
        InputManager.Instance.OnButtonUp.AddListener(OnClick);
        InputManager.Instance.OnHoldThreshold.AddListener(OnHoldThreshold);
        InputManager.Instance.OnHoldValue.AddListener(OnHoldValue);
        if (uIButtons.Length > 0)
        {
            Select();
        }

    }

    protected void Deactivate()
    {
        InputManager.Instance.OnButtonUp.RemoveListener(OnClick);
        InputManager.Instance.OnHoldThreshold.RemoveListener(OnHoldThreshold);
        InputManager.Instance.OnHoldValue.RemoveListener(OnHoldValue);
    }


    protected virtual void OnClick()
    {

        Next();
    }

    protected virtual void OnHoldThreshold()
    {
        uIButtons[selectedIndex].Execute();

    }

    protected virtual void OnHoldValue(float value, float time)
    {
        uIButtons[selectedIndex].UpdateHoldValue(value);

    }

    protected void Select()
    {
        for (int i = 0; i < uIButtons.Length; i++)
        {
    //        Debug.Log($"select {i} {selectedIndex == i}", uIButtons[i].gameObject);
            uIButtons[i].Select(selectedIndex == i);
        }
    }


    protected void Next()
    {
        selectedIndex++;

        if (selectedIndex >= uIButtons.Length)
        {
            selectedIndex = 0;
        }
        Select();

    }

}