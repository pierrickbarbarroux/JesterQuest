using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MusicManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource fauxSilence;
    
    [SerializeField]
    private AudioSource wind;

    [SerializeField]
    private AudioSource torche;

    [SerializeField]
    private AudioSource musiqueAller;

    [SerializeField]
    private AudioSource musiqueRetour;

    [SerializeField]
    private AudioSource musiqueVictory;

    [SerializeField]
    private AudioSource musiqueMenu;

    [SerializeField]
    private AudioSource jingleNewRun;

    [SerializeField]
    private AudioSource jingleFlute;

    [SerializeField]
    private AudioSource selectionSound;

    [SerializeField]
    private AudioSource validationSound;



    // Start is called before the first frame update
    void Start()
    {
        trimSilenceFromAudioSourceList(new List<AudioSource> {fauxSilence, wind, torche, musiqueAller, musiqueRetour, musiqueVictory, musiqueMenu, jingleNewRun, jingleFlute, selectionSound, validationSound});
    }

    /// <summary>
    /// Play a looping ambiant sub pink noise
    /// It should be playing nonstop from the beginning of a run until the end of a run
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayFauxSilence(float volume = 1f)
    {
        if (fauxSilence == null)
            return;

        if (volume != null)
            fauxSilence.volume = volume;
        fauxSilence.loop = true;

        fauxSilence.Play();
    }

    /// <summary>
    /// Stop the ambiant sound
    /// </summary>
    public void StopFauxSilence()
    {
        if (fauxSilence == null)
            return;
        fauxSilence.Stop();
    }

    /// <summary>
    /// Play a looping ambiant wind sound
    /// It should be playing from the beginning of each gameplay phase until the end of each gameplay phase
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayWindSound(float volume = 1f)
    {
        if (wind == null)
            return;

        if (volume != null)
            wind.volume = volume;
        wind.loop = true;
        wind.Play();
    }

    /// <summary>
    /// Stop the ambiant sound
    /// </summary>
    public void StopWindSound()
    {
        if (wind == null)
            return;
        wind.Stop();
    }

    /// <summary>
    /// Play a looping ambiant torch sound
    /// It should be playing from the beginning of each gameplay phase until the end of each gameplay phase
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayTorcheSound(float volume = 1f)
    {
        if (torche == null)
            return;

        if (volume == null)
            volume = torche.volume;
        AudioSource.PlayClipAtPoint(torche.clip, transform.position, volume);
        torche.loop = true;
    }

    /// <summary>
    /// Stop the ambiant sound
    /// </summary>
    public void StopTorcheSound()
    {
        if (torche == null)
            return;
        torche.Stop();
    }


    /// <summary>
    /// Play the looping music for the first half of the stage
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayMusiqueAller(float volume = 1f)
    {
        if (musiqueAller == null)
            return;

        if (volume != null)
            musiqueAller.volume = volume;
        musiqueAller.loop = true;
        musiqueAller.Play();
    }

    /// <summary>
    /// Stop the music
    /// </summary>
    public void StopMusiqueAller()
    {
        if (musiqueAller == null)
            return;
        musiqueAller.Stop();
    }

    /// <summary>
    /// Play the looping music for the last half of the stage
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayMusiqueRetour(float volume = 1f)
    {
        if (musiqueRetour == null)
            return;

        if (volume != null)
            musiqueRetour.volume = volume;
        musiqueRetour.loop = true;
        musiqueRetour.Play();
    }

    /// <summary>
    /// Stop the music
    /// </summary>
    public void StopMusiqueRetour()
    {
        if (musiqueRetour == null)
            return;
        musiqueRetour.Stop();
    }

    /// <summary>
    /// Play the looping music when the player wins
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayMusiqueVictory(float volume = 1f)
    {
        if (musiqueVictory == null)
            return;

        musiqueVictory.loop = true;

        musiqueVictory.Play();
    }

    /// <summary>
    /// Stop the music
    /// </summary>
    public void StopMusiqueVictory()
    {
        if (musiqueVictory == null)
            return;
        musiqueVictory.Stop();
    }

    /// <summary>
    /// Play the looping music for the menu
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayMusiqueMenu(float volume = 1f)
    {
        if (musiqueMenu == null)
            return;

        if (volume != null)
            musiqueMenu.volume = volume;
        musiqueMenu.loop = true;

        musiqueMenu.Play();
    }

    /// <summary>
    /// Stop the music
    /// </summary>
    public void StopMusiqueMenu()
    {
        if (musiqueMenu == null)
            return;
        musiqueMenu.Stop();
    }

    /// <summary>
    /// Play a ponctual sound when the player starts a new game run (eg. the gameplay loop)
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayJingleNewRunSound(float volume = 1f)
    {
        if (jingleNewRun == null)
            return;

        if (volume == null)
            volume = jingleNewRun.volume;
        jingleNewRun.PlayOneShot(jingleNewRun.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the player gets the the flute
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayJingleFlute(float volume = 1f)
    {
        if (jingleFlute == null)
            return;

        if (volume == null)
            volume = jingleFlute.volume;
        jingleFlute.PlayOneShot(jingleFlute.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the player selects something in the menu
    /// </summary>
    /// <param volume="volume"></param>
    public void PlaySelectionSound(float volume = 1f)
    {
        if (selectionSound == null)
            return;

        if (volume == null)
            volume = selectionSound.volume;
        selectionSound.PlayOneShot(selectionSound.clip, volume);
    }

    /// <summary>
    /// Play a ponctual sound when the player validates their selection in the menu
    /// </summary>
    /// <param volume="volume"></param>
    public void PlayValidationSound(float volume = 1f)
    {
        if (validationSound == null)
            return;

        if (volume == null)
            volume = selectionSound.volume;

        validationSound.PlayOneShot(validationSound.clip, volume); ;
    }


    private void trimSilenceFromAudioSourceList(List<AudioSource> list)
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
