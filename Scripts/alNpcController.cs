using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alNpcController : MonoBehaviour
{

    public float hp = 1f;
    public Animator anim;
    private string _status;


    void Start()
    {
        _status = "standBy";
    }

    void Update()
    {
        if (hp <= 0)
        {
            anim.SetBool("isAlive", false);
            _status = "dead";
        }
    }
}
