using System.Collections;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _spawnObject;
    [SerializeField] private float _spawnDelay;
    [SerializeField] private int _spawnedCyclesValue;
    [SerializeField] private Vector3[] _spawnCoordinates;


    private WaitForSeconds _waitForSeconds;
    private System.Random _random;

    private void Start()
    {
        _random = new System.Random();
        _waitForSeconds = new WaitForSeconds(_spawnDelay);
        StartCoroutine(SpawnWithDelay());
    }

    private IEnumerator SpawnWithDelay()
    {
        for (int i = 0; i < _spawnedCyclesValue; i++)
        {
            Instantiate(_spawnObject,
                _spawnCoordinates[_random.Next(_spawnCoordinates.Length)],
                Quaternion.identity);
            yield return _waitForSeconds;
        }
    }
}