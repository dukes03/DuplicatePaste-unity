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
    [SerializeField] private GridBlockTable gridBlockTable;

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
        gridBlockTable.init(Rows, Columns);

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

        //Because the index of the list starts at 0 and the number of cells starts at 1.
        blockGridOnLocation.x = (int)(mouseGridOnPostion.x / CellSize) - 1;
        blockGridOnLocation.y = (int)(mouseGridOnPostion.y / CellSize) - 1;

        return blockGridOnLocation;
    }
    public Vector2 GetGridPosittion(Vector2Int blockGridOnLocation)
    {



        blockGridOnPosittion.x = (blockGridOnLocation.x + 1) * CellSize + rectTransform.position.x - CellSize / 2;
        blockGridOnPosittion.y = (blockGridOnLocation.y + 1) * CellSize + rectTransform.position.y - CellSize / 2;

        return blockGridOnPosittion;
    }

    public bool LocationIsEmpty(Vector2Int location)
    {
        return gridBlockTable.IsEmpty(location.x, location.y);
    }
    public bool AddBlockinGrid(Vector2Int location, ColorPlayer player)
    {
        return gridBlockTable.AddBlock(location.x, location.y, player); ;
    }

    public bool SpawnObstacle(Vector2Int _location)
    {
        if (LocationIsEmpty(_location))
        {
            Vector2 _postion = GetGridPosittion(_location);
            Onhand = GameManager.Instance.NewBlock(CellSize, _postion, _location, transform.parent, ColorPlayer.Black);
            AddBlockinGrid(_location, ColorPlayer.Black);
            return true;
        }
        return false;
    }
    #endregion
    #region OnPointer  
    protected void OnRectTransformDimensionsChange()
    {
        if (rectTransform != null)
        {
            Vector2 size = rectTransform.rect.size;
            UpdateCellSizeByScreen();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        GameManager.Instance.OnPointerEnter(eventData); 

    }
    public void OnPointerMove(PointerEventData eventData)
    {
      GameManager.Instance.OnPointerMove(eventData); 
    }

    public void OnPointerClick(PointerEventData eventData)
    {
          GameManager.Instance.OnPointerClick(eventData); 

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        GameManager.Instance.OnPointerExit(eventData); 
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