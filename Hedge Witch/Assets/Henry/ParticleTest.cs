using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTest : MonoBehaviour
{

    public ParticleSystem puffParticle;
    public AudioSource itemSplash;

    public bool partPlayed;

    private void OnTriggerEnter(Collider other)
    {
        itemSplash.Play();
        puffParticle.Play();
        partPlayed = true;

        Debug.Log("Splash SFX played");

        if (partPlayed)
        {
            //puffParticle.Stop();
            //Debug.Log("Particle is destroyed after 4 seconds?");
            partPlayed=false;
        }
    }
}
