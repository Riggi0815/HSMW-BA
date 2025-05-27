using System;
using UnityEngine;
using UnityEngine.UI;

public class ExpBar : MonoBehaviour
{

    public static ExpBar Instance;
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

    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] Slider slider;
    [SerializeField] PlayerStats playerStats;

    void OnEnable()
    {
        playerStats.OnLevelUp += IncrementLvl;
        playerStats.OnExpUpdate += UpdateBar;
    }

    private void Start(){
        text.text = "Lvl 0";
        slider.value = 0;
    }

    public void IncrementLvl(float lvl){
        Debug.Log("Level Up: " + lvl);
        text.text = "Lvl " + lvl.ToString();
    }


    public void UpdateBar(float currentExp, float maxExp){
        slider.value = currentExp / maxExp;
    }

}
