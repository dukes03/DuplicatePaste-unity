using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
//For use only when playing games, can be reset.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }
    [SerializeField] public GridBlock InsGridBlock;
    [SerializeField] private GameObject block;
    private IGameState currentState;
    [SerializeField] public List<Playerdata> Playerdatas;
    [SerializeField] public int TurnOrder = 0;

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
    public bool NextTurnPlayer()
    {
        TurnOrder += 1;
        if (TurnOrder < Playerdatas.Count)
        {
            return true;
        }
        TurnOrder = 0;
        return false;
    }
    public void OnDoneState(int indexRow, int indexColumn)
    {
        currentState?.OnDone(indexRow, indexColumn);
    }
    #region OnPointer  
    public void OnPointerEnter(PointerEventData eventData)
    {
        currentState?.OnPointerEnter(eventData);
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        currentState?.OnPointerMove(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        currentState?.OnPointerClick(eventData);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        currentState?.OnPointerExit(eventData);
    }
    #endregion
    public void DestroyGameObj(GameObject obj)
    {
        Destroy(obj);
    }

}
