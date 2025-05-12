using System.Collections.Generic;
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
        }
        List<Playerdata> _playerdatas = new List<Playerdata>();

        _playerdatas.AddRange(gameManager.Playerdatas);
        _playerdatas.Sort((b, a) => a.Score.CompareTo(b.Score));
        gameManager.UIGameplay.ShowGameOver();
        for (int i = 0; i < _playerdatas.Count; i++)
        {
            Debug.Log(_playerdatas[i].Score);
            gameManager.UIGameplay.SlideTo(_playerdatas[i].Order, i);
        }
    }
    public void OnDone() { }
}
