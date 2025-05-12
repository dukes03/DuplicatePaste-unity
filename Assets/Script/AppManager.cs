using System.Collections.Generic;
using UnityEngine;
//To be used throughout the game
public class AppManager : Singleton<AppManager>
{
    [SerializeField] List<Playerdata> playerdatas;
    public List<Playerdata> Playerdatas { get { return playerdatas; } set { playerdatas = value; } }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

}
