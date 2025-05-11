using UnityEngine;
using UnityEngine.EventSystems;

public class TurnPlayerState : IGameState
{

    GameObject onhand;//block hold and wait to drop
    GridBlock gridBlock;
    RectTransform rectTransform;
    Vector2Int locationOnhand;
    GameManager gameManager;
    Playerdata playerdata; public void EnterState(GameManager _gameManager)
    {
        gameManager = _gameManager;
        gridBlock = gameManager.InsGridBlock;
        playerdata = gameManager.Playerdatas[gameManager.TurnOrder];
        onhand = null;
    }
    public void OnDone()
    {
        if (GameManager.Instance.CanNextTurn())
        {
            if (GameManager.Instance.NextTurnPlayer())
            {
                GameManager.Instance.SetState(new TurnPlayerState());
            }
            else
            {
                GameManager.Instance.TurnOrder = 0;
                GameManager.Instance.SetState(new TurnPlayerState());
            }
        }
        else
        {
            GameManager.Instance.SetState(new GameOverState());
        }
    }
    #region OnPointer  

    public void OnPointerMove(PointerEventData eventData)
    {
        if (onhand != null)
        {
            Vector2Int _location = gridBlock.GetGridLocation(eventData.position);
            if (gridBlock.GetGridLocation(eventData.position) != locationOnhand)
            {
                locationOnhand = _location;
                onhand.transform.position = gridBlock.GetGridPosittion(_location);
                gridBlock.BlockCanPlace(onhand, _location);
            }
        }
         
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2Int _location = gridBlock.GetGridLocation(eventData.position);
        Debug.Log("OnPointerClick");
        if (onhand != null)
        {

            if (gridBlock.LocationIsEmpty(_location))
            {
                Debug.Log("LocationIsEmpty");
                gridBlock.AddBlockinGrid(_location, playerdata.Color);

                GameManager.Instance.OnDoneState();

            }
        }
        else if (gridBlock.LocationIsOwn(_location, playerdata.Color))
        {

            Vector2 _postion = gridBlock.GetGridPosittion(_location);

            onhand = gridBlock.SpawnBlock(_location, playerdata.Color);
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
