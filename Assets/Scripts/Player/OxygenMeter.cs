using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMeter : MonoBehaviour
{
    public int Oxygen { get; set; }

    public Transform Meter;
    [Range(0, 60)] public float OxygenTick;
    public GameObject DeathPanelUI;

    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        Oxygen = 100;

        InvokeRepeating("DepleteOxygen", OxygenTick, OxygenTick);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            _animator.SetBool("isChecking", true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            _animator.SetBool("isChecking", false);

        if (Oxygen < 0)
        {
            CancelInvoke("DepleteOxygen");
            PlayerDead();
        }
    }

    private void DepleteOxygen()
    {
        Oxygen--;
        Meter.localScale = new Vector3(Meter.localScale.x - 0.01f, Meter.localScale.y, Meter.localScale.z);
    }

    public void UpdateMeterOnSuccessfulSuck(int increase)
    {
        Oxygen += increase;
        Oxygen = Mathf.Clamp(Oxygen, 0, 100);

        Meter.localScale = new Vector3(Oxygen / 100f, Meter.localScale.y, Meter.localScale.z);
    }

    private void PlayerDead()
    {
        DeathPanelUI.SetActive(true);
    }
}
