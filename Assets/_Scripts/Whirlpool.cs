using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class Whirlpool : MonoBehaviour
{
    public bool DrawGizmos;
    public bool play = true;
    private bool isPlaying = true;
    public bool isSucking = true;
    public float outerRange = 5f;
    public float deadZoneRange = 3f;
    public float innerRange = 1f;
    public float spinStrength = 10f;
    public float duration = 5;
    public float suckingTime = 1;
    public ParticleSystem[] ParticleSystems;
    private Volvox _volvox;
    private Rigidbody _volvoxRb;
    private float _distToVolvox;
    private Vector3 _centripetalVector;
    [SerializeField]private float _desiredDist;
    [SerializeField]private float _dist;
    private Vector3 _centripetalDir;
    private Vector3 _tangentDir;

    // Start is called before the first frame update

    private void Awake()
    {
        foreach (var particleSystem in ParticleSystems)
        {
            particleSystem.Stop();
            ParticleSystem.MainModule main = particleSystem.main;
            main.duration = duration;
            particleSystem.Play();
        }
    }

    void Start()
    {
        transform.localScale = Vector3.one * outerRange;
        _volvox = Volvox.Instance;
        _volvoxRb = _volvox.rb;
        _centripetalVector = transform.position - _volvox.transform.position;
        _dist = _centripetalVector.magnitude;
    }

    void OnDrawGizmos()
    {
        if (DrawGizmos)
        {
            deadZoneRange = Mathf.Min(outerRange, deadZoneRange);
            innerRange = Mathf.Min(innerRange, deadZoneRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, outerRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, deadZoneRange);
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(this.transform.position, innerRange);
            transform.localScale = Vector3.one * outerRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _centripetalVector = transform.position - _volvox.transform.position;
        _dist = _centripetalVector.magnitude;

        if (!isPlaying && play)
        {
            foreach (var particleSystem in ParticleSystems)
            {
                particleSystem.Play();
            }

            isPlaying = true;
        }
        else if (isPlaying && !play)
        {
            foreach (var particleSystem in ParticleSystems)
            {
                particleSystem.Stop();
            }
            
            isPlaying = false;
        }
        
        Debug.DrawLine(_volvox.transform.position, _volvox.transform.position + _volvoxRb.velocity.normalized);
    }

    private void FixedUpdate()
    {
        if (_dist < outerRange && isSucking)
        {
            // _desiredDist = _dist;
            _centripetalDir = _centripetalVector.normalized;

            // spin counter clockwise;
            if (Vector3.Dot(Vector3.up, Vector3.Cross(_centripetalDir, _volvoxRb.velocity)) > 0)
            {
                _tangentDir = Vector3.Cross(Vector3.up, _centripetalDir);
            }
            // spin clockwise;
            else
            {
                _tangentDir = Vector3.Cross(Vector3.down, _centripetalDir);
            }

            // float speed = _volvoxRb.velocity.magnitude;
            // speed += spinStrength / Time.fixedDeltaTime;
            // _volvoxRb.velocity = _volvoxRb.velocity.normalized * speed;
            // _volvoxRb.velocity = Quaternion.FromToRotation(_volvoxRb.velocity.normalized, _tangentDir) * _volvoxRb.velocity;
            
            Vector3 centripetalAccel = _centripetalDir * Mathf.Pow(_volvoxRb.velocity.magnitude, 2) / _desiredDist;
            _volvoxRb.AddForce(centripetalAccel, ForceMode.Acceleration);
            
            Vector3 tangentAccel = _tangentDir * spinStrength;
            _volvoxRb.AddForce(tangentAccel, ForceMode.Acceleration);


            // drag into the center (inner range)
            if (_desiredDist > innerRange)
            {
                _desiredDist *= 0.99f;
            }
            
            // 

            else if (_dist < deadZoneRange)
            {
                // _volvox.isFollowing = false;
            }
            
            
            // within inner range, start 
            else
            {
                // StartCoroutine(Sucking());
                // isSucking = true;
            }
        }
        else
        {
            _desiredDist = outerRange;
            // isSucking = false;
            // _volvox.isFollowing = true;
        }
    }

    IEnumerator Sucking()
    {
        _volvox.RemoveColony();
        yield return new WaitForSeconds(suckingTime);
    }

    public bool IsInDeadZone()
    {
        return _dist < deadZoneRange;
    }
}