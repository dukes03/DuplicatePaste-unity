using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GridBlockTable
{
    private List<GridBlockRow> gridBlockRow;

    public void init(int Rows, int Columns)
    {
        gridBlockRow = new List<GridBlockRow>();

        for (int row = 0; row < Rows; row++)
        {
            gridBlockRow.Add(new GridBlockRow(row.ToString()));
            for (int column = 0; column < Columns; column++)
            {
                gridBlockRow[row].Columns.Add(new GridBlockColumn(column.ToString()));
            }
        }
        GameManager.Instance.OnDoneState(0, 0);
        Debug.Log("GridBlockTable init Done");
    }
    public bool IsEmpty(int indexRow, int indexColumn)
    {
        if (indexRow < gridBlockRow.Count)
        {
            return GetRow(indexRow).ColIsEmpty(indexColumn);
        }
        return false;

    }
    private GridBlockColumn GetCol(int indexRow, int indexColumn)
    {
        return GetRow(indexRow).Columns[indexColumn];
    }
    private GridBlockRow GetRow(int indexRow)
    {
        return gridBlockRow[indexRow];
    }
    public bool AddBlock(int indexRow, int indexColumn, ColorPlayer player)
    {
        if (IsEmpty(indexRow, indexColumn))
        {
            GetCol(indexRow, indexColumn).SetOwner(player);
            return true;
        }
        else
        {
            return false;
        }
    }
}
[Serializable]
public class GridBlockRow
{
    public string Name;
    public List<GridBlockColumn> Columns;
    public GridBlockRow(int lengthColumns)
    {
        Columns = new List<GridBlockColumn>(lengthColumns);
    }
    public GridBlockRow(string name)
    {
        Name = name;
        Columns = new List<GridBlockColumn>();
    }
    public bool ColIsEmpty(int index)
    {
        if (index < Columns.Count)
        {
            return Columns[index].IsEmpty;
        }
        else
        {
            return false;
        }
    }

}
[Serializable]
public class GridBlockColumn
{
    public string Name;
    public bool IsEmpty;
    public ColorPlayer Owner;
    public GridBlockColumn()
    {
        IsEmpty = true;
        Owner = ColorPlayer.None;
    }
    public GridBlockColumn(string name)
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