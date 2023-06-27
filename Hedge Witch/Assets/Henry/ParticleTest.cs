using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{

    public ParticleSystem splashParticle;


    private void OnTriggerEnter(Collider other)
    {
        splashParticle.Play();

    }

}
