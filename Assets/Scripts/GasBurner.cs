using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasBurner : MonoBehaviour
{
    public GameObject gasVFX;
    public GameObject fireVFX;

    public bool isGasOn = false;
    public bool isFireOn = false;

    public void SetGas(bool state)
    {
        isGasOn = state;

        gasVFX.SetActive(state);

        if (!state)
        {
            fireVFX.SetActive(false);
            isFireOn = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isGasOn) return;

        if (other.CompareTag("Match"))
        {
            if (!isFireOn)
            {
                fireVFX.SetActive(true);
                isFireOn = true;
            }
        }
    }
}
