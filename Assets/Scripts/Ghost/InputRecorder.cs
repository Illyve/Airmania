using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class InputRecorder
{
    BinaryWriter inputs;

    public InputRecorder()
    {
         inputs = new BinaryWriter(new MemoryStream(4096));
    }

    // Update is called once per frame
    public void Step(float pitch, float roll, float yaw, bool thrust, bool flap, bool brake)
    {
        byte count = 0;

        if (pitch != 0.0f)
        {
            count++;
        }
        if (roll != 0.0f)
        {
            count++;
        }
        if (yaw != 0.0f)
        {
            count++;
        }
        if (thrust)
        {
            count++;
        }
        if (flap)
        {
            count++;
        }
        if (brake)
        {
            count++;
        }

        inputs.Write(count);
        if (pitch != 0.0f)
        {
            inputs.Write('P');
            inputs.Write(pitch);
        }
        if (roll != 0.0f)
        {
            inputs.Write('R');
            inputs.Write(roll);
        }
        if (yaw != 0.0f)
        {
            inputs.Write('Y');
            inputs.Write(yaw);
        }
        if (thrust)
        {
            inputs.Write('T');
        }
        if (flap)
        {
            inputs.Write('F');
        }
        if (brake)
        {
            inputs.Write('B');
        }
    }

    public void SaveFile(string file)
    {
        string path = Application.persistentDataPath + "/" + file + ".dat";
        FileStream f = File.Create(path);

        long pos = inputs.BaseStream.Position;
        inputs.BaseStream.Position = 0;
        inputs.BaseStream.CopyTo(f);

        f.Close();
        inputs.BaseStream.Position = pos;
    }
}
