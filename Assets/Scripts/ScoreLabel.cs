using UnityEngine;
using System.Collections;

public class ScoreLabel : MonoBehaviour {

    TextMesh label;

    // Use this for initialization
    void Start()
    {
        label = transform.Find("Score Label").GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        label.text = "Score: " + (Player.S.score).ToString();
    }
}
