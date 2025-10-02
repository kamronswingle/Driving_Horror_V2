using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField]
    CarHandler carHandler;

    void Update()
    {
        Vector2 input = Vector2.zero;
        
        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        
        carHandler.SetInput(input);
    }
}
