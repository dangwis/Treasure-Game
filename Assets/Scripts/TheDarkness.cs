using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheDarkness : MonoBehaviour {

    public static TheDarkness S;
    static Image darkness;
    public float time = 30f;
    float timeLeft;
    Color goTo;
    Color original;
    bool flashRed;

    // Use this for initialization
    void Start () {
        S = this;
        darkness = transform.Find("Darkness").GetComponent<Image>();
        original = darkness.color;
        flashRed = false;
	}
	
	public void SetDarkness(TileLight light)
    {
        if(light == TileLight.black)
        {
            goTo = darkness.color;
            goTo.a = 0.5f;
        }
        else if(light == TileLight.dim)
        {
            goTo = darkness.color;
            goTo.a = 0.25f;
        }
        else
        {
            goTo = darkness.color;
            goTo.a = 0;
        }
        original = goTo;
    }

    void Update()
    { 
        darkness.color = Color.Lerp(darkness.color, goTo, 0.05f);
    }
}
