using UnityEngine;

public class SetUpMapState : IGameState
{
    Vector2Int location;
    Vector2Int gridSize;
    public void EnterState(GameManager gameManager)
    {
        gridSize.x = gameManager.gridBlock.Columns;
        gridSize.y = gameManager.gridBlock.Rows;


    }
    private void spawnObstacle(int countObstacle, int countTry, int maxObstacle)
    {
        if (countObstacle < maxObstacle)
        {


            location.x = Random.Range(0, gridSize.x);
            location.y = Random.Range(0, gridSize.y);
            if (GameManager.Instance.gridBlock.SpawnObstacle(location))
            {
                spawnObstacle(countObstacle + 1, 0, maxObstacle);
            }
            else
            {
                Debug.Log("Fail Rnd countObstacle : " + countObstacle + " countTry: " + countTry);
                spawnObstacle(countObstacle, countTry + 1, maxObstacle);
            }
        }

    }

    public void OnDone(int indexRow, int indexColumn)
    {
        spawnObstacle(0, 0, 8);
        GameManager.Instance.SetState(new SetUpPlayerState());
    }
}
