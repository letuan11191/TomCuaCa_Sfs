using Sfs2X;
using Sfs2X.Core;
using Sfs2X.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Sfs2X.Requests;
using UnityEngine.SceneManagement;
using Sfs2X.Entities;
using Sfs2X.Entities.Data;

public class Connection : MonoBehaviour {

    public static string username { get; set; }
    public static string mess;

    public SmartFox sfs;
    public InputField nameInput;
    public InputField txtIP;
    public Text errorText;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (sfs != null)
            sfs.ProcessEvents();
    }
    public void OnLoginButtonClick()
    {
        

        // Set connection parameters
        ConfigData cfg = new ConfigData();
        cfg.Host = txtIP.text;
        cfg.Port = 9933;		
        cfg.Zone = "BasicExamples";

        sfs = new SmartFox();

        sfs.AddEventListener(SFSEvent.CONNECTION, OnConnection);
        sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
        sfs.AddEventListener(SFSEvent.LOGIN, OnLogin);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN, OnRoomJoin);
        sfs.AddEventListener(SFSEvent.ROOM_JOIN_ERROR, OnRoomJoinError);

        // Connect to SFS2X
        sfs.Connect(cfg);
    }

    private void OnConnection(BaseEvent evt)
    {
        if ((bool)evt.Params["success"])
        {
            // Save reference to the SmartFox instance in a static field, to share it among different scenes
            SmartFoxConnection.Connection = sfs;

            Debug.Log("SFS2X API version: " + sfs.Version);
            Debug.Log("Connection mode is: " + sfs.ConnectionMode);

            // Login
            //sfs.Send(new LoginRequest(nameInput.text));
            ISFSObject data = new SFSObject();
            data.PutUtfString(SF.COMMAND, SF.LOGIN);
            data.PutUtfString(SF.PASSWORD, "123456");
            data.PutLong(SF.MOBILE, 123456789);
            sfs.Send(new LoginRequest(nameInput.text, "", "BasicExamples", data));
            Debug.Log("Da Gui Len");
        }
        else
        {
            // Remove SFS2X listeners and re-enable interface
            reset();

            // Show error message
            errorText.text = "Connection failed; is the server running at all?";
        }
    }

    private void OnConnectionLost(BaseEvent evt)
    {
        reset();

        string reason = (string)evt.Params["reason"];

        if (reason != ClientDisconnectionReason.MANUAL)
        {
            // Show error message
            errorText.text = "Connection was lost; reason is: " + reason;
        }
    }

    private void OnLogin(BaseEvent evt)
    {
        User user = (User)evt.Params["user"];
        Debug.Log("Hello" + user.Name);

        if (sfs.RoomList.Count > 0)
        {
            sfs.Send(new JoinRoomRequest(sfs.RoomList[0].Name));
            Debug.Log(sfs.RoomList[0].Name);
        }
        ISFSObject data = (ISFSObject)evt.Params["data"];
        if(data != null)
        {
            username = data.GetUtfString(SF.USERNAME);
            mess = data.GetUtfString(SF.MESS);

            Debug.Log(username + "Thong Bao: " + mess);
            //SceneManager.LoadScene("Game");

        }
        else
        {
            Debug.Log("OnLogin khong co thong bao");
        }
    }

    private void OnRoomJoin(BaseEvent evt)
    {
        Room room = (Room)evt.Params["room"];
        Debug.Log("You Joined Room" + room.Name + "\n");
        reset();


        // Go to main game scene
        SceneManager.LoadScene("Game");
    }

    private void OnRoomJoinError(BaseEvent evt)
    {
        errorText.text = "Room join failed: " + (string)evt.Params["errorMessage"];
    }

    private void reset()
    {
        // Remove SFS2X listeners
        sfs.RemoveAllEventListeners();
    }
}
