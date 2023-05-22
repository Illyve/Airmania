using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedReplay : MonoBehaviour
{
    public GameObject player;
    LevelInfo levelInfo;

    // Start is called before the first frame update
    void Start()
    {
        levelInfo = player.GetComponent<LevelInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!player.activeInHierarchy && levelInfo.checkpointIndex != levelInfo.checkpoints.Length)   
        {
            StartCoroutine(Replay());
        }
    }

    IEnumerator Replay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
