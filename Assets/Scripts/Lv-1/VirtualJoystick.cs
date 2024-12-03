using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    public Transform joystickHandle;  // Reference to the joystick handle (the part that moves)
    public float moveRange = 50f;  // Maximum distance the joystick handle can move
    public Vector2 inputDirection;  // The current direction the joystick is pointing

    private RectTransform joystickBackground;

    void Start()
    {
        joystickBackground = GetComponent<RectTransform>();
    }

    // Called when the user begins dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        // Reset the handle position to the start position (center of the background)
        joystickHandle.localPosition = Vector3.zero;
    }

    // Called while dragging
    public void OnDrag(PointerEventData eventData)
    {
        // Calculate joystick position relative to the background
        Vector2 position = eventData.position - (Vector2)joystickBackground.position;

        // Limit the movement to the specified range (moveRange)
        if (position.magnitude > moveRange)
        {
            position = position.normalized * moveRange;
        }

        // Set the handle position
        joystickHandle.localPosition = new Vector3(position.x, position.y, 0);

        // Set the input direction based on the joystick handle's position
        inputDirection = position / moveRange;
    }

    // Called when the user stops dragging
    public void OnEndDrag(PointerEventData eventData)
    {
        // Return the joystick handle to the center when dragging ends
        joystickHandle.localPosition = Vector3.zero;
        inputDirection = Vector2.zero;
    }
}
