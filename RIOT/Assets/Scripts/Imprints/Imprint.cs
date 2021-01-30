using UnityEngine;
using System.Collections;

public class Imprint : MonoBehaviour
{
    public SpriteRenderer sprite;

    public void SetTransforms(Vector3 pos, Quaternion rot, Vector3 scale)
    {
        transform.position = pos;
        transform.localScale = scale;
        sprite.transform.rotation = rot;
    }
}
