using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particles;

    public void PlayAtPosition(Vector3 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
        StartCoroutine(PlayThenDisable());
    }

    IEnumerator PlayThenDisable()
    {
        _particles.Play();
        while (_particles.isPlaying)
        {
            yield return null;
        }

        gameObject.SetActive(false);
    }

}
