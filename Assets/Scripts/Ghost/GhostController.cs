using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GhostController : AirplaneController
{
    BinaryReader reader;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        string path = Application.persistentDataPath + "/tutorial.dat";
        if (File.Exists(path))
        {
            reader = new BinaryReader(new FileStream(path, FileMode.Open, FileAccess.Read));
            gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if (reader != null)
        {
            if (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                try
                {
                    byte count = reader.ReadByte();
                    while (count > 0)
                    {
                        switch (reader.ReadChar())
                        {
                            case 'P':
                                Pitch = reader.ReadSingle();
                                break;
                            case 'R':
                                Roll = reader.ReadSingle();
                                break;
                            case 'Y':
                                Yaw = reader.ReadSingle();
                                break;
                            case 'T':
                                thrustPercent = thrustPercent > 0 ? 0 : 1f;
                                break;
                            case 'F':
                                Flap = Flap > 0 ? 0 : 0.3f;
                                break;
                            case 'B':
                                brakesTorque = brakesTorque > 0 ? 0 : 500f;
                                break;
                        }
                        count--;
                    }
                }
                catch (IOException)
                {
                    reader.Close();
                    reader = null;
                    gameObject.SetActive(false);
                }
            }
            else
            {
                reader.Close();
                reader = null;
                gameObject.SetActive(false);
            }
        }

        base.FixedUpdate();
    }
}
