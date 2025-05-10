using UnityEngine;
using UnityEngine.UI;
public class Playerdata
{
    /*
    Used to store player data between Menu and GamePlay pages.
    */
    private string name;
    private ColorPlayer color;
    private int order;
    private int score;
    public string Name { get { return name; } set { name = value; } }
    public ColorPlayer Color { get { return color; } set { color = value; } }
    public int Order { get { return order; } set { order = value; } }
    public int Score { get { return score; } set { score = value; } }
    Playerdata()
    {
        name = "";
        order = 0;
        score = 0;
    }
    public void ReSetPlayerdata()
    {
        name = "";
        order = 0;
        score = 0;
    }

}
public enum ColorPlayer
{
    None = 0,
    Red = 1,
    Green = 2,
    Yellow = 3,
    Blue = 4,

}
