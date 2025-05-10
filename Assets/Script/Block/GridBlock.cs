using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
 IPointerMoveHandler, IPointerClickHandler
{
    public int Rows = 10;
    public int Columns = 10;
    public float CellSize = 67; // Cell Per pixels
    [SerializeField] private List<GridBlockkRow> gridBlockkRow;

    RectTransform rectTransform;
    Vector2 mouseGridOnPostion;
    Vector2Int blockGridOnLocation;
    Vector2 blockGridOnPosittion;

    GameObject Onhand;//block hold and wait to drop
    Vector2Int locationOnhand;

    [SerializeField] Canvas canvas;
    #region Lifecycle  
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

    }
    void Start()
    {

        Debug.Log(rectTransform.sizeDelta + " " + RectTransformUtility.PixelAdjustRect(rectTransform, canvas));
        UpdateCellSizeByScreen();
        gridBlockkRow = new List<GridBlockkRow>();

        for (int row = 0; row < Rows; row++)
        {
            gridBlockkRow.Add(new GridBlockkRow(row.ToString()));
            for (int column = 0; column < Columns; column++)
            {
                gridBlockkRow[row].Columns.Add(new GridBlockkColumn(column.ToString()));
            }
        }

    }
    #endregion
    #region Grid  
    void UpdateCellSizeByScreen()
    {
        Debug.Log((Screen.width / 1920));
        CellSize = (rectTransform.sizeDelta.x / Columns) * (Screen.width / 1920f);
    }

    public Vector2Int GetGridLocation(Vector2 mousePosition)
    {

        mouseGridOnPostion.x = mousePosition.x - rectTransform.position.x + CellSize;
        mouseGridOnPostion.y = mousePosition.y - rectTransform.position.y + CellSize;

        blockGridOnLocation.x = (int)(mouseGridOnPostion.x / CellSize);
        blockGridOnLocation.y = (int)(mouseGridOnPostion.y / CellSize);

        return blockGridOnLocation;
    }
    public Vector2 GetGridPosittion(Vector2Int blockGridOnLocation)
    {



        blockGridOnPosittion.x = blockGridOnLocation.x * CellSize + rectTransform.position.x - CellSize / 2;
        blockGridOnPosittion.y = blockGridOnLocation.y * CellSize + rectTransform.position.y - CellSize / 2;

        return blockGridOnPosittion;
    }
    #endregion
    #region OnPointer  
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Onhand == null)
        {
            Vector2Int _location = GetGridLocation(eventData.position);
            Vector2 _postion = GetGridPosittion(_location);
            locationOnhand = _location;
            Onhand = GameManager.Instance.NewBlock(CellSize, _postion, _location, transform.parent);
        }

    }
    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2Int _location = GetGridLocation(eventData.position);
        if (GetGridLocation(eventData.position) != locationOnhand)
        {
            locationOnhand = _location;
            Onhand.transform.position = GetGridPosittion(_location);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (Onhand != null)
        {
            Destroy(Onhand);
        }
    }
    #endregion
    #region DeBug
    public Color lineColor = Color.white;
    void OnDrawGizmos()
    {
        Gizmos.color = lineColor;

        for (int x = 0; x <= Columns; x++)
        {
            Vector3 start = transform.position + new Vector3(x * CellSize, 0, 0);
            Vector3 end = transform.position + new Vector3(x * CellSize, Rows * CellSize, 0);
            Gizmos.DrawLine(start, end);
        }

        for (int y = 0; y <= Rows; y++)
        {
            Vector3 start = transform.position + new Vector3(0, y * CellSize, 0);
            Vector3 end = transform.position + new Vector3(Columns * CellSize, y * CellSize, 0);
            Gizmos.DrawLine(start, end);
        }
    }
    void OnGUI()
    {
        Vector2 testmousePosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
                        rectTransform, Input.mousePosition, null, out testmousePosition);

        GUI.Label(new Rect(30, 10, 300, 30), (testmousePosition.x).ToString());
        GUI.Label(new Rect(30, 30, 300, 30), GetGridLocation(Input.mousePosition).ToString());
    }
    #endregion
}
[Serializable]
public class GridBlockkRow
{
    public string Name;
    public List<GridBlockkColumn> Columns;
    public GridBlockkRow(int lengthColumns)
    {
        Columns = new List<GridBlockkColumn>(lengthColumns);
    }
    public GridBlockkRow(string name)
    {
        Name = name;
        Columns = new List<GridBlockkColumn>();
    }

}
[Serializable]
public class GridBlockkColumn
{
    public string Name;
    public bool IsEmpty;
    public ColorPlayer Owner;
    public GridBlockkColumn()
    {
        IsEmpty = true;
        Owner = ColorPlayer.None;
    }
    public GridBlockkColumn(string name)
    {
        Name = name;
        IsEmpty = true;
        Owner = ColorPlayer.None;
    }
}