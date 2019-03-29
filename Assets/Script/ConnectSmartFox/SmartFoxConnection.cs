using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
public class SmartFoxConnection : MonoBehaviour
{
    private static SmartFox sfs;
    public static SmartFox Connection
    {
        get
        {
            return sfs;
        }
        set
        {
            sfs = value;
        }
    }
    public static bool CheckConnection()
    {
        return (sfs != null);
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
