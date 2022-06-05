using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthPositionLocator : MonoBehaviour
{
    [SerializeField] private float lat;
    [SerializeField] private float lon;

    [SerializeField] private GameObject pointPrefab;

    private float radius;

    void Start()
    {
        radius = GetComponent<SphereCollider>().radius;

        UpdateLocation();
    }

    public void UpdateLocation()
    {
        float _lat = Mathf.Clamp(lat, -90.0f, 90.0f) * Mathf.Deg2Rad;
        float _lon = Mathf.Clamp(lon, -180.0f, 180.0f) * Mathf.Deg2Rad;


        pointPrefab.transform.localPosition = new Vector3(
                radius * Mathf.Sin(_lon) * Mathf.Cos(_lat),
                radius * Mathf.Sin(_lat),
                radius * Mathf.Cos(_lat) * Mathf.Cos(_lon)
            );

    }

    private void Update()
    {
        UpdateLocation();
    }
}
