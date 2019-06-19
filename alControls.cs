/*  2019 Apepeha Lab.
 *  Character controls script
 *  by Efim Mikhailenko
 */

using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class alControls : MonoBehaviour
{
    public Camera cam; // камера
    private Vector3 offset; // смещение камеры относительно объекта
    public Rigidbody rig; //физика объекта
    public float speed = 5f; // скорость
    public float JumpHeight = 2f;
    
    private Vector3 movement = new Vector3(0, 0, 0); // трёхмерный вектор скорости
    public float maximumSpeed = 15f;
    private bool isGrounded = false;

    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Land" || hit.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.tag == "Land" || hit.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    // Update is called once per frame
    void Update()
    {
        float vertical = Input.GetAxis("Vertical");// принимает значения от -1 до 1 соответственно s w и ↓ ↑
        float horizontal = Input.GetAxis("Horizontal");// принимает значения от -1 до 1 соответственно a d и ← →

        float rad = rig.transform.eulerAngles.y * Mathf.PI / 180; //угол поворота rigidbody в радианах

        Vector3 horMove = new Vector3(0, 0, 0); // трёхмерный вектор для горизонтальной компоненты движения
        Vector3 verMove = new Vector3(0, 0, 0); // трёхмерный вектор для вертикальной компоненты движения
        float vel = 0; // модуль вектора скорости

        Quaternion angvelR = Quaternion.Euler(new Vector3(0, 100, 0) * Time.deltaTime); //угловая скорость поворота вправо
        Quaternion angvelL = Quaternion.Euler(new Vector3(0, -100, 0) * Time.deltaTime); //угловая скорость поворота влево

        if (horizontal != 0) // проверка горизонтальной оси
        {
            vel = speed * horizontal;
            horMove = new Vector3(-vel * (float)Math.Cos(rad + Mathf.PI / 2), 0, vel * (float)Math.Sin(rad + Mathf.PI / 2)) * Time.deltaTime; //вчислеие трёхмерной горизонтальной компоненты вектора скорости
        }

        if (vertical != 0) // проверка вертикальной оси
        {
            vel = speed * vertical;
            verMove += new Vector3(-vel * (float)Math.Cos(rad), 0, vel * (float)Math.Sin(rad)) * Time.deltaTime; //вчислеие трёхмерной вертикальной компоненты вектора скорости
        }

        movement = horMove + verMove; // сложение трёхмерных компонент скоростей
       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rig.AddForce(Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        if (Input.GetKey("q")) // если нажата кнопка q
        {
            rig.MoveRotation(rig.rotation * angvelL); //повернуть rigidbody против часовой
        }
        if (Input.GetKey("e")) // если нажата кнопка e
        {
            rig.MoveRotation(rig.rotation * angvelR); //повернуть rigidbody по часовой
        }

        if (Input.GetMouseButton(1))
        {
            Quaternion buffer = Quaternion.Euler(new Vector3(0, cam.transform.eulerAngles.y + 90, 0));
            rig.MoveRotation(buffer);
        }

        rig.position += movement; // перемещение обекта

        float testSpeed = Vector3.Magnitude(rig.velocity);  // test current object speed

        if (testSpeed > maximumSpeed)
        {
            float brakeSpeed = testSpeed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rig.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rig.AddForce(-brakeVelocity);  // apply opposing brake force
        }

        //Quaternion camRotation = Quaternion.Euler(30, rig.rotation.eulerAngles.y - 90, 0); // определение угла поворота камеры относительно объекта

        //cam.transform.rotation = camRotation; // поворот камеры

        //offset.x = Mathf.Sin(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        //offset.z = Mathf.Cos(cam.transform.eulerAngles.y * Mathf.PI / 180) * -camDistance; //тригонометрия 9 класс школы
        //offset.y = camDistance; // отдаление камеры

        //cam.transform.position = rig.position + offset; //подтаскивание камеры к нужным координатам
    }
}