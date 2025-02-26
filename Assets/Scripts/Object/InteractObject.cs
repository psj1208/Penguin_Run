using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    protected int influenceFigure;
    protected AudioClip sfx;

    protected BoxCollider2D col;
    protected SpriteRenderer objectRenderer;

    public abstract void OnInteraction(StatHandler sHandler);
}
