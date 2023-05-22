using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AudioSettings : MonoBehaviour
{
    Slider slider;
    TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponentInChildren<Slider>();
        text = GetComponentInChildren<TextMeshProUGUI>();
        slider.value = PlayerPrefs.GetInt("Volume", 100);
        text.text = slider.value.ToString("0");
        AudioListener.volume = slider.value / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVolume()
    {
        text.text = slider.value.ToString("0");
        PlayerPrefs.SetInt("Volume", (int)slider.value);
        AudioListener.volume = slider.value / 100.0f;
    }
}
