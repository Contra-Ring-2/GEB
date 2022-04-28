using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEditor.SceneManagement;

public class MasterModel : MonoBehaviour
{
    // singleton
    public static MasterModel TheModel {get; private set;} = null;

    public delegate void Callback();
    public delegate bool Predicate();
    //public delegate IEnumerator WaitEnumerator();

    /// <summary>
    /// Call callback() after specified seconds
    /// </summary>
    /// <param name="seconds">interval seconds</param>
    /// <param name="callback">callback function</param>
    public void CallbackInSecond(float seconds, Callback callback)
    {
        IEnumerator callbackRoutine()
        {
            yield return new WaitForSeconds(seconds);
            callback();
        }

        StartCoroutine(callbackRoutine());
    }

    public void CallbackWaitingFor(YieldInstruction instruction, Callback callback)
    {
        IEnumerator callbackRoutine()
        {
            yield return instruction;
            callback();
        }

        StartCoroutine(callbackRoutine());
    }

    public void CallbackWhen(Predicate predicate, Callback callback)
    {
        IEnumerator callbackRoutine()
        {
            yield return new WaitUntil(() => predicate());
            callback();
        }

        StartCoroutine(callbackRoutine());
    }

    // Start is called before the first frame update
    void Start()
    {
        // allow cross scene references
        // EditorSceneManager.preventCrossSceneReferences = true;

        Debug.Assert(TheModel == null);
        TheModel = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
