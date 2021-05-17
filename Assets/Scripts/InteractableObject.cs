using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObject : MonoBehaviour
{
    void OnMouseDown()
    {
        this.gameObject.active = false;
        GameObject.Find("ObjectName").GetComponent<Text>().text = this.gameObject.name;
        Debug.Log("Es funktioniert!");
    }



}
