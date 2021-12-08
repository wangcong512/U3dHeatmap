using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyImage : MaskableGraphic
{
    [SerializeField]
    public float R = 500;//半径
    public float r = 10;
    [Range(3, 100)]
    public int PointCount = 10;//多少个点组成圆形

    protected override void OnPopulateMesh(VertexHelper vh)
    {
        vh.Clear();
        //将360 分成PointCount份 
        //求出每份角度弧度
        float angle = 360f / PointCount;
        angle = angle * Mathf.Deg2Rad;
        Color[] c = new Color[] { Color.green, Color.red, Color.blue };


        //循环添加其余点
        for (int i = 0; i < PointCount; i++)
        {
            float a = angle * i;
            //这里用到了极坐标转直角坐标公式
            vh.AddVert(new Vector3(GetX(r, a), GetY(r, a), 0), c[0], Vector2.zero);
            vh.AddVert(new Vector3(GetX(R, a), GetY(R, a), 0), c[1], Vector2.zero);
        }

        int imax = PointCount * 2;
        //添加三角形索引
        for (int i = 0; i < imax; i += 2)
        {
            if (i == imax - 2)
            {
                vh.AddTriangle(i, i + 1, 1);
                vh.AddTriangle(i, 1, 0);
            }
            else
            {
                vh.AddTriangle(i, i + 1, i + 3);
                vh.AddTriangle(i, i + 3, i + 2);
            }
        }
    }

    public float GetX(float r, float angle)
    {
        return r * Mathf.Sin(angle);
    }

    public float GetY(float r, float angle)
    {
        return r * Mathf.Cos(angle);
    }
}

