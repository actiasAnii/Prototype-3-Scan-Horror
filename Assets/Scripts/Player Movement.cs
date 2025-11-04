using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // basic first person controls
    
    [Header("Movement Settings")]
    public float speed = 5f;

    [Header("Mouse Look Settings")]
    public float sensitivity = 2f;
    public Transform playerCamera; 
    public float lowerBound = 90f;
    public float upperBound = 25f;

    private float pitch = 0f;
    private Rigidbody rb;
    private Vector3 moveinput;

    [Header("Audio Settings")]
    public AudioClip footsteps;
    private AudioSource playerAudio;
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        AudioSource[] sources =GetComponents<AudioSource>();
        playerAudio = sources[0];
        playerAudio.clip = footsteps;
        playerAudio.loop = true;
        playerAudio.playOnAwake = false;
        playerAudio.spatialBlend = 0f;
        playerAudio.volume = 10f;

        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

    }

    void Update()
    {
        HandleLook();
        HandleMoveInput();
    }

    private void FixedUpdate()
    {
        HandleMove();
    }

    void HandleLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // yaw
        transform.Rotate(Vector3.up * mouseX);

        // pitch
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, -upperBound, lowerBound);

        playerCamera.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void HandleMove()
    {
        Vector3 targetVelocity = moveinput * speed;
        Vector3 velocity = rb.linearVelocity;
        Vector3 velocityChange = targetVelocity - new Vector3(velocity.x, 0, velocity.z);
        rb.AddForce(velocityChange, ForceMode.VelocityChange);

        bool isMoving = moveinput.magnitude > 0.1f;

        if (isMoving )
        {
            if (!playerAudio.isPlaying)
            {
                playerAudio.Play();
                // Debug.Log("footsteps playing");

            }
               

        }
        else
        {
            if (playerAudio.isPlaying)
            {
                playerAudio.Pause();
                // Debug.Log("footsteps stopped");
            }

        }
    }

    void HandleMoveInput()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        moveinput = (transform.right * x + transform.forward * z).normalized;
    }
}