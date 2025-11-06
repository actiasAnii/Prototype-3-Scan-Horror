using UnityEngine;
using System.Collections;
using TMPro;

public class GameStartManager : MonoBehaviour
{
    public GameObject startCanvas;

    public float startingTime = 5f;
    public TMP_Text countdown;

    private float remainingTime;

    void Start()
    {
        startCanvas.SetActive(true);

        remainingTime = startingTime;

        StartCoroutine(ShowGameAfterDelay());
    }

    IEnumerator ShowGameAfterDelay()
    {
        while (remainingTime > 0)
        {
            countdown.text = $"BEGIN SCENARIO IN: {Mathf.CeilToInt(remainingTime)}";
            yield return null;

            remainingTime -= Time.deltaTime;
        }

        countdown.text = "0";

        startCanvas.SetActive(false);
    }
}
