using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class SetUpPlayerState : IGameState
{
    GameObject onhand;//block hold and wait to drop
    GridBlock gridBlock;
    RectTransform rectTransform;
    Vector2Int locationOnhand;
    GameManager gameManager;
    Playerdata playerdata;
    public void EnterState(GameManager _gameManager)
    {
        gameManager = _gameManager;
        gridBlock = gameManager.InsGridBlock;
        playerdata = gameManager.Playerdatas[gameManager.TurnOrder];
    }
    public void OnDone()
    {

        if (GameManager.Instance.NextTurnPlayer())
        {
            GameManager.Instance.SetState(new SetUpPlayerState());
        }
        else
        {
            GameManager.Instance.TurnOrder = 0;
            GameManager.Instance.SetState(new TurnPlayerState());
        }
    }
    #region OnPointer  

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (onhand == null)
        {
            Vector2Int _location = gridBlock.GetGridLocation(eventData.position);
            Vector2 _postion = gridBlock.GetGridPosittion(_location);
            locationOnhand = _location;
            onhand = gameManager.NewBlock(gridBlock.CellSize, _postion, _location, gridBlock.transform.parent, playerdata.Color);
        }

    }
    public void OnPointerMove(PointerEventData eventData)
    {
        Vector2Int _location = gridBlock.GetGridLocation(eventData.position);
        if (onhand != null)
        {

            if (gridBlock.GetGridLocation(eventData.position) != locationOnhand)
            {
                locationOnhand = _location;
                onhand.transform.position = gridBlock.GetGridPosittion(_location);
                gridBlock.BlockCanPlace(onhand, _location);
            }
        }
        else
        {
            onhand = gridBlock.SpawnBlock(_location, playerdata.Color);
            gridBlock.BlockCanPlace(onhand, _location);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("OnPointerClick");
        if (onhand != null)
        {
            Vector2Int _location = gridBlock.GetGridLocation(eventData.position);
            if (gridBlock.LocationIsEmpty(_location))
            {
                Debug.Log("LocationIsEmpty");
                gridBlock.AddBlockinGrid(_location, playerdata.Color);

                GameManager.Instance.OnDoneState();
                onhand = null;
            }
        }

    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onhand != null)
        {
            gameManager.DestroyGameObj(onhand);
            onhand = null;
        }
    }
    #endregion
}
