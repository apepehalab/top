using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alTargetOverlay : MonoBehaviour
{
	public string target_str;
	public Text target_text;
    void Start()
    {
        target_str = "hui";
		target_text.text = target_str;
    }
}
