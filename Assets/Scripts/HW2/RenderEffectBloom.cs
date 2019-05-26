using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RenderEffectBloom : MonoBehaviour
{

    public Shader BloomShader;
    [Range(0.0f, 100.0f)]
    public float BloomFactor;
    public float pulseRate = 1;
    private Material screenMat;

    private float currBloom;
    private float bloomMin;
    private float bloomMax;
    private bool up;

    Material ScreenMat
    {
        get
        {
            if (screenMat == null)
            {
                screenMat = new Material(BloomShader);
                screenMat.hideFlags = HideFlags.HideAndDontSave;
            }
            return screenMat;
        }
    }


    void Start()
    {
        currBloom = BloomFactor;
        bloomMin = currBloom - 5;
        if (bloomMin < 0)
            bloomMin = 0;
        bloomMax = currBloom + 5;
        up = true;

        if (!BloomShader && !BloomShader.isSupported)
        {
            enabled = false;
            Debug.Log("garbage");
        }
       
    }

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        if (BloomShader != null)
        {
            RenderTexture brightTexture = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, 0, sourceTexture.format);
            RenderTexture blurTexture = RenderTexture.GetTemporary(sourceTexture.width, sourceTexture.height, 0, sourceTexture.format);

            ScreenMat.SetFloat("_Steps", currBloom);
            ScreenMat.SetTexture("_BaseTex", sourceTexture);
            Graphics.Blit(sourceTexture, brightTexture, ScreenMat, 0);
            Graphics.Blit(brightTexture, blurTexture, ScreenMat, 1);
            Graphics.Blit(blurTexture, destTexture, ScreenMat, 2);

            RenderTexture.ReleaseTemporary(brightTexture);
            RenderTexture.ReleaseTemporary(blurTexture);

        }
        else
        {
            Graphics.Blit(sourceTexture, destTexture);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (up)
        {
            currBloom += pulseRate * Time.deltaTime;
            if (currBloom > bloomMax)
            {
                up = false;
            }
        }
        else
        {
            currBloom -= pulseRate * Time.deltaTime;
            if (currBloom < bloomMin)
            {
                up = true;
            }
        }
    }

    void OnDisable()
    {
        if (screenMat)
        {
            DestroyImmediate(screenMat);
        }
    }
}
