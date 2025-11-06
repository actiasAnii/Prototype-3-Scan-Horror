using UnityEngine;

[RequireComponent(typeof(Collider))]
public class TriggerEndCondition : MonoBehaviour
{
    [Tooltip("Door = Win，Monster = Lose")]
    public string objectTag = "Door";
    public string playerTag = "Player";

    private void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(playerTag)) return;

        if (objectTag == "Door")
        {
            foreach (AudioSource audio in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
                audio.Stop();
            GameEndManager.I?.EndGame(true);
        }
            
        else if (objectTag == "???????")
        {
            foreach (AudioSource audio in FindObjectsByType<AudioSource>(FindObjectsSortMode.None))
                audio.Stop();
            GameEndManager.I?.EndGame(false);
        }
            
    }
}
