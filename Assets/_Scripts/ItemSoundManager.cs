using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ItemSoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource saw;

    [SerializeField]
    private AudioSource saw_hit;

    [SerializeField]
    private AudioSource spikes;


    // Start is called before the first frame update
    void Start()
    {
        trimSilenceFromAudioSourceList(new List<AudioSource> { saw, saw_hit, spikes });
    }

    /// <summary>
    /// Play a ponctual sound when the saw hits something
    /// </summary>
    /// <param volume="volume"></param>
    public void PlaySawHitSound(float volume = 1f)
    {
        if (saw_hit == null)
            return;

        if (volume == null)
            volume = saw_hit.volume;

        AudioSource.PlayClipAtPoint(saw_hit.clip, transform.position, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the spikes are triggered
    /// </summary>
    /// <param volume="volume"></param>
    public void PlaySpikesSound(float volume = 1f)
    {
        if (spikes == null)
            return;

        if (volume == null)
            volume = spikes.volume;
        spikes.PlayOneShot(spikes.clip, volume);
    }

    /// <summary>
    /// Play a looping sound for the saw to move in an hostile manner
    /// </summary>
    /// <param volume="volume"></param>
    public IEnumerator PlaySawSound(float volume = 1f)
    {
        //dsnt work yet
        yield return new WaitForSeconds(0);
        if (saw == null)
            yield return new WaitForSeconds(0);

        AudioSource.PlayClipAtPoint(saw.clip, transform.position, volume);
        saw.loop = true;

        yield return new WaitForSeconds(2);
        StartCoroutine(PlaySawSound());
    }

    /// <summary>
    /// Stop the ambiant sound of the saw
    /// </summary>
    public void StopSawSound()
    {
        if (saw == null)
            return;
        saw.Stop();
    }

    public void trimSilenceFromAudioSourceList(List<AudioSource> list)
    {

        foreach (AudioSource a in list)
        {
            if (a != null && a.clip != null)
            {
                a.clip = trimSilence(a.clip);
                a.playOnAwake = false;
            }
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