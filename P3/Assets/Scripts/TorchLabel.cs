using UnityEngine;
using System.Collections;

public class TorchLabel : MonoBehaviour {

    TextMesh label;
    Renderer textRenderer;

    // Use this for initialization
    void Start()
    {
        label = transform.Find("Torch Label").GetComponent<TextMesh>();
        textRenderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        label.text = "Torches: " + ((int)Player.S.torches).ToString("D1");
    }
}
