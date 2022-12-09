using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{

    private float minSpeed = 12;
    private float maxSpeed = 15;
    private float maxTorque = 4;
    private float xRange = 2.6f;
    private float ySpawnPos = -5;
    public ParticleSystem explosionParticle;

    private Rigidbody2D targetRb;
    private GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(),ForceMode2D.Impulse);
        targetRb.AddTorque(RandomTorque(),(ForceMode2D)ForceMode.Impulse);
        transform.position = RandomSpawn();


    }


    //private void Update()
    //{
    //    if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
    //    {
    //        Destroy(this.gameObject);
    //    }
    //}

    private void OnMouseDown()
    {
        if(gameManager.isGameOn)
        {
            Destroy(this.gameObject);

            Instantiate(explosionParticle, this.gameObject.transform.position, explosionParticle.transform.rotation);

            if (gameManager.blowfruits && this.gameObject.CompareTag("Fruits"))
                gameManager.UpdateScore(5);
            else if (!gameManager.blowfruits && this.gameObject.CompareTag("Vegatables"))
                gameManager.UpdateScore(5);
            else
                gameManager.UpdateScore(-5);
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    } 

    Vector3 RandomSpawn()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }
}
