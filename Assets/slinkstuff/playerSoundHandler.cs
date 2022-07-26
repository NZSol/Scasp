using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSoundHandler : MonoBehaviour
{
    [SerializeField] AudioSource aud, loaderAud;
    [SerializeField] AudioClip[] walkSounds;
    [SerializeField] AudioClip pickUpShellSound, loadShellSound;
    [SerializeField] float footstepSoundVol;

    private void Awake()
    {
        loaderAud = GameObject.Find("Cockpit View/COCKPIT/Gun Load Audio").GetComponent<AudioSource>();
    }

    public void playFootstepSound()
    {
        aud.PlayOneShot(walkSounds[Random.Range(0, walkSounds.Length)]);
    }

    public void playPickUpShellSound()
    {
        aud.PlayOneShot(pickUpShellSound);
    }

    public void playLoadShellSound()
    {
        loaderAud.PlayOneShot(loadShellSound, 0.5f);
    }

}
