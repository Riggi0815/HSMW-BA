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
    [SerializeField] GameObject[] upgradeTexts;
    private float currentLvl;

    void OnEnable()
    {
        playerStats.OnLevelUp += IncrementLvl;
        playerStats.OnExpUpdate += UpdateBar;
    }

    private void Start(){
        text.text = "Lvl 0";
        slider.value = 0;
    }

    public void IncrementLvl(float lvl)
    {
        currentLvl = lvl;
        if (currentLvl >= 5)
        {
            text.text = "Lvl MAX";
        }
        else
        {
            text.text = "Lvl " + currentLvl.ToString();
        }
        if (currentLvl == 0)
        {
            for (int i = 0; i < upgradeTexts.Length; i++)
            {
                upgradeTexts[i].SetActive(false);
            }
        }else if (currentLvl <= 5)
        {
            upgradeTexts[(int)currentLvl - 1].SetActive(true);
        }
        
    }


    public void UpdateBar(float currentExp, float maxExp){
        if (currentLvl >= 5)
        {
            slider.value = 1;
            return;
        }
        else
        {
            slider.value = currentExp / maxExp;
        }
        
    }

}
