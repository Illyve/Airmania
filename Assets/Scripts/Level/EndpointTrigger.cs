using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndpointTrigger : MonoBehaviour
{
    public GameObject levelFinish;
    public GhostController ghost;

    // Start is called before the first frame update
    void Start()
    {

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
            ghost.Stop();
            ghost.gameObject.SetActive(false);
            player.SaveFile(levelInfo.levelName);
            levelFinish.SetActive(true);
        }
    }
}
