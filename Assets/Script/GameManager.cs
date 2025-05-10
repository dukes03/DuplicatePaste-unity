using UnityEngine;
using UnityEngine.EventSystems;
//For use only when playing games, can be reset.
public class GameManager : MonoBehaviour, IPointerClickHandler
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] private GameObject block;
    #region Lifecycle
    //Set Instance
    void Awake()
    {
        if (Instance == null)
        {
            instance = this;
        }
    }
    #endregion
    public GameObject NewBlock(float CellSize, Vector2 pos, Vector2Int location, Transform Parent)
    {
        GameObject blockData = Instantiate(block, pos, block.transform.rotation, Parent);
        blockData.GetComponent<BlockData>().init(location, ColorPlayer.Red);
        return blockData;
    }
    public void OnPointerClick(PointerEventData eventData)
    {

    }


}
