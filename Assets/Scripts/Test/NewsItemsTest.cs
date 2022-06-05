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
            GameObject item = Instantiate(newsItemPrefab);
            item.transform.SetParent(targetSphere);
            item.GetComponent<EarthPositionLocator>().SetPosition(newsitem.lat, newsitem.lon);

            GameObject tooltip = Instantiate(tooltipPrefab);
            tooltip.GetComponent<Tooltip>().SetupToolTip(newsitem.title, item.transform);

            item.GetComponent<ToolipPositioner>().SetTooltip(tooltip.transform);

            
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
    public string description;
    public float lat, lon;
}
