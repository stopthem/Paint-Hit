using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] soundEffects;

    public AudioSource PlayClipAtPoint(AudioClip clip, Vector3 position, float volume = .5f)
    {
        if (clip != null)
        {
            GameObject go = new GameObject("SoundFX "+ clip.name);
            go.transform.position = position;

            AudioSource source = go.AddComponent<AudioSource>();
            source.clip = clip;

            source.volume = volume;

            source.Play();
            Destroy(go,clip.length);
            return source;
        }
        return null;
    }

    public void PlayCompleteSound()
    {
        PlayClipAtPoint(soundEffects[0],Vector3.zero);
    }
    public void PlayFailSound()
    {
        PlayClipAtPoint(soundEffects[1],Vector3.zero);
    }
    public void PlayHitSound()
    {
        PlayClipAtPoint(soundEffects[2],Vector3.zero);
    }
}
