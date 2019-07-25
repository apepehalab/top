/*  2019 Apepeha Lab.
 *  Underwater Fog script
 *  by Nikita Ponomarev
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class alWaterFog : MonoBehaviour
{
	public Transform camera;
	public Image panel;
    void Start()
    {
		panel = GetComponent<Image>();
    }
	
    void Update()
    {
		if(camera.transform.position.y < 199.54)
		{
			panel.color = new Color(0.28125f, 0.671875f, 0.8125f, 0.5f);
		}
		else panel.color = new Color(1, 1, 1, 0);
    }
}
