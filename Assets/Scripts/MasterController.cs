using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterController : MonoBehaviour
{
    // singleton
    public static MasterController TheController { get; private set; } = null;

#region INTERNAL [DO NOT MODIFY]
    // Start is called before the first frame update
    void Start()
    {
        Debug.Assert(TheController == null);
        TheController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
#endregion
}
