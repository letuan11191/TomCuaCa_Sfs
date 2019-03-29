using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sfs2X;
using Sfs2X.Core;
using UnityEngine.SceneManagement;
using Sfs2X.Entities;
using System;
using Sfs2X.Entities.Variables;
using Sfs2X.Requests;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public Text Pos_Health;
    private GameObject localPlayer;
    private SmartFox sfs;
    private Dictionary<SFSUser, GameObject> remotePlayers = new Dictionary<SFSUser, GameObject>();
    public GameObject[] Players;
    public Transform Posplayer;
    private PlayerController localPlayerController;

    void Awake()
    {
        Application.runInBackground = true;

    }
    // Use this for initialization
    void Start()
    {
        if (SmartFoxConnection.CheckConnection())
        {
            sfs = SmartFoxConnection.Connection;
            CreatePlayer();
            sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariablesUpdate);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
            sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserExitRoom);
        }
        else
        {
            SceneManager.LoadScene("Login");
        }


    }

    private void OnUserExitRoom(BaseEvent evt)
    {
        SFSUser user = (SFSUser)evt.Params["user"];
        if (user == sfs.MySelf) return;

        if (remotePlayers.ContainsKey(user))
        {
            Destroy(remotePlayers[user]);
            remotePlayers.Remove(user);
        }
    }

    private void OnUserEnterRoom(BaseEvent evt)
    {
        if (localPlayer != null && localPlayerController != null)
        {
            List<UserVariable> userVariables = new List<UserVariable>();
            userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
            userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));
            userVariables.Add(new SFSUserVariable("z", (double)localPlayer.transform.position.z));

            sfs.Send(new SetUserVariablesRequest(userVariables));

        }
    }

    private void OnConnectionLost(BaseEvent evt)
    {
        throw new NotImplementedException();
    }

    private void OnUserVariablesUpdate(BaseEvent evt)
    {
        
        List<string> changedVars = (List<string>)evt.Params["changedVars"];

        SFSUser user = (SFSUser)evt.Params["user"];

        if (user == sfs.MySelf) return;

        Vector3 pos = Posplayer.position;        
        int index = 0;
        if (!remotePlayers.ContainsKey(user))
        {
            if(user.ContainsVariable("x") && user.ContainsVariable("y"))
            {
                pos.x = (float)user.GetVariable("x").GetDoubleValue();
                pos.y = (float)user.GetVariable("y").GetDoubleValue();
                pos.z = (float)user.GetVariable("z").GetDoubleValue();
            }
            
            if(user.ContainsVariable("index"))
            {
                index = (int)user.GetVariable("index").GetIntValue();
            }

            

            GameObject remotePlayer = GameObject.Instantiate(Players[index]) as GameObject;
            remotePlayer.AddComponent<SimpleRemoteInterpolation>();
            remotePlayer.GetComponent<SimpleRemoteInterpolation>().SetTransform(pos, Quaternion.identity, false);

            // Color and name
            

            // Lets track the dude
            remotePlayers.Add(user, remotePlayer);
        }
        

        if (changedVars.Contains("x") && changedVars.Contains("y") && changedVars.Contains("z"))
        {
            // Move the character to a new position...
            remotePlayers[user].GetComponent<SimpleRemoteInterpolation>().SetTransform(
                new Vector3((float)user.GetVariable("x").GetDoubleValue(), (float)user.GetVariable("y").GetDoubleValue(), (float)user.GetVariable("z").GetDoubleValue()),
                //Quaternion.Euler(0, (float)user.GetVariable("rot").GetDoubleValue(), 0),
                Quaternion.identity,
                true);
        }

        //if (changedVars.Contains("x_Health") && changedVars.Contains("y_Health") && changedVars.Contains("z_Health"))
        //{
        //    remotePlayers[user].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<SimpleRemoteInterpolation>().SetTransform(
        //        new Vector3((float)user.GetVariable("x_Health").GetDoubleValue(), (float)user.GetVariable("y_Health").GetDoubleValue(),
        //        (float)user.GetVariable("z_Health").GetDoubleValue()), Quaternion.identity, true);
        //}
        //if(user.ContainsVariable("index"))
        //{
        //    Debug.Log(" Gia tri Index:" + user.GetVariable("index").GetIntValue());
        //}
    }

    private void CreatePlayer()
    {
        Vector3 pos;
        
        Quaternion rot;
        int index = 0;
        if (localPlayer != null)
        {
            pos = localPlayer.transform.position;
            rot = localPlayer.transform.rotation;
            Destroy(localPlayer);
        }
        else
        {
            pos = Posplayer.position;
            rot = Quaternion.identity;
        }
        index = UnityEngine.Random.Range(0, Players.Length);
        localPlayer = GameObject.Instantiate(Players[index], pos, rot) as GameObject;
        localPlayer.AddComponent<PlayerController>();

        localPlayerController = localPlayer.GetComponent<PlayerController>();

        List<UserVariable> userVariables = new List<UserVariable>();
        userVariables.Add(new SFSUserVariable("index", index));
        sfs.Send(new SetUserVariablesRequest(userVariables));

    }

    // Update is called once per frame
    void Update()
    {

    }
    void FixedUpdate()
    {
        if (sfs != null)
        {
            sfs.ProcessEvents();
            if(localPlayer != null && localPlayerController != null && localPlayerController.MovementDirty)
            {
                List<UserVariable> userVariables = new List<UserVariable>();
                userVariables.Add(new SFSUserVariable("x", (double)localPlayer.transform.position.x));
                userVariables.Add(new SFSUserVariable("y", (double)localPlayer.transform.position.y));
                userVariables.Add(new SFSUserVariable("z", (double)localPlayer.transform.position.z));

                userVariables.Add(new SFSUserVariable("x_Health", (double)localPlayerController.x_Health));                
                userVariables.Add(new SFSUserVariable("y_Health", (double)localPlayerController.y_Health));
                userVariables.Add(new SFSUserVariable("z_Health", (double)localPlayerController.z_Health));



                sfs.Send(new SetUserVariablesRequest(userVariables));

                localPlayerController.MovementDirty = false;

            }
        }
    }
}
