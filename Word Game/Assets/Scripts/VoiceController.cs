using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TextSpeech;
public class VoiceController : MonoBehaviour
{
    const string LANG_CODE = "en-US";

    void Start()
    {
        Setup(LANG_CODE);
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;
    }
    void Setup(string code)
    {
        TextToSpeech.instance.Setting(code, 1, 1);
        SpeechToText.instance.Setting(code);
    }
    
    void OnSpeakStart()
    {
        Debug.Log("Começou a falar.");
    }

    void OnSpeakStop()
    {
        Debug.Log("Parou de falar.");
    }
    public void StartSpeaking()
    {
        Setup(LANG_CODE);
        TextToSpeech.instance.onStartCallBack = OnSpeakStart;
        TextToSpeech.instance.onDoneCallback = OnSpeakStop;
        TextToSpeech.instance.StartSpeak("Hello");
    }
    public void StopSpeaking()
    {
        TextToSpeech.instance.StopSpeak();
    }
}
