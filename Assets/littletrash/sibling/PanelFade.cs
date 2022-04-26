// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.UI;

// public class PanelFade : MonoBehaviour
// {
//     private Image panelImg;
//     private Vector3 panelScale;
//     private float _zoomout_scale = 1;
//     private float _zoomin_scale = 0.05f;
//     private bool _clickOn_p = true;
//     private bool zoomon = false;
//     public float opacity = 0.7f;
//     void Start()
//     {
//         panelImg = GetComponent<Image>();
//         panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, 0f);
//         panelScale = panelImg.transform.localScale;
//         panelImg.transform.localScale = new Vector3(panelScale.x * 0.05f, panelScale.y * 0.05f, panelScale.z * 0.05f);

//         // Destroy(this.gameObject, fadeInTime);
//     }
//     private void Update()
//     {
//        // Debug.Log(_clickOn_p);
//         if (_clickOn_p && zoomon)
//         {
//             Debug.Log("click when nonactive");
//             ZoomIn();
            
//         }
//         else if(!_clickOn_p && !zoomon)
//         {
//             Debug.Log("click when active");
//             ZoomOut();
//         }

//     }
//     public void SetZoomIn()
//     {
//         Debug.Log("clickon");
//         if (!zoomon)
//         {
//             Debug.Log(panelImg);
//             panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, opacity);
//             _zoomin_scale = 0.05f;
//             zoomon = true;
//             _clickOn_p = true;
//         }
//         else
//         {
//             _zoomout_scale = 1;
//             zoomon = false;
//            // _clickOn_p = false;
//         }

//     }
//     private void ZoomOut()
//     {
//         panelImg.transform.localScale = new Vector3(panelScale.x * _zoomout_scale, panelScale.y * _zoomout_scale, panelScale.z * _zoomout_scale);
//         _zoomout_scale -= 0.05f;
//         if (_zoomout_scale < 0.05f)
//         {
//             //zoomon = false;
//             _clickOn_p = true;
//             panelImg.color = new Color(panelImg.color.r, panelImg.color.g, panelImg.color.b, 0f);
//         }
//     }
//     private void ZoomIn()
//     {
//         Debug.Log("i am in zoom in");
//         panelImg.transform.localScale = new Vector3(panelScale.x * _zoomin_scale, panelScale.y * _zoomin_scale, panelScale.z * _zoomin_scale);
//         _zoomin_scale += 0.05f;
//        // Debug.Log(_zoomin_scale);
//         if (_zoomin_scale >= 1)
//         {
//             _clickOn_p = false;
//             //zoomon = true
       
//         }
//     }
// }
