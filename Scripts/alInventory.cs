using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alInventory : MonoBehaviour
{
	GameObject Invent, Character;
	
	int[,] invent_array = new int[5,5];
	string obj_str;
	
	bool Invent_Status = false, Character_Status = false, Menu_Status = false;
	
	void Inventory_Print()
	{
		for(int i = 0; i <= 4; i++)
		{
			for(int j = 0; j <= 4; j++)
			{
				switch(invent_array[j,i])
				{
					case 1:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("1");
						break;
					case 2:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("2");
						break;
					case 3:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("3");
						break;
					case 4:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("4");
						break;
					case 5:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("5");
						break;
					case 6:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("6");
						break;
					default:
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("blank");
						break;
				}
			}
		}
	}
	public void OpenInvent()
	{
		Invent_Status = true;
		Invent.SetActive(true);
	}
	public void CloseInvent()
	{
		Invent_Status = false;
		Invent.SetActive(false);
	}
	public void OpenCharacter()
	{
		Character_Status = true;
		Character.SetActive(true);
	}
	public void CloseCharacter()
	{
		Character_Status = false;
		Character.SetActive(false);
	}
    void Start()
    {
        Invent = GameObject.Find ("Inventory");
		Character = GameObject.Find ("CharacterMenu");
		
		invent_array[0,0] = 5;
		invent_array[0,1] = 0;
		invent_array[0,2] = 4;
		invent_array[0,3] = 1;
		invent_array[0,4] = 6;
		
		invent_array[4,0] = 6;
		invent_array[4,1] = 3;
		invent_array[4,2] = 1;
		invent_array[4,3] = 4;
		invent_array[4,4] = 0;
		
		Inventory_Print();
		Invent.SetActive(false);
		Character.SetActive(false);
    }
	
    void Update()
    {
		if(Invent_Status == true)
		{
			Inventory_Print();
		}
		if(Menu_Status == false)
		{
			if(Input.GetKeyDown(KeyCode.I))
			{
				if(Invent_Status == false)
				{
					OpenInvent();
				}
				else if(Invent_Status == true)
				{
					CloseInvent();
				}
			}
			if(Input.GetKeyDown(KeyCode.O))
			{
				if(Character_Status == false)
				{
					OpenCharacter();
				}
				else if(Character_Status == true)
				{
					CloseCharacter();
				}
			}
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			if(Menu_Status == false) Menu_Status = true;
			else if(Menu_Status == true) Menu_Status = false;
			if(Invent_Status == true)
			{
				CloseInvent();
			}
			if(Character_Status == true)
			{
				CloseCharacter();
			}
		}
    }
}
