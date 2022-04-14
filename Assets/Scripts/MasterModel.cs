using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterModel : MonoBehaviour
{
    // singleton
    public static MasterModel TheModel {get; private set;} = null;

    public delegate void Callback();

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

#region INTERNAL [DO NOT MODIFY]

    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(TheModel == null);
        TheModel = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion
}
