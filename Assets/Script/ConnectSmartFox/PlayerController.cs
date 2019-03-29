using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float x_Health { get; set; }
    public float y_Health { get; set; }
    public float z_Health { get; set; }
    public float speed = 30;

    public bool MovementDirty { get; set; }
    // Use this for initialization
    void Start()
    {
        MovementDirty = false;
    }

    // Update is called once per frame
    void Update()
    {
        x_Health = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position.x;
        y_Health = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position.y;
        z_Health = gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position.z;

        float translation = Input.GetAxis("Horizontal");
        float translation_Ver = Input.GetAxis("Vertical");

        //if (translation != 0)
        //{
        //    Debug.Log(translation);
        //    //this.transform.position += new Vector3(this.transform.position.x * 1 * Time.deltaTime, -3.36f, -0.04859542f);
        //    this.transform.Translate(translation * speed * Time.deltaTime, -3.36f, 0);
        //    MovementDirty = true;
        //}
        if (translation != 0 || translation_Ver != 0)
        {
            
            
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                this.transform.position -= Vector3.left * Time.deltaTime * translation * 30;
                MovementDirty = true;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                this.transform.position += Vector3.right * Time.deltaTime * translation * 30;
                MovementDirty = true;
            }

            if(Input.GetKey(KeyCode.UpArrow))
            {                
                this.transform.position += Vector3.up * Time.deltaTime * translation_Ver * 30;
                MovementDirty = true;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                this.transform.position -= Vector3.down * Time.deltaTime * translation_Ver * 30;
                MovementDirty = true;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log(col.gameObject.name);        
        Vector3 oldPos = col.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position;
        col.gameObject.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().transform.position = new Vector3(oldPos.x - 0.5f, oldPos.y, oldPos.z);
    }
}
