using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewsItemsTest : MonoBehaviour
{
    [SerializeField] private List<NewsItem> newsItems = new List<NewsItem>();
    [SerializeField] private Transform targetSphere;
    [SerializeField] private GameObject newsItemPrefab;
    [SerializeField] private GameObject tooltipPrefab;


    public void SetUpNewsItems()
    {
        foreach (var newsitem in newsItems)
        {
            GameObject item = Instantiate(newsItemPrefab, targetSphere);
            item.GetComponent<EarthPositionLocator>().SetPosition(newsitem.lat, newsitem.lon);

            GameObject tooltip = Instantiate(tooltipPrefab);
            tooltip.GetComponent<Tooltip>().SetupToolTip(newsitem.title, item.transform);

            item.GetComponent<ToolipPositioner>().SetTooltip(tooltip.transform);

            //setup the news item
            item.GetComponent<NewsOpener>().SetNews(newsitem.title, newsitem.description);
            
        }
    }

    private void Start()
    {
        SetUpNewsItems();
    }

}

[System.Serializable]
class NewsItem
{
    public string title;
    [TextArea(15, 20)]
    public string description;
    public float lat, lon;
}
