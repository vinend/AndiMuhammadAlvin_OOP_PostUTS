using UnityEngine;

public class Player : MonoBehaviour
{   
    // Pada keseluruhan dari game, sebuah instance dibuat. Untuk mengakses player dari mana saja di game kita.
    public static Player Instance { get; private set; }
   // Variabel untuk menyimpan komponen PlayerMovement dan juga Animator, untuk animation mesin kita di spaceship
    private PlayerMovement playerMovement;
    private Animator animator;

    // Fungsi untuk menginisialisasikan objek Player
    private void Awake()
    {
        // Menginisialisasikan singleton
        InitializeSingleton();
        // Menyimpan komponen dari PlayerMovements ke dalam playerMovement
        playerMovement = GetComponent<PlayerMovement>();
        // Mendapatkan komponen animator dari EngineEffect
        animator = GameObject.Find("EngineEffect")?.GetComponent<Animator>();
    }

    // FixedUpdate dipanggil setiap frame fisika
    private void FixedUpdate()
    {   
        // Memanggil fungsi Move() dari playerMovement
        playerMovement?.Move();
    }

    // Fungsi yang dipanggil setelah semua update selesai, untuk memperbarui animasi berdasarkan pergerakan pemain
    private void LateUpdate()
    {
        UpdateAnimationState();
    }

    // Fungsi untuk menginisialisasikan singleton 
    private void InitializeSingleton()
    {   
        // Kalau misalkan instance-nya sudah ada, maka objek yang akan dibuat tidak akan diinisialisasikan.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        // Jika instance objek belum ada maka objek akan diinisialisasikan
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    //Untuk update status animator untuk efek mesin
    private void UpdateAnimationState()
    {
        if (animator != null)
        {
            animator.SetBool("IsMoving", playerMovement.IsMoving());
        }
    }
}

