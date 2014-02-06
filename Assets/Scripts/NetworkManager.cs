using UnityEngine;
using System.Collections;
using System.Text;
using System;

public class NetworkManager : MonoBehaviour {
	// Random string generator for server names.
	private static System.Random random = new System.Random((int)DateTime.Now.Ticks);
	private static string RandomString(int size) {
		StringBuilder builder = new StringBuilder();
		char ch;
		for (int i = 0; i < size; i++)
		{
			ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));                 
			builder.Append(ch);
		}
		
		return builder.ToString();
	}

	private const string uniqueGameIdentifier = "_Tzarr";
	private string serverName = RandomString(10);
	private const string masterServerIP = "23.23.173.241";
	private const int masterServerPort = 23466;
	private const string facilitatorServerIP = "23.23.173.241";
	private const int facilitatorServerPort = 50005;
	private HostData[] serverList;

	// Use this for initialization
	void Start () {
		MasterServer.ipAddress = masterServerIP;
		MasterServer.port = masterServerPort;
		Network.natFacilitatorIP = facilitatorServerIP;
		Network.natFacilitatorPort = facilitatorServerPort;
	}
	
	// Update is called once per frame
	void Update () {
	}

	// Start a server, register it with the Master Server
	void StartServer() {
		Network.InitializeServer(2, 55000, true);
		MasterServer.RegisterHost(uniqueGameIdentifier, serverName);
	}
	// Called when the server is created successfully.
	void OnServerInitialized() {
		Debug.Log("Server Initialized: " + serverName + " on " + uniqueGameIdentifier + ".");
	}

	// Update the server list.
	void RefreshServerList() {
		MasterServer.RequestHostList(uniqueGameIdentifier);
    }
	void OnMasterServerEvent(MasterServerEvent msEvent) {
		if (msEvent == MasterServerEvent.HostListReceived)
			serverList = MasterServer.PollHostList ();
	}

	// Join a server.
	void JoinServer(HostData serverData) {
		Network.Connect(serverData);
	}
	// Called when a connection to a server is established.
	void OnConnectedToServer() {
		Debug.Log("Joined a server.");
	}

	// This sucks. Don't do this. This shit will be called a LOT.
	void OnGUI() {
		if (Network.peerType == NetworkPeerType.Disconnected) {
			GUI.Label(new Rect(10, 10, 300, 20), "Status: Disconnected");
			if (GUI.Button(new Rect(10, 30, 300, 20), "Start a Server"))
				StartServer();
			if (GUI.Button(new Rect(10, 50, 300, 20), "Refresh Server List")) {
				RefreshServerList();
			}
			if (serverList != null) {
				GUI.Label(new Rect(310, 10, 300, 20), "Connect to a server");
				for (int i = 0; i < serverList.Length; i++) {
					if (GUI.Button(new Rect(310, 30+(20*i), 300, 20), serverList[i].gameName))
						JoinServer(serverList[i]);
				}
			}
		}
		else if (Network.peerType == NetworkPeerType.Client ||
		         Network.peerType == NetworkPeerType.Server) {
			GUI.Label(new Rect(10, 10, 300, 20), (Network.peerType == NetworkPeerType.Client ? 
			                                      "Status: Connected (Client)" : 
			                                      "Status: Connected (Server)"));
			if (GUI.Button(new Rect(10, 30, 300, 20), "Disconnect"))
				Network.Disconnect(200);
		}
	}
}
