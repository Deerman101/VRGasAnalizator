using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class GasAnalyzer : MonoBehaviour
{
    public GameObject screenCanvas; // Canvas на экране газоанализатора
    public TextMeshProUGUI statusText; // текст на экране газоанализатора
    public GameObject resultCanvas;

    private XRGrabInteractable _grab;
    private AudioSource _audioSource;
    public bool _checkedGas = false;
    public bool _checkedFire = false;

    void Awake()
    {
        _grab = GetComponent<XRGrabInteractable>();
        _audioSource = GetComponent<AudioSource>();
        _audioSource.mute = true;
    }

    void OnEnable()
    {
        _grab.selectEntered.AddListener(OnGrab);
        _grab.selectExited.AddListener(OnRelease);
    }

    void OnDisable()
    {
        _grab.selectEntered.RemoveListener(OnGrab);
        _grab.selectExited.RemoveListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        screenCanvas.SetActive(true);
        _audioSource.mute = true;
        SetNormal();
    }

    void OnRelease(SelectExitEventArgs args)
    {
        //screenCanvas.SetActive(false); 
    }

    private void OnTriggerStay(Collider other)
    {
        GasBurner burner = other.GetComponentInParent<GasBurner>();
        if (burner != null)
        {
            if (burner.isGasOn && !burner.isFireOn)
            {
                SetAlarm();
                _checkedGas = true;
                _audioSource.mute = false;
            }
            else
            {
                SetNormal();
                _checkedFire = true;
                _audioSource.mute = true;
            }
        }

        CheckComplete();
    }

    void CheckComplete()
    {
        if (_checkedGas && _checkedFire)
            resultCanvas.SetActive(true);
    }

    void SetNormal()
    {
        statusText.text = "Содержание газа в воздухе в норме.";
        statusText.color = Color.green;
    }

    void SetAlarm()
    {
        statusText.text = "Тревога! Норма газа в воздухе превышена! Утечка газа!";
        statusText.color = Color.red;
    }
}
