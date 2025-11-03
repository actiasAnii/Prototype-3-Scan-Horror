using UnityEngine;
using TMPro;

// referenced this tuto https://youtu.be/nEtUkUfQXXo?si=zn6XURWclwpODMv0 

public class Compass : MonoBehaviour
{
    [Header("Compass Labels")]
    public RectTransform North;
    public RectTransform East;
    public RectTransform South;
    public RectTransform West;

    [Header("Camera")]
    public Camera mainCamera;

    private TMP_Text northText;
    private TMP_Text eastText;
    private TMP_Text southText;
    private TMP_Text westText;

    private float camAngle;

    private void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;

        if (North != null) northText = North.GetComponent<TMP_Text>();
        if (East != null) eastText = East.GetComponent<TMP_Text>();
        if (South != null) southText = South.GetComponent<TMP_Text>();
        if (West != null) westText = West.GetComponent<TMP_Text>();

        //// debug checks
        //if (mainCamera == null)
        //    Debug.LogError("Compass: no camera assigned");

        //if (northText == null || eastText == null || southText == null || westText == null)
        //    Debug.LogError("Compass: tmps not found");
    }

    private void Update()
    {

        camAngle = mainCamera.transform.eulerAngles.y;

        string activeDir = GetCurrentDirection(camAngle);
        UpdateCompassDisplay(activeDir);
        UpdateCompassPosition(activeDir, camAngle);
    }

    private string GetCurrentDirection(float angle)
    {
        if (angle >= 315 || angle < 45) return "North";
        if (angle >= 45 && angle < 135) return "East";
        if (angle >= 135 && angle < 225) return "South";
        return "West";
    }

    private void UpdateCompassDisplay(string active)
    {
        northText.text = (active == "North") ? "N" : "";
        eastText.text = (active == "East") ? "E" : "";
        southText.text = (active == "South") ? "S" : "";
        westText.text = (active == "West") ? "W" : "";
    }

    private void UpdateCompassPosition(string dir, float angle)
    {
        float xPos = 0f;
        const float yPos = 175;
        const float scale = 4f;

        switch (dir)
        {
            case "North":
                xPos = ((angle >= 315) ? (360 - angle) : (0 - angle)) * scale;
                North.localPosition = new Vector3(xPos, yPos, North.localPosition.z);
                break;
            case "East":
                xPos = (90 - angle) * scale;
                East.localPosition = new Vector3(xPos, yPos, East.localPosition.z);
                break;
            case "South":
                xPos = (180 - angle) * scale;
                South.localPosition = new Vector3(xPos, yPos, South.localPosition.z);
                break;
            case "West":
                xPos = (270 - angle) * scale;
                West.localPosition = new Vector3(xPos, yPos, West.localPosition.z);
                break;
        }
    }
}

