using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGameplay : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelPlayer;
    [SerializeField] public List<GameObject> Ranking;
    [SerializeField] private GameObject GameOver;
    [SerializeField] private GameObject bntPass;
    [SerializeField] private GameObject Podium;
    private GameManager gameManager;
    void Awake()
    {
        gameManager = GameManager.Instance;

    }

    public void SetupPanelPlayer()
    {
        gameManager = GameManager.Instance;
        GameOver.SetActive(false);
        bntPass.SetActive(false);
        for (int i = 0; i < gameManager.Playerdatas.Count; i++)
        {
            PanelPlayer _panelPlayer = panelPlayer[i].GetComponent<PanelPlayer>();
            _panelPlayer.gameObject.SetActive(true);
            _panelPlayer.ApplyColor(gameManager.Playerdatas[i].Color, _panelPlayer.IconPlayer);
            _panelPlayer.Playername.text = gameManager.Playerdatas[i].Name;
            _panelPlayer.Playername.gameObject.SetActive(true);
            _panelPlayer.SetHide();
            Ranking[i].SetActive(true);

        }
    }
    public void ShowGameOver()
    { GameOver.SetActive(true); }
    public void ShowbntPass()
    { bntPass.SetActive(true); }
    public void SlideTo(int player, int rank)
    {
        GameObject _objpanelPlayer = panelPlayer[player];
        PanelPlayer _panelPlayer = _objpanelPlayer.GetComponent<PanelPlayer>();  
          _panelPlayer.SetScore(GameManager.Instance.Playerdatas[player].Score);
        _panelPlayer.ComeBack();
        _objpanelPlayer.transform.SetParent(Podium.transform);
    
        _panelPlayer.panel.sizeDelta = new Vector2(400, 200);
    }
    public void TogglePanelPlayer()
    {
        panelPlayer[GameManager.Instance.TurnOrder].GetComponent<PanelPlayer>().TogglePanel();
    }
    public void PassTurn()
    {
        GameManager.Instance.PassTurn();
    }
}
