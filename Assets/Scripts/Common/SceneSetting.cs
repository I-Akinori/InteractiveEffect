using System;
using UnityEngine;

namespace InteractiveEffector.Common
{
    [Serializable]
    public class SceneSetting
    {
        public SceneTitle Title;
        public float Time = 1f;
        public Material Mat0;
        public CompositeType Type0;
        public bool Inv0;
        public Material Mat1;
        public CompositeType Type1;
        public bool Inv1;
        public Material Mat2;
        public CompositeType Type2;
        public bool Inv2;
        public Material IntEffect;
        public bool EnableCpuEffect;
        public SceneTitle Next;
    }

    public enum CompositeType
    {
        AlphaBlend, 
        AdditiveBlend,
        Masking,
        Reversal
    }

    public enum SceneTitle
    {
        Dawn,
        Departure,
        Tumult,
        HeatUp,
        Flashback,
        NoisySea,
        NetWorking,
        ChillInTheSea,
        Revolving,
        
    }
}