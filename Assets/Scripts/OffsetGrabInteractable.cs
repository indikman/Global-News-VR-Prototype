using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class OffsetGrabInteractable : XRGrabInteractable
{
    [SerializeField] private bool toggleHand;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        attachTransform.position = args.interactorObject.GetAttachTransform(args.interactableObject).position;
        attachTransform.rotation = args.interactorObject.GetAttachTransform(args.interactableObject).rotation;

        base.OnSelectEntering(args);
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        if (toggleHand)
        {
            // Will disable the children of the hand interactor
            foreach (Transform child in args.interactorObject.transform)
            {
                child.gameObject.SetActive(false);
            }
                
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        if (toggleHand)
        {

            // Will enable the children of the hand interactor
            foreach (Transform child in args.interactorObject.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

}
