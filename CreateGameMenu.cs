using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateGameMenu : MonoBehaviour
{
    public TMP_Text MaxPlayersText;
    //public Slider MaxPlayersSlider;
    public GameObject MaxPlayersSlider;
    public string SliderInt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SliderInt = MaxPlayersSlider.GetComponent<Slider>().value.ToString();
       //SliderInt = MaxPlayersSlider.value;
        MaxPlayersText.text = SliderInt;
    }
}
