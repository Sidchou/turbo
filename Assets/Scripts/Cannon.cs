using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    BoxCollider boxCollider;

    public CannonBehavior behavior;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
        behavior = CannonBehavior.Rotate;
    }


    public void Init(float difficulty)
    {
        this.difficulty = difficulty;
    }
   
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
    }

    float time = 0;
    float difficulty  = 1f;


    public void Move()
    {
        if (behavior.HasFlag(CannonBehavior.Rotate))
        {
            transform.Rotate(Vector3.forward * difficulty);
        }

        if (behavior.HasFlag(CannonBehavior.LinearHorizontal))
        {
            transform.Translate(Vector3.left * Mathf.Cos(time * difficulty));
            time += Time.deltaTime;

        }

        if (behavior.HasFlag(CannonBehavior.LinearVertical))
        {
            transform.Translate(Vector3.up * Mathf.Cos(time * difficulty));
            time += Time.deltaTime;

        }


    }
}

