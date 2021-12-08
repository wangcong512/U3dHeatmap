using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIHeatMapTest : MonoBehaviour
{
    List<UIHeatPoint> heatPoints = new List<UIHeatPoint>();
    public UIHeatImage heatmap;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            heatPoints.Add(new UIHeatPoint()
            {
                point = new Vector3(Random.Range(-1000f, 1000f), Random.Range(-500f, 500f)),
                radius = Random.Range(50f, 500f),
                intensity = Random.Range(0.1f, 0.8f),
            });
        }

        heatmap.SetHeatPoints(heatPoints);
        heatmap.Repaint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
