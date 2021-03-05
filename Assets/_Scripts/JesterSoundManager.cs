using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class JesterSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource[] footsteps;

    private int footstepIndicator = 0;

    [SerializeField]
    private AudioSource dash;

    [SerializeField]
    private AudioSource takes_damage;

    [SerializeField]
    private AudioSource dies;


    // Start is called before the first frame update
    void Start()
    {
        List<AudioSource> list = new List<AudioSource> { dash, takes_damage, dies };
        foreach (AudioSource a in footsteps)
            list.Add(a);

        trimSilenceFromAudioSourceList(list);
    }

    /// <summary>
    /// Play a ponctual sound when the character is hit
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayTakesDamageSound(float volume = 1f)
    {
        if (takes_damage == null)
            return;

        if (volume == null)
            volume = takes_damage.volume;
        takes_damage.PlayOneShot(takes_damage.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the character dies
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayDiesSound(float volume = 1f)
    {
        if (dies == null)
            return;

        if (volume == null)
            volume = dies.volume;
        dies.PlayOneShot(dies.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the character dashes
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayDashSound(float volume = 1f)
    {
        if (dash == null)
            return;

        if (volume == null)
            volume = dash.volume;
        dash.PlayOneShot(dash.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when any foot of the character stamples the ground
    /// <param volume="volume"></param>
    public void PlayFootstepsSound(float volume = 1f)
    {
        if (dash == null)
            return;

        if (footsteps.Length == 2)
            footstepIndicator = footstepIndicator == 1 ? 0 : 1;
        else
            footstepIndicator = UnityEngine.Random.Range(0, footsteps.Length);
        footsteps[footstepIndicator].PlayOneShot(footsteps[footstepIndicator].clip, volume);
    }


    public void trimSilenceFromAudioSourceList(List<AudioSource> list)
    {

        foreach (AudioSource a in list)
        {
            if (a == null || a.clip != null)
            {
                a.clip = trimSilence(a.clip);
                a.playOnAwake = false;
            }
            else
                Debug.Log(a + " n'est pas encore fourni");
        }

    }

    /// <summary>
    /// Trims silence from both ends in an AudioClip.
    /// Makes mp3 files seamlessly loopable.
    /// </summary>
    /// <param name="inputAudio"></param>
    /// <param name="threshold"></param>
    /// <returns></returns>
    AudioClip trimSilence(AudioClip inputAudio, float threshold = 0.05f)
    {
        // Copy samples from input audio to an array. AudioClip uses interleaved format so the length in samples is multiplied by channel count
        float[] samplesOriginal = new float[inputAudio.samples * inputAudio.channels];
        inputAudio.GetData(samplesOriginal, 0);
        // Find first and last sample (from any channel) that exceed the threshold
        int audioStart = Array.FindIndex(samplesOriginal, sample => sample > threshold),
            audioEnd = Array.FindLastIndex(samplesOriginal, sample => sample > threshold);
        // Copy trimmed audio data into another array
        float[] samplesTrimmed = new float[audioEnd - audioStart];
        Array.Copy(samplesOriginal, audioStart, samplesTrimmed, 0, samplesTrimmed.Length);
        // Create new AudioClip for trimmed audio data
        AudioClip trimmedAudio = AudioClip.Create(inputAudio.name, samplesTrimmed.Length / inputAudio.channels, inputAudio.channels, inputAudio.frequency, false);
        trimmedAudio.SetData(samplesTrimmed, 0);
        return trimmedAudio;
    }

}