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
    
    public void JumpSound()
    {
        audioSource.PlayOneShot(audioClips[1]);
    }
    public  void LandSound()
    {
        audioSource.PlayOneShot(audioClips[2]);
    }
    public void WalkSound1()
    {
        audioSource.PlayOneShot(audioClips[3]);
    }
    public void WalkSound2()
    {
        audioSource.PlayOneShot(audioClips[4]);
    }
    public void WalkSound3()
    {
        audioSource.PlayOneShot(audioClips[5]);
    }
    public void WalkSound4()
    {
        audioSource.PlayOneShot(audioClips[6]);
    }

}
