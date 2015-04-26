using UnityEngine;
using System.Collections;

public class SoundList : MonoBehaviour
{
    static SoundList m_pInstance;
    [SerializeField] protected AudioClip[] m_SoundList;
    [SerializeField] protected AudioSource[] m_SoundSourceList;

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
	
    public static void Play3D(string sSoundName)
    {
        AudioSource pSound = GetSoundSource(sSoundName);
        if (pSound)
        {
            pSound.Play();
        }
    }
	
	public static void Play3DAt(string sSoundName, Vector3 pPosition)
	{
		AudioSource pSound = GetSoundSource(sSoundName);
		if (pSound)
		{
			pSound.transform.position = pPosition;
			pSound.Play();
		}
	}

    public static AudioSource GetSoundSource(string sSoundName)
    {
        foreach (AudioSource pSound in m_pInstance.m_SoundSourceList)
        {
            if (pSound != null && pSound.name == sSoundName)
                return pSound;
        }
        Debug.LogError("Sound Not Found[" + sSoundName + "]");
        return null;
    }

}