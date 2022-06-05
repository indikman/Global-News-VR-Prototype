using UnityEngine;
using TMPro;

public class Tooltip : MonoBehaviour
{
    [SerializeField] private TMP_Text txtTitle;
    [SerializeField] private LineRenderer line;

    private Transform target;
    private Transform mainCam;

    public void SetupToolTip(string title, Transform target)
    {
        txtTitle.SetText(title);
        this.target = target;
        mainCam = Camera.main.transform;
    }

    private void Update()
    {
        if (!target)
            return;

        line.SetPosition(0, transform.position);
        line.SetPosition(1, target.position);

        if (mainCam)
            transform.LookAt(mainCam);
    }
}
