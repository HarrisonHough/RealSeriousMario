using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    private Vector2[] _localWaypoints;
    private Vector3[] _globalWaypoints;

    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private bool _cyclic = false;
    [SerializeField]
    private float _waitTime;
    [Range(0, 2)]
    public float easeAmout;
    private float _nextMoveTime;
    private int _fromWaypointIndex;
    private float _percentBetweenWaypoints;

    private Vector3 _velocity;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _globalWaypoints = new Vector3[_localWaypoints.Length];
        for (int i = 0; i < _globalWaypoints.Length; i++)
        {
            _globalWaypoints[i] = (Vector3)_localWaypoints[i] + transform.position;
        }
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _velocity = CalculatePlatformMovement();
        
    }

    private void FixedUpdate()
    {
        //transform.Translate(_velocity);
        _rigidbody2D.MovePosition(transform.position + _velocity);
    }

    private Vector3 CalculatePlatformMovement()
    {
        if (Time.time < _nextMoveTime)
        {
            return Vector3.zero;
        }

        _fromWaypointIndex %= _globalWaypoints.Length;
        int toWaypointIndex = (_fromWaypointIndex + 1) % _globalWaypoints.Length;

        float distanceBetweenWaypoints = Vector3.Distance(_globalWaypoints[_fromWaypointIndex], _globalWaypoints[toWaypointIndex]);
        _percentBetweenWaypoints += Time.deltaTime * _speed / distanceBetweenWaypoints;
        _percentBetweenWaypoints = Mathf.Clamp01(_percentBetweenWaypoints);
        float easedPercentBetweenWaypoints = Ease(_percentBetweenWaypoints);

        Vector3 newPos = Vector3.Lerp(_globalWaypoints[_fromWaypointIndex], _globalWaypoints[toWaypointIndex], easedPercentBetweenWaypoints);

        if (_percentBetweenWaypoints >= 1)
        {

            _percentBetweenWaypoints = 0;
            _fromWaypointIndex++;
            //have to reverse array order if cyclic
            if (!_cyclic)
            {
                if (_fromWaypointIndex >= _globalWaypoints.Length - 1)
                {
                    _fromWaypointIndex = 0;
                    System.Array.Reverse(_globalWaypoints);
                }
            }
            _nextMoveTime = Time.time + _waitTime;
        }

        return newPos - transform.position;
    }

    private float Ease(float x)
    {
        float a = easeAmout + 1;
        return Mathf.Pow(x, a) / (Mathf.Pow(x, a) + Mathf.Pow(1 - x, a));
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float size = 0.3f;

        for (int i = 0; i < _localWaypoints.Length; i++)
        {
            Vector3 globalWaypointPosition = (Application.isPlaying) ? _globalWaypoints[i] : (Vector3)_localWaypoints[i] + transform.position;
            Gizmos.DrawLine(globalWaypointPosition - Vector3.up * size, globalWaypointPosition + Vector3.up * size);
            Gizmos.DrawLine(globalWaypointPosition - Vector3.left * size, globalWaypointPosition + Vector3.left * size);
        }
    }
}
