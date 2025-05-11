using UnityEngine;

public interface IGameState
{
    void EnterState(GameManager gameManager);
    void OnDone(int indexRow, int indexColumn);
}
