using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIContainerGame : UIContainerBase
{

    [SerializeField]
    Image FadeImage;


    [SerializeField]
    GameObject EndScreen;


    [SerializeField]
    string MenuSceneName;

    [SerializeField]
    TextMeshProUGUI scoreText;

    public void Awake()
    {
        FadeImage.DOFade(1, 0f);
        FadeImage.DOFade(0, 0.3f);

    }

    protected override void Start()
    {
        CannonManager.Instance.OnPlayerDied.AddListener(Show);

        CannonManager.Instance.OnScoreChanged.AddListener(UpdateScore);
        EndScreen.gameObject.SetActive(true);
        base.Start();
        EndScreen.gameObject.SetActive(false);

    }



    void UpdateScore(float score)
    {
        scoreText.text = $"Score\n{(int)score}";

    }

    void Show()
    {
        EndScreen.gameObject.SetActive(true);
        Activate();

    }

    public void Restart()
    {
        CannonManager.Instance.Restart();

        selectedIndex = 0;
        EndScreen.gameObject.SetActive(false);
        Deactivate();
    }

    public void GoToMainMenu()
    {
        FadeImage.DOFade(1f, 0.3f).OnComplete(() => SceneManager.LoadScene(MenuSceneName));
    }


}
