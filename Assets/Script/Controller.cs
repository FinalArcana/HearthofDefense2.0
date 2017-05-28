using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;
using UnityEngine.SceneManagement;

public class Controller : MonoBehaviour {
	public LoginPanelController LoginPanel;
	public WaitPanelController WaitPanel;
	public SocketIOComponent socket;

	// Use this for initialization
	void Start () {
		StartCoroutine(ConnectToServer());
		socket.On("PLAY", OnUserPlay);
		socket.On("LOGIN", OnUserLogin);
		socket.On("USER_CONNECT", OnUserConnected);
		socket.On("USER_CONNECTED", OnUserConnected);
		LoginPanel.plaBtn.onClick.AddListener(OnClickPlayBtn);
	}

	IEnumerator ConnectToServer(){
		yield return new WaitForSeconds(0.5f);

		socket.Emit("USER_CONNECT");

		yield return new WaitForSeconds(1f);

		Dictionary<string, string> data = new Dictionary<string, string>();
		data["name"] = "Watching";
		Vector3 position = new Vector3(0,0,0);
		data["position"] = position.x+","+position.y+","+position.z;
		// socket.Emit("PLAY", new JSONObject(data));
	}
	
	private void OnUserConnected(SocketIOEvent evt){
		Debug.Log("User has login"+ evt.data.GetField("size").ToString());
		if(int.Parse(evt.data.GetField("size").ToString()) == 1){
			SceneManager.LoadScene("Multiplayer_1");
		}
	}

	private void OnUserPlay( SocketIOEvent evt ){
		Debug.Log("Get the message from server is:"+evt.data + "OnUserPlay");
		// LoginPanel.gameObject.SetActive(false);
	}

	private void OnUserLogin( SocketIOEvent evt ){
		Debug.Log("Get the message from server is:"+evt.data + " LOGIN");

		LoginPanel.gameObject.SetActive(false);
		WaitPanel.gameObject.SetActive(true);
		if(int.Parse(evt.data.GetField("size").ToString()) == 1){
			SceneManager.LoadScene("Multiplayer_2");
		}	
	}
	public void OnClickPlayBtn ()
	{
		Debug.Log("CLICKKKKK");
		if(LoginPanel.inputField.text != ""  ){
	
			Dictionary<string, string> data = new Dictionary<string, string>();
			data["name"] = LoginPanel.inputField.text;
			socket.Emit("LOGIN", new JSONObject(data));
			socket.Emit("USER_CONNECT", new JSONObject(data));

		}else{
			LoginPanel.inputField.text = "Please enter your name again ";
		}
	}
}
