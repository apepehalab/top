/*  2019 Apepeha Lab.
 *  Character Animation script
 *  by Efim Mikhailenko
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alPlayerAnim : MonoBehaviour
{
    public Animator anim;
    private float _animState;
    private int _animTransition;
    private float _animShiftSpeed;
    private string _currentAxis;

    void Start()
    {
        anim = GetComponent<Animator>();
        _animState = 0.5f;
        anim.SetFloat("offsetX", _animState);
        _animTransition = 0;
        _currentAxis = "";
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");
        float axis = 0;

        if (Input.GetKey("e"))
        {
            _currentAxis = "qe";
            axis += 1;
            _animTransition = 1;
        }

        if (Input.GetKey("q"))
        {
            _currentAxis = "qe";
            axis -= 1;
            _animTransition = 1;
        }

        if (horizontal != 0)
        {
            _currentAxis = "Horizontal";
            axis = horizontal;
            _animTransition = 0;
        }

        else if (vertical != 0)
        {
            _currentAxis = "Vertical";
            axis = vertical;
            _animTransition = 0;
        }


        anim.SetInteger("movement", _animTransition);

        if (axis > 0)
        {
            smoothAnim(1, 1.2f, 0.05f);
        }
        else if (axis < 0)
        {
            if (_currentAxis == "Horizontal")
                smoothAnim(1, 1.2f, 0.05f);
            else
                smoothAnim(0, 1.2f, 0.05f);
        }
        else
        {
            smoothAnim(0.5f, 1.2f, 0.05f);
        }


        
    }

    void smoothAnim(float value, float shiftSpeed, float accuracy)
    {
        if (_animState < value - accuracy)
            _animState += shiftSpeed * Time.deltaTime;

        else if (_animState > value + accuracy)
            _animState -= shiftSpeed * Time.deltaTime;

        else
            _animState = value;
        anim.SetFloat("offsetX", _animState);
    }
}