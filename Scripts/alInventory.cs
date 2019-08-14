using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alInventory : MonoBehaviour
{
	GameObject Invent, Character;
	
	int[,] invent_array = new int[5,5];
	int[] slot_mouse = new int[] {0,0,0};
	int count_item = 6;
	string obj_str, slot_str;
	Vector3 Cursor;
	float slot_left, slot_right, slot_up, slot_down;
	
	bool Character_Status = false, Menu_Status = false, Mouse_Down = false, slot_click = false;
	public static bool Invent_Status = false;
	
	void Inventory_Print()
	{
		for(int i = 0; i <= 4; i++)
		{
			for(int j = 0; j <= 4; j++)
			{
				obj_str = "slot_"+j.ToString()+"_"+i.ToString();
				GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("blank");
				for(int item = 1; item <= count_item; item++)
				{
					if(invent_array[j,i] == item)
					{
						obj_str = "slot_"+j.ToString()+"_"+i.ToString();
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.ToString());
					}
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
			if(Input.GetMouseButton(0))
			{
				Cursor = Input.mousePosition;
				if(slot_click == false && Mouse_Down == false)
				{
					Mouse_Down = true;
					
					for(int i = 0; i <= 4; i++)
					{
						for(int j = 0; j <= 4; j++)
						{
							if(slot_click == false)
							{
								slot_str = "slot_"+j.ToString()+"_"+i.ToString();
								slot_left = GameObject.Find(slot_str).GetComponent<RectTransform>().position.x - GameObject.Find(slot_str).GetComponent<RectTransform>().rect.width / 2;
								slot_right = GameObject.Find(slot_str).GetComponent<RectTransform>().position.x + GameObject.Find(slot_str).GetComponent<RectTransform>().rect.width / 2;
								slot_up = GameObject.Find(slot_str).GetComponent<RectTransform>().position.y + GameObject.Find(slot_str).GetComponent<RectTransform>().rect.height / 2;
								slot_down = GameObject.Find(slot_str).GetComponent<RectTransform>().position.y - GameObject.Find(slot_str).GetComponent<RectTransform>().rect.height / 2;
								if(Cursor.x > slot_left && Cursor.x < slot_right && Cursor.y < slot_up && Cursor.y > slot_down)
								{
									slot_click = true;
									if(invent_array[j,i] > 0 && invent_array[j,i] <= count_item)
									{
										slot_mouse[0] = invent_array[j,i];
										slot_mouse[1] = j;
										slot_mouse[2] = i;
									}
								}
							}
						}
					}
				}
			}else if(Mouse_Down == true)
			{
				Cursor = Input.mousePosition;
				if(slot_click == true)
				{
					for(int i = 0; i <= 4; i++)
					{
						for(int j = 0; j <= 4; j++)
						{
							slot_str = "slot_"+j.ToString()+"_"+i.ToString();
							slot_left = GameObject.Find(slot_str).GetComponent<RectTransform>().position.x - GameObject.Find(slot_str).GetComponent<RectTransform>().rect.width / 2;
							slot_right = GameObject.Find(slot_str).GetComponent<RectTransform>().position.x + GameObject.Find(slot_str).GetComponent<RectTransform>().rect.width / 2;
							slot_up = GameObject.Find(slot_str).GetComponent<RectTransform>().position.y + GameObject.Find(slot_str).GetComponent<RectTransform>().rect.height / 2;
							slot_down = GameObject.Find(slot_str).GetComponent<RectTransform>().position.y - GameObject.Find(slot_str).GetComponent<RectTransform>().rect.height / 2;
							if(slot_mouse[0] != 0 && Cursor.x > slot_left && Cursor.x < slot_right && Cursor.y < slot_up && Cursor.y > slot_down)
							{
								if(invent_array[j,i] > 0 && invent_array[j,i] <= count_item)
								{
									invent_array[slot_mouse[1],slot_mouse[2]] = invent_array[j,i];
								}
								else
								{
									invent_array[slot_mouse[1],slot_mouse[2]] = 0;
								}
								invent_array[j,i] = slot_mouse[0];
							}
						}
					}
					slot_mouse[0] = 0;
					slot_mouse[1] = 0;
					slot_mouse[2] = 0;
					slot_click = false;
				}
				Mouse_Down = false;
			}
			
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
