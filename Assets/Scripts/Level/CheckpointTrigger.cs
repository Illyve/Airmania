using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointTrigger : MonoBehaviour
{
    public int checkpointIndex;

    AudioSource audio;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<LevelInfo>() is LevelInfo levelInfo && levelInfo.checkpointIndex == checkpointIndex - 1)
        {
            levelInfo.checkpointIndex = checkpointIndex;
            audio.Play();
        }
        
    }
}
