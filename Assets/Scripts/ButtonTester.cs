using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using TMPro;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{


    [SerializeField]
    TextMeshProUGUI TextClick;
    [SerializeField]
    TextMeshProUGUI TextDoubleClick;

    [SerializeField]
    TextMeshProUGUI TextHold;

    [SerializeField]
    Image HoldIndicator;

    [SerializeField]
    Image ClickIndicator;


    Dictionary<TextMeshProUGUI, Tween> tweens = new Dictionary<TextMeshProUGUI, Tween>();

    

    void Start()
    {
        InputManagerOld.Instance.Click.AddListener(() => OnClick(TextClick));
        InputManagerOld.Instance.DoubleClick.AddListener(() => OnClick(TextDoubleClick));
        InputManagerOld.Instance.HoldThreshold.AddListener(() => OnClick(TextHold));

        InputManagerOld.Instance.HoldValue.AddListener(OnHold);

        InputManagerOld.Instance.HoldValue.AddListener(OnHoldIndicator);
    }




    void OnClick(TextMeshProUGUI textMeshProUGUI)
    {


        var tween = textMeshProUGUI.DOColor(Color.white, 0.1f).OnComplete(() => textMeshProUGUI.DOColor(Color.black, 0.2f));
        if (tweens.TryGetValue(textMeshProUGUI, out var tmpTween))
        {

            DOTween.Kill(tmpTween);
            tweens[textMeshProUGUI] = tween;
        }
        else
        {
            tweens.Add(textMeshProUGUI, tween);
        }

    }

    void OnHold(float value, float time)
    {
        
        HoldIndicator.fillAmount = value;
    }

    bool wasHeld;
    void OnHoldIndicator(float value, float time)
    {
        wasHeld = true;
        ClickIndicator.gameObject.SetActive(true);
    }

    private void Update()
    {

        if (wasHeld)
        {
            wasHeld = false;
        }
        else
        {
            ClickIndicator.gameObject.SetActive(false);
            HoldIndicator.fillAmount = 0;
        }
    }




}
