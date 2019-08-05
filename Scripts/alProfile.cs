using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class alProfile : MonoBehaviour
{
	public Text profile, character;
	public InputField inputfield;
	
    void Start()
    {
		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			profile.text = alPlayerName.PlayerName;
			character.text = alPlayerName.PlayerName;
		}
    }
	
	public void LoadText()
	{
		alPlayerName.PlayerName = inputfield.text;
	}
}
