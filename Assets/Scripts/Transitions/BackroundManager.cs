using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Sprite backgroundSprite;
    [SerializeField] private RuntimeAnimatorController animatorController;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        CheckCurrentScene();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CheckCurrentScene();
    }

    private void CheckCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name != "Main")
        {
            TurnVisual(false);
            return;
        }

        SetupComponents();
        TurnVisual(true);
    }

    private void SetupComponents()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        }

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = gameObject.AddComponent<Animator>();
        }

        if (backgroundSprite != null)
        {
            spriteRenderer.sprite = backgroundSprite;
            spriteRenderer.sortingOrder = -1;
        }

        if (animatorController != null)
        {
            animator.runtimeAnimatorController = animatorController;
        }
    }
    
    private void TurnVisual(bool on)
    {
        this.GetComponent<SpriteRenderer>().enabled = on;
        this.GetComponent<Animator>().enabled = on;
    }
}
