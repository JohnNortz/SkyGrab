using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextSwap2 : MonoBehaviour {

    public Text me;
    public int house;

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetInt("House" + house) == -1) me.text = "No House";
        if (PlayerPrefs.GetInt("House" + house) == 0) me.text = "Cinder Forge              ..........................          The Cinder Forge use remotely controlled skiffs rigged with explosives to deal massive damage to all ships nearby";
        if (PlayerPrefs.GetInt("House" + house) == 1) me.text = "House of Porcelain          ..........................          The House of Porcelain use stronger more expensive materials to give their buoys greater strenght";
        if (PlayerPrefs.GetInt("House" + house) == 2) me.text = "Jove's Desciples            ..........................          Jove's Desciples use a cheaper but slower design in their Bouy placing blimps";
        if (PlayerPrefs.GetInt("House" + house) == 3) me.text = "Silent Stride                ..........................          Silent Stride's larger blimp design allows for greater speed for a slight increase in cost";
        if (PlayerPrefs.GetInt("House" + house) == 4) me.text = "Sol's Wrath                 ..........................          Sol's Wrath outfit their skiffs with improvised explosives that can damage multiple boats at once";
        if (PlayerPrefs.GetInt("House" + house) == 5) me.text = "Heaven's Reach             ..........................           Heaven's Reach use modified sounding-rockets to attack from a greater ranger";
        if (PlayerPrefs.GetInt("House" + house) == 6) me.text = "The Iron Maw               ..........................          The Iron Maws tugs use booster engines to attack faster but their weight slows movement";
        if (PlayerPrefs.GetInt("House" + house) == 7) me.text = "House of Blight            ..........................          The House of Blight are willing to potential sacrifice pilots for their cause and rig their tugs with explosives in case they are attacked";


    }
}
