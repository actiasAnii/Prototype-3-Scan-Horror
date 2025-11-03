using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.Rendering;


public class MonsterResponse : MonoBehaviour
{
    [Header("Audio Settings")]
    private AudioSource monsterAudio;
    public int clipCount = 15;
    public string clipPath = "Sounds/monster-growl_";
    public float maxDist = 100f;
    private List<AudioClip> growlClips = new List<AudioClip>();

    [Header("UI Indicator References")]
    public Image front;
    public Image behind;
    public Image right;
    public Image left;
    public Color flashColor = Color.white;
    public Color restColor = new Color32(56, 56, 56, 255);
    public float flashDuration = 0.5f;


    //not implementing actual monster movement/attack functionality yet

    void Start()
    {
        monsterAudio = GetComponent<AudioSource>();

        // check
        monsterAudio.loop = false;
        monsterAudio.playOnAwake = false;
        monsterAudio.volume = 10f; // adjust probably ???
        monsterAudio.spatialBlend = 1f; // 3d
        monsterAudio.rolloffMode = AudioRolloffMode.Linear; // also maybe adjust
        monsterAudio.maxDistance = maxDist;


        LoadGrowlClips();
    }


    void LoadGrowlClips()
    {
        for (int i = 1; i <= clipCount; i++)
        {
            string clipName = $"{clipPath}{i}";
            AudioClip clip = Resources.Load<AudioClip>(clipName);
            if (clip != null)
            {
                growlClips.Add(clip);
                // Debug.Log($"Loaded clip {i}");
            }
            else
                Debug.LogWarning($"Missing AudioClip: {clipName}");
        }

    }

    // play random audio clip and show direction indicator
    public void OnPlayerPing()
    {
        // limit monster perception to a certain range?
        AudioClip clip = growlClips[Random.Range(0, growlClips.Count)];
        monsterAudio.PlayOneShot(clip);
        Debug.Log("Monster growl tiggered");

        DirectionIndicate();

    }
    // referenced this https://discussions.unity.com/t/using-vector3-dot-for-direction-facing-calculation/499619
    void DirectionIndicate()
    {
        if (Camera.main == null) return;

        Transform cam = Camera.main.transform;
        Vector3 toMonster = (transform.position - cam.position).normalized;

        // flatten to point forward and right horizontally
        Vector3 flatCamForward = Vector3.ProjectOnPlane(cam.forward, Vector3.up).normalized;
        Vector3 flatCamRight = Vector3.ProjectOnPlane(cam.right, Vector3.up).normalized;

        float forwardDot = Vector3.Dot(flatCamForward, toMonster);  // >0 = front, <0 = behind
        float rightDot = Vector3.Dot(flatCamRight, toMonster);      // >0 = right, <0 = left

        Image target = null;
        if (Mathf.Abs(forwardDot) > Mathf.Abs(rightDot)) // go with whatevers larger.
                                                         // could adjust to flash up to 2 diff indiaticators if we want it to be more specific
            target = (forwardDot > 0) ? front : behind;
        else
            target = (rightDot > 0) ? right : left;

        if (target != null)
            StartCoroutine(FlashIndicator(target));
    }

    IEnumerator FlashIndicator(Image img)
    {
        if (img == null) yield break;

        img.color = flashColor;
        yield return new WaitForSeconds(flashDuration);
        img.color = restColor;
    }


}
