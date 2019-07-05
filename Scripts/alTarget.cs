/*  2019 Apepeha Lab.
 *  Target System script
 *  by Efim Mikhailenko
 */

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class alTarget : MonoBehaviour
{
    public GameObject activeObject;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { // if rightbutton pressed...
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                activeObject = (hit.transform.gameObject);
            }
            else
            {
                activeObject = null;

            }
            
        }
        //if (Input.GetMouseButton(0))
        //    activeObject = new GameObject();

    }

    public GameObject getActiveObject()
    {
        return activeObject;
    }

}
