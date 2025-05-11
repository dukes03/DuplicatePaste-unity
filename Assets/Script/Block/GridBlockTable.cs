using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridBlockTable
{
    private List<GridBlockAxisX> gridBlockAxisX;

    public void init(int AxisX, int AxisY)
    {
        gridBlockAxisX = new List<GridBlockAxisX>();

        for (int axisx = 0; axisx < AxisX; axisx++)
        {
            gridBlockAxisX.Add(new GridBlockAxisX(axisx.ToString()));
            for (int column = 0; column < AxisY; column++)
            {
                gridBlockAxisX[axisx].AxisY.Add(new GridBlockAxisY(column.ToString()));
            }
        }
        GameManager.Instance.OnDoneState();
    }
    public bool IsEmpty(int indexAxisX, int indexAxisY)
    {
        if (indexAxisX < gridBlockAxisX.Count && indexAxisX >= 0 && indexAxisY >= 0 && indexAxisY < GetAxisX(0).AxisY.Count
        )
        {
            return GetAxisX(indexAxisX).ColIsEmpty(indexAxisY);
        }
        return false;
    }
    public bool IsOwn(int indexAxisX, int indexAxisY, ColorPlayer own)
    {
        if (indexAxisX < gridBlockAxisX.Count)
        {
            return GetAxisY(indexAxisX, indexAxisY).Owner == own;
        }
        return false;
    }
    private GridBlockAxisY GetAxisY(int indexAxisX, int indexAxisY)
    {
        return GetAxisX(indexAxisX).AxisY[indexAxisY];
    }
    private GridBlockAxisX GetAxisX(int indexAxisX)
    {

        return gridBlockAxisX[indexAxisX];
    }
    public bool AddBlock(int indexAxisX, int indexAxisY, ColorPlayer player)
    {
        if (IsEmpty(indexAxisX, indexAxisY))
        {
            GetAxisY(indexAxisX, indexAxisY).SetOwner(player);
            return true;
        }
        else
        {
            return false;
        }
    }
}
[Serializable]
public class GridBlockAxisX
{
    public string Name;
    public List<GridBlockAxisY> AxisY;
    public GridBlockAxisX(int lengthAxisY)
    {
        AxisY = new List<GridBlockAxisY>(lengthAxisY);
    }
    public GridBlockAxisX(string name)
    {
        Name = name;
        AxisY = new List<GridBlockAxisY>();
    }
    public bool ColIsEmpty(int index)
    {
        if (index < AxisY.Count)
        {
            return AxisY[index].IsEmpty;
        }
        else
        {
            return false;
        }
    }

}
[Serializable]
public class GridBlockAxisY
{
    public string Name;
    public bool IsEmpty;
    public ColorPlayer Owner;
    public GridBlockAxisY()
    {
        IsEmpty = true;
        Owner = ColorPlayer.None;
    }
    public GridBlockAxisY(string name)
    {
        Name = name;
        IsEmpty = true;
        Owner = ColorPlayer.None;
    }
    public void SetOwner(ColorPlayer owner)
    {
        Owner = owner;
        if (Owner != ColorPlayer.None)
        {
            IsEmpty = false;
        }
    }
}