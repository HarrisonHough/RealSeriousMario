using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableBrick : MonoBehaviour, IDestructable
{
    public void Destroy()
    {
        ParticleEffect particles = GameManager.Instance.destroyParticlesPool.GetObject().GetComponent<ParticleEffect>();

        particles.PlayAtPosition(transform.position);
        gameObject.SetActive(false);
    }
}
