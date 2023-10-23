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
            Vector3 leftLightPos = _playerManager.lightSourceLeft.transform.position;
            leftLightPos.y = 0;
            Vector3 rightLightPos = _playerManager.lightSourceRight.transform.position;
            rightLightPos.y = 0;
            Vector3 dirBetweenLight = (rightLightPos - leftLightPos).normalized;
            Vector3 nearestPoint = Vector3.Dot(transform.position - leftLightPos, dirBetweenLight) * dirBetweenLight +
                                   leftLightPos;
            
            transform.position = Vector3.Lerp(transform.position,nearestPoint, speedFactor * Time.deltaTime);
        }
        else if (_playerManager.lightSourceLeft.isOn || _playerManager.lightSourceRight.isOn)
        {
            LightSource light = _playerManager.lightSourceLeft.isOn
                ? _playerManager.lightSourceLeft
                : _playerManager.lightSourceRight;
            Vector3 lightSorcePos = light.transform.position;
            lightSorcePos.y = 0;
            transform.position = Vector3.Lerp(transform.position,
                lightSorcePos, speedFactor * Time.deltaTime);
        }
        else
        {
        }
    }
}