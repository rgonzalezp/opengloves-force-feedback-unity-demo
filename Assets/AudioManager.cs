using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioMixerGroup pitchRateGroup;
    public List<AudioSource> audios;
    public float audioSpeedStep = 0.1f;
    private float currentMultiplier = 0;
    public void PlaySound(string objectName)
    {
        foreach (var item in audios)
        {
            if (objectName == item.name)
            {

                item.Play();

                if(item.name == "up")
                {
                    currentMultiplier += 1;
                    var currentSpeechRate = currentMultiplier * audioSpeedStep;
                    foreach (var sound in audios)
                    {
                        sound.outputAudioMixerGroup = pitchRateGroup;
                        sound.pitch = 1 + currentSpeechRate;
                        pitchRateGroup.audioMixer.SetFloat("pitchRate", 1 / sound.pitch);
                        Debug.Log(sound.pitch);
                        Debug.Log(1 / sound.pitch);


                    }
                }
                if(item.name == "down")
                {
                    currentMultiplier -= 1;
                    var currentSpeechRate = currentMultiplier * audioSpeedStep;
                    foreach (var sound in audios)
                    {
                        
                        sound.outputAudioMixerGroup = pitchRateGroup;
                        sound.pitch = 1 + currentSpeechRate;
                        pitchRateGroup.audioMixer.SetFloat("pitchRate", 1 / sound.pitch);
                        Debug.Log(sound.pitch);
                        Debug.Log(1 / sound.pitch);
                    }
                }
            }
        }
        
    }

    public void PlayCabbageSound()
    {
        foreach (var item in audios)
        {
            if ("right" == item.name)
            {

                item.Play();
            }
        }
    }

    public void PlayTomatoSound()
    {
        foreach (var item in audios)
        {
            if ("left" == item.name)
            {

                item.Play();
            }
        }
    }

    public void PlayRockSound()
    {
        foreach (var item in audios)
        {
            if ("rock" == item.name)
            {

                item.Play();
            }
        }
    }
}
