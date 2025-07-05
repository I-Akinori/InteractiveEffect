using UnityEngine;

public class MicRecorder : MonoBehaviour
{
    public string selectedDevice;
    public AudioSource audioSource;
    private AudioClip recordedClip;

    void Start()
    {
        // マイクデバイス一覧の取得
        if (Microphone.devices.Length > 0)
        {
            selectedDevice = Microphone.devices[0]; // 最初のマイクを使用
            Debug.Log("Using mic: " + selectedDevice);
        }
        else
        {
            Debug.LogWarning("No microphone detected");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            StartRecording();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopRecordingAndPlay();
        }
    }

    public void StartRecording(int recordLengthSec = 5)
    {
        if (selectedDevice != null)
        {
            // 録音開始（引数：デバイス名, ループ再生, 録音秒数, サンプリングレート）
            recordedClip = Microphone.Start(selectedDevice, false, recordLengthSec, 44100);
            Debug.Log("Recording started");
        }
    }

    public void StopRecordingAndPlay()
    {
        if (Microphone.IsRecording(selectedDevice))
        {
            Microphone.End(selectedDevice);
            Debug.Log("Recording stopped");

            // AudioSourceにセットして再生
            audioSource.clip = recordedClip;
            audioSource.Play();
            Debug.Log("Playback started");
        }
    }
}
