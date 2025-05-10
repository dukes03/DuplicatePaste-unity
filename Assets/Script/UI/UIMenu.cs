using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialPages;
    [SerializeField] private GameObject tutorial;
    [SerializeField] private Toggle toggleSFX;
    [SerializeField] private Toggle toggleMusic;
    private int currentPage = 0;
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
        tutorial.SetActive(false);
       
    }
    void Start()
    {
         UpdateSoundToggle();
    }

    #endregion
    #region Tutorial
    /* When you first open it, always start on page 1. 
    But if it is already open, turn it off. */
    public void OpenTutorial()
    {
        if (!tutorial.activeSelf)
        {
            tutorial.SetActive(true);
            currentPage = 0;
            ShowPage(currentPage);
        }
        else { EndTutorial(); }

    }
    private void ShowPage(int index)
    {
        for (int i = 0; i < tutorialPages.Length; i++)
        {
            tutorialPages[i].SetActive(i == index);
        }
    }
    public void NextPage()
    {
        currentPage++;

        if (currentPage >= tutorialPages.Length)
        {
            PlaySoundBnt();
            EndTutorial();
        }
        else
        {
            PlaySoundBnt();
            ShowPage(currentPage);
        }
    }

    public void PreviousPage()
    {
        if (currentPage > 0)
        {
            currentPage--;
            PlaySoundBnt();
            ShowPage(currentPage);
        }
    }

    // When you open to the last page
    public void EndTutorial()
    {
        tutorial.SetActive(false);
        ShowPage(tutorialPages.Length);
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
        toggleSFX.isOn = SoundManager.Instance.TurnOnSFX;
        toggleMusic.isOn = SoundManager.Instance.TurnOnMusic;
    }
    #endregion


}
