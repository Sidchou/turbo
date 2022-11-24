using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveConfiner : MonoBehaviour
{
    [SerializeField]
    GameObject vcam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _pos = vcam.transform.position;
        _pos.y = Mathf.Max(transform.position.y, _pos.y);
        transform.position = _pos;
    }
}
