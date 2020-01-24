using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMeter : MonoBehaviour
{
    public Transform Meter;
    [Range(0, 60)] public float OxygenTick;
    public GameObject DeathPanelUI;

    private Animator _animator;
    private int _oxygen;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _oxygen = 100;

        InvokeRepeating("DepleteOxygen", OxygenTick, OxygenTick);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            _animator.SetBool("isChecking", true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            _animator.SetBool("isChecking", false);

        if (_oxygen < 0)
        {
            CancelInvoke("DepleteOxygen");
            PlayerDead();
        }
    }

    private void DepleteOxygen()
    {
        _oxygen--;
        Meter.localScale = new Vector3(Meter.localScale.x - 0.01f, Meter.localScale.y, Meter.localScale.z);
    }

    public void UpdateMeterOnSuccessfulSuck(int increase)
    {
        _oxygen += increase;
        _oxygen = Mathf.Clamp(_oxygen, 0, 100);

        Meter.localScale = new Vector3(_oxygen / 100f, Meter.localScale.y, Meter.localScale.z);
    }

    private void PlayerDead()
    {

    }
}
