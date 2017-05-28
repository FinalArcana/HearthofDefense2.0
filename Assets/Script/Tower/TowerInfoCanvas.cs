using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TowerInfoCanvas : MonoBehaviour {

	public Tower tower;
	public string title;
	private string description;
	private string price;
	private string sell;
    public Text titleTextUI;
	public Text descriptionTextUI;
	public Text priceTextUI;
	public Text sellTextUI;
	public Canvas myCanvas;
    public float fadeTime;
    public bool displayInfo;
	
    // Use this for initialization
    void Start () {
		description = "Damage " + tower.damage;
		price = "Price \n" + tower.damage + " $";
		sell = "Sell \n" + tower.sellCost + " $";
        // myText = GameObject.Find ("TowerText").GetComponent<Text> ();
        // myText.color = Color.clear;
        //Screen.showCursor = false;
        //Screen.lockCursor = true;
    }
     
    // Update is called once per frame
    void Update () 
    {
        FadeText ();
        /*if (Input.GetKeyDown (KeyCode.Escape))     
                {
                        Screen.lockCursor = false;
                }
                */
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
		Debug.Log("Hover tower");
    }
 
    void OnMouseExit()
    {
        displayInfo = false;
 
    }
 
 
    public void FadeText ()
    {
		Debug.Log("FadeText" + displayInfo);
        if(displayInfo)
        {	
			titleTextUI.text = tower.name + " Lv." + tower.level ;
			description = "Damage " + tower.damage;
			price = "Price \n" + tower.cost + " $";
			sell = "Sell \n" + tower.sellCost + " $";
			
			descriptionTextUI.text = description;
			priceTextUI.text = price;
			sellTextUI.text = sell;
            // myText.text = myString;
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
