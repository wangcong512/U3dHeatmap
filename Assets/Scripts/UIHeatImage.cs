using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIHeatImage : MaskableGraphic
{
    // Start is called before the first frame update
    public Texture2D heatTex;
    public Shader heatShader;
    public RawImage render;
    //Material material;

    List<UIHeatPoint> heatPoints = new List<UIHeatPoint>();


    protected override void Start()
    {
        base.Start();
        render = this.GetComponent<RawImage>();
        GenerateMeterial();
        CommitToShaderProgram();
    }


    protected override void OnPopulateMesh(VertexHelper vh)
    {
        Texture tex = mainTexture;
        vh.Clear();
        if (tex != null)
        {
            var r = GetPixelAdjustedRect();
            var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
            var scaleX = tex.width * tex.texelSize.x;
            var scaleY = tex.height * tex.texelSize.y;
            {
                var color32 = Color.white;
                vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(1, 0));
                vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(1, 0));
                vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(1, 0));
                vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(1, 0));

                vh.AddTriangle(0, 1, 2);
                vh.AddTriangle(2, 3, 0);
            }
        }
        //var r = GetPixelAdjustedRect();
        //var v = new Vector4(r.x, r.y, r.x + r.width, r.y + r.height);
        //vh.Clear();
        //var color32 = Color.clear;
        //vh.AddVert(new Vector3(v.x, v.y), color32, new Vector2(0, 0));
        //vh.AddVert(new Vector3(v.x, v.w), color32, new Vector2(0, 1));
        //vh.AddVert(new Vector3(v.z, v.w), color32, new Vector2(1, 1));
        //vh.AddVert(new Vector3(v.z, v.y), color32, new Vector2(1, 0));
        //vh.AddTriangle(0, 1, 2);
        //vh.AddTriangle(2, 3, 0);

    }

    protected override void Awake()
    {
        base.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetHeatPoints(List<UIHeatPoint> heatList)
    {
        heatPoints.Clear();
        heatPoints.AddRange(heatList);
    }

    /// <summary>
    /// 重新生成材质
    /// </summary>
    private void GenerateMeterial()
    {
        /*
        * 开始时重新生成一下材质，Unity的 SetXXArray 有一个问题是，当第一次给数组赋值时，数组的最大尺寸就固定了
        * 以后再赋值更大的数组时，将会被裁减
        */
        //if (this.material != null)
        //    Destroy(this.material);
        material = new Material(heatShader);
        material.SetTexture("_HeatTex", heatTex);
        //material.SetTextureOffset("_HeatTex", new Vector2(0.0f, 0.5f));

        //render.material = material;
    }

    private void CommitToShaderProgram()
    {
        if (heatPoints.Count == 0)
            return;

        this.material.SetInt("_Points_Length", heatPoints.Count);
        this.material.SetVectorArray("_Points", heatPoints.Select((v) => new Vector4(v.point.x, v.point.y, v.point.z)).ToList());
        this.material.SetVectorArray("_Properties", heatPoints.Select((v) => new Vector4(v.radius, v.intensity)).ToList());
    }

    public void Repaint()
    {
        CommitToShaderProgram();
    }


}

[System.Serializable]
public class UIHeatPoint
{
    /// <summary>
    /// 世界坐标系位置
    /// </summary>
    public Vector3 point;

    /// <summary>
    /// 半径
    /// </summary>
    public float radius;

    /// <summary>
    /// 强度
    /// </summary>
    public float intensity;
}


