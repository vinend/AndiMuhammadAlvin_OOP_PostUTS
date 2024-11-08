using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Serialized field untuk menentukan maxSpeed, timeToFullSpeed, timeToStop, dan juga stopClamp dalam sumbu X dan juga Y
    [SerializeField] private Vector2 maxSpeed = new Vector2(7.0f, 5.0f); 
    [SerializeField] private Vector2 timeToFullSpeed = new Vector2(1.0f, 1.0f);
    [SerializeField] private Vector2 timeToStop = new Vector2(0.5f, 0.5f); 
    [SerializeField] private Vector2 stopClamp = new Vector2(2.5f, 2.5f); 

    // Penginisialisasian vector-vector yang digunakan di dalam script PlayerMovement kita.
    private Vector2 moveDirection; // Arah pergerakan yang dimasukkan oleh pemain
    private Vector2 moveVelocity; // Kecepatan bergerak dengan pengaturan akselerasi
    private Vector2 moveFriction; // Gaya gesekan yang ditentukan untuk menghentikan Player perlahan
    private Vector2 stopFriction; // Gaya gesekan untuk memperlambat player secara cepat
    private Rigidbody2D rb; // Menginisialisasikan komponen Rigidbody2D untuk mengatur gerakan objek kita

    void Start()
    {   
        // Mendapatkan komponen Rigidbody2D pada objek dan juga menentukan perhitungan Velocity, moveFriction, dan juga stopFriction
        rb = GetComponent<Rigidbody2D>();
        moveVelocity = 2 * maxSpeed / timeToFullSpeed;  
        moveFriction = -2 * maxSpeed / (timeToFullSpeed * timeToFullSpeed); 
        stopFriction = -2 * maxSpeed / (timeToStop * timeToStop);  
    }

    // Fungsi untuk menggerakkan spaceship kita berdasarkan input dari pemain
    public void Move()
    {   
        // Menentukan input gerakan horizontal dan vertikal beserta menormalkannya untuk menghindari pergerakan yang terlalu cepat
        moveDirection = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;

        // Jika ada input dari player, maka akan dijalankan block code untuk menjalankan spaceship di game kita.
        if (moveDirection != Vector2.zero)
        {   
            // Mengatur velocity untuk sumbu X
            rb.velocity = new Vector2(
                Mathf.Clamp(rb.velocity.x + moveDirection.x * moveVelocity.x * Time.fixedDeltaTime * 2 + (GetFriction().x * Time.fixedDeltaTime), -maxSpeed.x, maxSpeed.x),
                // Mengatur velocity untuk sumbu Y
                Mathf.Clamp(rb.velocity.y + moveDirection.y * moveVelocity.y * Time.fixedDeltaTime * 2 + (GetFriction().y * Time.fixedDeltaTime), -maxSpeed.y, maxSpeed.y)
            );
        }
        // Jika tidak ada input maka dia akan set kecepatan di 0
        else
        {
            rb.velocity = Vector2.zero;
        }

    }

    // Friction untuk mendapatkan gaya gesekan yang diperlukan
    private Vector2 GetFriction()
    {
        float frictionScale = moveDirection != Vector2.zero ? moveFriction.magnitude : stopFriction.magnitude;
        return -rb.velocity.normalized * frictionScale;
    }

    public bool IsMoving()
    {
        return Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0;
    }
}
