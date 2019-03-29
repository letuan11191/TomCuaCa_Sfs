using Sfs2X.Entities.Data;
using Sfs2X.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseButton : MonoBehaviour
{
    public GameObject DicePos;

    public GameObject Bau;
    public GameObject Ca;
    public GameObject Cua;
    public GameObject Ga;
    public GameObject Huou;
    public GameObject Tom;

    private Sprite bauPic;
    private Sprite caPic;
    private Sprite cuaPic;
    private Sprite gaPic;
    private Sprite huouPic;
    private Sprite tomPic;

    private static bool choose;
    public GameObject cv;
    // Use this for initialization
    void Start()
    {
        bauPic = Bau.GetComponent<Image>().sprite;
        caPic = Ca.GetComponent<Image>().sprite;
        cuaPic = Cua.GetComponent<Image>().sprite;
        gaPic = Ga.GetComponent<Image>().sprite;
        huouPic = Huou.GetComponent<Image>().sprite;
        tomPic = Tom.GetComponent<Image>().sprite;
        choose = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!choose)
        {
            Bau.GetComponent<Image>().sprite = bauPic;
            Ca.GetComponent<Image>().sprite = caPic;
            Cua.GetComponent<Image>().sprite = cuaPic;
            Ga.GetComponent<Image>().sprite = gaPic;
            Huou.GetComponent<Image>().sprite = huouPic;
            Tom.GetComponent<Image>().sprite = tomPic;
        }
    }

    public void ChooseBau()
    {
        if (choose == false)
        {
            Sprite img = Resources.Load<Sprite>("Bau1");
            Bau.GetComponent<Image>().sprite = img;
            choose = true;
            Controller.ChooseDice = 0;
        }
    }
    public void ChooseCa()
    {
        if (choose == false)
        {
            Sprite img = Resources.Load<Sprite>("Ca1");
            Ca.GetComponent<Image>().sprite = img;
            choose = true;
            Controller.ChooseDice = 1;
        }
    }
    public void ChooseCua()
    {
        if (choose == false)
        {
            Sprite img = Resources.Load<Sprite>("Cua1");
            Cua.GetComponent<Image>().sprite = img;
            choose = true;
            Controller.ChooseDice = 2;
        }
    }
    public void ChooseGa()
    {
        if (choose == false)
        {
            Sprite img = Resources.Load<Sprite>("Ga1");
            Ga.GetComponent<Image>().sprite = img;
            choose = true;
            Controller.ChooseDice = 4;
        }
    }
    public void ChooseHuou()
    {
        if (choose == false)
        {
            Sprite img = Resources.Load<Sprite>("Huou1");
            Huou.GetComponent<Image>().sprite = img;
            choose = true;
            Controller.ChooseDice = 5;
        }
    }
    public void ChooseTom()
    {
        if (choose == false)
        {
            Sprite img = Resources.Load<Sprite>("Tom1");
            Tom.GetComponent<Image>().sprite = img;
            choose = true;
            Controller.ChooseDice = 3;
        }
    }

    public void ChooseReset()
    {
        DicePos.transform.position = Dice.oldPos;
        DicePos.GetComponent<Dice>().RandomPos();        
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);        
        cv.SetActive(false);
        choose = false;
        Controller.ChooseDice = 6;
        Controller.reset = true;
        
    }
}
