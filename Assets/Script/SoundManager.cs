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

    // Update is called once per frame
    void Update()
    {

    }

    public void PlaySound()
    {
        Debug.Log("PlaySound");
    }
    public void PlaySoundBnt()
    {
        Debug.Log("PlaySound");
    }

}
