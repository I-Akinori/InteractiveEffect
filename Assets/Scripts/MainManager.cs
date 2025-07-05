using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using InteractiveEffector.Common;
using InteractiveEffector.InteractiveEffect;
using InteractiveEffector.UserInterface;
using Debug = UnityEngine.Debug;

namespace InteractiveEffector
{
    public class MainManager : MonoBehaviour
    {
        [SerializeField] private float time = 0;
        [SerializeField] private float loopTime = 0;
        [SerializeField] private bool enableInteractiveCPU = true;

        [SerializeField] private FullScreenEffect screen = null;
        [SerializeField] private EffectManager effectManager = null;

        [SerializeField] private TextureGenerator channel0 = null;
        [SerializeField] private TextureGenerator channel1 = null;
        [SerializeField] private TextureGenerator channel2 = null;

        [SerializeField] private SceneTitle sceneTitle;
        [SerializeField] private List<SceneSetting> sceneSettings = new List<SceneSetting>();
        [SerializeField] private List<AudioSource> sounds = new List<AudioSource>();

        private SceneSetting currentSetting = null;
        private bool sceneChanged = false;
        private float prevTime = 0f;
        
        private void Start()
        {
            time = 0;
            currentSetting = sceneSettings[0];
            sceneTitle = currentSetting.Title;
            sceneChanged = false;
        }

        private void Update()
        {
            var dt = Time.deltaTime;
            time += dt;
            screen.CustomTime = time;
            
            if (time > currentSetting.Time)
            {
                sceneTitle = currentSetting.Next;
                currentSetting = sceneSettings.Find(s => s.Title == sceneTitle);
                sceneChanged = false;
                prevTime = time;
                time = 0;
            }
            
            switch (sceneTitle)
            {
                case SceneTitle.Dawn:
                    DawnScene();
                    break;
                case SceneTitle.Departure:
                    DepartureScene();
                    break;
                case SceneTitle.Tumult:
                    TumultScene();
                    break;
                case SceneTitle.HeatUp:
                    HeatUpScene();
                    break;
                case SceneTitle.Flashback:
                    FlashbackScene();
                    break;
                case SceneTitle.NoisySea:
                    NoisySeaScene();
                    break;
                case SceneTitle.NetWorking:
                    NetWorkingScene();
                    break;
                case SceneTitle.ChillInTheSea:
                    ChillInTheSeaScene();
                    break;
                case SceneTitle.Revolving:
                    RevolvingScene();
                    break;
                default:
                    break;
            }

            // インタラクティブCPUエフェクト
            if (enableInteractiveCPU)
            {
                foreach (var info in UserInterfaceManager.KeyInformations)
                {
                    if (info.Down)
                    {
                        effectManager.GenerateEffect();
                    }
                }
            }

            // インタラクティブGPUエフェクト
            UserInterfaceManager.UpdateInput(dt);

            if (Input.GetKeyDown(KeyCode.R))
            {
                ForceResetTime();
            }
            
            // インタラクティブサウンド
            for (var i = 0; i < UserInterfaceManager.KeyInformations.Count; i++)
            {
                var info = UserInterfaceManager.KeyInformations[i];
                if (info.Down)
                {
                    sounds[i].Play();
                }
            }
        }

        private void SceneCheck()
        {
        }

        public void ForceResetTime()
        {
            time = 0;
            currentSetting = sceneSettings[0];
            sceneTitle = currentSetting.Title;
            sceneChanged = false;
            var audioSource = GetComponent<AudioSource>();
            audioSource.Stop(); 
            audioSource.time = 10.7f;
            audioSource.Play();
        }

        private void SetMaterials(SceneSetting setting)
        {
            channel0.SetMaterial(setting.Mat0);
            channel1.SetMaterial(setting.Mat1);
            channel2.SetMaterial(setting.Mat2);

            screen.Type0 = setting.Type0;
            screen.Type1 = setting.Type1;
            screen.Type2 = setting.Type2;
            
            enableInteractiveCPU = setting.EnableCpuEffect;
        }

        private void DawnScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                screen.Bright = 0;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;

            screen.Bright = Mathf.Clamp01((time - 0.25f) * 0.25f);
        }

        private void DepartureScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = prevTime + time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
            
            screen.Ratio0 = Mathf.Clamp01(4.7f - time);
            screen.Ratio1 = Mathf.Clamp01(time * 0.3f) * 0.5f;
        }
        private void TumultScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = prevTime + time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
            
            screen.Ratio0 = 0.5f;
            //screen.Ratio1 = Mathf.Clamp01(time * 0.15f) * 0.5f;
        }
        private void HeatUpScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
        }
        private void FlashbackScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
        }
        private void NoisySeaScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
        }
        private void NetWorkingScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
        }
        private void ChillInTheSeaScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
        }
        private void RevolvingScene()
        {
            if (!sceneChanged)
            {
                sceneChanged = true;
                SetMaterials(currentSetting);
            }
            channel0.CustomTime = time;
            channel1.CustomTime = time;
            channel2.CustomTime = time;
        }
    }
}