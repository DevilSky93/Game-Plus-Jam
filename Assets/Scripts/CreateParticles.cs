using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateParticles : MonoBehaviour
{
    [SerializeField] private GameObject particles;
    [SerializeField] private Transform zone;
    private GameObject _inst;
    public void InstantiateParticle()
    {
        _inst = Instantiate(particles, zone.transform.position, particles.transform.rotation);
        _inst.transform.SetParent(zone);
        _inst.transform.localScale = particles.transform.localScale;
        _inst.transform.localEulerAngles = particles.transform.localEulerAngles;
    }

    public void DestroyParticle()
    {
        Destroy(_inst);
    }
}
