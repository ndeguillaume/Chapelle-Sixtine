

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows.Speech;

public class SpeechRecognition : MonoBehaviour {

    public bool List;
    public Canvas canvasList;
    public Canvas canvasWiki;
    public Canvas canvasFeedback;
    public GameObject player;
    public GameObject playerParent;
    public Variables variables;
    KeywordRecognizer keywordRecognizer;
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public ConfidenceLevel confidence = ConfidenceLevel.Medium;
    public Text uiList;
    public Text wikiText;
    public Text feedbackText;
    public float targetTime = 1.0f;
    private float time;
    private bool timerStart = false;
   
    void Start()
    {
        time = targetTime;
        variables = player.GetComponent<Variables>();

        keywords.Add("move", () =>
        {
            MoveCalled();
        });

        keywords.Add("list", () =>
        {
            ListCalled();
        });
        keywords.Add("bigger", () =>
        {
            BiggerCalled();
        });
        keywords.Add("smaller", () =>
        {
            SmallerCalled();
        });
        keywords.Add("closer", () =>
        {
            CloserCalled();
        });
        keywords.Add("select", () =>
        {
            SelectCalled();
        });
        keywords.Add("okey", () =>
        {
            OkeyCalled();
        });
        keywords.Add("remove", () =>
        {
            DeleteCalled();
        });
        keywords.Add("face me", () =>
        {
            FaceMeCalled();
        });
        keywords.Add("up", () =>
        {
            UpCalled();
        });
        keywords.Add("down", () =>
        {
           DownCalled();
        });
        keywords.Add("search", () =>
        {
            SearchCalled();
        });
        keywords.Add("close", () =>
        {
            CloseCalled();
        });

        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray(),confidence);
        keywordRecognizer.OnPhraseRecognized += KeywordRecognizedOnPhraseRecognized;
        keywordRecognizer.Start();


        canvasList.enabled = true;
        canvasWiki.enabled = false;
        canvasFeedback.enabled = false;
    }

    void Update()
    {
        if (timerStart)
        {
            time -= Time.deltaTime;
            if (time <= 0.0f)
            {
                timerStart = false;
                time = targetTime;
                canvasFeedback.enabled = false;
            }   
        }
    }

    void KeywordRecognizedOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
            feedbackText.text = args.text;
            canvasFeedback.enabled = true;
            timerStart = true;

        }

    }
     
    void MoveCalled()
    {
        variables.go = true;
    }

    void CloseCalled()
    {
        canvasWiki.enabled = false;
        canvasList.enabled=false;

    }
    void ListCalled()
    {
        if (canvasList.isActiveAndEnabled)
        {
            List = false;
            canvasList.enabled = false;
        }
        else
        {
            canvasList.enabled = true;
            canvasWiki.enabled = false;
            variables.vect = new Vector3(1.05f, -0.45f, -13.75f);
            variables.search = false;
        }

    }

    void BiggerCalled()
    {
        if (variables.focusedGameObject != null)
        variables.bigger = true;

    }

    void SmallerCalled()
    {
        if (variables.focusedGameObject != null)
        variables.smaller = true;

    }
    
    void CloserCalled()
    {
        if (variables.focusedGameObject != null)
        variables.closer = true;

    }
    void SelectCalled()
    {
        if (variables.focusedGameObject != null)
        variables.select = true;

    }
    void OkeyCalled()
    {
        if (variables.focusedGameObject != null)
        variables.ok = true;

    }
    void DeleteCalled()
    {
        if (variables.focusedGameObject != null)
        variables.delete = true;

    }
    void FaceMeCalled()
    {
        if (variables.focusedGameObject != null)
        variables.faceMe = true;

    }
    void UpCalled()
    {
        if (playerParent.transform.position.y < 15f)
         playerParent.transform.position += new Vector3(0f, 1f, 0f);

    }
    void DownCalled()
    {
   
        if (playerParent.transform.position.y> 0f)
         playerParent.transform.position -= new Vector3(0f, 1f, 0f);

    }
    void SearchCalled()
    {
        if (variables.focusedGameObject != null)
            variables.search = true;
        

    }

}
