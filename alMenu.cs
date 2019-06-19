/*  2019 Apepeha Lab.
 *  Game Menu script
 *  by Nikita Ponomarev
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class alMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
	
	public void Quit()
    {
		Application.Quit();
    }
	
	public void LoadScene(int Scene)
    {
        SceneManager.LoadScene(Scene);
    }
}
