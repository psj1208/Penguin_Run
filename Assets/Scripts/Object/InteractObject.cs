using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractObject : MonoBehaviour
{
    protected int influenceFigure;

    protected BoxCollider2D col;
    protected SpriteRenderer objectRenderer;

    public abstract void OnInteraction(PlayerController pController);
}
