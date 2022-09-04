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
        levelBar.fillAmount = currentGroundNumber / gm.groundNumbers; //progress bar game managerdan zemin numaras�n� al�yor.

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

        currentPos.Normalize(); //d�z �ekilde gitmesi i�in.

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
        if (other.gameObject.GetComponent<MeshRenderer>().material.color != new Color(0.67f, 0.14f, 0.80f)) // bu komut yaz�ld� ��nk� top ge�ti�i yerlerden ge�ti�inde currentgroundnumber tekrar artmas�n diye. ilerleme �ubu�u etkilenmesin diye.
        {
            if(other.gameObject.tag == "Ground")
            {
                other.gameObject.GetComponent<MeshRenderer>().material.color = new Color(0.67f, 0.14f, 0.80f); // color code topun rengi, mor.
                Constraints();
                currentGroundNumber++; //rengi de�i�tirilen bloklar� say, level bar i�in.
            }
        }
    }



    private void Constraints() //asl�nda ball rigidbody'de freeze rotation vs yapmak yerine buradan yap�yoruz. oyun ba�lad���nda de�il de yere de�di�i an kilitlensin diye.
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotation;
    }
}
