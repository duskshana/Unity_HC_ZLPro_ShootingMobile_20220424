using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// 大廳管理器
/// 玩家按下對戰按鈕後開始匹配房間
/// </summary>
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // GameObject 遊戲物件：存放 Unity 場景內所有物件
    // SerializeField 將私人欄位顯示在屬性面板上
    // Heder 標題，在屬性面板上顯示粗體字標題
    [SerializeField, Header("連線中畫面")]
    private GameObject goConnectView;
	//對戰按鈕識別名
	[SerializeField, Header("對戰按鈕")]
	private Button btnBattle;
	[SerializeField, Header("連線人數")]
	private Text textcountPlayer;

	//Awake(喚醒)遊戲播放會執行一次，通常初始化遊戲設定
	private void Awake()
	{
        //Photon連線的連線使用設定
        PhotonNetwork.ConnectUsingSettings();
	}
	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();
        print("<color=gray>1.已經進入控制台</color>");
        PhotonNetwork.JoinLobby();
	}
	public override void OnJoinedLobby()
	{
		base.OnJoinedLobby();
		print("<color=gray>2.已經進入大廳</color>");

		//啟動對戰按鈕使其可執行
		btnBattle.interactable = true;
	}
	// 註解：說明
	// 讓按鈕跟程式溝通的流程
	// 1. 提供公開的方法 Public Method
	// 2. 按鈕在點擊 On Click 後設定呼叫此方法

	public void StartConnect()
    {
        print("<color=gray>3.開始連線...</color>");

        // 遊戲物件.啟動設定（布林值）- true 顯示，false 隱藏
        goConnectView.SetActive(true);

		PhotonNetwork.JoinRandomRoom();
    }

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		base.OnJoinRoomFailed(returnCode, message);
		print("<color=gray>4.連線失敗</color>");

		RoomOptions ro = new RoomOptions();
		ro.MaxPlayers = 5;
		PhotonNetwork.CreateRoom("", ro);
	}
	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		print("<color=gray>5.開房者進入房間</color>");
		int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
		int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

		textcountPlayer.text = "連線人數" + currentCount + "/" + maxCount;

	}
}
