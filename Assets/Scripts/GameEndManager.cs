using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndManager : MonoBehaviour
{
    public static GameEndManager I { get; private set; }

    [Header("UI")]
    public GameObject endPanel;
    public TMP_Text titleText;
    public TMP_Text subtitleText;

    [Header("Cursor")]
    public bool unlockCursorOnEnd = true;

    private bool ended;

    private void Awake()
    {
        if (I != null && I != this) { Destroy(gameObject); return; }
        I = this;
        if (endPanel) endPanel.SetActive(false);
    }

    public void EndGame(bool win)
    {
        if (ended) return;
        ended = true;

        if (endPanel) endPanel.SetActive(true);

        if (titleText)
            titleText.text = win ? "You Escaped!" : "You DIED!";
        if (subtitleText)
            subtitleText.text = "Press R to Restart\nEsc to Quit";

        Time.timeScale = 0f;

        if (unlockCursorOnEnd)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void Update()
    {
        if (!ended) return;

        if (Input.GetKeyDown(KeyCode.R))
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
        }
    }
}
