using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private float rowHeight = 30.0f;
    private void Awake() {
        entryContainer = transform.Find("scoreEntryContainer");
        entryTemplate = entryContainer.Find("scoreEntryTemplate");    

        entryTemplate.gameObject.SetActive(false);
        
        
        
    }

    // Start is called before the first frame update
    private void Start()
    {
        
    }

    public void updateScoreboard(){
        if (DataModels.runnerListResults!=null)
        {
        for (int i=0; i<DataModels.runnerListResults.Count; i++)
        {
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0 , -rowHeight*i);
            entryTransform.gameObject.SetActive(true);

            string rankString;
            switch (i+1)
            { 
                default: rankString = (i+1).ToString() + "th"; break;
                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

            entryTransform.Find("rankText").GetComponent<UnityEngine.UI.Text>().text = rankString;
            entryTransform.Find("nameText").GetComponent<UnityEngine.UI.Text>().text = DataModels.runnerListResults[i].runner.id+" - "+DataModels.runnerListResults[i].runner.attributes.name+" - "+DataModels.runnerListResults[i].runner.attributes.runner_id;
            entryTransform.Find("timeText").GetComponent<UnityEngine.UI.Text>().text = DataModels.runnerListResults[i].timeResult.ToString("0.00");
        }
        }
    }
}
