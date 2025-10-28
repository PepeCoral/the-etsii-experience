using UnityEngine;

using UnityEngine.InputSystem;

[RequireComponent(typeof(Collider2D))]
public class CardDragNewInput : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 offset;
    private bool isDragging = false;
    [SerializeField] private Vector2 defaultTargetPosition;
    [SerializeField] private Vector3 targetPosition;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float followSpeed;

    [SerializeField] private float activationDistanceThreshold;
    [SerializeField] private float targetXOnActivation;

    private void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
        defaultTargetPosition = targetPosition;
    }

    private void Update()
    {
        var mouse = Mouse.current;
        if (mouse == null) return;
        Vector3 mouseWorldPos = getMouseWorldPosition(mouse);

        if (mouse.leftButton.wasPressedThisFrame)
        {
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                startDragging(mouseWorldPos);
            }
        }

        if (mouse.leftButton.isPressed && isDragging)
        {
            transform.position = Vector3.Lerp(transform.position, mouseWorldPos + offset,Time.deltaTime * followSpeed);
        }

        if (mouse.leftButton.wasReleasedThisFrame && isDragging)
        {
            isDragging = false;
            setNewTargetPosition();
        }

        if (!isDragging)
        {
            returnToTargetPosition();
        }


        if(Mathf.Abs(Mathf.Abs(transform.position.x) - targetXOnActivation) < 0.01f)
        {
            if(transform.position.x < 0) 
            { GameManager.Instance.MakeDecision(true); }
            else 
            { GameManager.Instance.MakeDecision(false); }

                returnToTheCenter();
        } 
    }

    private void returnToTheCenter()
    {
        targetPosition = defaultTargetPosition;
        transform.position = defaultTargetPosition;
    }

    private void startDragging(Vector3 mouseWorldPos)
    {
        isDragging = true;
        offset = transform.position - mouseWorldPos;
    }

    private void setNewTargetPosition()
    {
        float targetX = transform.position.x > activationDistanceThreshold ? targetXOnActivation : (transform.position.x < -activationDistanceThreshold ? -targetXOnActivation : 0);
        targetPosition = new Vector3(targetX, defaultTargetPosition.y);
    }

    private void returnToTargetPosition()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * returnSpeed);
    }

    private Vector3 getMouseWorldPosition(Mouse mouse)
    {
        return mainCamera.ScreenToWorldPoint(new Vector3(
            mouse.position.x.ReadValue(),
            mouse.position.y.ReadValue(),
           0f
        ));
    }
}
