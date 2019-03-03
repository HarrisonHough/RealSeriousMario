using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField]
    private float _shakeDuration = 0.2f;
    [SerializeField]
    private float _shakeMagnitude = 0.4f;

    public void StartShakeEffect()
    {
        StartCoroutine(ShakeRoutine());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartShakeEffect();
        }
    }

    IEnumerator ShakeRoutine()
    {
        Vector3 startPosition = transform.localPosition;

        float elapsedTime = 0.0f;

        while (elapsedTime < _shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * _shakeMagnitude;
            float y = Random.Range(-1f, 1f) * _shakeMagnitude;

            transform.localPosition = new Vector3(x, y, startPosition.z);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = startPosition;
       
    }
}
