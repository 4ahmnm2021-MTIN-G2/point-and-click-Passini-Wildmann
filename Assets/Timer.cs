using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    Text timerText;
    int currCountdownValue = 120;

    // Start is called before the first frame update
    void Start()
    {
        this.timerText = GameObject.Find("timer-text").GetComponent<Text>();
        this.timerText.text = "" + currCountdownValue;
        StartCoroutine(this.doTimer());
    }

    // Update is called once per frame
    void Update()
    {
    }
    
    IEnumerator doTimer() {
        while (currCountdownValue > 0) {
            // Debug.Log("Countdown: " + currCountdownValue);
            currCountdownValue--;
            if(currCountdownValue == 1) {
                Application.Quit();
            }
            this.timerText.text = "" + currCountdownValue;
            yield return new WaitForSeconds(1f);
        }
    }
}
