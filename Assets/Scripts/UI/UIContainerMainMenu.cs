using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIContainerMainMenu : UIContainerBase
{

    [SerializeField]
    string GameSceneName;

    [SerializeField]
    Image FadeImage;


    

    public void StartGame()
    {
        FadeImage.DOFade(1f, 0.3f).OnComplete(() => SceneManager.LoadScene(GameSceneName));
        
    }






}
