using UnityEngine;
//For use only when playing games, can be reset.
public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance { get { return instance; } }

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
}
