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
using Sfs2X.Entities.Data;

public class Controller : MonoBehaviour
{
    static int gold {get; set;}
    static int facedice { get; set; }

    public static bool reset;
    public static Controller Ctr
    {
        get
        {
            return ctr;
        }
    }
    private static Controller ctr;
    public static string DiceName { get; set; }
    public static int ChooseDice { get; set; }
    public static int Score { get; set; }
    public GameObject cv;
    public Text ScoreTxt;
    public Text UserName;
    public Text TimeDown;

    //Manager//
    private SmartFox sfs;

    private float oldTime = 0;
    void Awake()
    {
        ctr = this;
        Application.runInBackground = true;

    }
    // Use this for initialization
    void Start()
    {
        if (SmartFoxConnection.CheckConnection())
        {
            ChooseDice = 6;
            Debug.Log("CheckConnection");
            sfs = SmartFoxConnection.Connection;
            sfs.AddEventListener(SFSEvent.EXTENSION_RESPONSE, OnExtensionResponse);
            sfs.AddEventListener(SFSEvent.USER_VARIABLES_UPDATE, OnUserVariablesUpdate);
            sfs.AddEventListener(SFSEvent.CONNECTION_LOST, OnConnectionLost);
            sfs.AddEventListener(SFSEvent.USER_ENTER_ROOM, OnUserEnterRoom);
            sfs.AddEventListener(SFSEvent.USER_EXIT_ROOM, OnUserExitRoom);

            reset = false;
            Score = 100;
            cv.SetActive(false);           
            UserName.text = "User Name: " + Connection.username;
            

        }
        else
        {
            SceneManager.LoadScene("Login");
        }


    }

    void FixedUpdate()
    {
        //if (sfs != null)
        //{
        //    Debug.Log("sfs != null");
            
        //}
        //else
        //{
        //    Debug.Log(sfs);
        //}
    }

    private void OnExtensionResponse(BaseEvent evt)
    {        
        try
        {
            string cmd = (string)evt.Params["cmd"];
            ISFSObject dt = (SFSObject)evt.Params["params"];
            if (cmd == "login")
            {

            }
            else if (cmd == "Thongbaoserver")
            {
                //string str = dt.GetUtfString("mess");
                //Debug.Log(str);
                string str = dt.GetUtfString("mess");
                gold = (int)dt.GetLong("gold");
                int timeGame = dt.GetInt("timegame");
                facedice = dt.GetInt("facedice");
                Debug.Log("Thong bao tu server: " + str);
                Debug.Log("gold: " + gold  + " facedice: " + facedice);
                ScoreTxt.text = gold.ToString();                
                DiceName = imageDice(facedice);
                if (DiceName != null)
                {
                    cv.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(DiceName);
                }
                else
                {
                    Debug.Log("DiceName is null");
                }


            }
            else if(cmd == "ThongBaoTime")
            {
                int timeGame = dt.GetInt("timegame");
                //Debug.Log(timeGame);
                TimeDown.text = timeGame.ToString();

            }
            else
            {
                Debug.Log("no mess");
            }
        }
        catch (Exception e)
        {
            Debug.Log("Exception e: " + e);
        }
    }

    private void OnUserVariablesUpdate(BaseEvent evt)
    {
        throw new NotImplementedException();
    }

    private void OnConnectionLost(BaseEvent evt)
    {
        throw new NotImplementedException();
    }

    private void OnUserEnterRoom(BaseEvent evt)
    {
        throw new NotImplementedException();
    }

    private void OnUserExitRoom(BaseEvent evt)
    {
        
    }

    public void SendToServer()
    {
        Debug.Log("SendToServer");
        ISFSObject data = new SFSObject();
        data.PutUtfString("logDebug", "Data gui len tu client");
        data.PutLong("gold", Score);
        data.PutInt("Choose", ChooseDice);
        //data.PutUtfString("result", DiceName);

        ExtensionRequest request = new ExtensionRequest("ChooseDice", data);
        sfs.Send(request);

    }

    void demNguoc()
    {        
        ISFSObject data = new SFSObject();
        data.PutBool("demNguoc", true);     
        data.PutBool("reset", reset);
        ExtensionRequest request = new ExtensionRequest("DebugClient", data);
        sfs.Send(request);
    }

    void resetDemNguoc()
    {
        Debug.Log("resetTime");
        Debug.Log(reset);
        ISFSObject data = new SFSObject();
        data.PutBool("reset", reset);
        ExtensionRequest request = new ExtensionRequest("DebugClient", data);
        sfs.Send(request);
        reset = false;
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log("ChooseDice: " +ChooseDice);
        //Debug.Log("Time.time: " + Time.time);
        //Debug.Log("deltaTime: " + (Time.time - oldTime));
        if (sfs != null)
        {
            sfs.ProcessEvents();                      
            if(Time.time - oldTime > 1f && ChooseDice == 6)
            {                                
                oldTime = Time.time;
                //Debug.Log("oldTime:" + oldTime);
                demNguoc();
            }
            if(reset)
            {
                resetDemNguoc();
            }
            else 
            {
                
            }
        }
        else
        {
            Debug.Log(sfs);
        }
        
        
        //if (ChooseDice == null)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    if (DiceName != null)
        //    {
        //        //cv.SetActive(true);
        //        cv.transform.GetChild(0).GetComponent<Image>().sprite = Resources.Load<Sprite>(DiceName);
        //        Time.timeScale = 0;
        //        if (DiceName == ChooseDice)
        //        {
        //            Score += 10;
        //        }
        //        else
        //        {
        //            Score -= 10;
        //        }
        //    }

        //}
        //ScoreTxt.text = Score.ToString();

    }

    String imageDice(int rd)
    {
        String diceName = null;
        switch(rd)
        {
            case 0: diceName = "Bau"; break;
            case 1: diceName = "Ca"; break;
            case 2: diceName = "Cua"; break;
            case 3: diceName = "Tom"; break;
            case 4: diceName = "Ga"; break;
            case 5: diceName = "Huou"; break;
        }
        return diceName;
    }
}
