using UnityEngine;
using System.Collections;

public class SphereSounds : MonoBehaviour {

    private AudioSource audioSource;
    private AudioClip impactClip, rollingClip;
    private bool rolling;
	// Use this for initialization
	void Start () {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialize = true;
        audioSource.spatialBlend = 1f;
        audioSource.dopplerLevel = 0f;
        audioSource.rolloffMode = AudioRolloffMode.Logarithmic;
        audioSource.maxDistance = 20f;

        impactClip = Resources.Load<AudioClip>("Impact");
        rollingClip = Resources.Load<AudioClip>("Rolling");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision){
        if(collision.relativeVelocity.magnitude >= 0.1f){
            audioSource.clip = impactClip;
            audioSource.Play();
        }
    }

    void OnCollisionStay(Collision collision){
        if (!rolling && collision.relativeVelocity.magnitude >= 0.1f){
            rolling = true;
            audioSource.clip = rollingClip;
            audioSource.Play();
        }
        else if (rolling && collision.relativeVelocity.magnitude < 0.1f){
            rolling = false;
            audioSource.Stop();
        }
    }

    void OnCollisionExit(Collision collision){
        if (rolling){
            audioSource.Stop();
            rolling = false;
        }
    }
}
