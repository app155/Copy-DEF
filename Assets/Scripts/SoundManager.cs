using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource bgmaudio;
    public List<AudioSource> sfxaudio;
    public List<AudioClip> sfxList;
    int sfxidx;

    public enum Sound
    {
        Victory,
        Defeat,
        Buy,
        Upgrade,
        Sword,
        Range,
        RangeHit,
        Guard,
        Magic,
    }

    public void Stop()
    {
        bgmaudio.Stop();
    }

    public void SFX(Sound sound)
    {
        sfxaudio[sfxidx].clip = sfxList[(int)sound];
        sfxaudio[sfxidx].Play();
        sfxidx = (sfxidx + 1) % 3;
    }
}
