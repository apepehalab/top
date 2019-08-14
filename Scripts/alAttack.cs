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
    private GameObject _currentTarget;
    private alTarget _target;
    private bool _newClick = true;
    //public float currentDamage = Camera.main.GetComponent<alTarget>().activeObject.gameObject.GetComponent<EnemyCombatScript>().Damage;

    void Start()
    {
        _target = cam.GetComponent<alTarget>();
    }

    void Update()
    {
        _currentTarget = _target.getActiveObject();
        if (Input.GetMouseButtonDown(0))
            _newClick = true;

        if (_currentTarget != null && _newClick == true)
        {
            _newClick = false;
            Vector3 p1 = rig.transform.position;
            Vector3 p2 = _currentTarget.transform.position;
            Quaternion angle = Quaternion.Euler(new Vector3(0, Mathf.Atan2(p2.x - p1.x, p2.z - p1.z) * Mathf.Rad2Deg + 90, 0));

            rig.MoveRotation(angle);

            if (Vector3.Distance(p1, p2) <= attackDist)
            {
                var controller = _currentTarget.GetComponent(typeof(alNpcController)) as alNpcController;

                if(controller.getHP() > 0)
                {
                    controller.addHP(-1);
                }
            }
        }
    }
}