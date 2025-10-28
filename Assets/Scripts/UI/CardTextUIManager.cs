using System.Collections;
using TMPro;
using UnityEngine;

public class CardTextUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI main,left,right;
    [SerializeField] float typingSpeed;

    private void Start()
    {
        GameManager.Instance.UpdateUI();

    }
    private void OnEnable()
    {
        CardManager.OnCardUpdated += updateTexts;
    }

    private void OnDisable()
    {
        CardManager.OnCardUpdated -= updateTexts;
        
    }

    private void updateTexts()
    {
        StartCoroutine(TypeAllTexts(main, GameManager.Instance.getCurrentCard().mainText));
    }


    private IEnumerator TypeAllTexts(TextMeshProUGUI textComponent, string fullText)
    {
        textComponent.text = "";
        left.text = "";
        right.text = "";

        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
        StartCoroutine(TypeText(left, GameManager.Instance.getCurrentCard().leftOptionName));
        StartCoroutine(TypeText(right, GameManager.Instance.getCurrentCard().rightOptionName));
        yield return null;


    }
    public IEnumerator TypeText(TextMeshProUGUI textComponent, string fullText)
    {
        textComponent.text = ""; // Clear any existing text first

        for (int i = 0; i <= fullText.Length; i++)
        {
            textComponent.text = fullText.Substring(0, i);
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
