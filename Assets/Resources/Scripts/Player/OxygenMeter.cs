using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxygenMeter : MonoBehaviour
{
    public int Oxygen { get; set; }

    public Transform Meter;
    [Range(0, 60)] public float OxygenTick;
    [Range(0, 100)] public int ShortOfBreathTrigger;
    public GameObject DeathPanelUI;

    private Animator _animator;
    private bool _shortOfBreath;
    private bool _dead;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _shortOfBreath = false;
        _dead = false;
        Oxygen = 100;
    }

    public void BeginDepletingOxygen()
    {
        InvokeRepeating("DepleteOxygen", OxygenTick, OxygenTick);
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
            _animator.SetBool("isChecking", true);
        else if (Input.GetKeyUp(KeyCode.Tab))
            _animator.SetBool("isChecking", false);

        //when player is struggling they start to lose their breath

        if (Oxygen <= ShortOfBreathTrigger && !_shortOfBreath)
        {
            _shortOfBreath = true;
            FindObjectOfType<AudioManager>().PlayAudio("breathShortness");
        }
        else if (Oxygen > ShortOfBreathTrigger && _shortOfBreath)
        {
            FindObjectOfType<AudioManager>().End("breathShortness");
            _shortOfBreath = false;
        }

        if (Oxygen < 0 && !_dead)
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
        _dead = true;
        FindObjectOfType<AudioManager>().PlayAudio("deathMusic");
        DeathPanelUI.SetActive(true);

        Cursor.lockState = CursorLockMode.None; // Show and unlock cursor
        Cursor.visible = true;
    }
}
