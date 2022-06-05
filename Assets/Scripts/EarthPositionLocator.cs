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

    void Start()
    {
        sphereCollider = planetReference.GetComponent<SphereCollider>();
        radius = sphereCollider.radius;
    }

    public void SetPosition(float lat, float lon)
    {
        this.lat = lat;
        this.lon = lon;
    }

    private void Update()
    {
        radius = sphereCollider.radius;

        _lat = Mathf.Clamp(lat, -90.0f, 90.0f) * Mathf.Deg2Rad;
        _lon = Mathf.Clamp(lon, -180.0f, 180.0f) * Mathf.Deg2Rad;

        transform.localPosition = new Vector3(
                radius * Mathf.Sin(_lon) * Mathf.Cos(_lat),
                radius * Mathf.Sin(_lat),
                radius * Mathf.Cos(_lat) * Mathf.Cos(_lon)
            );
    }
}
