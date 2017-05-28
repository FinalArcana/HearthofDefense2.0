using UnityEngine;
using UnityEngine.UI;
using System.Collections;
 
public class DisplayTowerUI : MonoBehaviour {
 
    public string myString;
    public Text myText;
	public Canvas myCanvas;
    public float fadeTime;
    public bool displayInfo;
	public BuildingManager bm;
	
    // Use this for initialization
    void Start () {
		Debug.Log("Start UI tower");
        // myText = GameObject.Find ("TowerText").GetComponent<Text> ();
        // myText.color = Color.clear;
        //Screen.showCursor = false;
        //Screen.lockCursor = true;
		bm = GameObject.FindObjectOfType<BuildingManager>();
    }
     
    // Update is called once per frame
    void Update () 
    {
        FadeText ();
    }
	
    /// <summary>
	/// OnMouseUp is called when the user has released the mouse button.
	/// </summary>
	void OnMouseUp()
	{
		displayInfo = true;
	}

    void OnMouseEnter()
    {
        // displayInfo = true;
		bm.getDummyTower().SetActive(false);
    }
 
    void OnMouseExit()
    {
        displayInfo = false;
		bm.getDummyTower().SetActive(true);
    }
 
 
    void FadeText ()
    {
        if(displayInfo)
        {
            myText.text = myString;
            // myText.color = Color.Lerp (myText.color, Color.white, fadeTime * Time.deltaTime);
			 myCanvas.gameObject.SetActive(true);
			 this.GetComponent<BoxCollider>().size = new Vector3(7.0f,3.4f,11.5f);
        }
        else
        {
            // myText.color = Color.Lerp (myText.color, Color.clear, fadeTime * Time.deltaTime);
			 myCanvas.gameObject.SetActive(false);
			 this.GetComponent<BoxCollider>().size = new Vector3(1.5f,3.4f,1.6f);
        }

        }
 
 
 
}