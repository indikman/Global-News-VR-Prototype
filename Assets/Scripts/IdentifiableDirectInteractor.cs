using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class IdentifiableDirectInteractor : XRDirectInteractor
{
    [SerializeField] private Hands hand;
    [SerializeField] private GameObject handVisual;

    public Hands getHand() { return hand; }

    public void SetHandVisualVisibility(bool visibility)
    {
        if (!handVisual)
            return;

        handVisual.SetActive(visibility);
    }
}
