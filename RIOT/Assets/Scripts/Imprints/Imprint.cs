using UnityEngine;
using System.Collections;

public class Imprint : MonoBehaviour
{
    public SpriteRenderer sprite;

    public void SetTransforms(Vector3 pos, Quaternion rot, Vector3 parentScale)
    {
        transform.position = pos;
        //transform.localScale = parentScale;
        sprite.transform.rotation = rot;
    }
}
