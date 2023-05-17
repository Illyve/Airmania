using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : AirplaneController
{
    [SerializeField]
    Text displayText;

    InputRecorder recorder;
    LevelInfo levelInfo;

    bool thrust;
    bool flap;
    bool brake;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        recorder = new InputRecorder();
        levelInfo = GetComponent<LevelInfo>();
    }

    // Update is called once per frame
    void Update()
    {
        Pitch = Input.GetAxis("Vertical");
        Roll = Input.GetAxis("Horizontal");
        Yaw = Input.GetAxis("Yaw");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            thrust = true;
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            flap = true;
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            brake = true;
        }

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
    }

    protected override void FixedUpdate()
    {
        recorder.Step(Pitch, Roll, Yaw, thrust, flap, brake);

        thrust = false;
        flap = false;
        brake = false;

        displayText.text = "V: " + ((int)rb.velocity.magnitude).ToString("D3") + " m/s\n";
        displayText.text += "A: " + ((int)transform.position.y).ToString("D4") + " m\n";
        displayText.text += "T: " + (int)(thrustPercent * 100) + "%\n";
        displayText.text += brakesTorque > 0 ? "B: ON" : "B: OFF" + "\n";
        displayText.text += levelInfo.checkpointIndex;

        base.FixedUpdate();
    }

    public void SaveFile(string level) => recorder.SaveFile(level);
}
