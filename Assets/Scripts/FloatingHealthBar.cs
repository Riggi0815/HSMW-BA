using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    
    [SerializeField] Slider slider;
    [SerializeField] Camera camera;
    [SerializeField] Transform target;
    [SerializeField] UnityEngine.Vector3 offset;

    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        slider.value = currentHealth / maxHealth;
    }

    private void Start() {
        if (camera == null)
        {
            camera = Camera.main;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = camera.transform.rotation;
        transform.position = target.position + offset;
    }
}
