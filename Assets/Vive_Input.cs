using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Vive_Input : MonoBehaviour
{
    public float m_Sensitivity = 0.1f;
    public float m_MaxSpeed = 1.0f;

    //public SteamVR_Action_Boolean m_MovePress = null;
    public SteamVR_Action_Vector2 m_MoveValueLeft = null;
    public SteamVR_Action_Vector2 m_MoveValueRight = null;
    private float m_Speed = 0.0f;
    // private CharacterController m_CharacterController = null;

    private Transform m_CameraRig = null;
    private Transform m_Head = null;

    private void Awake(){
        // m_CharacterController = GetComponent<CharacterController>();
        
    }
    private void Start(){
        //m_CameraRig = SteamVR_Render.Top().origin;
        //m_Head = SteamVR_Render.Top().head;
    }
    private void Update(){
        //Debug.Log(m_MoveValue);
        // Debug.Log(string.Format("Vector2({0})", m_MoveValueLeft.axis));
        //Debug.Log(string.Format("Vector2({0})", m_MoveValueRight.axis));

        // Valve.VR.InteractionSystem.Player player = GameObject.FindWithTag("Player").GetComponent<Valve.VR.InteractionSystem.Player>();
        // Debug.Log(string.Format("player origin: Pos=({0}), Rot=({1})", player.trackingOriginTransform.position, player.trackingOriginTransform.rotation));
        // Debug.Log(string.Format("player: Pos=({0}), Rot=({1})", player.transform.position, player.transform.rotation));

       HandleHead();
        CalculateMovement();
        HandleHeight();
    }
    private void HandleHead(){
        // Vector3 oldPosition = m_CameraRig.position;
        // Quaternion oldRotation = m_CameraRig.rotation;

        // transform.eulerAngles = new Vector3(0.0f,m_Head.rotation.eulerAngles.y,0.0f);
        // m_CameraRig.position = oldPosition;
        // m_CameraRig.rotation = oldRotation;

        Vector2 axis = m_MoveValueRight.axis;
        // Quaternion axisRot = Quaternion.Euler(new Vector3(0, axis.x, axis.y));
        Vector3 axisRot = new Vector3(0, axis.x, 0); // new Vector3(0, axis.x, axis.y);

        // Quaternion camRot = GameObject.FindWithTag("MainCamera").transform.rotation * axisRot;
        // GameObject.FindWithTag("MainCamera").transform.rotation = camRot;

        Vector3 dAxisRot = axisRot * Time.deltaTime;
        GameObject.FindWithTag("Player").transform.Rotate(90 * dAxisRot);
        // GameObject.FindWithTag("MainCamera").transform.Rotate(90 * dAxisRot);

        // GameObject.FindWithTag("Player").transform.Translate(flatTrans);
    }
    private void CalculateMovement(){
        // Vector3 oldPosition = m_CameraRig.position;
        // Quaternion oldRotation = m_CameraRig.rotation;

        // transform.eulerAngles = new Vector3(0.0f,m_Head.rotation.eulerAngles.y,0.0f);
        Vector2 axis = m_MoveValueLeft.axis;
        Vector3 front = (new Vector3(axis[0], 0, axis[1]));
        
        // Vector3 camFront = m_CameraRig.rotation * front;
        Vector3 camFront = GameObject.FindWithTag("MainCamera").transform.rotation * front;
        Vector3 flatNorm = (new Vector3(camFront.x, 0, camFront.z)).normalized;
        Vector3 flatTrans = flatNorm * (5.0f * Time.deltaTime);

        // Vector3 dpos = camFront.position - m_CameraRig.position;
        // GameObject.FindWithTag("")

        // GameObject.FindWithTag("Player").transform.Translate(front);
        GameObject.FindWithTag("Player").transform.Translate(flatTrans);
        
        // // TODO:
        // GameObject.FindWithTag("Player").GetComponent<Rigidbody>().velocity = flatNorm * 5.0f; //flatTrans;

        // Debug.Log("Camera rotaion: " + m_CameraRig.rotation);

        // front.

        // Debug.Log(front)

        // m_CameraRig.position = oldPosition;
        // m_CameraRig.rotation = oldRotation;
    }
    private void HandleHeight(){

    }
}
