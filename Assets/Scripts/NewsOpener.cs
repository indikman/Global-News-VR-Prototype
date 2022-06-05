using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsOpener : MonoBehaviour
{

    private string title, description;

    public void SetNews(string title, string description)
    {
        this.title = title;
        this.description = description;
    }

    public void OpenNews()
    {
        NewsTablet.GetInstance().SetNews(title, description);
    }
}
