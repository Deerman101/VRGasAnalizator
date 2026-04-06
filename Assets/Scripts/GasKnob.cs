using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GasKnob : MonoBehaviour
{
    public Transform knob;
    public GasBurner burner;

    private XRBaseInteractable _interactable;
    private bool _isOn = false;

    void Awake()
    {
        _interactable = GetComponent<XRBaseInteractable>();
    }

    void OnEnable()
    {
        _interactable.selectEntered.AddListener(OnClick);
    }

    void OnDisable()
    {
        _interactable.selectEntered.RemoveListener(OnClick);
    }

    void OnClick(SelectEnterEventArgs args)
    {
        ToggleGas();
    }

    void ToggleGas()
    {
        _isOn = !_isOn;

        if (_isOn)
        {
            knob.localRotation = Quaternion.Euler(0, 0, -90);
            burner.SetGas(true);
        }
        else
        {
            knob.localRotation = Quaternion.Euler(0, 0, 0);
            burner.SetGas(false);
        }
    }
}