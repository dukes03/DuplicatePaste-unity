using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridBlock : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,
 IPointerMoveHandler, IPointerClickHandler
{
    public int AxisX = 10;
    public int AxisY = 10;
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
    void Awake() => rectTransform = GetComponent<RectTransform>();
    void Start()
    {
        Debug.Log(rectTransform.sizeDelta + " " + RectTransformUtility.PixelAdjustRect(rectTransform, canvas));
        UpdateCellSizeByScreen();
        gridBlockTable.init(AxisX, AxisY);
    }
    #endregion
    #region Grid  
    void UpdateCellSizeByScreen() => CellSize = (rectTransform.sizeDelta.x / AxisX) * (Screen.width / 1920f);

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
    public void BlockCanPlace(GameObject Block, Vector2Int location)
    {
        if (LocationIsEmpty(location))
        {
            Block.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            Block.transform.localScale = new Vector3(.75f, .75f, .75f);
        }
    }
    public bool LocationIsEmpty(Vector2Int location) => gridBlockTable.IsEmpty(location.x, location.y);
    public bool LocationIsOwn(Vector2Int location, ColorPlayer player) => gridBlockTable.IsOwn(location.x, location.y, player);
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
    public GameObject SpawnBlock(Vector2Int _location, ColorPlayer player)
    {
        Vector2 _postion = GetGridPosittion(_location);
        locationOnhand = _location;
        return GameManager.Instance.NewBlock(CellSize, _postion, _location, transform.parent, player);
    }
    public List<Vector2Int> GetConnectedOwnBlock(int startX, int startY, ColorPlayer own)
    {
        var result = new List<Vector2Int>();
        var queue = new Queue<Vector2Int>();
        var visited = new bool[AxisX, AxisY];
        queue.Enqueue(new Vector2Int(startX, startY));
        visited[startX, startY] = true;
        Vector2Int[] directions = { new Vector2Int(0, -1), new Vector2Int(-1, 0), new Vector2Int(1, 0), new Vector2Int(0, 1), };
        while (queue.Count > 0)
        {
            var grid = queue.Dequeue();
            result.Add(grid);
            foreach (var direc in directions)
            {
                int newX = grid.x + direc.x;
                int newY = grid.y + direc.y;
                if (newX < 0 || newY < 0 || newX >= AxisX || newY >= AxisY)
                    continue;
                if (visited[newX, newY])
                    continue;
                visited[newX, newY] = true;
                if (gridBlockTable.IsOwn(newX, newY, own))
                {
                    queue.Enqueue(new Vector2Int(newX, newY));
                }
            }
        }
        return result;
    }
    #endregion
    #region OnPointer  
    protected void OnRectTransformDimensionsChange()
    {
        if (rectTransform == null)
        {
            return;
        }
        Vector2 size = rectTransform.rect.size;
        UpdateCellSizeByScreen();
    }
    public void OnPointerEnter(PointerEventData eventData) => GameManager.Instance.OnPointerEnter(eventData);
    public void OnPointerMove(PointerEventData eventData) => GameManager.Instance.OnPointerMove(eventData);

    public void OnPointerClick(PointerEventData eventData) => GameManager.Instance.OnPointerClick(eventData);
    public void OnPointerExit(PointerEventData eventData) => GameManager.Instance.OnPointerExit(eventData);
    #endregion

}