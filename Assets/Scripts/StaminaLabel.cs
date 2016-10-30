using UnityEngine;
using System.Collections;

public class StaminaLabel : MonoBehaviour {

    TextMesh label;

    // Use this for initialization
    void Start()
    {
        label = transform.Find("Stamina Label").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        label.text = "Stamina: " + ((int)Player.S.stamina).ToString("D1");
    }
}
