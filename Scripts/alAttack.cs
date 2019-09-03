/*  2019 Apepeha Lab.
 *  Character Attack script
 *  by Efim Mikhailenko
 */

using UnityEngine;
using System.Collections;
using System;

public class alAttack : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rig;
    public float attackDist = 1f;
    public float attackSpeed = 1.5f;
    private GameObject _currentTarget;
    private alTarget _target;
    private bool _newClick = true;
    private float _timer = 0;
    private bool _isAttacking = false;
    private bool _attackAnimTrigger = false;
    //public float currentDamage = Camera.main.GetComponent<alTarget>().activeObject.gameObject.GetComponent<EnemyCombatScript>().Damage;

    void Start()
    {
        _target = cam.GetComponent<alTarget>();
    }

    void Update()
    {
        if (_isAttacking) _timer += Time.deltaTime;
        else _timer = 0;

        _currentTarget = _target.getActiveObject();
        if (Input.GetMouseButtonDown(0))
        {
            _isAttacking = false;
            _newClick = true;
        }
        if (_currentTarget != null)
        {
            Vector3 p1 = rig.transform.position;
            Vector3 p2 = _currentTarget.transform.position;
            if (_newClick)
            {
                
                Quaternion angle = Quaternion.Euler(new Vector3(0, Mathf.Atan2(p2.x - p1.x, p2.z - p1.z) * Mathf.Rad2Deg + 90, 0));
                rig.MoveRotation(angle);
            }

            _newClick = false;
            
            if (Vector3.Distance(p1, p2) <= attackDist)
            {
                _isAttacking = true;
                var controller = _currentTarget.GetComponent(typeof(alNpcController)) as alNpcController;

                if (controller.getHP() > 0)
                {

                    _attackAnimTrigger = true;

                    if (_timer >= attackSpeed)
                    {
                        controller.addHP(-1);
                        _timer = 0;
                    }

                }
                else if (controller.getHP() <= 0)
                {
                    _attackAnimTrigger = false;
                }
            }

            else
            {
                _isAttacking = false;
            }
        }
    }

    public bool isAttacking()
    {
        return _attackAnimTrigger;
    }

    public float getAttackSpeed()
    {
        return attackSpeed;
    }
}