using Sfs2X;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using Sfs2X.Core;

public class NetworkManager : MonoBehaviour {

    private SmartFox sfs;
	// Use this for initialization
	void Start () {
		if(SmartFoxConnection.CheckConnection())
        {
            sfs = SmartFoxConnection.Connection;
            
        }
        else
        {
            SceneManager.LoadScene("Login");
        }
        EventListener();
    }

    private void EventListener()
    {
        sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionReponse);
    }

    private void OnExtensionReponse(BaseEvent evt)
    {
        Debug.Log("OnExtensionReponse");
    }

    // Update is called once per frame
    void Update () {
		
	}
}
