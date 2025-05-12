using System.Collections.Generic;
using UnityEngine;
//To be used throughout the game
public class AppManager : Singleton<AppManager>
{

    [SerializeField] List<Playerdata> playerdatas;
    public List<Playerdata> Playerdatas { get { return playerdatas; } set { playerdatas = value; } }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
