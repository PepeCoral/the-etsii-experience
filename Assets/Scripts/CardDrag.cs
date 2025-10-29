using System.Collections;
using UnityEngine;

using UnityEngine.InputSystem;
public enum ScreenZone { LEFT, CENTER, RIGHT };

[RequireComponent(typeof(Collider2D))]
public class CardDrag : MonoBehaviour
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

    [SerializeField] private SpriteRenderer baseSpriteRenderer;

    [SerializeField] private Sprite baseCard, reverseCard;
    [SerializeField] private float turnOverSpeed;


    

    private ScreenZone currentScreenZone;

    public delegate void _OnCardMovedToZone(ScreenZone screenZone);
    public static event _OnCardMovedToZone OnCardMovedToZone;


    private void Start()
    {
        mainCamera = Camera.main;
        targetPosition = transform.position;
        defaultTargetPosition = targetPosition;
    }


    private void OnEnable()
    {
        CardManager.OnFinishedProcessingEffects += resetCard;
    }

    private void OnDisable()
    {
        CardManager.OnFinishedProcessingEffects += resetCard;
        
    }

    private void resetCard()
    {
        targetPosition = defaultTargetPosition;
        transform.position = defaultTargetPosition;
        currentScreenZone = ScreenZone.CENTER;
        if (OnCardMovedToZone != null)
        {
            OnCardMovedToZone(currentScreenZone);
        }
        StartCoroutine(DealCard());
    }


    IEnumerator DealCard()
    {
        baseSpriteRenderer.sprite = reverseCard;
        yield return null;

        for (int i = 0; i <90; i++ )
        {
            Vector3 newRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + 1, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newRotation);
            yield return new WaitForSeconds(turnOverSpeed);
        }

        baseSpriteRenderer.sprite = baseCard;

        for (int i = 0; i < 90; i++)

        {
            Vector3 newRotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y - 1, transform.rotation.eulerAngles.z);
            transform.rotation = Quaternion.Euler(newRotation);
            yield return new WaitForSeconds(turnOverSpeed);
        }

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

        }

        if (currentScreenZone != getScreenZone())
        {
            currentScreenZone = getScreenZone();
            if (OnCardMovedToZone != null)
            {
                OnCardMovedToZone(currentScreenZone);
            }

        }

    }

    private ScreenZone getScreenZone()
    {
       return transform.position.x > activationDistanceThreshold ? ScreenZone.RIGHT : (transform.position.x < -activationDistanceThreshold ? ScreenZone.LEFT : ScreenZone.CENTER);
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
