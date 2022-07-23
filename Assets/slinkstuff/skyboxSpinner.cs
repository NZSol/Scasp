using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skyboxSpinner : MonoBehaviour
{
    [SerializeField] float spinSpeed;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", RenderSettings.skybox.GetFloat("_Rotation") + spinSpeed * Time.deltaTime);
        if (RenderSettings.skybox.GetFloat("_Rotation") > 360) RenderSettings.skybox.SetFloat("_Rotation", 0); 
    }
}
