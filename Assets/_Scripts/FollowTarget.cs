using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class FollowTarget : MonoBehaviour
{
    public static FollowTarget Instance;
    // Start is called before the first frame update

    [FormerlySerializedAs("targetlerpSpeed")] public float speedFactor = 0.1f;
    private PlayerManager _playerManager;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    void Start()
    {
        _playerManager = PlayerManager.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerManager.lightSourceLeft.isOn && _playerManager.lightSourceRight.isOn)
        {

            Vector3 leftLightProjectedPos = GetLightSourceProjectedPosion(_playerManager.lightSourceLeft);
            Vector3 rightLightProjectedPos = GetLightSourceProjectedPosion(_playerManager.lightSourceRight);
            Vector3 dirBetweenLight = (rightLightProjectedPos - leftLightProjectedPos).normalized;
            Vector3 nearestPoint = Vector3.Dot(transform.position - leftLightProjectedPos, dirBetweenLight) * dirBetweenLight +
                                   leftLightProjectedPos;
            
            transform.position = Vector3.Lerp(transform.position,nearestPoint, speedFactor * Time.deltaTime);
        }
        else if (_playerManager.lightSourceLeft.isOn || _playerManager.lightSourceRight.isOn)
        {
            LightSource light = _playerManager.lightSourceLeft.isOn
                ? _playerManager.lightSourceLeft
                : _playerManager.lightSourceRight;
            Vector3 lightSorcePos = GetLightSourceProjectedPosion(light);
            transform.position = Vector3.Lerp(transform.position,
                lightSorcePos, speedFactor * Time.deltaTime);
        }
        else
        {
        }
    }

    public Vector3 GetLightSourceProjectedPosion(LightSource lightSource)
    {
        Vector3 lightSourcePos = lightSource.transform.position;
        Vector3 lightSourceProjectDir = (lightSourcePos - Camera.main.transform.position).normalized;
        Vector3 lightSourceProjectedPos = lightSourcePos + lightSourceProjectDir * Vector3.Dot(lightSourcePos, Vector3.up);
        return lightSourceProjectedPos;
    }
}