using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIButtonBase : MonoBehaviour
{

    [SerializeField]
    Image SelectedImage;

    [SerializeField]
    Image FillImage;

    [SerializeField]
    UnityEvent Event;

    protected bool m_IsSelected;


    public virtual void Select(bool Select)
    {
        m_IsSelected = Select;
        SelectedImage.enabled = m_IsSelected;

     //   Debug.Log($"{m_IsSelected} {SelectedImage} {SelectedImage.enabled}", SelectedImage);

        FillImage.fillAmount = 0;

    }

    public virtual void Execute()
    {
        Event.Invoke();

    }

    public virtual void UpdateHoldValue(float value)
    {
        FillImage.fillAmount = DOVirtual.EasedValue(0, 1, value, Ease.InCubic);

    }
}
