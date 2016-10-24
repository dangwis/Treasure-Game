using UnityEngine;
using System.Collections;

public class SanityLabel : MonoBehaviour {

    TextMesh label;

    // Use this for initialization
    void Start()
    {
        label = transform.Find("Sanity Label").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        label.text = "Sanity: " + ((int)Player.S.sanity).ToString("D2");
    }
}
