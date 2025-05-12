using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
public class PanelPlayer : MonoBehaviour
{
    [SerializeField] public TMP_InputField textInput;
    [SerializeField] public TextMeshProUGUI Playername;
    [SerializeField] public TextMeshProUGUI Score;
    [SerializeField] public GameObject PanelScore;
    [SerializeField] public RawImage IconPlayer;
    public RectTransform panel;
    private Vector2 hiddenPosition = new Vector2(-300f, 0f);
    private Vector2 shownPosition = new Vector2(0f, 0f);
    private bool isVisible = true;
    private Coroutine slideCoroutine;
    public void ApplyColor(ColorPlayer colorType, RawImage targetImage)
    {
        Color color = Color.white;

        switch (colorType)
        {
            case ColorPlayer.Red:
                color = Color.red;
                break;
            case ColorPlayer.Green:
                color = Color.green;
                break;
            case ColorPlayer.Blue:
                color = Color.blue;
                break;
            case ColorPlayer.Yellow:
                color = Color.yellow;
                break;
            case ColorPlayer.Black:
                color = Color.black;
                break;
        }

        targetImage.color = color;
    }
    public void SetScore(int score)
    {
        PanelScore.SetActive(true);
        Score.text = score.ToString();
    }
    public void SetHide()
    {
        shownPosition = new Vector2(-150, panel.anchoredPosition.y);
        hiddenPosition = new Vector2(100, panel.anchoredPosition.y);
        TogglePanel();
        // panel.anchoredPosition = hiddenPosition;
    }
    public void ComeBack()
    {
        if (slideCoroutine != null)
            StopCoroutine(slideCoroutine);
   
    }
    public void TogglePanel()
    {
        isVisible = !isVisible;
        if (slideCoroutine != null)
            StopCoroutine(slideCoroutine);
        slideCoroutine = StartCoroutine(Slide(isVisible ? shownPosition : hiddenPosition));
    }
    public void SlideTo(Vector2 targetPosition)
    {
        slideCoroutine = StartCoroutine(Slide(targetPosition));
    }

    IEnumerator Slide(Vector2 targetPosition)
    {
        while (Vector2.Distance(panel.anchoredPosition, targetPosition) > 0.3f)
        {
            panel.anchoredPosition = Vector2.Lerp(panel.anchoredPosition, targetPosition, Time.deltaTime * 10f);
            yield return null;
        }
        panel.anchoredPosition = targetPosition;
    }
}
