using UnityEngine;
using UnityEngine.EventSystems;

public interface IGameState
{
    
    void EnterState(GameManager gameManager);
    void OnDone();
    #region OnPointer  
    void OnPointerEnter(PointerEventData eventData)
    {
    }
    void OnPointerMove(PointerEventData eventData)
    {

    }

    void OnPointerClick(PointerEventData eventData)
    {

    }
    void OnPointerExit(PointerEventData eventData)
    {

    }
    #endregion
}
