using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alProfile : MonoBehaviour
{
	public Text profile;
	public InputField inputfield;
	
    void Start()
    {
        profile.text = alPlayerName.PlayerName;
    }
	
	public void LoadText()
	{
		alPlayerName.PlayerName = inputfield.text;
	}
}
