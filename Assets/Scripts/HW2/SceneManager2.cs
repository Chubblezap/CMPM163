using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager2 : MonoBehaviour
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
        if (scene.name == "2-A")
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SceneManager.LoadScene("2-B");
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SceneManager.LoadScene("2-A");
        }
    }
}
