//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class circularmove : MonoBehaviour
//{
//    public float angularSpeed = 1f;
//    public float circleRad = 100f;

//    private Vector3 fixedPoint;
//    private float onecircle=6;
//    public float[] keyangleup = new float[1]; //key y pos movement
//    public float[] keyangledown = new float[1];
//    public int key=0;
//    private int keynum = 1;
//    private bool moveup = false;
//    public float currentAngle;

//    void Start()
//    {
//        fixedPoint = transform.position;
//            //GameObject.Find("middlepoint").transform.position;
//    }

//    void Update()
//    {
//        currentAngle += angularSpeed * Time.deltaTime;
//        Vector3 move = new Vector3(Mathf.Sin(currentAngle), 0, Mathf.Cos(currentAngle));
//        float Pm = 0.01f;

//        if (currentAngle % onecircle>keyangleup[key])
//        {
//            Debug.Log("moveup");
//            //moveup = true;
//            fixedPoint.y += Pm;
//        }
//        else if(currentAngle % onecircle < keyangledown[key])
//        {
//            //moveup = false;
//            fixedPoint.y -= Pm;
//            key++;
//            if (key >= keynum)
//            {
//                key = 0;
//            }
//        }
//        Debug.Log(move.y);
//        Vector3 offset = move * circleRad;
//        transform.position = fixedPoint + offset;
        
//    }
//}
