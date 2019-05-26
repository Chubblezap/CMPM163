using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    Renderer render;
    ParticleSystem ps;
    float timer;
    float curExAmnt;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        curExAmnt = 1;
        render = GetComponent<ParticleSystemRenderer>();
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
            timer = 0;
        render.material.SetFloat("_DissolveAmount", timer);

        var sz = ps.sizeOverLifetime;

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            curExAmnt += 0.1f;
            render.material.SetFloat("_ExplodeAmount", curExAmnt);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            curExAmnt -= 0.1f;
            render.material.SetFloat("_ExplodeAmount", curExAmnt);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            sz.sizeMultiplier += 10;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            sz.sizeMultiplier -= 10;
        }
    }
}
