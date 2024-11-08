using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject backgroundPrefab; 

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneCoroutine(sceneName));
    }

    private IEnumerator LoadSceneCoroutine(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        yield return null; 
        TeleportPlayerToCenter(); 
        gameManager.StartTransition(); 
        yield return new WaitForSeconds(1f);
    }

    private void TeleportPlayerToCenter()
    {
        Player player = FindObjectOfType<Player>();
        if (player != null)
        {
            player.transform.position = Vector3.zero;
        }
    }

}
