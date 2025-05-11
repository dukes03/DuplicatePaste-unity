using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TurnPlayerState : IGameState
{

    List<GameObject> onhand;//block hold and wait to drop
    List<Vector2Int> difflocationOnhand;
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
                if (!GameManager.Instance.Playerdatas[0].IsPass)
                {
                     GameManager.Instance.SetState(new TurnPlayerState());
                }
                else{
                    OnDone();
                }
               
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
                for (int i = 0; i < onhand.Count; i++)
                {
                    onhand[i].transform.position = gridBlock.GetGridPosittion(_location + difflocationOnhand[i]);
                    gridBlock.BlockCanPlace(onhand[i], _location + difflocationOnhand[i]);
                }

            }
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Vector2Int _location = gridBlock.GetGridLocation(eventData.position);
        Debug.Log("OnPointerClick");
        if (onhand != null)
        {
            bool allIsEmpty = true;
            for (int i = 0; i < onhand.Count; i++)
            {
                if (!gridBlock.LocationIsEmpty(_location + difflocationOnhand[i]))
                {
                    allIsEmpty = false;
                }
            }
            if (allIsEmpty)
            {
                for (int i = 0; i < onhand.Count; i++)
                {
                    gridBlock.AddBlockinGrid(_location + difflocationOnhand[i], playerdata.Color);
                }
                GameManager.Instance.OnDoneState();
            }
        }
        else if (gridBlock.LocationIsOwn(_location, playerdata.Color))
        {
            List<Vector2Int> group = new List<Vector2Int>();
            onhand = new List<GameObject>();
            difflocationOnhand = new List<Vector2Int>();
            group = gridBlock.GetConnectedOwnBlock(_location.x, _location.y, playerdata.Color);
            locationOnhand = _location;
            foreach (var item in group)
            {
                onhand.Add(gridBlock.SpawnBlock(_location, playerdata.Color));
                difflocationOnhand.Add(item - locationOnhand);
                //Debug.Log(item.ToString());
            }

        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (onhand != null)
        {
            int count = onhand.Count;
            for (int i = 0; i < count; i++)
            {
                gameManager.DestroyGameObj(onhand[onhand.Count - 1]);
                onhand.RemoveAt(onhand.Count - 1);
            }
            difflocationOnhand = null;
            onhand = null;
        }
    }
    #endregion
}
