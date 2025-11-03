using UnityEngine;
using TMPro;

public class Scan : MonoBehaviour
{
    // UI + canvas will be updated to black out entire screen
    [Header("UI")]
    public TMP_Text objectScanned;
    public TMP_Text distance;
    public TMP_Text certainty;

    [Header("Scan System")]
    public Transform playerCamera;
    public float scanDist = 100f;
    // play with range + default scan result + scene probably so theres less floor

    [Header("Audio Settings")]
    public AudioClip sonarPing;
    private AudioSource playerAudio;

    private void Start()
    {
        playerAudio = GetComponent<AudioSource>();
    }



    void Update()
    {
        // change to occur on keypress
        if (Input.GetKeyDown(KeyCode.E))
        {
            ScanForObject();
            playerAudio.PlayOneShot(sonarPing);
        }

        //ScanForObject();

    }

    void ScanForObject()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward); 
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, scanDist)) 
        {
            Debug.DrawLine(ray.origin, ray.direction * scanDist, Color.green, 5f); //debug raycast line
            objectScanned.text = hit.collider.tag;
            distance.text = $"{hit.distance:F2} m";

            Certainty certaintyBase = hit.collider.GetComponent<Certainty>();
            if (certaintyBase != null)
            {
                float t = Mathf.Clamp01(1f - (hit.distance / scanDist));
                float certaintyCalc = Mathf.Lerp(certaintyBase.minRange, certaintyBase.maxRange, t);

                certainty.text = $"{certaintyCalc*100f:F2}%";
            }
            // will add % certainty based on distance
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.direction * scanDist, Color.red, 5f);
            // reset text also ?
        }


    }
}
