using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Camera _mainCamera;
    private float _leftEndWorldPosition;

    private void Start()
    {
        Vector3 leftBottomWorldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(0, 0, 0));
        _leftEndWorldPosition = leftBottomWorldPosition.x;
    }
}
