using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerCompletePanel : MonoBehaviour
{
    public Animator ScoreAnimator;
    public OxygenMeter OxygenMeter;

    private bool _triggered;

    private void Start()
    {
        _triggered = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player") && !_triggered)
        {
            _triggered = true;
            OxygenMeter.enabled = false;
            ScoreAnimator.SetTrigger("Complete");
        }
    }
}
