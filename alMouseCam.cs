using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alMouseCam : MonoBehaviour
{
    public Camera cam; // камера
    public Transform transform;
   // private Vector3 offset; // смещение камеры относительно объекта
    private Vector3 camRotation;
    private Vector3 lastPos;
    public float camDistance = 10f; //отдаление камеры
    private Vector2 prevMousePos;
    public float camSpeed = 1f;
    private float roundSpeed = 4.6f;

    // Start is called before the first frame update
    void Start()
    {
        prevMousePos.x = Input.mousePosition.x;
        prevMousePos.y = Input.mousePosition.y;
        camRotation = new Vector3(0, transform.rotation.y, 0);
        cam.transform.Rotate(new Vector3(0, 0, 0), Space.World);
        lastPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos;
        mousePos.x = Input.mousePosition.x;
        mousePos.y = Input.mousePosition.y;

        cam.transform.LookAt(transform);
        if (Input.GetMouseButton(0))
        { 
            if (mousePos.x - prevMousePos.x != 0)
            {
                cam.transform.RotateAround(transform.position, new Vector3(0, 1, 0), camSpeed * (mousePos.x - prevMousePos.x) * Time.deltaTime);
            }

            if (mousePos.y - prevMousePos.y != 0)
            {
                cam.transform.RotateAround(transform.position, new Vector3(-Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180), 0, Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180)), camSpeed * (mousePos.y - prevMousePos.y) * Time.deltaTime);
            }

        }


        if (Input.GetKey("e"))
        {
            cam.transform.RotateAround(transform.position, new Vector3(0, 1, 0), camSpeed * roundSpeed * Time.deltaTime);
        }

        if (Input.GetKey("q"))
        {
            cam.transform.RotateAround(transform.position, new Vector3(0, -1, 0), camSpeed * roundSpeed * Time.deltaTime);
        }

        float dist = Vector3.Distance(cam.transform.position, transform.position);

        //if (dist != camDistance || dist != camDistance)
        //{
        //    Vector3 offset = new Vector3(0, 0, 0);
        //    //offset.y += transform.position.y - cam.transform.position.y + camDistance;
        //    //offset.z += transform.position.z - cam.transform.position.z + camDistance;
        //    cam.transform.position += offset;
        //}
        cam.transform.position += transform.position - lastPos;
        lastPos = transform.position;


        //cam.transform.Translate();

        //offset.x = Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        //offset.z = Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
       // offset.y = camDistance; // отдаление камеры

       // cam.transform.position = transform.position + offset; //подтаскивание камеры к нужным координатам

        prevMousePos.x = mousePos.x;
        prevMousePos.y = mousePos.y;
    }
}
