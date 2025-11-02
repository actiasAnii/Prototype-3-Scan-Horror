using UnityEngine;
using TMPro;

public class Scan : MonoBehaviour
{
    // UI + canvas will be updated to black out entire screen
    [Header("UI")]
    public TMP_Text objectScanned;
    public TMP_Text distance;

    [Header("Scan System")]
    public Transform playerCamera;
    public float scanDist = 50f;

    

    void Update()
    {
        // if we want it to be on keypress
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    ScanForObject();
        //}

        ScanForObject();

    }

    void ScanForObject()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward); 
        RaycastHit hit;

        
        if (Physics.Raycast(ray, out hit, scanDist)) 
        {
            Debug.DrawLine(ray.origin, ray.direction * scanDist, Color.green, 5f); //debug raycast line
            objectScanned.text = hit.collider.tag;
            distance.text = hit.distance.ToString("F2") + " m";
            // will add % certainty based on distance
        }
        else
        {
            Debug.DrawLine(ray.origin, ray.direction * scanDist, Color.red, 5f);
        }


    }
}
