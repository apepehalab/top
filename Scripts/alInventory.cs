/*  2019 Apepeha Lab.
 *  Inventory script
 *  by Nikita Ponomarev
 */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alInventory : MonoBehaviour
{
	GameObject Invent, Character, loot_obj;
	
	int[,,] invent_array = new int[5,5,2] {{{0,0},{0,0},{0,0},{0,0},{0,0}},{{0,0},{0,0},{0,0},{0,0},{0,0}},{{0,0},{0,0},{0,0},{0,0},{0,0}},{{0,0},{0,0},{0,0},{0,0},{0,0}},{{0,0},{0,0},{0,0},{0,0},{0,0}}};
	int[,] slot_mouse = new int[2,2] {{0,0},{0,0}};
	int count_item_id = 6;
	string obj_str, count_str, slot_str, loot_name;
	Vector3 Cursor;
	float slot_left, slot_right, slot_up, slot_down, invent_left, invent_right, invent_up, invent_down;
	
	bool Character_Status = false, Menu_Status = false, Mouse_Down = false, slot_click = false, inventory_click = false;
	public static bool Invent_Status = false;
	
	void Inventory_Print()
	{
		for(int i = 0; i <= 4; i++)
		{
			for(int j = 0; j <= 4; j++)
			{
				obj_str = "slot_"+j.ToString()+"_"+i.ToString();
				count_str = "count_"+j.ToString()+"_"+i.ToString();
				GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>("blank");
				GameObject.Find(count_str).GetComponent<Text>().text = "";
				for(int item = 1; item <= count_item_id; item++)
				{
					if(invent_array[j,i,0] == item)
					{
						GameObject.Find(obj_str).GetComponent<Image>().sprite = Resources.Load<Sprite>(item.ToString());
						GameObject.Find(count_str).GetComponent<Text>().text = invent_array[j,i,1].ToString();
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
		
		invent_array[0,0,0] = 5;	invent_array[0,0,1] = 1;
		invent_array[0,1,0] = 0;	invent_array[0,1,1] = 0;
		invent_array[0,2,0] = 4;	invent_array[0,2,1] = 5;
		invent_array[0,3,0] = 1;	invent_array[0,3,1] = 3;
		invent_array[0,4,0] = 6;	invent_array[0,4,1] = 15;
		
		invent_array[4,0,0] = 6;	invent_array[4,0,1] = 34;
		invent_array[4,1,0] = 3;	invent_array[4,1,1] = 6;
		invent_array[4,2,0] = 1;	invent_array[4,2,1] = 27;
		invent_array[4,3,0] = 4;	invent_array[4,3,1] = 9;
		invent_array[4,4,0] = 0;	invent_array[4,4,1] = 0;
		
		Inventory_Print();
		Invent.SetActive(false);
		Character.SetActive(false);
    }
	
    void Update()
    {
		for(int i = 0; i <= 4; i++)
		{
			for(int j = 0; j <= 4; j++)
			{
				if(invent_array[j,i,1] == 0) invent_array[j,i,0] = 0;
			}
		}
		if(Invent_Status == true)
		{
			Inventory_Print();
			Cursor = Input.mousePosition;
					
			invent_left = GameObject.Find("Invent").GetComponent<RectTransform>().position.x - GameObject.Find("Invent").GetComponent<RectTransform>().rect.width;
			invent_right = GameObject.Find("Invent").GetComponent<RectTransform>().position.x + GameObject.Find("Invent").GetComponent<RectTransform>().rect.width;
			invent_up = GameObject.Find("Invent").GetComponent<RectTransform>().position.y + GameObject.Find("Invent").GetComponent<RectTransform>().rect.height;
			invent_down = GameObject.Find("Invent").GetComponent<RectTransform>().position.y - GameObject.Find("Invent").GetComponent<RectTransform>().rect.height;
					
			if(Cursor.x > invent_left && Cursor.x < invent_right && Cursor.y < invent_up && Cursor.y > invent_down)
				inventory_click = true;
			else
				inventory_click = false;
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
									if(invent_array[j,i,0] > 0 && invent_array[j,i,0] <= count_item_id)
									{
										slot_mouse[0,0] = invent_array[j,i,0];
										slot_mouse[0,1] = invent_array[j,i,1];
										slot_mouse[1,0] = j;
										slot_mouse[1,1] = i;
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
							if(slot_mouse[0,0] != 0 && Cursor.x > slot_left && Cursor.x < slot_right && Cursor.y < slot_up && Cursor.y > slot_down)
							{
								if(invent_array[j,i,0] > 0 && invent_array[j,i,0] <= count_item_id)
								{
									invent_array[slot_mouse[1,0],slot_mouse[1,1],0] = invent_array[j,i,0];
									invent_array[slot_mouse[1,0],slot_mouse[1,1],1] = invent_array[j,i,1];
								}
								else
								{
									invent_array[slot_mouse[1,0],slot_mouse[1,1],0] = 0;
									invent_array[slot_mouse[1,0],slot_mouse[1,1],1] = 0;
								}
								invent_array[j,i,0] = slot_mouse[0,0];
								invent_array[j,i,1] = slot_mouse[0,1];
							}
						}
					}
					slot_mouse[0,0] = 0;
					slot_mouse[0,1] = 0;
					slot_mouse[1,0] = 0;
					slot_mouse[1,1] = 0;
					slot_click = false;
				}
				Mouse_Down = false;
			}
		}
		if (Input.GetMouseButtonDown(0))
        {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			
			if (Physics.Raycast(ray, out hit) && inventory_click == false)
			{
				if(hit.transform.gameObject.tag == "Loot")
				{
					loot_name = hit.transform.gameObject.name;
					loot_obj = GameObject.Find(loot_name);
					ArrayList loot_name_parse = alFunction.parsing_string(loot_name, '_');
					bool empty_slot = false;
					for(int i = 0; i <= 4; i++)
					{
						if(empty_slot == false)
						{
							for(int j = 0; j <= 4; j++)
							{
								if(empty_slot == false)
								{
									if(invent_array[i,j,0] <= 0 || invent_array[i,j,0] > count_item_id)
									{
										Destroy(loot_obj);
										invent_array[i,j,0] = Convert.ToInt32(loot_name_parse[1].ToString());
										invent_array[i,j,1] = 1;
										empty_slot = true;
									}
								}
							}
						}
					}
					empty_slot = false;
				}
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
