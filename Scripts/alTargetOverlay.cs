using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alTargetOverlay : MonoBehaviour
{
    public Camera cam;
    private string _targetStr = "";
    private alTarget _target;
    private GameObject _currentTarget;
    public Text _targetText;


    void Start()
    {
        _target = cam.GetComponent<alTarget>();
        _targetText.text = _targetStr;

    }

    void Update()
    {

        _currentTarget = _target.getActiveObject();
        if (_currentTarget != null)
        {
            _targetStr = _currentTarget.name;
            _targetText.text = _targetStr;
        }
        
    }
}
