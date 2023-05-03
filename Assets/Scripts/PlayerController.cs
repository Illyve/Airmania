using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : AirplaneController
{
    [SerializeField]
    Text displayText = null;

    InputRecorder recorder;


    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        recorder = new InputRecorder();
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void FixedUpdate()
    {
        Pitch = Input.GetAxis("Vertical");
        Roll = Input.GetAxis("Horizontal");
        Yaw = Input.GetAxis("Yaw");

        bool thrust = Input.GetKeyDown(KeyCode.Space);
        bool flap = Input.GetKeyDown(KeyCode.F);
        bool brake = Input.GetKeyDown(KeyCode.B);

        if (thrust)
        {
            thrustPercent = thrustPercent > 0 ? 0 : 1f;
        }

        if (flap)
        {
            Flap = Flap > 0 ? 0 : 0.3f;
        }

        if (brake)
        {
            brakesTorque = brakesTorque > 0 ? 0 : 500f;
        }

        recorder.Step(Pitch, Roll, Yaw, thrust, flap, brake);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            recorder.SaveFile("tutorial");
        }

        displayText.text = "V: " + ((int)rb.velocity.magnitude).ToString("D3") + " m/s\n";
        displayText.text += "A: " + ((int)transform.position.y).ToString("D4") + " m\n";
        displayText.text += "T: " + (int)(thrustPercent * 100) + "%\n";
        displayText.text += brakesTorque > 0 ? "B: ON" : "B: OFF";

        base.FixedUpdate();
    }
}
