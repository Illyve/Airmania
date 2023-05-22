using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraFOV : MonoBehaviour
{
    public AirplaneController controller;
    public int startFOV;

    CinemachineVirtualCamera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        cam.m_Lens.FieldOfView = startFOV;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.thrustPercent > 0)
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, startFOV + 20.0f, Time.deltaTime / 2.0f);
        }
        else
        {
            cam.m_Lens.FieldOfView = Mathf.Lerp(cam.m_Lens.FieldOfView, startFOV, Time.deltaTime / 2.0f);
        }
    }
}
