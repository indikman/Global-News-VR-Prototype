using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewsTablet : MonoBehaviour
{
    private static NewsTablet instance;

    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtDescription;
    [SerializeField] private ScrollRect scrollViewDescription;
    [SerializeField] private float distaceFromFace = 0.5f;

    Transform mainCam;

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        instance = this;

        mainCam = Camera.main.transform;
    }

    public static NewsTablet GetInstance()
    {
        return instance;
    }

    public void SetNews(string title, string description)
    {
        txtTitle.SetText(title);
        txtDescription.SetText(description);
        scrollViewDescription.verticalNormalizedPosition = 1;

        Debug.Log(description);

        //Get the camera position and place 50 cm in front of the face
        transform.position = mainCam.position + mainCam.forward.normalized * distaceFromFace;
        transform.LookAt(mainCam);
    }

    public void Close()
    {
        // Will just sent it off screen =D
        transform.position = Vector3.one * 10000;
    }

}
