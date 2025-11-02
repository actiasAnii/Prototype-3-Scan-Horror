using UnityEditor.Rendering;
using UnityEngine;

[DisallowMultipleComponent]
public class Certainty : MonoBehaviour
{
    [Range(0f, 1f)] public float certainty;   
    [Range(0f, 1f)] public float minRange;    
    [Range(0f, 1f)] public float maxRange;    
    private bool initialized = false;

    private void Awake()
    {
        if (initialized) return;


        Vector2 tagRange = GetTagRange(gameObject.tag);
        certainty = Random.Range(tagRange.x, tagRange.y);

        float variance = certainty * Random.Range(0.05f, 0.10f);
        minRange = Mathf.Clamp01(certainty - variance);
        maxRange = Mathf.Clamp01(certainty + variance);

        initialized = true;
    }

    private Vector2 GetTagRange(string tag)
    {
        return tag switch
        {
            "Wall" => new Vector2(0.9f, 0.97f),
            "Floor" => new Vector2(0.92f, 0.98f),
            "Chair" => new Vector2(0.2f, 0.3f),
            "Door" => new Vector2(0.7f, 0.8f),
            "Vending Machine" => new Vector2(0.3f, 0.5f),
            "Battery" => new Vector2(0.3f, 0.6f),
            "Table" => new Vector2(0.7f, 0.88f),
            "Shield Core" => new Vector2(0.6f, 0.8f),
            "Shield Generator" => new Vector2(0.45f, 0.65f),
            "Storage Container" => new Vector2(0.65f, 0.8f),
            "Projector" => new Vector2(0.32f, 0.6f),
            "???????" => new Vector2(0.01f, 0.05f),
            _ => new Vector2(0f, 1f)
        };
    }


}
