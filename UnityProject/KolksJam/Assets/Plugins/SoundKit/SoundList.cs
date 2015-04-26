using UnityEngine;
using System.Collections;

public class SoundList : MonoBehaviour
{
    static SoundList m_pInstance;
    [SerializeField] protected AudioClip[] m_SoundList;

    void Awake()
    {
        m_pInstance = this;
    }


    public static void Play(string sSoundName)
    {
        AudioClip pSound = GetSound(sSoundName);
        if (pSound)
        {
            SoundKit.instance.playSound(pSound);
        }
    }
    public static void PlayOneShot(string sSoundName)
    {
        AudioClip pSound = GetSound(sSoundName);
        if (pSound)
        {
            SoundKit.instance.playOneShot(pSound);
        }
    }

    public static AudioClip GetSound(string sSoundName)
    {
        foreach (AudioClip pSound in m_pInstance.m_SoundList)
        {
            if (pSound != null && pSound.name == sSoundName)
                return pSound;
        }
        Debug.LogError("Sound Not Found[" + sSoundName + "]");
        return null;
    }

}