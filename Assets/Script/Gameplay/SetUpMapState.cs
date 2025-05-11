using UnityEngine;

public class SetUpMapState : IGameState
{
    Vector2Int location;
    Vector2Int gridSize;
    GridBlock gridBlock;
    public void EnterState(GameManager gameManager)
    {
        gridBlock = gameManager.InsGridBlock;
        gridSize.x = gridBlock.Columns;
        gridSize.y = gridBlock.Rows;
    }
    private void spawnObstacle(int countObstacle, int countTry, int maxObstacle)
    {
        if (countObstacle < maxObstacle)
        {
            location.x = Random.Range(0, gridSize.x);
            location.y = Random.Range(0, gridSize.y);
            if (gridBlock.SpawnObstacle(location))
            {
                spawnObstacle(countObstacle + 1, 0, maxObstacle);
            }
            else
            {
                spawnObstacle(countObstacle, countTry + 1, maxObstacle);
            }
        }

    }

    public void OnDone()
    {
        spawnObstacle(0, 0, 8);
        GameManager.Instance.SetState(new SetUpPlayerState());
        Debug.Log("OnDone SetUpPlayerState");
    }
}
