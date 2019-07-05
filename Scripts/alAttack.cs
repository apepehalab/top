/*  2019 Apepeha Lab.
 *  Character Attack script
 *  by Efim Mikhailenko
 */

using UnityEngine;
using System.Collections;

public class alAttack : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rig;
    private GameObject _currentTarget;
    private alTarget _target;
    private bool _newClick = true;
    //public float currentDamage = Camera.main.GetComponent<alTarget>().activeObject.gameObject.GetComponent<EnemyCombatScript>().Damage;

    // Use this for initialization
    void Start()
    {
        _target = cam.GetComponent<alTarget>();
        
    }

    // Update is called once per frame
    void Update()
    {
        _currentTarget = _target.getActiveObject();
        if (Input.GetMouseButtonDown(0))
            _newClick = true;

        if (_currentTarget != null && _currentTarget.tag != "Land" && _newClick == true)
        {
            _newClick = false;
            Vector3 p1 = rig.transform.position;
            Vector3 p2 = _currentTarget.transform.position;
            Quaternion angle = Quaternion.Euler(new Vector3(0, Mathf.Atan2(p2.x - p1.x, p2.z - p1.z) * Mathf.Rad2Deg + 90, 0));

            rig.MoveRotation(angle);
        }
    }
}