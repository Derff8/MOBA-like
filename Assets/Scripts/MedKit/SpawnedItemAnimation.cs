using UnityEngine;

public class SpawnedItemAnimation : MonoBehaviour
{
    [SerializeField] private float _amplitude;
    [SerializeField] private float _frequency;
    [SerializeField] private float _rotationSpeed;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        float newY = _startPosition.y + Mathf.Sin(Time.time * _frequency) * _amplitude;
        
        transform.position = new Vector3(_startPosition.x, newY, _startPosition.z);

        transform.Rotate(Vector3.up * _rotationSpeed * Time.deltaTime);
    }
}
