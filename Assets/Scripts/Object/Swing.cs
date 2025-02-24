using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{
    [Range(0, 180)]
    [SerializeField] private int SwingAngle = 120;
    [SerializeField] private int SwingSpeed = 1;

    // Update is called once per frame
    void Update()
    {
        SwingAction();
    }

    void SwingAction()
    {
        float newZrotation = Mathf.PingPong(Time.time * SwingSpeed, SwingAngle) - SwingAngle / 2;
        transform.rotation = Quaternion.Euler(0, 0, newZrotation);
    }
}
