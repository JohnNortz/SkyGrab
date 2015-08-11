using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextSwap : MonoBehaviour {

    
    public Text me;
    public int house;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("House" + house) == -1) me.text = "No House";
        if (PlayerPrefs.GetInt("House" + house) == 0) me.text = "Cinder Skiff                                              --------------------------------------------                           +1 Gas per Skiff                                               .                                                                      >Skiff Doesn't launch bombs                                              .                                                                      >Rams enemy boats and explodes";
        if (PlayerPrefs.GetInt("House" + house) == 1) me.text = "Porcelains Buoys                                              --------------------------------------------                         +1 Gas per Buoy                                              .                                                                      +66% Buoy strength";
        if (PlayerPrefs.GetInt("House" + house) == 2) me.text = "Jove's Blimp                                              --------------------------------------------                           -2 Gas per Blimp                                              .                                                                      -40% Blimp Speed";
        if (PlayerPrefs.GetInt("House" + house) == 3) me.text = "Stride Blimp                                              --------------------------------------------                           +1 Gas per Blimp                                              .                                                                      +20% Blimp Speed";
        if (PlayerPrefs.GetInt("House" + house) == 4) me.text = "Wrathful Skiff                                              --------------------------------------------                           +3 Gas per Skiff                                              .                                                                      +50% Skiff attack Range                                              .                                                                      >Skiffs projectiles have small area of effect                                                          .                                                                      -50% Skiff attack speed";
        if (PlayerPrefs.GetInt("House" + house) == 5) me.text = "Heaven's  Spears                                              --------------------------------------------                         +3 Gas per Skiff                                              .                                                                      +200% Skiff attack Range                                              .                                                                      -60% Skiff attack speed";
        if (PlayerPrefs.GetInt("House" + house) == 6) me.text = "Iron Tug                                                          --------------------------------------------                          +3 Gas per Tug                                              .                                                                      +50% Tug attack speed                                              .                                                                      -36% Tug Move Speed";
        if (PlayerPrefs.GetInt("House" + house) == 7) me.text = "Blighted Tug                                              --------------------------------------------                        +3 Gas per Tug                                              .                                                                      >Tug self-destructs when attacked                                              .                                                                      -16% Tug move speed";


    }
}

