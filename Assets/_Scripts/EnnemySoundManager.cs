using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnnemySoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource ambiant;

    [SerializeField]
    private AudioSource idle;

    [SerializeField]
    private AudioSource suspicious;

    [SerializeField]
    private AudioSource triggered;

    [SerializeField]
    private AudioSource pre_attack;

    [SerializeField]
    private AudioSource during_attack;

    [SerializeField]
    private AudioSource post_attack;

    [SerializeField]
    private AudioSource takes_damage;

    [SerializeField]
    private AudioSource dies;


    // Start is called before the first frame update
    void Start()
    {
        //trimSilenceFromAudioSourceList(new List<AudioSource> {ambiant, idle, suspicious, triggered, pre_attack, during_attack, post_attack, takes_damage, dies});
    }

    /// <summary>
    /// Play a looping ambiant sound of the character
    /// The loop goes on forever until it is stopped with StopAmbiantSound()
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayAmbiantSound(float volume = 1f)
    {
        if (ambiant == null)
            return;

        //AudioSource.PlayClipAtPoint(ambiant.clip, transform.position, volume);
        ambiant.loop = true;
    }

    /// <summary>
    /// Stop the ambiant sound of the character
    /// </summary>
    public void StopAmbiantSound()
    {
        if (ambiant == null)
            return;
        ambiant.Stop();
    }


    /// <summary>
    /// Play a ponctual sound displaying the character attitude in an iddle mood
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayIddleSound(float volume = 1f)
    {
        if (idle == null)
            return;

        idle.PlayOneShot(idle.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the character is nearby the heroe
    /// </summary>
    /// <param volume="volume"></param>
    public void PlaySuspiciousSound(float volume = 1f)
    {
        if (suspicious == null)
            return;

        suspicious.PlayOneShot(suspicious.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the character discovers the heroes and start chasing him
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayTriggeredSound(float volume = 1f)
    {
        if (triggered == null)
            return;

        triggered.PlayOneShot(triggered.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the character start an attack
    /// Correspond to the very beginning of an attack
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayPreAttackSound(float volume = 1f)
    {
        if (pre_attack == null)
            return;

        pre_attack.PlayOneShot(pre_attack.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound the attack makes when its castde
    /// Correspond to the route of the attack (between the begining and the end of the attack)
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayDuringAttackSound(float volume = 1f)
    {
        if (during_attack == null)
            return;

        during_attack.PlayOneShot(during_attack.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound the attack makes when it hits something
    /// Correspond to the end of the attack
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayPostAttackSound(float volume = 1f)
    {
        if (post_attack == null)
            return;

        post_attack.PlayOneShot(post_attack.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the character is hit
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayTakesDamageSound(float volume = 1f)
    {
        if (takes_damage == null)
            return;

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

        dies.PlayOneShot(dies.clip, volume);
    }

    
    private void trimSilenceFromAudioSourceList(List<AudioSource> list)
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
