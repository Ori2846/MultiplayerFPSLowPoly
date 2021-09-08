using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using System.Linq;
using UnityEngine.UI;


    [System.Serializable]
    public class MapData
    {
        public string name;
        public int scene;
    }
public class Launcher : MonoBehaviourPunCallbacks
{
    public TMP_Text mapValue;
    public TMP_Dropdown MapValueDropDown;
    public MapData[] maps;
    private int currentmap = 0;
    public static Launcher Instance;
    [SerializeField] TMP_InputField roomNameInputField;
    [SerializeField] TMP_Text errorText;
    [SerializeField] TMP_Text roomNameText;
    [SerializeField] Transform roomListContent;
    [SerializeField] Transform playerListContent;
    [SerializeField] GameObject roomListItemPrefab;
    [SerializeField] GameObject PlayerListItemPrefab;
    [SerializeField] GameObject startGameButton;
    [SerializeField] GameObject MaxPlayersSlider_;
    private byte integer;
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        Debug.Log("Connecting to Lobby");
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
        PhotonNetwork.JoinLobby();
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    public override void OnJoinedLobby()
    {
        MenuManager.Instance.OpenMenu("Title");
        Debug.Log("Connected to Lobby");
        PhotonNetwork.NickName = "Player" + Random.Range(0,1000).ToString("0000");
    }

    public void CreateRoom()
    {
        if(string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        integer = System.Convert.ToByte(MaxPlayersSlider_.GetComponent<Slider>().value);
        RoomOptions roomOps = new RoomOptions() {IsVisible = true, IsOpen = true, MaxPlayers = integer};
        roomOps.CustomRoomPropertiesForLobby = new string[] { "map", "mode" };
        ExitGames.Client.Photon.Hashtable properties = new ExitGames.Client.Photon.Hashtable(); 
        properties.Add("map", currentmap);
        //properties.Add("mode", (int)GameSettings.GameMode);
        roomOps.CustomRoomProperties = properties; 
        PhotonNetwork.CreateRoom(roomNameInputField.text, roomOps);
        MenuManager.Instance.OpenMenu("Loading");
    }
        public void ChangeMap ()
        {
            //currentmap++;
            //if (currentmap >= maps.Length) currentmap = 0;
            mapValue.text = "MAP: " + maps[currentmap].name.ToUpper();
        }
    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu("Room");
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        foreach(Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        Player[] players = PhotonNetwork.PlayerList;
        for(int i = 0; i < players.Count(); i++)
        {
            Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        errorText.text = "Room Creation Failed:" + message;
        MenuManager.Instance.OpenMenu("Error");
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(maps[currentmap].scene);
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
        MenuManager.Instance.OpenMenu("Loading");
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu("Loading");
    }
    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu("Title");
    }
        public void ClearRoomList ()
        {
            foreach (Transform trans in roomListContent) Destroy(trans.gameObject);
        }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        ClearRoomList();
        foreach(Transform trans in roomListContent)
        {
            Destroy(trans.gameObject);
        }
            //GameObject newRoomButton = Instantiate(roomListItemPrefab, roomListContent) as GameObject;
            foreach (RoomInfo a in roomList)
            {
            GameObject newRoomButton = Instantiate(roomListItemPrefab, roomListContent) as GameObject;
            for(int i = 0; i < roomList.Count; i++)
		    {
			if(roomList[i].RemovedFromList)
				continue;
                newRoomButton.GetComponent<RoomListItem>().SetUp(roomList[i]);
                            //newRoomButton.transform.Find("Name").GetComponent<Text>().text = a.Name;
            newRoomButton.transform.Find("PlayersInScene/Players").GetComponent<TMP_Text>().text = a.PlayerCount + " / " + a.MaxPlayers;

            if (a.CustomProperties.ContainsKey("map"))
                newRoomButton.transform.Find("MapNamePanel/Name").GetComponent<TMP_Text>().text = maps[(int)a.CustomProperties["map"]].name;
            else
                newRoomButton.transform.Find("MapNamePanel/Name").GetComponent<TMP_Text>().text = "-----";
            }
            }
			//Instantiate(roomListItemPrefab, roomListContent).GetComponent<RoomListItem>().SetUp(roomList[i]);
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Instantiate(PlayerListItemPrefab, playerListContent).GetComponent<PlayerListItem>().SetUp(newPlayer); 
    }
    // Update is called once per frame
    void Update()
    {
                Pause.paused = false;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UpdateMapValue();
    }
    void UpdateMapValue()
    {
        currentmap = MapValueDropDown.value;
        ChangeMap();
    }
    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game is exiting");
    }
}
