using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(){
         Debug.Log("PlaySound");
    }

}
