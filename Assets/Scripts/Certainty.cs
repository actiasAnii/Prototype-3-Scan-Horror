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
            "Chair" => new Vector2(0.1f, 0.25f),
            "Door" => new Vector2(0.5f, 0.8f),
            "Vending Machine" => new Vector2(0.15f, 0.45f),
            "Battery" => new Vector2(0.25f, 0.4f),
            "Table" => new Vector2(0.4f, 0.6f),
            "Shield Core" => new Vector2(0.3f, 0.7f),
            "Shield Generator" => new Vector2(0.2f, 0.4f),
            "Storage Container" => new Vector2(0.65f, 0.8f),
            "Projector" => new Vector2(0.12f, 0.35f),
            "???????" => new Vector2(0.01f, 0.05f),
            _ => new Vector2(0f, 1f)
        };
    }


}
