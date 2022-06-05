using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public class NewsTablet : MonoBehaviour
{
    private static NewsTablet instance;

    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private TMP_Text txtDescription;
    [SerializeField] private ScrollRect scrollViewDescription;
    [SerializeField] private float distaceFromFace = 0.5f;
    [SerializeField] private Vector3 distanceOffset;

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

        //Get the camera position and place 50 cm in front of the face

        
        SFXPlayer.GetInstance().PlaySound("pop");
        transform.position = mainCam.position + distanceOffset + mainCam.forward.normalized * distaceFromFace;
        transform.localScale = Vector3.one;
        transform.DOScale(transform.localScale / 2, 1.0f).From().SetEase(Ease.OutElastic);


        transform.LookAt(mainCam);
    }

    public void Close()
    {
        // Will just send it off screen =D
        transform.position = Vector3.one * 10000; 
        SFXPlayer.GetInstance().PlaySound("error");
    }

}


