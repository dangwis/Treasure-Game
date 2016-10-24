using UnityEngine;
using System.Collections;

public class SanityLabel : MonoBehaviour {

    TextMesh label;
    Renderer textRenderer;

    // Use this for initialization
    void Start()
    {
        label = transform.Find("Sanity Label").GetComponent<TextMesh>();
        textRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        label.text = "Sanity: " + ((int)Player.S.sanity).ToString("D2");
    }
}
