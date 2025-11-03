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

    [Header("Audio Settings")]
    public AudioClip footsteps;
    private AudioSource playerAudio;
    void Start()
    {

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        playerAudio = GetComponent<AudioSource>();
        playerAudio.clip = footsteps;
        playerAudio.loop = true;
        playerAudio.playOnAwake = false;

    }

    void Update()
    {
        HandleLook();
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
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        if (move.sqrMagnitude > 1f)
            move.Normalize();

        bool isMoving = move.magnitude > 0.1f;

        transform.position += move * speed * Time.deltaTime;

        if (isMoving )
        {
            playerAudio.Play();

        }
        else
        {
            if (playerAudio.isPlaying)
            {
                playerAudio.Stop();
            }

        }
    }
}