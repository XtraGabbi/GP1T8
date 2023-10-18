using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSource : MonoBehaviour
{
    public GameObject lightBeam;

    public bool isOn;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Toggle()
    {
        if (isOn)
        {
            turnOffTheLight();
        }
        else
        {
            turnOnTheLight();
        }
    }

    public void turnOnTheLight()
    {
        if (isOn) return;

        isOn = true;
        lightBeam.SetActive(true);
        LightManager.instance.ShiningLight = this;
    }

    public void turnOffTheLight()
    {
        if (!isOn) return;

        isOn = false;
        lightBeam.SetActive(false);
    }

    public void shootLightTowardsTargetPosition(Vector3 targetPos)
    {
        if ((targetPos - transform.position).magnitude < 0.001f) return;
        lightBeam.transform.forward = targetPos - transform.position;
    }
}