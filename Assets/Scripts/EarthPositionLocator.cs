using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPositionLocator : MonoBehaviour
{

    [SerializeField] private GameObject planetReference;

    private float radius;
    private SphereCollider sphereCollider;

    public float lat, lon;
    float _lat, _lon;

    Vector3 fixScale;

    private void Start()
    {
        //SetPosition(0, 0);
    }

    public void SetPosition(float lat, float lon)
    {
        this.lat = lat;
        this.lon = lon;

        sphereCollider = transform.parent.GetComponent<SphereCollider>();
        radius = sphereCollider.radius;

        fixScale = transform.localScale;
    }

    private void Update()
    {
        if (!sphereCollider)
            return;

        radius = sphereCollider.radius;

        _lat = Mathf.Clamp(lat, -90.0f, 90.0f) * Mathf.Deg2Rad;
        _lon = -Mathf.Clamp(lon, -180.0f, 180.0f) * Mathf.Deg2Rad;

        transform.localPosition = new Vector3(
                radius * Mathf.Sin(_lon) * Mathf.Cos(_lat),
                radius * Mathf.Sin(_lat),
                radius * Mathf.Cos(_lat) * Mathf.Cos(_lon)
            );

        // Scale
        transform.localScale = new Vector3(
            fixScale.x / transform.parent.localScale.x,
            fixScale.y / transform.parent.localScale.y,
            fixScale.z / transform.parent.localScale.z);
    }
}
