using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;

public class ChooseWeaponUIDocument : MonoBehaviour
{
    private UIDocument document;
    private VisualElement root;
    [SerializeField] private float fadeOutDuration = 1.0833334f;
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        document = GetComponent<UIDocument>();
        root = document.rootVisualElement;
        
        // Set initial opacity
        if (root != null)
        {
            root.style.opacity = 1f;
            Debug.Log("UI initialized with opacity: " + root.style.opacity.value);
        }
        else
        {
            Debug.LogError("Root visual element is null!");
        }
    }

    public void FadeOutUI()
    {
        if (root == null)
        {
            Debug.LogError("Cannot fade UI - root element is null!");
            return;
        }
        Debug.Log("Starting fade out from opacity: " + root.style.opacity.value);
        StartCoroutine(FadeOutRoutine());
    }

    private IEnumerator FadeOutRoutine()
    {
        float elapsedTime = 0f;
        float startValue = root.style.opacity.value;
        Debug.Log("Fade starting from value: " + startValue);

        while (elapsedTime < fadeOutDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startValue, 0f, elapsedTime / fadeOutDuration);
            root.style.opacity = newAlpha;
            Debug.Log("Current opacity: " + newAlpha);
            yield return null;
        }

        root.style.opacity = 0f;
        Debug.Log("Fade complete, final opacity: " + root.style.opacity.value);
        gameManager.StartTransition();
    }
}