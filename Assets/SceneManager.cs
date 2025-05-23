using UnityEngine;

public class SceneManager : MonoBehaviour
{
    [SerializeField] private Camera menuCamera;
    [SerializeField] private Camera gameCamera;
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject menuUI;

    private void Start()
    {
        Time.timeScale = 0f;
        menuCamera.gameObject.SetActive(true);
        gameCamera.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        Time.timeScale = 1f;
        menuCamera.gameObject.SetActive(false);
        gameCamera.gameObject.SetActive(true);
        gameUI.SetActive(true);
        menuUI.SetActive(false);
    }

    public void MenuSwitch()
    {
        Time.timeScale = 0f;
        menuCamera.gameObject.SetActive(true);
        gameCamera.gameObject.SetActive(false);
        gameUI.SetActive(false);
        menuUI.SetActive(true);
    }
}
