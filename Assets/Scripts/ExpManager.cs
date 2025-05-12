using UnityEngine;

public class ExpManager : MonoBehaviour
{
    
    public static ExpManager Instance;

    public delegate void ExpChangeHandler(int amout);
    public event ExpChangeHandler OnExpChange;

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


    public void AddExp(int amout)
    {
        //Add exp event
        OnExpChange?.Invoke(amout);
    }
}
