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
    public float jumpBoost = 100f;
    
    private Vector3 movement = new Vector3(0, 0, 0); // трёхмерный вектор скорости
    private Quaternion turn = Quaternion.Euler(new Vector3(0, 90, 0));
    public float maximumSpeed = 15f;
    private float horizontalBuffer = 0;
    private bool isGrounded = false;
    private bool isTurning = false;



    void OnCollisionEnter(Collision hit)
    {
        if (hit.gameObject.tag == "Land" || hit.gameObject.tag == "Solid Object")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit(Collision hit)
    {
        if (hit.gameObject.tag == "Land" || hit.gameObject.tag == "Solid Object")
        {
            isGrounded = false;
        }
    }

    void Start()
    {
        rig = GetComponent<Rigidbody>();
        rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    void Update()
    {
        float vertical = Input.GetAxis("Vertical");// принимает значения от -1 до 1 соответственно s w и ↓ ↑
        float horizontal = Input.GetAxis("Horizontal");// принимает значения от -1 до 1 соответственно a d и ← →

        float rad = rig.transform.eulerAngles.y * Mathf.Deg2Rad; //угол поворота rigidbody в радианах

        Vector3 xMove = new Vector3(0, 0, 0); // трёхмерный вектор для горизонтальной компоненты движения
        Vector3 zMove = new Vector3(0, 0, 0); // трёхмерный вектор для вертикальной компоненты движения
        Vector3 yMove = new Vector3(0, 0, 0);
       
        float vel = 0; // модуль вектора скорости

        Quaternion angvelR = Quaternion.Euler(new Vector3(0, 100, 0) * Time.deltaTime); //угловая скорость поворота вправо
        Quaternion angvelL = Quaternion.Euler(new Vector3(0, -100, 0) * Time.deltaTime); //угловая скорость поворота влево
        

        if (horizontal != 0) // проверка горизонтальной оси
        {
            if (!isTurning)
            {
                isTurning = true;

                if (horizontal > 0)
                {
                    turn = Quaternion.Euler(new Vector3(0, 90, 0));
                    horizontalBuffer = 1;
                }

                else
                {
                    turn = Quaternion.Euler(new Vector3(0, -90, 0));
                    horizontalBuffer = -1;
                }
                rig.MoveRotation(rig.rotation * turn);
                
            }
            rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            vel = speed;
            xMove = new Vector3(-vel * (float)Math.Cos(rad), 0, vel * (float)Math.Sin(rad)) * Time.deltaTime; //вчислеие трёхмерной горизонтальной компоненты вектора скорости
        }
        else
        {
            if(isTurning)
                isTurning = false;   
 
            horizontalBuffer = 0;
            rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
            
        }

        if (vertical != 0 && horizontal == 0) // проверка вертикальной оси
        {
            Quaternion buffer = Quaternion.Euler(new Vector3(0, cam.transform.eulerAngles.y + 90 + (90 * horizontalBuffer), 0));
            rig.MoveRotation(buffer);
            rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            vel = speed * vertical;
            zMove += new Vector3(-vel * (float)Math.Cos(rad), 0, vel * (float)Math.Sin(rad)) * Time.deltaTime; //вчислеие трёхмерной вертикальной компоненты вектора скорости
        }
        else
        {
            rig.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        //if (!isGrounded)
        //    yMove += new Vector3(0, -gravityBoost * Time.deltaTime, 0);

        
       
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            yMove += Vector3.up * Mathf.Sqrt(JumpHeight * -2f * Physics.gravity.y) * jumpBoost;
        }

        movement = xMove + zMove + yMove; // сложение трёхмерных компонент скоростей

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
            Quaternion buffer = Quaternion.Euler(new Vector3(0, cam.transform.eulerAngles.y + 90 + (90 * horizontalBuffer), 0));
            rig.MoveRotation(buffer);
        }

        rig.AddForce(movement, ForceMode.VelocityChange); // перемещение обекта

        float testSpeed = Vector3.Magnitude(rig.velocity);  // test current object speed

        if (testSpeed > maximumSpeed)
        {
            float brakeSpeed = testSpeed - maximumSpeed;  // calculate the speed decrease

            Vector3 normalisedVelocity = rig.velocity.normalized;
            Vector3 brakeVelocity = normalisedVelocity * brakeSpeed;  // make the brake Vector3 value

            rig.AddForce(-brakeVelocity, ForceMode.VelocityChange);  // apply opposing brake force
        }

    /*  ===== Old script =====
     *  Quaternion camRotation = Quaternion.Euler(30, rig.rotation.eulerAngles.y - 90, 0); // определение угла поворота камеры относительно объекта
     *  cam.transform.rotation = camRotation; // поворот камеры
     *  offset.x = Mathf.Sin(cam.transform.eulerAngles.y * Mathf.Deg2Rad) * -camDistance; //тригонометрия 9 класс школы
     *  offset.z = Mathf.Cos(cam.transform.eulerAngles.y * Mathf.Deg2Rad) * -camDistance; //тригонометрия 9 класс школы
     *  offset.y = camDistance; // отдаление камеры
     *  cam.transform.position = rig.position + offset; //подтаскивание камеры к нужным координатам
     */
    }
}