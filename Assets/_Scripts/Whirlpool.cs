using System.Collections;
using UnityEngine;

public class Whirlpool : MonoBehaviour
{
    public bool DrawGizmos;
    public bool isSucked = false;
    public float outerRange = 5f;
    public float innerRange = 3f;
    public float spinStrength = 10f;
    public float suckingTime = 1;
    private Volvox _volvox;
    private Rigidbody _volvoxRb;
    private float _distToVolvox;
    private Vector3 _centripetalVector;
    private float _desiredDist;
    private float _dist;
    private Vector3 _centripetalDir;
    private Vector3 _tangentDir;

    // Start is called before the first frame update
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
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(this.transform.position, outerRange);
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, innerRange);
            transform.localScale = Vector3.one * outerRange;
        }
    }

    // Update is called once per frame
    void Update()
    {
        _centripetalVector = transform.position - _volvox.transform.position;
        _dist = _centripetalVector.magnitude;
    }

    private void FixedUpdate()
    {
        if (_dist < outerRange && !isSucked)
        {
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

            Vector3 tangentAccel = _tangentDir * spinStrength;
            _volvoxRb.AddForce(tangentAccel, ForceMode.Acceleration);

            Vector3 centripetalAccel = _centripetalDir * Mathf.Pow(_volvoxRb.velocity.magnitude, 2) / _desiredDist;
            _volvoxRb.AddForce(centripetalAccel, ForceMode.Acceleration);

            // drag into the center (inner range)
            if (_desiredDist > innerRange)
            {
                _desiredDist *= 0.99f;
            }
            // within inner range, start 
            else
            {
                StartCoroutine(Sucking());
                isSucked = true;
            }
        }
        else
        {
            _desiredDist = outerRange;
            isSucked = false;
        }
    }

    IEnumerator Sucking()
    {
        _volvox.RemoveColony();
        yield return new WaitForSeconds(suckingTime);
        
    }
}