using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnetic : MonoBehaviour
{
    [SerializeField] private float pullSpeed = 5;
    [SerializeField] private float duration;
    bool isTurnOn = false;

    public void Init(float pull,float dur)
    {
        isTurnOn = true;
        pullSpeed = pull;
        duration = dur;
        transform.localPosition = Vector2.zero;
    }

    private void Update()
    {
        if (isTurnOn)
        {
            if(duration <= 0)
                Destroy(gameObject);
            int itemLayerMask = 1 << LayerMask.NameToLayer("Item");
            Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(transform.position, 5, itemLayerMask);
            foreach (Collider2D collider in collider2Ds)
            {
                PullObject(collider.transform);
            }
            duration -= Time.deltaTime;
        }
    }

    void PullObject(Transform target)
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            Vector2 direction = (transform.position - target.position).normalized;
            rb.MovePosition(rb.position + direction * pullSpeed * Time.deltaTime);
        }
    }
}
