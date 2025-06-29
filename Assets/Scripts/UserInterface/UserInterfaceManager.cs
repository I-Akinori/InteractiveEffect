using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InteractiveEffector.UserInterface
{
    public class KeyInformation
    {
        public KeyInformation(KeyCode key)
        {
            Key = key;
        }
        public KeyCode Key;
        public float Time = 0;
        public bool Down = false;
    }
    public static class UserInterfaceManager
    {
        public static float[] InputTimes => times;
        public static List<KeyInformation> KeyInformations => keyInformations;
        private static float duration = 2f;
        private static float[] times = new float[16];
        private static List<KeyInformation> keyInformations = new List<KeyInformation>()
        {
            new KeyInformation(KeyCode.Alpha1),
            new KeyInformation(KeyCode.Alpha2),
            new KeyInformation(KeyCode.Alpha3),
            new KeyInformation(KeyCode.Alpha4),
            new KeyInformation(KeyCode.Alpha5),
            new KeyInformation(KeyCode.Alpha6),
            new KeyInformation(KeyCode.Alpha7),
            new KeyInformation(KeyCode.Alpha8),
            new KeyInformation(KeyCode.Alpha9),
            new KeyInformation(KeyCode.Alpha0),
            
            new KeyInformation(KeyCode.Space),
            new KeyInformation(KeyCode.Q),
            new KeyInformation(KeyCode.W),
            new KeyInformation(KeyCode.E),
            new KeyInformation(KeyCode.R),
            new KeyInformation(KeyCode.T),
        };
        
        public static void UpdateInput(float dt)
        {
            times = new float [keyInformations.Count];
            for (var i = 0; i < keyInformations.Count; i++)
            {
                keyInformations[i].Time -= dt;
                keyInformations[i].Time = Mathf.Max(0, keyInformations[i].Time);
                keyInformations[i].Down = false;
                if (Input.GetKeyDown(keyInformations[i].Key))
                {
                    keyInformations[i].Time = duration;
                    keyInformations[i].Down = true;
                }
                times[i] = keyInformations[i].Time;
            }
        }
    }
    
    
}