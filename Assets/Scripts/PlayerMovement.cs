using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] float moveSpeed; 
    [SerializeField] float rotationSpeed; 
    private Rigidbody2D rb;
    private Vector2 moveInput;

    [Header("Dash Settings")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    private bool isDashing = false;
    private bool canDash = true;

    [SerializeField] PlayerStats playerStats;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing)
            return;

        rb.linearVelocity = moveInput * moveSpeed; 
        
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, moveInput);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        moveInput.Normalize();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && canDash && playerStats.CurrentLvl >= 1)
        {
            StartCoroutine(DashCoroutine());
        }
    }

    IEnumerator DashCoroutine()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = moveInput * dashSpeed;
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
}
