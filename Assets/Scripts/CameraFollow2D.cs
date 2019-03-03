using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraFollow2D : MonoBehaviour
{

    [SerializeField]
    private GameObject _targetToFollow;

    [SerializeField]
    private bool _follow = true;

    [SerializeField]
    private float _cameraZOffset = -10f;
    [SerializeField]
    private float _cameraXOffset = -5f;

    // Use this for initialization
    void Start()
    {
        if (_targetToFollow == null)
        {
            Debug.Log("No target set - Camera follow disabled");
            _follow = false;
        }
    }

    private void LateUpdate()
    {
        Follow();
    }

    private void Follow()
    {
        if (_follow)
        {
            //follow ONLY on the X axis
            transform.position = new Vector3(_targetToFollow.transform.position.x + _cameraXOffset, 0, _cameraZOffset);
        }
    }
}
