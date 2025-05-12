using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private List<GameObject> panelPlayer;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private Toggle toggleSFX;
    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Button bntAddplayer;
    [SerializeField] private Button bntReMove;
    private int currentPlayer = 1;
    private static UIMenu instance;
    public static UIMenu Instance { get { return instance; } }


    #region Lifecycle
    //Set Instance
    void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        UpdateSoundToggle();
        for (int i = 0; i < panelPlayer.Count; i++)
        {
            PanelPlayer _panelPlayer = panelPlayer[i].GetComponent<PanelPlayer>();
            _panelPlayer.Playername.transform.gameObject.SetActive(false);
            _panelPlayer.textInput.transform.gameObject.SetActive(true);
        }
    }

    #endregion
    #region UI
    public void AddPlayer()
    {
        currentPlayer += 1;
        bntReMove.interactable = true;
        panelPlayer[currentPlayer].SetActive(true);
        if (currentPlayer >= 3)
        {
            bntAddplayer.interactable = false;
            currentPlayer = 3;
        }

    }
    public void RemovePlayer()
    {
        panelPlayer[currentPlayer].SetActive(false);
        currentPlayer -= 1;
        bntAddplayer.interactable = true;
        if (currentPlayer <= 1)
        {
            bntReMove.interactable = false;
            currentPlayer = 1;
        }
        else
        {

        }

    }
    public void SetPlayerdatas()
    {
        AppManager.Instance.Playerdatas = GetPlayerdatas();
        NextScene();
    }
    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }
    public List<Playerdata> GetPlayerdatas()
    {
        List<Playerdata> playerdatas = new List<Playerdata>();
        for (int i = 0; i <= currentPlayer; i++)
        {
            Playerdata _Playerdata = new Playerdata();
            PanelPlayer _panelPlayer = panelPlayer[i].GetComponent<PanelPlayer>();
            _Playerdata.Name = _panelPlayer.textInput.text;
            _Playerdata.Color = (ColorPlayer)i + 1;
            _Playerdata.Order = i;
            playerdatas.Add(_Playerdata);
        }
        return playerdatas;
    }
    #endregion

    #region Sound
    public void SetTurnOnMusic(bool isTurnOn)
    {
        SoundManager.Instance.TurnOnMusic = isTurnOn;
    }
    public void SetTurnOnSFX(bool isTurnOn)
    {
        SoundManager.Instance.TurnOnSFX = isTurnOn;
    }
    private void PlaySoundBnt()
    {
        SoundManager.Instance.PlaySoundBnt();

    }

    private void UpdateSoundToggle()
    {
        if (toggleSFX != null)
        {
            toggleSFX.isOn = SoundManager.Instance.TurnOnSFX;
        }
        if (toggleMusic != null)
        {
            toggleMusic.isOn = SoundManager.Instance.TurnOnMusic;
        }
    }
    #endregion


}
