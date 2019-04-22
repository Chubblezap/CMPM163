using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, 45 * Time.deltaTime, 0));
        transform.Translate(new Vector3(0, 0, 1.58f * Time.deltaTime));
    }
}
