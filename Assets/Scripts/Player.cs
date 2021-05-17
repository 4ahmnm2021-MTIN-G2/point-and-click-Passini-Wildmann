
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public Vector3 mPosition;

    public Vector3 playerPos;

    public float speed = 0.1f;

    void Start()
    {
        playerPos = this.transform.position;
    }
    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {

            mPosition = Input.mousePosition;
            
            Ray ray = Camera.main.ScreenPointToRay(mPosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
                playerPos = hit.point;
        }
        if (Input.GetMouseButton(0))
        {

        }
            ;
        
            
            this.transform.position = Vector3.MoveTowards(this.transform.position, playerPos, speed);
    }
}