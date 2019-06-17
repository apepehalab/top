using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class alControls : MonoBehaviour
{

    public Camera cam; // камера
    private Vector3 offset; // смещение камеры относительно объекта
   // public float camDistance = 10f; //отдаление камеры
    public Rigidbody rig; //физика объекта
    private float speed = 7f; // скорость
    private float vel = 0f; // модуль вектора скорости
    private Vector3 movement = new Vector3(0, 0, 0); // трёхмерный вектор скорости

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");// принимает значения от -1 до 1 соответственно s w и ↓ ↑
        float horizontal = Input.GetAxis("Horizontal");// принимает значения от -1 до 1 соответственно a d и ← →

        float ang = rig.transform.eulerAngles.y * Mathf.PI / 180; //угол поворота rigidbody в радианах

        Vector3 horMove = new Vector3(0, 0, 0); // трёхмерный вектор для горизонтальной компоненты движения
        Vector3 verMove = new Vector3(0, 0, 0); // трёхмерный вектор для вертикальной компоненты движения
        

        Quaternion angvelR = Quaternion.Euler(new Vector3(0, 100, 0) * Time.deltaTime); //угловая скорость поворота вправо
        Quaternion angvelL = Quaternion.Euler(new Vector3(0, -100, 0) * Time.deltaTime); //угловая скорость поворота влево

        if (horizontal != 0) // проверка горизонтальной оси
        {
            if (horizontal > 0) // если нажата кнопка вправо
            {
               vel = speed; //модуль вектора скорости равен скорости
            }

            else if (horizontal < 0)// если нажата кнопка влево
            {
                vel = -speed; //модуль вектора скорости равен -скорости
            }
            horMove = new Vector3(-vel * (float)Math.Cos(ang + Mathf.PI / 2), 0, vel * (float)Math.Sin(ang + Mathf.PI / 2)) * Time.deltaTime; //вчислеие трёхмерной горизонтальной компоненты вектора скорости
        }

        if (vertical != 0) // проверка вертикальной оси
        {
            if (vertical > 0) // если нажата кнопка вверх
            {
                vel = speed; //модуль вектора скорости равен скорости
            }


            else if (vertical < 0) // если нажата кнопка вниз
            {
                vel = -speed; //модуль вектора скорости равен -скорости
            }
            verMove += new Vector3(-vel * (float)Math.Cos(ang), 0, vel * (float)Math.Sin(ang)) * Time.deltaTime; //вчислеие трёхмерной вертикальной компоненты вектора скорости
        }

        movement = horMove + verMove; // сложение трёхмерных компонент скоростей

        if (Input.GetKey("q")) // если нажата кнопка q
        {
            rig.MoveRotation(rig.rotation * angvelL); //повернуть rigidbody против часовой


        }
        if (Input.GetKey("e")) // если нажата кнопка e
        {
            rig.MoveRotation(rig.rotation * angvelR); //повернуть rigidbody по часовой
        }

        rig.MovePosition(transform.position + movement); // перемещение обекта


        //Quaternion camRotation = Quaternion.Euler(30, rig.rotation.eulerAngles.y - 90, 0); // определение угла поворота камеры относительно объекта

        //cam.transform.rotation = camRotation; // поворот камеры

        //offset.x = Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        //offset.z = Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        //offset.y = camDistance; // отдаление камеры

        //cam.transform.position = rig.position + offset; //подтаскивание камеры к нужным координатам
    }
}