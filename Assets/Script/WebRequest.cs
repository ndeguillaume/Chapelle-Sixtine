using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class WebRequest : MonoBehaviour {

    public Variables variables;
    public Canvas canvasList;
    public Canvas canvasWiki;
    public GameObject player;
    public Text wikiText;
    private const string URL = "https://en.wikipedia.org/w/api.php?format=json&action=query&prop=extracts&exintro=&explaintext=&titles=";
    private string url = URL;

    public void Start()
    {
        variables = player.GetComponent<Variables>();
    }

    public void Update()
    {
            if (variables.search)
            {
            url = URL + variables.texture.name;
            Request();
            }

    }

    public void Request()
    {
        WWW request = new WWW(url);
        StartCoroutine(OnResponse(request));
        canvasWiki.enabled = true;
        canvasList.enabled = false;
        variables.vect = new Vector3(0f, 0f, 0f);
    }

    private IEnumerator OnResponse(WWW request)
    {
        yield return request;
        if (request.text.Contains("\"missing\""))
        {
            wikiText.text = "Wikipedia page not found " + Regex.Unescape("\\u2639");
        }
        else
        {
            wikiText.text = (splitWiki(request.text));
        }
        variables.search = false;
    }

    private string splitWiki(string text)
    {
        string[] tmp = Regex.Split(text, ("\"extract\":\""));
        string res = string.Concat(tmp[1]);
        string toReplace = "\\u";
        while (res.Contains(toReplace))
        {
            int index = res.IndexOf(toReplace); 
            string code = res;
            res = res.Substring(0, index);
            string str = code.Substring(index + 6, code.Length-(index+6));
            code = code.Substring(index,6);
            code = Regex.Unescape(code);
            res += code + str;
        }   

        var charsToRemove = new string[] { "\"}}}}"};
        foreach (var c in charsToRemove)
        {
            res = res.Replace(c, string.Empty);
        }
         charsToRemove = new string[] { "\\\"" };
        foreach (var c in charsToRemove)
        {
            res = res.Replace(c, "\"");
        }

        charsToRemove = new string[] { "\\n" };
        foreach (var c in charsToRemove)
        {
            res = res.Replace(c, string.Empty);
        }

      /*  Debug.Log("longueur : " + res.Length);
        int lignes = res.Length / 34;
        Debug.Log("ligne : " + lignes);
        wikiText.transform.height += 15 * lignes;*/
        return res;
    }
}
    