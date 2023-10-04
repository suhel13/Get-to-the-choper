using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStatuses : MonoBehaviour
{
    [HideInInspector] public Acid acid;
    [HideInInspector] public Bleed bleed;
    [HideInInspector] public Cold cold;
    [HideInInspector] public Frozen frozen;
    [HideInInspector] public Incinirate incinirate;
    [HideInInspector] public Shock shock;
    [HideInInspector] public Smoke smoke;
    [HideInInspector] public Wet wet;

    [SerializeField] AcidSO acidSO;
    [SerializeField] BleedSO bleedSO;
    [SerializeField] ColdSO coldSO;
    [SerializeField] FrozenSO frozenSO;
    [SerializeField] IncinirateSO incinirateSO;
    [SerializeField] ShockSO shockSO;
    [SerializeField] SmokeSO smokeSO;
    [SerializeField] WetSO wetSO;

    private void Start()
    {
        acid = new Acid(acidSO);
        bleed = new Bleed(bleedSO);
        cold = new Cold(coldSO);
        frozen = new Frozen(frozenSO);
        incinirate = new Incinirate(incinirateSO);
        shock = new Shock(shockSO);
        smoke = new Smoke(smokeSO);
        wet = new Wet(wetSO);
    }
}