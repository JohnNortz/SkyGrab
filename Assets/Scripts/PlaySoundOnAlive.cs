using UnityEngine;
using System.Collections;

public class PlaySoundOnAlive : MonoBehaviour {

    public AudioSource source;
    public AudioClip clip;
    public float pitchVariance;
    public bool looping;
	// Update is called once per frame
    void Start () 
    {
        if (!looping) audio.PlayOneShot(clip);
        if (source != null) source.pitch = source.pitch + (Random.Range(pitchVariance, -pitchVariance));
    }

}
