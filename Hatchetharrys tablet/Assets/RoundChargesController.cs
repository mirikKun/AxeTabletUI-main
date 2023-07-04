using UnityEngine;

public class RoundChargesController : MonoBehaviour
{
    [SerializeField] private RoundCharge roundChargePrefab;
    [SerializeField] private Transform roundsHolder;
    private RoundCharge[] _rounds;

    public void CreateRoundCharges(int axeCounts)
    {
        foreach (Transform child in roundsHolder)
        {
            Destroy(child.gameObject);
        }

        _rounds = new RoundCharge[axeCounts];
        for (int i = 0; i < axeCounts; i++)
        {
            _rounds[i] = Instantiate(roundChargePrefab, roundsHolder);
        }
    }

    public void ResetRoundCharges()
    {
        if (_rounds == null)
            return;
        foreach (var round in _rounds)
        {
            round.ResetRound();
        }
    }

    public void RoundPass(int axeNumber)
    {
        _rounds?[axeNumber].UseAxe();
    }
}