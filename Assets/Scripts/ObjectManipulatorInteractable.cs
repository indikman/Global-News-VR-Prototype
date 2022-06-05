using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectManipulatorInteractable : XRSimpleInteractable
{
    [Header("Object Manipulator")]
    /// <summary>
    /// Enables the ability to autorotate once drag ends based on the drag speed
    /// </summary>
    [SerializeField] private bool allowDragSpin;
    [SerializeField] private float spinBoost = 10f;
    [SerializeField] private float spinDeceleration = 0.99f;

    /// <summary>
    /// Does not spin if the interaction time is greater than this
    /// </summary>
    [SerializeField] private float spinCheckThresholdTime = 2.0f;

    private bool isLeft, isRight;
    private Transform positionRefLeft, positionRefRight;
    private Transform _handTransform;

    /*
     * Scaling parameters
     */
    private bool scaleStart = true;
    private float initialDistance = 0;
    private float currentDistance = 0;
    private float scalePropotion = 0;
    private Vector3 initialScale;
    private Vector3 startScale;

    /*
     * Spinning parameters
     */
    private bool spinStart = true;
    private Quaternion initialRotation;
    private Vector3 initialDirection;
    private Vector3 currentDirection;
    private float spinAngle;

    /*
     * Drag Spin
     */
    private float startTime, endTime;
    private Vector3 startPos, endPos;
    private float spinSpeed = 0;
    

    private void Start()
    {
        scaleStart = true;
        initialScale = transform.localScale;
        startScale = initialScale;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        _handTransform = args.interactorObject.transform;
        Hands _hand = _handTransform.GetComponent<IdentifiableDirectInteractor>().getHand();

        //Hide the hand
        _handTransform.GetComponent<IdentifiableDirectInteractor>().SetHandVisualVisibility(false);

        if (_hand == Hands.Left)
        {
            positionRefLeft = _handTransform;
            isLeft = true;
        }
        else if(_hand == Hands.Right)
        {
            positionRefRight = _handTransform;
            isRight = true;
        }   
        
        // Drag Spin
        if(allowDragSpin)
        {
            spinSpeed = 0;
            startTime = Time.time;
            startPos = _handTransform.position;
        }

    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        _handTransform = args.interactorObject.transform;
        Hands _hand = _handTransform.GetComponent<IdentifiableDirectInteractor>().getHand();

        //Show the hand
        _handTransform.GetComponent<IdentifiableDirectInteractor>().SetHandVisualVisibility(true);

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

        //Reset scale if smaller than start
        if (transform.localScale.magnitude < startScale.magnitude)
            transform.localScale = startScale;

        // Allow scaling again
        scaleStart = true;
        initialDistance = 0;

        // Allow spinning again
        spinStart = true;


        // Drag Spin
        // Drag Spin
        if (allowDragSpin)
        {
            endTime = Time.time;
            endPos = _handTransform.position;

            // Assign the automatic rotation
            if (Mathf.Approximately(spinAngle, 0)) return; // Safety, uses this to get the swipe direction

            if(endTime - startTime < spinCheckThresholdTime)
                spinSpeed = (spinAngle/Mathf.Abs(spinAngle)) * spinBoost * Vector3.Distance(startPos, endPos) / (endTime - startTime);
        }
    }

    

    private void Update()
    {
        CalculateScale();
        GrabRotate();

        if(allowDragSpin && !(isLeft || isRight))
        {
            transform.Rotate(Vector3.up * spinSpeed);

            spinSpeed *= spinDeceleration; //Reduce the speed over time
        }
    }


    void GrabRotate()
    {
        if (isLeft && isRight)
            return;

        if(isLeft || isRight)
        {
            if(spinStart)
            {
                spinStart = false;

                initialRotation = transform.localRotation;
                initialDirection =  _handTransform.position - transform.position;
                initialDirection = new Vector3(initialDirection.x, 0, initialDirection.z);
            }

            currentDirection = _handTransform.position - transform.position;
            currentDirection = new Vector3(currentDirection.x, 0, currentDirection.z);

            //Calculate the angle
            spinAngle = Vector3.SignedAngle(initialDirection, currentDirection, Vector3.up);

            transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(initialRotation.eulerAngles + new Vector3(0, spinAngle, 0)), 0.15f);

        }
    }

    void CalculateScale()
    {
        // Calculate scale
        if (isLeft && isRight)
        {
            if (scaleStart)
            {
                scaleStart = false;
                initialDistance = Vector3.Distance(positionRefLeft.position, positionRefRight.position);
                initialScale = transform.localScale;
                spinStart = true;
            }

            currentDistance = Vector3.Distance(positionRefLeft.position, positionRefRight.position);

            if (Mathf.Approximately(initialDistance, 0)) return;

            scalePropotion = currentDistance / initialDistance;

            transform.localScale = initialScale * scalePropotion;

            //Set the position to the midpoint and jump to it
            transform.position = Vector3.Lerp(transform.position, (positionRefRight.position + positionRefLeft.position) / 2.0f, 0.05f);
        }
    }
}
