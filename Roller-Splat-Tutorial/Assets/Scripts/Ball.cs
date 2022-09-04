using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    private GameManager gm; 
    
    private Rigidbody rb;
    public Image levelBar;

    private Vector2 firstPos;
    private Vector2 secondPos;
    private Vector2 currentPos;

    public float moveSpeed;
    public float currentGroundNumber;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        gm = GameObject.FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        Swipe();
        levelBar.fillAmount = currentGroundNumber / gm.groundNumbers; //progress bar game managerdan zemin numarasýný alýyor.

        if(levelBar.fillAmount == 1)
        {
            gm.LevelUpdate();
        }
    }

    private void Swipe()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firstPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        if (Input.GetMouseButtonUp(0))
        {
            secondPos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            currentPos = new Vector2(secondPos.x - firstPos.x, secondPos.y - firstPos.y);
        }

        currentPos.Normalize(); //düz þekilde gitmesi için.

        if (currentPos.y < 0 && currentPos.x > -0.5f && currentPos.x < 0.5f) //back
        {
            rb.velocity = Vector3.back * moveSpeed;
        }
        else if (currentPos.y > 0 && currentPos.x > -0.5f && currentPos.x < 0.5f) //forward
        {
            rb.velocity = Vector3.forward * moveSpeed;
        }
        else if (currentPos.x < 0 && currentPos.y > -0.5f && currentPos.y < 0.5f) //left
        {
            rb.velocity = Vector3.left * moveSpeed;
        }
        else if (currentPos.x > 0 && currentPos.y > -0.5f && currentPos.y < 0.5f) //right
        {
            rb.velocity = Vector3.right * moveSpeed;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != new Color(0.67f, 0.14f, 0.80f)) // bu komut yazýldý çünkü top geçtiði yerlerden geçtiðinde currentgroundnumber tekrar artmasýn diye. ilerleme çubuðu etkilenmesin diye.
        {
            if(other.gameObject.tag == "Ground")
            {
                other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.67f, 0.14f, 0.80f); // color code topun rengi, mor.
                Constraints();
                currentGroundNumber++; //rengi deðiþtirilen bloklarý say, level bar için.
            }
        }
    }



    private void Constraints() //aslýnda ball rigidbody'de freeze rotation vs yapmak yerine buradan yapýyoruz. oyun baþladýðýnda deðil de yere deðdiði an kilitlensin diye.
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
