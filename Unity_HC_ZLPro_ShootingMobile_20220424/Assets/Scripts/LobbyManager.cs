using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

/// <summary>
/// �j�U�޲z��
/// ���a���U��ԫ��s��}�l�ǰt�ж�
/// </summary>
public class LobbyManager : MonoBehaviourPunCallbacks
{
    // GameObject �C������G�s�� Unity �������Ҧ�����
    // SerializeField �N�p�H�����ܦb�ݩʭ��O�W
    // Heder ���D�A�b�ݩʭ��O�W��ܲ���r���D
    [SerializeField, Header("�s�u���e��")]
    private GameObject goConnectView;
	//��ԫ��s�ѧO�W
	[SerializeField, Header("��ԫ��s")]
	private Button btnBattle;
	[SerializeField, Header("�s�u�H��")]
	private Text textcountPlayer;

	//Awake(���)�C������|����@���A�q�`��l�ƹC���]�w
	private void Awake()
	{
        //Photon�s�u���s�u�ϥγ]�w
        PhotonNetwork.ConnectUsingSettings();
	}
	public override void OnConnectedToMaster()
	{
		base.OnConnectedToMaster();
        print("<color=gray>1.�w�g�i�J����x</color>");
        PhotonNetwork.JoinLobby();
	}
	public override void OnJoinedLobby()
	{
		base.OnJoinedLobby();
		print("<color=gray>2.�w�g�i�J�j�U</color>");

		//�Ұʹ�ԫ��s�Ϩ�i����
		btnBattle.interactable = true;
	}
	// ���ѡG����
	// �����s��{�����q���y�{
	// 1. ���Ѥ��}����k Public Method
	// 2. ���s�b�I�� On Click ��]�w�I�s����k

	public void StartConnect()
    {
        print("<color=gray>3.�}�l�s�u...</color>");

        // �C������.�Ұʳ]�w�]���L�ȡ^- true ��ܡAfalse ����
        goConnectView.SetActive(true);

		PhotonNetwork.JoinRandomRoom();
    }

	public override void OnJoinRoomFailed(short returnCode, string message)
	{
		base.OnJoinRoomFailed(returnCode, message);
		print("<color=gray>4.�s�u����</color>");

		RoomOptions ro = new RoomOptions();
		ro.MaxPlayers = 5;
		PhotonNetwork.CreateRoom("", ro);
	}
	public override void OnJoinedRoom()
	{
		base.OnJoinedRoom();
		print("<color=gray>5.�}�Ъ̶i�J�ж�</color>");
		int currentCount = PhotonNetwork.CurrentRoom.PlayerCount;
		int maxCount = PhotonNetwork.CurrentRoom.MaxPlayers;

		textcountPlayer.text = "�s�u�H��" + currentCount + "/" + maxCount;

	}
}
