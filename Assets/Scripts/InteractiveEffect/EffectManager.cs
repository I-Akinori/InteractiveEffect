using System.Collections.Generic;
using UnityEngine;

namespace InteractiveEffector.InteractiveEffect
{
    public class EffectManager : MonoBehaviour
    {
        [SerializeField] private Effect src;
        private List<Effect> effects = new List<Effect>();
        
        // Update is called once per frame
        void Update()
        {
            var delta = Time.deltaTime;
            for (var i = effects.Count - 1; i >= 0; i--)
            {
                var e = effects[i];
                if (e.Step(delta))
                {
                    e.Dispose();
                    effects.RemoveAt(i);
                }
            }
        }

        public void GenerateEffect()
        {
            var efct = Instantiate(src, transform, true);
            efct.transform.localPosition = new Vector3(
                Random.Range(-4f, 4f),
                Random.Range(-2f, 2f), 
                5
            );
            effects.Add(efct);
        } 
    }
}