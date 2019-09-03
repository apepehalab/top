/*  2019 Apepeha Lab.
 *  Game Menu script
 *  by Nikita Ponomarev
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class alMenu : MonoBehaviour
{
	GameObject Menu;
	bool Menu_Status = false;

	public void OpenMenu()
	{
		Menu_Status = true;
		Menu.SetActive(true);
	}

	public void CloseMenu()
	{
		Menu_Status = false;
		Menu.SetActive(false);
	}

	void Start()
	{
		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			Menu = GameObject.Find ("Menu");

			CloseMenu();
		}
	}

	void Update()
	{
		if(SceneManager.GetActiveScene().buildIndex == 1)
		{
			if(Input.GetKeyDown(KeyCode.Escape))
			{
				if(Menu_Status == false) Menu_Status = true;
				else if(Menu_Status == true) Menu_Status = false;
			}
			if (Menu_Status == true)
			{
				OpenMenu();
			}
			if (Menu_Status == false)
			{
				CloseMenu();
			}
		}
	}

	public void Quit()
	{
		Application.Quit();
	}

	public void LoadScene(int Scene)
	{
		SceneManager.LoadScene(Scene);
	}
}