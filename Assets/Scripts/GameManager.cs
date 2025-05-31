using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject player;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMPro.TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameCompletePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject bulletHolder;
    [SerializeField] private GameObject spawner;
    [SerializeField] private WeaponManager weaponManager;

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

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = $"Bis Welle {WaveManager.Instance.CurrentWave} überlebt";
        Time.timeScale = 0f;
    }

    public void GameComplete()
    {
        gameCompletePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ResetGame()
    {
        gameOverPanel.SetActive(false);
        player.transform.position = new Vector3(0, 0, 0);
        player.transform.rotation = new Quaternion(0, 0, 0, 0);
        WaveManager.Instance.CurrentWave = 0;
        weaponManager.Reset();
        foreach (Transform child in bulletHolder.transform)
        {
            Destroy(child.gameObject);
        }
        foreach (Transform child in spawner.transform)
        {
            Destroy(child.gameObject);
        }

        player.GetComponent<PlayerStats>().Reset();
        player.GetComponent<PlayerStats>().CurrentLvl = 0;
        Spawner.Instance.Reset();

    }
}
