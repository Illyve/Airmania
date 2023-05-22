using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.volume = PlayerPrefs.GetInt("Volume", 100) / 100.0f;    
    }

    // Update is called once per frame
    void Update()
    {
        source.volume = PlayerPrefs.GetInt("Volume", 100) / 100.0f;
    }
}
