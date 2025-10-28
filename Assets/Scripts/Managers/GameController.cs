using UnityEngine;

public class GameController : MonoBehaviour
{
    TestInput input;

    private void Awake()
    {
        input = new TestInput();
    }
    private void OnEnable()
    {
        input.Enable();
        input.Test.Left.performed += ctx => GameManager.Instance.MakeDecision(true);
        input.Test.Right.performed += ctx => GameManager.Instance.MakeDecision(false);
    }

    private void OnDisable()
    {
        input.Disable();
        input.Test.Left.performed -= ctx => GameManager.Instance.MakeDecision(true);
        input.Test.Right.performed -= ctx => GameManager.Instance.MakeDecision(false);
    }
}
