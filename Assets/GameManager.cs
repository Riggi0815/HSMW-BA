using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    public GameObject Player => player;

    public static GameManager Instance;

    //Singleton Check
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
}
