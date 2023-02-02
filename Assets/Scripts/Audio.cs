using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public static Audio instance { get; private set; }

    AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
        Debug.Log($"Sound played: {clip}");
    }
}
