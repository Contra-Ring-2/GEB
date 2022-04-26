using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followtrack : MonoBehaviour
{
    // Start is called before the first frame update
    private LineRenderer lineRender;
    public float speed = 0.5f;
    private float length = 1;
    private int baseCount = 10; //maybe constant
    private int stairnum = 4;
    private Vector3[] basePoint;
    private List<Vector3> lsPoint = new List<Vector3>();

    /// <summary>
    /// get base stair position
    /// </summary>
    void InitBasePoint()
    {
        basePoint = new Vector3[stairnum];
        for (int i = 0; i < stairnum; i++)
        {
            int t = i + 1;
            string temp = "stair" + t;
            Vector3 pos = GameObject.Find(temp).transform.position;
            basePoint[i] = new Vector3(pos.x,pos.y+0.6f,pos.z);
        }
        GetTrackPoint(basePoint);
    }

    /// <summary>
    /// 根據節點繪製曲線
    /// </summary>
    public void GetTrackPoint(Vector3[] track)
    {
        Vector3[] vector3s = PathControlPointGenerator(track);
        int SmoothAmount = track.Length * baseCount;
        lineRender.positionCount = SmoothAmount;
        for (int i = 1; i < SmoothAmount; i++)
        {
            float pm = (float)i / SmoothAmount;
            Vector3 currPt = Interp(vector3s, pm);
            lineRender.SetPosition(i, currPt);
            lsPoint.Add(currPt);
        }
    }

    /// <summary>
    /// 算所有節點and控制點座標
    /// </summary>
    public Vector3[] PathControlPointGenerator(Vector3[] path)
    {
        Vector3[] suppliedPath;
        Vector3[] vector3s;

        suppliedPath = path;
        int offset = 2;
        vector3s = new Vector3[suppliedPath.Length + offset];
        System.Array.Copy(suppliedPath, 0, vector3s, 1, suppliedPath.Length);
        vector3s[0] = vector3s[1] + (vector3s[1] - vector3s[2]);
        vector3s[vector3s.Length - 1] = vector3s[vector3s.Length - 2] + (vector3s[vector3s.Length - 2] - vector3s[vector3s.Length - 3]);
        if (vector3s[1] == vector3s[vector3s.Length - 2])
        {
            Vector3[] tmpLoopSpline = new Vector3[vector3s.Length];
            System.Array.Copy(vector3s, tmpLoopSpline, vector3s.Length);
            tmpLoopSpline[0] = tmpLoopSpline[tmpLoopSpline.Length - 3];
            tmpLoopSpline[tmpLoopSpline.Length - 1] = tmpLoopSpline[2];
            vector3s = new Vector3[tmpLoopSpline.Length];
            System.Array.Copy(tmpLoopSpline, vector3s, tmpLoopSpline.Length);
        }
        return (vector3s);
    }


    /// <summary>
    /// 計算曲線任意點的位置
    /// </summary>
    public Vector3 Interp(Vector3[] pos, float t)
    {
        int length = pos.Length - 3;
        int currPt = Mathf.Min(Mathf.FloorToInt(t * length), length - 1);
        float u = t * (float)length - (float)currPt;
        Vector3 a = pos[currPt];
        Vector3 b = pos[currPt + 1];
        Vector3 c = pos[currPt + 2];
        Vector3 d = pos[currPt + 3];
        return .5f * (
           (-a + 3f * b - 3f * c + d) * (u * u * u)
           + (2f * a - 5f * b + 4f * c - d) * (u * u)
           + (-a + c) * u
           + 2f * b
       );
    }

    void Start()
    {
        lineRender = this.GetComponent<LineRenderer>();
        InitBasePoint();
    }

    // Update is called once per frame
    void Update()
    {
        length += Time.deltaTime * speed;
        if (length >= lsPoint.Count - 1)
        {
            length = lsPoint.Count - 1;
        }
        transform.localPosition = lsPoint[(int)(length)];
    }
}
