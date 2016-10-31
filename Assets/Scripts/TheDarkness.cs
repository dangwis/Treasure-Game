using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TheDarkness : MonoBehaviour {

    public static TheDarkness S;
    static Image darkness;
    public float time = 30f;
    float timeLeft;
    Color goTo;

    // Use this for initialization
    void Start () {
        S = this;
        darkness = transform.Find("Darkness").GetComponent<Image>();
      
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
    }

    void Update()
    { 
        darkness.color = Color.Lerp(darkness.color, goTo, 0.05f);
    }
}
