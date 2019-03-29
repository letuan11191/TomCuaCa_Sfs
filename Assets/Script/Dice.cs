using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dice : MonoBehaviour {
    public GameObject cv;

    public static Vector3 oldPos;

    public GameObject Tom;
    private float tomPosition;
    public GameObject Huou;
    private float huouPosition;
    public GameObject Ga;
    private float gaPosition;
    public GameObject Cua;
    private float cuaPosition;
    public GameObject Ca;
    private float caPosition;
    public GameObject Bau;
    private float bauPosition;    

    private List<float> lstPos;
    private List<GameObject> lstObj;
    private float oldTime;
    private float rd;
    // Use this for initialization
    void Start () {
        oldPos = gameObject.transform.position;
        oldTime = 0;
        RandomPos();
        lstPos = new List<float>();
        lstObj = new List<GameObject>();
        lstObj.Add(Tom);
        lstObj.Add(Huou);
        lstObj.Add(Ga);
        lstObj.Add(Cua);
        lstObj.Add(Ca);
        lstObj.Add(Bau);
    }

    public void RandomPos()
    {
        rd = Random.Range(1, 180);
        gameObject.transform.Rotate(new Vector3(rd, rd, rd));
    }
	
	// Update is called once per frame
	void Update () {

        //if(Time.time -oldTime > 7)
        //{
        //    oldTime = Time.time;
        //}
        float timer = Time.time - oldTime;
        //Debug.Log(timer);
        if (timer > 7)
        {
            oldTime = Time.time;
            cv.SetActive(true);
            GetPosition();       
        }
        else
        {
            Controller.DiceName = null;
        }
    }

    void GetPosition()
    {
        tomPosition = Tom.transform.position.y;
        huouPosition = Huou.transform.position.y;
        gaPosition = Ga.transform.position.y;
        cuaPosition = Cua.transform.position.y;
        caPosition = Ca.transform.position.y;
        bauPosition = Bau.transform.position.y;
        lstPos.Add(tomPosition);
        lstPos.Add(huouPosition);
        lstPos.Add(gaPosition);
        lstPos.Add(cuaPosition);
        lstPos.Add(caPosition);
        lstPos.Add(bauPosition);
        float a = MaxPosition(lstPos);
        Debug.Log("MaxPos:" + a);        
        GameObject b = MaxObject(a);
        Debug.Log("Obj " + b.name +"ObjPos:" + b.transform.position.y);
        //Debug.Log(b.name);
        Controller.DiceName = b.name;
        resetList();
        
        
    }
    float MaxPosition(List<float> lstPos)
    {
        float max = lstPos[0];
        for( int i = 1; i < (lstPos.Count - 1); i ++)
        {
            if (max < lstPos[i])
                max = lstPos[i];
        }
        return max;
    }
    GameObject MaxObject(float a)
    {
        foreach(var item in lstObj)
        {
            if (item.transform.position.y == a)
                return item;
        }
        return null;
    }

    void resetList()
    {
        lstPos.Clear();
    }
}
