using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputRecorder : MonoBehaviour
{
    BinaryWriter inputs;

    void Start()
    {
         inputs = new BinaryWriter(new MemoryStream(4096));
    }

    // Update is called once per frame
    void Update()
    {
        float pitch = Input.GetAxis("Vertical");
        if (pitch != 0.0f)
        {
            inputs.Write('P');
            inputs.Write(pitch);
        }

        float roll = Input.GetAxis("Horizontal");
        if (roll != 0.0f)
        {
            inputs.Write('R');
            inputs.Write(roll);
        }

        float yaw = Input.GetAxis("Yaw");
        if (yaw != 0.0f)
        {
            inputs.Write('Y');
            inputs.Write(yaw);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inputs.Write('T');
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            inputs.Write('F');
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            inputs.Write('B');
        }
    }
}
