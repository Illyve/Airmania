using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndpointTrigger : MonoBehaviour
{
    public GameObject levelFinish;
    public GhostController ghost;
    public Timer timer;
    public TextMeshProUGUI time;

    AudioSource source;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponentInParent<PlayerController>();
        LevelInfo levelInfo = other.GetComponentInParent<LevelInfo>();
        if (player != null && 
            levelInfo != null &&
            levelInfo.checkpointIndex == levelInfo.checkpoints.Length)
        {
            source.Play();

            ghost.Stop();
            ghost.gameObject.SetActive(false);
            timer.running = false;
            player.SaveFile(levelInfo.levelName);

            time.text = timer.GetTime(); 
            levelFinish.SetActive(true);
        }
    }
}
