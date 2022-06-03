using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectManipulator : XRSimpleInteractable
{
    private bool isLeft, isRight;
    private Transform positionRefLeft, positionRefRight;

    private bool scaleStart = true;
    private float initialDistance = 0;
    private float currentDistance = 0;
    private float scalePropotion = 0;

    private void Start()
    {
        scaleStart = true;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        Transform _handTransform = args.interactorObject.transform;
        Hands _hand = _handTransform.GetComponent<IdentifiableDirectInteractor>().getHand();

        if (_hand == Hands.Left)
        {
            positionRefLeft = _handTransform;
            isLeft = true;
        }else if(_hand == Hands.Right)
        {
            positionRefRight = _handTransform;
            isRight = true;
        }    

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        Transform _handTransform = args.interactorObject.transform;
        Hands _hand = _handTransform.GetComponent<IdentifiableDirectInteractor>().getHand();

        if (_hand == Hands.Left)
        {
            positionRefLeft = null;
            isLeft = false;
        }
        else if (_hand == Hands.Right)
        {
            positionRefRight = null;
            isRight = false;
        }

        // Allow scaling again
        scaleStart = true;
        initialDistance = 0;
    }

    private void Update()
    {
        // Calculate scale
        if(isLeft && isRight)
        {
            if(scaleStart)
            {
                scaleStart = false;
                initialDistance = Vector3.Distance(positionRefLeft.position, positionRefRight.position);
            }

            currentDistance = Vector3.Distance(positionRefLeft.position, positionRefRight.position);

            if (initialDistance == 0) // No divisions by zero
                initialDistance = 1;

            scalePropotion = currentDistance / initialDistance;

            transform.localScale *= scalePropotion;
        }
    }
}
