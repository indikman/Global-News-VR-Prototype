using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolipPositioner : MonoBehaviour
{
    [SerializeField] private float offsetLength;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform tooltipRef;

    Vector3 directionVec;

    public void SetTooltip(Transform _tooltipRef, Vector3 _offset=default(Vector3), float _offsetLength=0.05f)
    {
        tooltipRef = _tooltipRef;
        offsetLength = _offsetLength;
        offset = _offset;
    }

    void Update()
    {
        if (!tooltipRef)
            return;

        directionVec = (transform.position - transform.parent.transform.position).normalized;
        tooltipRef.position = transform.position + directionVec * offsetLength + offset;
    }
}
