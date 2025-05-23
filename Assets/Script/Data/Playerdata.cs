using System;
using UnityEngine;
using UnityEngine.UI;
[Serializable]
public class Playerdata
{
    /*
    Used to store player data between Menu and GamePlay pages.
    */
    [SerializeField] private string name;
    [SerializeField] private ColorPlayer color;
    [SerializeField] private int order;
    [SerializeField] private int score;
    public string Name { get { return name; } set { name = value; } }
    public ColorPlayer Color { get { return color; } set { color = value; } }
    public int Order { get { return order; } set { order = value; } }
    public int Score { get { return score; } set { score = value; } }
    public bool IsPass;
    public Playerdata()
    {
        name = "";
        order = 0;
        score = 0;
        IsPass = false;
    }
    public void ReSetPlayerdata()
    {
        name = "";
        order = 0;
        score = 0;
        IsPass = false;
    }

}
public enum ColorPlayer
{
    None = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Blue = 4,
    Black = 5,



}
