using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockApple : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("TEST");
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit, 100.0f)){
            if(hit.transform.gameObject != null){
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }

    
}
