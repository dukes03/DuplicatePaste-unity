using UnityEngine;
//To be used throughout the game
public class SoundManager : Singleton<SoundManager>
{
    public bool TurnOnMusic = true;
    public bool TurnOnSFX = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }
    public void PlaySound()
    {

    }
    public void PlaySoundBnt()
    {

    }

}
