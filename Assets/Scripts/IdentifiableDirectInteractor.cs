using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IdentifiableDirectInteractor : XRDirectInteractor
{
    [SerializeField] private Hands hand;

    public Hands getHand() { return hand; }
}
