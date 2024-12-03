using System.Collections.Generic;
using UnityEngine;

public class FirstPersonMovement : MonoBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 inputDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        // Si no hay movimiento, no realizar cambios en la rotación
        if (inputDirection.sqrMagnitude > 0.01f)
        {
            // Obtener la dirección deseada
            Vector3 direction = new Vector3(inputDirection.x, 0, inputDirection.y).normalized;

            // Rotar al jugador hacia la dirección de movimiento
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, 0.2f);
        }

        // Calcular la velocidad objetivo en base al input y la velocidad actual
        Vector2 targetVelocity = inputDirection * targetMovingSpeed;

        // Aplicar movimiento.
        Vector3 velocity = new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
        rigidbody.velocity = velocity;
    }
}
