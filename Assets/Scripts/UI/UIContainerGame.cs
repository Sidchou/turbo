using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIContainerGame : UIContainerBase
{

    [SerializeField]
    Image FadeImage;


    public void Awake()
    {
        FadeImage.DOFade(1, 0f);
        FadeImage.DOFade(0, 0.3f);

    }



}
