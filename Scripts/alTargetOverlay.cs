using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alTargetOverlay : MonoBehaviour
{
    public Camera cam;
	private bool TargetStatusActive = false;
	private float targetHp;
    private string _targetStr = "";
    private alTarget _target;
    private GameObject _currentTarget, TargetStatus;
    public Text _targetText, hp_text;


    void Start()
    {
        _target = cam.GetComponent<alTarget>();
        _targetText.text = _targetStr;
		TargetStatus = GameObject.Find("TargetStatus");
		TargetStatus.SetActive(TargetStatusActive);

    }

    void Update()
    {

        _currentTarget = _target.getActiveObject();
		
        if(_currentTarget != null)
        {
			var controller = _currentTarget.GetComponent(typeof(alNpcController)) as alNpcController;
            _targetStr = _currentTarget.name;
			targetHp = controller.getHP();
            _targetText.text = _targetStr;
			hp_text.text = targetHp.ToString()+" HP";
			TargetStatusActive = true;
			TargetStatus.SetActive(TargetStatusActive);
        }
        
    }
}
