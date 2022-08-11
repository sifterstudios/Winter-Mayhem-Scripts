using UnityEngine;
using UnityEngine.Animations;
using Normal.Realtime;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private Transform _playerController;
    [SerializeField] private CameraFollow _cameraRig;
    [SerializeField] private CinemachineVirtualCamera _cinemachine;

    private Realtime _realtime;
    private GameObject[] _spawns;

    private void Awake()
    {
        _realtime = GetComponent<Realtime>();
        _realtime.didConnectToRoom += DidConnectToRoom;

        // Make sure we active the right cameras
        Camera.main.enabled = false;
        _cameraRig.GetComponentInChildren<Camera>().enabled = true;

        _spawns = GameObject.FindGameObjectsWithTag(TagConstants.Respawn);
    }

    private Transform findSpawnForPlayer()
    {
        var rnd = new System.Random();
        int index = rnd.Next(_spawns.Length);

        var s = _spawns[index];
        return s.transform;
    }

    private void DidConnectToRoom(Realtime realtime)
    {
        var spawn = findSpawnForPlayer();

        // Create networkable player representation
        var instantiateOptions = new Realtime.InstantiateOptions();
        GameObject playerRepresentationGameObject = Realtime.Instantiate(prefabName: "PlayerRepresentation",
            position: spawn.position, rotation: spawn.rotation, instantiateOptions);

        // Create local player controller
        Transform playerControllerGameObject = Instantiate(_playerController, spawn.position, spawn.rotation);
        var playerController = playerControllerGameObject.GetComponent<PlayerControllerSnowball>();
        playerController.SetPlayerRepresentation(playerRepresentationGameObject.transform);

        // Make camera follow player
        _cinemachine.m_Follow = playerControllerGameObject.transform;
        _cinemachine.m_LookAt = playerControllerGameObject.transform;

        GameManager.Instance.RestartGame();
    }
}
