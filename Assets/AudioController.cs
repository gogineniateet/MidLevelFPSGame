using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public List<AudioClip> audioClips;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void ShotFire()
    {
        audioSource.PlayOneShot(audioClips[0]);
    }
    public void WalkSound()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public void JumpSound()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public  void LandSound()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
}
