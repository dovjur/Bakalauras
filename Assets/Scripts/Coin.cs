using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public delegate void CoinCollected();
    public static event CoinCollected onCoinCollected;

    [SerializeField]
    private GameObject prefab;

    private Rigidbody2D rb;
    private Transform target;
    private float delay = 1;
    private float pastTime = 0;
    private Vector3 offset;

    private void Awake()
    {
        offset = new Vector3 (Random.Range(-2,2), Random.Range(-2,2), transform.position.z);
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        target = RunManager.Player.transform;
    }

    private void Update()
    {
        if (delay >= pastTime)
        {
            pastTime += Time.deltaTime;
            rb.MovePosition(transform.position + offset * Time.deltaTime);
        }

        if (RunManager.Player.isMagnetOn)
        {
            Vector3 targetPos = Vector3.MoveTowards(transform.position, target.position, 20 * Time.deltaTime);
            rb.MovePosition(targetPos);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            RunData.Instance.AddCoin();
            onCoinCollected();
            Destroy(gameObject);
        }
    }

    public void SpawnCoins(int count, Transform transform)
    {
        for (int i = 0; i < count; i++)
        {
            Instantiate(prefab, transform.position, Quaternion.identity);
        }    
    }
}
