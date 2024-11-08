using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public float rotateSpeed = 0.5f;
    private Player player;
    private LevelManager levelManager;

    void Start()
    {
        player = FindObjectOfType<Player>();
        levelManager = FindObjectOfType<LevelManager>();
        transform.position = Vector3.zero; 
    }

    void Update()
    {
        if (player == null || player.currentWeapon == null)
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            return;
        }

        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<Collider2D>().enabled = true;

        transform.Rotate(Vector3.forward, rotateSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(WaitAndLoadScene());
        }
    }

    private IEnumerator WaitAndLoadScene()
    {
        levelManager.gameManager.EndTransition(); 
        yield return new WaitForSeconds(2f); 
        levelManager.LoadScene("Main");
    }
}
