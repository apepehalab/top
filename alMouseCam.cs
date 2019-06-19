/*  2019 Apepeha Lab.
 *  Camera controls script
 *  by Efim Mikhailenko
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alMouseCam : MonoBehaviour
{
    public Camera cam; // камера
    public Transform target;
    private Vector3 camRotation;
    private Vector3 lastPos;
    //public float camDistance = 10f; //отдаление камеры
    private Vector2 prevMousePos;
    public float camSpeed = 1f;
    public float roundSpeed = 100f;
    public float camShiftSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        target = GetComponent<Transform>();
        prevMousePos.x = Input.mousePosition.x;
        prevMousePos.y = Input.mousePosition.y;
        camRotation = new Vector3(0, target.rotation.y, 0);
        cam.transform.Rotate(new Vector3(0, 0, 0), Space.World);
        lastPos = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos;
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        Vector3 offset = new Vector3(
            target.position.x - lastPos.x + Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180) * Input.mouseScrollDelta.y * camShiftSpeed,
            target.position.y - lastPos.y - Mathf.Tan(cam.transform.eulerAngles.x * Mathf.PI / 180) * Input.mouseScrollDelta.y * camShiftSpeed,
            target.position.z - lastPos.z + Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180) * Input.mouseScrollDelta.y * camShiftSpeed);

        cam.transform.position += offset;
        lastPos = target.position;

        cam.transform.LookAt(target);
        if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
        { 
            if (mousePos.x - prevMousePos.x != 0)
            {
                cam.transform.RotateAround(target.position, new Vector3(0, 1, 0), camSpeed * (mousePos.x - prevMousePos.x) * Time.deltaTime);
            }

            if (mousePos.y - prevMousePos.y != 0)
            {
                cam.transform.RotateAround(
                    target.position, new Vector3(
                        -Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180),
                        0,
                        Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180)),
                        camSpeed * (mousePos.y - prevMousePos.y) * Time.deltaTime);
            }
        }

        if (Input.GetKey("e"))
        {
            cam.transform.RotateAround(target.position, new Vector3(0, 1, 0), roundSpeed * Time.deltaTime);
        }

        if (Input.GetKey("q"))
        {
            cam.transform.RotateAround(target.position, new Vector3(0, -1, 0), roundSpeed * Time.deltaTime);
        }

        // float dist = Vector3.Distance(cam.transform.position, target.position);

        //if (dist != camDistance || dist != camDistance)
        //{
        //    Vector3 offset = new Vector3(0, 0, 0);
        //    //offset.y += transform.position.y - cam.transform.position.y + camDistance;
        //    //offset.z += transform.position.z - cam.transform.position.z + camDistance;
        //    cam.transform.position += offset;
        //}

        //cam.transform.Translate();

        //offset.x = Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        //offset.z = Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        // offset.y = camDistance; // отдаление камеры

        // cam.transform.position = transform.position + offset; //подтаскивание камеры к нужным координатам

        prevMousePos.x = mousePos.x;
        prevMousePos.y = mousePos.y;
    }
}
