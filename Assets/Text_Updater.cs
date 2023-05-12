using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Text_Updater : MonoBehaviour
{
    Text timeDisplayText = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        timeDisplayText.text = "Does this work" + " m/s\n";
    }
}