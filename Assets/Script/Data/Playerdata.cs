using UnityEngine;

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
    Red = 0,
    Green = 1,
    Yellow = 2,
    Blue = 3,
}
