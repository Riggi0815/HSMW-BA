using System.Linq;
using UnityEngine;

public class RotateAroundPlayer : MonoBehaviour
{

    [Header("Rotation Settings")]
    [SerializeField] GameObject pivot;
    [SerializeField] AutoWeapon autoWeapon;
    [SerializeField] private float rotationSpeed;

    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Rotate(Transform target)
    {
        // Rotates Weapon around the player
        Vector2 direction = target.position - transform.position;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.FromToRotation(Vector2.up, direction), rotationSpeed * Time.deltaTime);
    }

    
}
