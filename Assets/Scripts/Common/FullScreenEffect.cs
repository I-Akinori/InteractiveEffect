using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InteractiveEffector.Common
{
    [ExecuteInEditMode]
    public class FullScreenEffect : MonoBehaviour
    {
        public Material InteractiveEffector
        {
            get => interactiveEffector;
            set => interactiveEffector = value; 
        }
        
        public float Bright
        {
            get => bright;
            set => bright = value;
        }
        public float CustomTime
        {
            get => time;
            set => time = value;
        }
        public CompositeType Type0
        {
            get => type0;
            set => type0 = value;
        }
        public float Ratio0
        {
            get => ratio0;
            set => ratio0 = value;
        }
        public CompositeType Type1
        {
            get => type1;
            set => type1 = value;
        }
        public float Ratio1
        {
            get => ratio1;
            set => ratio1 = value;
        }
        public CompositeType Type2
        {
            get => type2;
            set => type2 = value;
        }
        public float Ratio2
        {
            get => ratio2;
            set => ratio2 = value;
        }
        [SerializeField] private Material compositor;
        [SerializeField] private Material interactiveEffector;
        [SerializeField] private Material simpleEffector;
        [SerializeField] private List<RenderTexture> channels = new List<RenderTexture>();
        [SerializeField] private RenderTexture interactiveEffect;
        [SerializeField] private RenderTexture tempBuffer0;
        [SerializeField] private RenderTexture tempBuffer1;

        [SerializeField] private bool vignette = true;
        [SerializeField] private float bright = 1;
        [SerializeField] private float time = 0;
        
        [SerializeField] private CompositeType type0 = CompositeType.AlphaBlend;
        [SerializeField] private float ratio0 = 1;
        [SerializeField] private CompositeType type1 = CompositeType.AlphaBlend;
        [SerializeField] private float ratio1 = 1;
        [SerializeField] private CompositeType type2 = CompositeType.AlphaBlend;
        [SerializeField] private float ratio2 = 1;

        void OnRenderImage(RenderTexture src, RenderTexture dest)
        {
            // Channel 0
            if (compositor != null)
            {
                compositor.SetTexture("_BaseTex", src);
                compositor.SetTexture("_OverTex", channels[0]);
                compositor.SetInt("_CompositeType", (int)type0);
                compositor.SetFloat("_OverRatio", ratio0);
                Graphics.Blit(null, tempBuffer0, compositor);
            }
            else
            {
                Graphics.Blit(src, tempBuffer0);
            }
            
            // Channel 1
            if (compositor != null)
            {
                compositor.SetTexture("_BaseTex", tempBuffer0);
                compositor.SetTexture("_OverTex", channels[1]);
                compositor.SetInt("_CompositeType", (int)type1);
                compositor.SetFloat("_OverRatio", ratio1);
                Graphics.Blit(null, tempBuffer1, compositor);
            }
            else
            {
                Graphics.Blit(src, tempBuffer0);
            }
            
            // Channel 2
            if (compositor != null)
            {
                compositor.SetTexture("_BaseTex", tempBuffer1);
                compositor.SetTexture("_OverTex", channels[2]);
                compositor.SetInt("_CompositeType", (int)type2);
                compositor.SetFloat("_OverRatio", ratio2);
                Graphics.Blit(null, tempBuffer0, compositor);
            }
            else
            {
                Graphics.Blit(src, tempBuffer1);
            }

            if (interactiveEffector != null)
            {
                interactiveEffector.SetTexture("_BaseTex", tempBuffer0);
                interactiveEffector.SetTexture("_OverTex", interactiveEffect);
                Graphics.Blit(null, tempBuffer1, interactiveEffector);
            }
            
            if (simpleEffector != null)
            {
                simpleEffector.SetTexture("_MainTex", tempBuffer1);
                simpleEffector.SetFloat("_EnableVignette", vignette ? 1f : 0f);
                simpleEffector.SetFloat("_Bright", bright);
                Graphics.Blit(null, dest, simpleEffector);
                return;
            }
            Graphics.Blit(tempBuffer0, dest);
        }

        public void Setup(SceneSetting sceneSetting)
        {
            
        }

        public void SetRenderTexture(RenderTexture rt, int index)
        {
            if (index > channels.Count - 1)
            {
                Debug.LogError("Index out of range");
                return;
            }

            channels[index] = rt;
        }
    }
}