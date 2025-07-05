using UnityEngine;
using MidiJack;

public class MidiInputExample : MonoBehaviour
{
    private float[] ccVal = new float[128];

    void Start()
    {
        for (int cc = 0; cc < 128; cc++)
        {
            ccVal[cc] = MidiMaster.GetKnob(cc);
        }
    }
    void Update()
    {
        for (int note = 0; note < 128; note++)
        {
            float velocity = MidiMaster.GetKey(note);
            if (velocity > 0)
            {
                Debug.Log("Note On: " + note + " (velocity: " + velocity + ")");
            }
        }
        for (int cc = 0; cc < 128; cc++)
        {
            float velocity = MidiMaster.GetKnob(cc);
            if (!Mathf.Approximately(velocity, ccVal[cc]))
            {
                Debug.Log("CC On: " + cc + " (velocity: " + velocity + ")");
                ccVal[cc] = velocity;
            }
        }
    }
}
