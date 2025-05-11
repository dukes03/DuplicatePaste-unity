using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelPlayer;
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;

    }

    public void SetupPanelPlayer()
    {
        gameManager = GameManager.Instance;
        for (int i = 0; i < gameManager.Playerdatas.Count; i++)
        {
            PanelPlayer _panelPlayer = panelPlayer[i].GetComponent<PanelPlayer>();
            _panelPlayer.gameObject.SetActive(true);
            _panelPlayer.ApplyColor(gameManager.Playerdatas[i].Color, _panelPlayer.IconPlayer);
            _panelPlayer.Playername.text = gameManager.Playerdatas[i].Name;
            _panelPlayer.Playername.gameObject.SetActive(true);
            _panelPlayer.SetHide();

        }
    }
    public void TogglePanelPlayer()
    {
        panelPlayer[ GameManager.Instance.TurnOrder].GetComponent<PanelPlayer>().TogglePanel();
    }
    public void PassTurn()
    {
        GameManager.Instance.PassTurn();
    }
}
