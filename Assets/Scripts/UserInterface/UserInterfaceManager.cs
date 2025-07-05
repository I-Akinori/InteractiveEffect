using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using MidiJack;

namespace InteractiveEffector.UserInterface
{
    public class KeyInformation
    {
        public KeyInformation(KeyCode key, int note)
        {
            Key = key;
            Note = note;
        }
        public KeyCode Key;
        public int Note;
        public float Time = 0;
        public bool Down = false;
        public bool PadTouched = true;
    }
    public static class UserInterfaceManager
    {
        public static float[] InputTimes => times;
        public static List<KeyInformation> KeyInformations => keyInformations;
        private static float duration = 2f;
        private static float[] times = new float[16];
        private static List<KeyInformation> keyInformations = new List<KeyInformation>()
        {
            new KeyInformation(KeyCode.Alpha1, 80),
            new KeyInformation(KeyCode.Alpha2, 81),
            new KeyInformation(KeyCode.Alpha3, 82),
            new KeyInformation(KeyCode.Alpha4, 83),
            new KeyInformation(KeyCode.Alpha5, 76),
            new KeyInformation(KeyCode.Alpha6, 77),
            new KeyInformation(KeyCode.Alpha7, 78),
            new KeyInformation(KeyCode.Alpha8, 79),
            new KeyInformation(KeyCode.Alpha9, 72),
            new KeyInformation(KeyCode.Alpha0, 73),
            
            new KeyInformation(KeyCode.Space, 74),
            new KeyInformation(KeyCode.Q, 75),
            new KeyInformation(KeyCode.W, 68),
            new KeyInformation(KeyCode.E, 69),
            new KeyInformation(KeyCode.R, 70),
            new KeyInformation(KeyCode.T, 71),
        };
        
        public static void UpdateInput(float dt)
        {
            times = new float [keyInformations.Count];
            for (var i = 0; i < keyInformations.Count; i++)
            {
                var keyInfo = keyInformations[i];
                keyInfo.Time -= dt;
                keyInfo.Time = Mathf.Max(0, keyInfo.Time);
                keyInfo.Down = false;
                if (Input.GetKeyDown(keyInfo.Key) 
                    || (MidiMaster.GetKey(keyInfo.Note) > 0 && !keyInfo.PadTouched))
                {
                    if (!keyInfo.PadTouched)
                    {
                        keyInfo.PadTouched = true;
                    }
                    keyInfo.Time = duration;
                    keyInfo.Down = true;
                }

                if (MidiMaster.GetKey(keyInfo.Note) == 0)
                {
                    keyInfo.PadTouched = false;
                }
                times[i] = keyInfo.Time;
            }
        }
    }
    
    
}