using UnityEngine;
using UnityEngine.EventSystems;
//For use only when playing games, can be reset.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] public GridBlock gridBlock;
    [SerializeField] private GameObject block;
    private IGameState currentState;
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
        SetState(new SetUpMapState());
    }



    #endregion
    public GameObject NewBlock(float CellSize, Vector2 pos, Vector2Int location, Transform Parent, ColorPlayer player)
    {
        GameObject blockData = Instantiate(block, pos, block.transform.rotation, Parent);
        blockData.GetComponent<BlockData>().init(location, player);
        return blockData;
    }
    public void SetState(IGameState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public void OnDoneState(int indexRow, int indexColumn)
    {
        currentState?.OnDone(indexRow, indexColumn);
    }


}
