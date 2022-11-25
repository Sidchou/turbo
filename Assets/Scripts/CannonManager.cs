using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CannonManager : MonoBehaviour
{
    [SerializeField]
    GameObject PlayerGO;

    List<Cannon> ActiveCannons = new List<Cannon>();


    [SerializeField]
    Cannon CannonPrefab;

    float playerMaxHeight = 0;

    float destroyHeight = 12f;

    float maxDropBeforeGameOver = 20f;

    float deltaHeightForNewCannon = 6f;

    float nextCannonHeight = 15.6f;
    int initialCannons = 5;

    float currentDifficulty = 1f;

    public bool IsPlayerDead;


    public static CannonManager Instance;
    public UnityEvent OnPlayerDied = new UnityEvent();

    public UnityEvent<float> OnScoreChanged = new UnityEvent<float>();

    private void Awake()
    {
        Instance = this;
        
    }

    private void Start()
    {
        IsPlayerDead = false;
        float initialHeight = 0.5f;
        float deltaHeight = 5f;
        for (int i = 0; i < initialCannons; i++)
        {
            //inital cannon
            if (i == 0)
            {
                var cannon = InstantiateCannon(0, initialHeight);

                PlayerGO.transform.localPosition = Vector3.up * 2f + cannon.transform.position;
                PlayerGO.transform.localRotation = Quaternion.identity;

            }
            else
            {

                InstantiateCannon(ActiveCannons[ActiveCannons.Count - 1].transform.position.x, initialHeight + deltaHeight * i);
            }

        }
    }


    private void Update()
    {
        var height = PlayerGO.transform.position.y;


        if (height > playerMaxHeight)
        {
            playerMaxHeight = height;
            OnScoreChanged.Invoke(playerMaxHeight);
        }


        if (playerMaxHeight >= nextCannonHeight)
        {
            nextCannonHeight += deltaHeightForNewCannon;
            InstantiateCannon(ActiveCannons[ActiveCannons.Count - 1].transform.position.x, nextCannonHeight);
        }


        if (height < playerMaxHeight - maxDropBeforeGameOver || height < -2)
        {
            OnPlayerDied.Invoke();
        }
        for (int i = ActiveCannons.Count-1; i >= 0; i--)
        {
            if (ActiveCannons[i].transform.position.y <= playerMaxHeight - destroyHeight)
            {
                DestroyCannon(ActiveCannons[i]);

            }
        }
    }

    public void Restart()
    {
        playerMaxHeight = 0;
        PlayerGO.transform.parent = null;
        for (int i = ActiveCannons.Count - 1; i >= 0; i--)
        {
            DestroyCannon(ActiveCannons[i]);
        }
        PlayerGO.GetComponent<PlayerControls>().Reset();
        Start();

    }


    Cannon InstantiateCannon(float previousX, float height)
    {
        var cannon = Instantiate(CannonPrefab, new Vector3(UnityEngine.Random.Range(-5f, 5f)+ previousX, height, 0), new Quaternion(), transform);
        cannon.Init(currentDifficulty);
        ActiveCannons.Add(cannon);
        return cannon;

    }

    void DestroyCannon(Cannon cn)
    {
        ActiveCannons.Remove(cn);
        Destroy(cn.gameObject);
    }

}


[Flags]
public enum CannonBehavior
{
    Rotate = 0,
    LinearHorizontal = 1,
    LinearVertical = 2,
}
