using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextTest : MonoBehaviour {

    public string url = "http://www.mon-ip.com/";

    IEnumerator Extract()
    {
        WWW www = new WWW(url);
        yield return www;
        //GetComponent<TextTest>().GetComponent<Text>().text = www.text;
        this.GetComponent<TextMesh>().text = ExtractIp(www.text);
    }

    string ExtractIp (string txt)
    {
        //var Ip = "157.228.92.146";
        int pos1 = 0;
        int pos2 = 0;
        pos1 = txt.IndexOf("var Ip = ");
        pos1 += "var Ip = ".Length;
        pos2 = txt.IndexOf(";", pos1);
        string ip = "";
        ip = txt.Substring(pos1, pos2-pos1);
        return ip;
    }

    // Use this for initialization
    void Start () {
        StartCoroutine(Extract());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

/*
<div class="t m0 x14 ha y6f ff1 fs8 fc0 sc0 ls1 ws5c">In the paint “
<span class="ff4 ws5d">The Prophet Daniel</span>” (Figure<span class="_ _2"> </span>1, frame 1) </div>

*/
