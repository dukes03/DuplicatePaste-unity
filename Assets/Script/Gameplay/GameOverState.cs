using UnityEngine;

public class GameOverState : IGameState
{
    public void EnterState(GameManager gameManager)
    {
        for (int numPlayer = 0; numPlayer < gameManager.Playerdatas.Count; numPlayer++)
        {
            gameManager.Playerdatas[numPlayer].Score = 0;
            for (int x = 0; x < gameManager.InsGridBlock.AxisX; x++)
            {
                for (int y = 0; y < gameManager.InsGridBlock.AxisY; y++)
                {
                    if (gameManager.InsGridBlock.LocationIsOwn(new Vector2Int(x, y), gameManager.Playerdatas[numPlayer].Color))
                    {
                        gameManager.Playerdatas[numPlayer].Score += 1;
                    }
                }
            }
            Debug.Log(gameManager.Playerdatas[numPlayer].Name + gameManager.Playerdatas[numPlayer].Score);
        }


    }
    public void OnDone() { }
}
