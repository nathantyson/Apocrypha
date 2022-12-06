using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableText : MonoBehaviour
{
    Text text;
    Image box;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        box = this.GetComponentInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            text.enabled = false;
            box.enabled = false;
            
        }
    }
}
