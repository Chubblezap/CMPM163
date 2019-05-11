using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager1 : MonoBehaviour
{
    Scene scene;
    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene();
    }

    // Update is called once per frame
    void Update()
    {
        if(scene.name == "1-A")
        {
            if(Input.GetKeyDown(KeyCode.RightArrow))
            {
                SceneManager.LoadScene("1-B");
            }
        }
        else if (scene.name == "1-B")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SceneManager.LoadScene("1-C");
            }
            else if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SceneManager.LoadScene("1-A");
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SceneManager.LoadScene("1-B");
            }
        }
    }
}
