using UnityEngine;
using UnityEngine.UI;

public class BlockData : MonoBehaviour
{
    public Vector2Int Location { get { return location; } set { location = value; } }
    public ColorPlayer Owner { get { return owner; } set { owner = value; } }
    private Vector2Int location;
    private ColorPlayer owner;
    public void init(Vector2Int _location, ColorPlayer _owner)
    {
        owner = _owner;
        location = _location;
        Image image = GetComponent<Image>();
        ApplyColor(_owner, image);
    }
    public void ApplyColor(ColorPlayer colorType, Image targetImage)
    {
        Color color = Color.white;

        switch (colorType)
        {
            case ColorPlayer.Red:
                color = Color.red;
                break;
            case ColorPlayer.Green:
                color = Color.green;
                break;
            case ColorPlayer.Blue:
                color = Color.blue;
                break;
            case ColorPlayer.Yellow:
                color = Color.yellow;
                break;
            case ColorPlayer.Black:
                color = Color.black;
                break;
        }

        targetImage.color = color;
    }

}
