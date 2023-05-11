using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMusicPlay : MonoBehaviour
{

    [SerializeField] List<AudioClip> sfxs = new List<AudioClip>();
    AudioSource playHere;

    // Start is called before the first frame update
    void Start()
    {
        playHere = FindObjectOfType<Camera>().transform.GetComponentInChildren<AudioSource>();
    }

    public void PlayAudio()
    {

        playHere.PlayOneShot(sfxs[Random.Range(0, sfxs.Count)]);

    }

}
