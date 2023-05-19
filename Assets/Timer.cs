using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    public bool running;

    TextMeshProUGUI text;
    float elapsedSeconds;
    bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && Input.GetKeyDown(KeyCode.Space))
        {
            started = true;
            running = true;
        }

        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            TimeSpan time = TimeSpan.FromSeconds(elapsedSeconds);
            if (time.Hours > 0)
            {
                text.text = time.ToString("hh\\:mm\\:ss\\:ff");
            }
            else
            {
                text.text = time.ToString("mm\\:ss\\:ff");
            }
        }
    }

    public string GetTime()
    {
        return text.text;
    }
}
