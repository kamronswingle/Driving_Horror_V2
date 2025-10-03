using Unity.VisualScripting.InputSystem;
using UnityEngine;

public class CarHandler : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField] 
    Transform gameModel;
    
    // Max limits
    private float maxSteerVelocity = 10f;
    private float accelerationMultipler = 3f;
    private float breakMultipler = 15f;
    private float steeringMultiplier = 5f;
    private float maxForwardVelocity = 8f;
    
    Vector2 input = Vector2.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate car model when turning
        gameModel.transform.rotation = Quaternion.Euler(0, rb.linearVelocity.x * 5, 0);
    }

    private void FixedUpdate()
    {
        if (input.y > 0)
        {
            Accelerate();
        }
        else
        {
            rb.linearDamping = 0.2f;
        }

        if (input.y < 0)
        {
            Break(); 
        }

        Steer();
        
        // Force the car to not go backwards
        if (rb.linearVelocity.z <= 0)
        {
            rb.linearVelocity = Vector3.zero;
        }
    }

    private void Accelerate()
    {
        rb.linearDamping = 0;
        if (rb.linearVelocity.z >= maxForwardVelocity)
        {
            return;
        }
        rb.AddForce(rb.transform.forward * accelerationMultipler * input.y);
    }

    private void Break()
    {
        if (rb.linearVelocity.z <= 0)
        {
            return;
        }
        rb.AddForce(rb.transform.forward * breakMultipler * input.y);
    }

    private void Steer()
    {
        if (Mathf.Abs(input.x) > 0)
        {
            // Moving the car sideways
            float speedBasedSteerLimit = rb.linearVelocity.z / 5.0f;
            speedBasedSteerLimit = Mathf.Clamp01(speedBasedSteerLimit);
            
            
            rb.AddForce(rb.transform.right * steeringMultiplier * input.x * speedBasedSteerLimit);
            
            // Normalize the x-velocity
            float normalizedX = rb.linearVelocity.x / maxSteerVelocity;
            
            // Ensure we don't allow it to get bigger than 1 in magnitude
            normalizedX = Mathf.Clamp(normalizedX, -1.0f, 1.0f);
            
            // Make sure we turn within the limit
            rb.linearVelocity = new Vector3(normalizedX * maxSteerVelocity, 0, rb.linearVelocity.z);
        }
        else
        {
            {
                // Auto center car
                rb.linearVelocity = Vector3.Lerp(rb.linearVelocity, new Vector3(0, 0, rb.linearVelocity.z), Time.fixedDeltaTime * 3);
            }
        }
    }

    public void SetInput(Vector2 inputVector)
    {
        inputVector.Normalize();
        
        input = inputVector;
    }
}
