using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class UIContainerBase : MonoBehaviour
{



    protected UIButtonBase[] uIButtons;
    protected int selectedIndex = 0;



  
    private void Start()
    {
        selectedIndex = 0;
        uIButtons = GetComponentsInChildren<UIButtonBase>();
        InputManagerOld.Instance.Click.AddListener(OnClick);
        InputManagerOld.Instance.HoldThreshold.AddListener(OnHoldThreshold);
        InputManagerOld.Instance.HoldValue.AddListener(OnHoldValue);
        Debug.Log($"Found {uIButtons.Length} buttons.");
        if (uIButtons.Length > 0)
        {
            Select();
        }
    }

    private void OnDestroy()
    {
        InputManagerOld.Instance.Click.RemoveListener(OnClick);
        InputManagerOld.Instance.HoldThreshold.RemoveListener(OnHoldThreshold);
        InputManagerOld.Instance.HoldValue.RemoveListener(OnHoldValue);
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
            Debug.Log($"select {i} {selectedIndex == i}", uIButtons[i].gameObject);
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