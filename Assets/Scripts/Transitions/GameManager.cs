using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject sceneTransition;
    private Animator transitionAnimator;

    private void Awake()
    {
        transitionAnimator = sceneTransition.GetComponent<Animator>();
        DontDestroyOnLoad(gameObject);
    }

    public void StartTransition()
    {
        transitionAnimator.Play("StartTransition");
    }

    public void EndTransition()
    {
        transitionAnimator.Play("EndTransition");
    }

    public void ClearObjects()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag != "Player" && obj.tag != "MainCamera")
            {
                Destroy(obj);
            }
        }
    }
}
