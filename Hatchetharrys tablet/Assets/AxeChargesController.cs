using UnityEngine;

public class AxeChargesController : MonoBehaviour
{
    [SerializeField] private AxeCharge axeChargePrefab;
    [SerializeField] private Transform axesHolder;
    private AxeCharge[] _axes;

    public void CreateAxeCharges(int axeCounts)
    {
        foreach (Transform child in axesHolder)
        {
            Destroy(child.gameObject);
        }

        _axes = new AxeCharge[axeCounts];
        for (int i = 0; i < axeCounts; i++)
        {
            _axes[i] = Instantiate(axeChargePrefab, axesHolder);
        }
    }

    public void ResetAxeCharges()
    {
        if (_axes == null)
            return;
        foreach (var axe in _axes)
        {
            axe.ResetAxe();
        }
    }

    public void UseAxe(int axeNumber, bool miss)
    {
        if (_axes == null)
            return;
        if (miss)
        {
            _axes[axeNumber].ShowMiss();
        }
        else
        {
            _axes[axeNumber].UseAxe();
        }
    }
}