﻿/*  2019 Apepeha Lab.
 *  Camera controls script
 *  by Efim Mikhailenko
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alMouseCam : MonoBehaviour
{
    public Camera cam;
    public Transform target;
    private Vector3 camRotation;
    private Vector3 lastPos;
    private Vector2 prevMousePos;
    [Header("Camera Mouse Rotation Speed")]
    public float camSpeed = 1f;
    [Header("Camera QE Rotation Speed")]
    public float roundSpeed = 100f;
    [Header("Camera Zoom Speed")]
    public float camShiftSpeed = 1f;
    [Header("Camera Height")]
    public float centerOffsetY = 0f;
    [Header("Camera Max Distance")]
    public float maxCamDistance = 50f;
    private Vector3 centerOffset;
    private bool isReleased = false;
	
	Vector3 Cursor_Position;
	bool Mouse_Down = false, inventory_click = false; //Панамарь
	float invent_left, invent_right, invent_up, invent_down; //Панамарь

    void Start()
    {
        target = GetComponent<Transform>();
        prevMousePos.x = Input.mousePosition.x;
        prevMousePos.y = Input.mousePosition.y;
        camRotation = new Vector3(0, target.rotation.y, 0);
        cam.transform.Rotate(new Vector3(0, 0, 0), Space.World);
        lastPos = target.position + centerOffset;
        centerOffset = new Vector3(0, centerOffsetY, 0);
    }

    void Update()
    {
        Vector2 mousePos;
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;
        Vector3 pos = target.position + centerOffset;

        // ===== Rotating cam =====
        float dist = Vector3.Distance(cam.transform.position, pos);
        float deltaCam;

        if(dist > maxCamDistance)
            deltaCam = Mathf.Cos(cam.transform.eulerAngles.x * Mathf.Deg2Rad);
        else
            deltaCam = Input.mouseScrollDelta.y * camShiftSpeed * Mathf.Cos(cam.transform.eulerAngles.x * Mathf.Deg2Rad);

        Vector3 offset = new Vector3(
            pos.x - lastPos.x + Mathf.Sin(cam.transform.eulerAngles.y * Mathf.Deg2Rad) * deltaCam,
            pos.y - lastPos.y - Mathf.Tan(cam.transform.eulerAngles.x * Mathf.Deg2Rad) * deltaCam,
            pos.z - lastPos.z + Mathf.Cos(cam.transform.eulerAngles.y * Mathf.Deg2Rad) * deltaCam);

        cam.transform.position += offset;

        lastPos = pos;

        cam.transform.LookAt(pos);
		
		if (Input.GetMouseButton(0))//Панамарь
		{
			if(inventory_click == false && Mouse_Down == false)//Панамарь
			{
				Mouse_Down = true;//Панамарь
				if(alInventory.Invent_Status == true)//Панамарь
				{
					Cursor_Position = Input.mousePosition;//Панамарь
					
					invent_left = GameObject.Find("Invent").GetComponent<RectTransform>().position.x - GameObject.Find("Invent").GetComponent<RectTransform>().rect.width;//Панамарь
					invent_right = GameObject.Find("Invent").GetComponent<RectTransform>().position.x + GameObject.Find("Invent").GetComponent<RectTransform>().rect.width;//Панамарь
					invent_up = GameObject.Find("Invent").GetComponent<RectTransform>().position.y + GameObject.Find("Invent").GetComponent<RectTransform>().rect.height;//Панамарь
					invent_down = GameObject.Find("Invent").GetComponent<RectTransform>().position.y - GameObject.Find("Invent").GetComponent<RectTransform>().rect.height;//Панамарь
					
					if(Cursor_Position.x > invent_left && Cursor_Position.x < invent_right && Cursor_Position.y < invent_up && Cursor_Position.y > invent_down)//Панамарь
					{
						inventory_click = true;//Панамарь
					}
				}
			}
		}
		else if(Mouse_Down == true)//Панамарь
		{
			inventory_click = false;//Панамарь
			Mouse_Down = false;//Панамарь
		}
		if (inventory_click == false)//Панамарь
		{
			if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
			{
				isReleased = true;
				Cursor.lockState = CursorLockMode.Confined;

				if (mousePos.x - prevMousePos.x != 0)
				{
					Cursor.visible = false;
					cam.transform.RotateAround(pos, new Vector3(0, 1, 0), camSpeed * (mousePos.x - prevMousePos.x) * Time.deltaTime);
				}

				if (mousePos.y - prevMousePos.y != 0)
				{
					Cursor.visible = false;
					cam.transform.RotateAround(
						pos, new Vector3(
							-Mathf.Cos(cam.transform.eulerAngles.y * Mathf.Deg2Rad),
							0,
							Mathf.Sin(cam.transform.eulerAngles.y * Mathf.Deg2Rad)),
							camSpeed * (mousePos.y - prevMousePos.y) * Time.deltaTime);
				}
			}

			else if(isReleased)
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
				isReleased = false;
			}
		}

        if (Input.GetKey("e"))
        {
            cam.transform.RotateAround(pos, new Vector3(0, 1, 0), roundSpeed * Time.deltaTime);
        }

        if (Input.GetKey("q"))
        {
            cam.transform.RotateAround(pos, new Vector3(0, -1, 0), roundSpeed * Time.deltaTime);
        }

        prevMousePos.x = mousePos.x;
        prevMousePos.y = mousePos.y;
    }
}