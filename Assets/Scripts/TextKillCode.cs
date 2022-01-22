using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextKillCode : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartCoroutine(Unfreeze());
    }
  
    IEnumerator Unfreeze()
    {
        while(Time.timeScale == 0)
        {
            yield return new WaitForSecondsRealtime(3f);

            Time.timeScale = 1;
        }
        
        if (Time.timeScale == 1)
        {
            Destroy(gameObject);
        }
    }

    
}
