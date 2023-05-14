/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject brick;
    public GameObject player;
    public float spawnTime;
    float m_spawnTime;
    int coutn = 0;
    private void Start()
    {
        m_spawnTime = 0;
    }
    private void Update()
    {
        m_spawnTime -= Time.deltaTime;
        if (m_spawnTime <= 0&& coutn<15)
        {
            SpawnBrick();
            m_spawnTime = spawnTime;
            coutn++;
        }
    }
    public void SpawnBrick()
    {
        //int row = Random(player.transform.position.x - 8, player.transform.position.x + 8);
        Vector3 point = new Vector3(Random.Range(player.transform.position.x - 8, player.transform.position.x + 8), 1, Random.Range(player.transform.position.z - 8, player.transform.position.z + 8));
        if (brick)
        {
           GameObject brickPos= Instantiate(brick, point, Quaternion.identity);
            brickPos.transform.localScale=new Vector3(0.8f, 0.3f,0.3f);
        }
    }
}
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public bool isStart;
    public bool isFinish;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    private void OnInit()
    {
        isStart = true;
        isFinish = false;
        
    }

    

    
    // Update is called once per frame
    void Update()
    {

    }
}
