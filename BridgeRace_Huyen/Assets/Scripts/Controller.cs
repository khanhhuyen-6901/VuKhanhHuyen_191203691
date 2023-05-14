using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    [SerializeField] protected Animator anim;
    [SerializeField] protected GameObject brickPoint;
    [SerializeField] protected Rigidbody rb;
    [SerializeField] public Material characterMaterial;
    public StateManager currentStage;
    public Stack<GameObject> bricks = new Stack<GameObject>();
    protected string currentAnim;
    protected float speed=5f;
    protected float rotateSpeed=5f;

    [SerializeField] LayerMask stageLayer;
    [SerializeField] LayerMask bridgeLayer;
    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }
    private void OnInit()
    {
        ChangeAnim("Idle");
        gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material = characterMaterial;
      
    }

    protected virtual void Update()
    {

        RaycastHit hit_stage;

        if (Physics.Raycast(transform.position + Vector3.forward + Vector3.up, Vector3.down, out hit_stage, 5f, stageLayer))
        {
            {
                if (currentStage != hit_stage.collider.gameObject.GetComponent<StateManager>())
                {
                    if (currentStage != null)
                    {
                        currentStage.RemoveCharacter(characterMaterial, bricks);
                    }
                    currentStage = hit_stage.collider.gameObject.GetComponent<StateManager>();
                    currentStage.Character(characterMaterial);

                }
            }

        }

        RaycastHit hit_bridge;

        if (Physics.Raycast(transform.position + Vector3.forward + Vector3.up, Vector3.down, out hit_bridge, 5f, bridgeLayer))
        {
            if (hit_bridge.collider.gameObject.GetComponent<MeshRenderer>().material.color != characterMaterial.color)
            {
                if (bricks.Count > 0)
                {
                    BuildBridge(hit_bridge.collider.gameObject);

                }
                else
                {
                    Block(hit_bridge.collider.gameObject);
                }
            }
            else
            {
                UnBlock(hit_bridge.collider.gameObject);
            }
        }

    }
    private void AddBrick(GameObject brick)
    {
        currentStage.listPosition.Add(brick.transform.position);
        brick.transform.position = brickPoint.transform.position + new Vector3(0f, bricks.Count * 0.32f, 0f);
        brick.transform.rotation = brickPoint.transform.rotation;
        brick.transform.parent = brickPoint.transform;
        bricks.Push(brick);

    }

    private void Clear()
    {
        foreach (GameObject obj in bricks)
        {
            currentStage.RemoveBrick(obj);
            //Destroy(obj);
        }
        bricks.Clear();
    }
    private void BuildBridge(GameObject obj)
    {
        obj.GetComponent<MeshRenderer>().material.color = characterMaterial.color;
        obj.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        obj.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
        currentStage.RemoveBrick(bricks.Peek());
        bricks.Pop();
        if (currentStage != null)
        {
            currentStage.ReSpawnBrick();
        }
    }
    private void Block(GameObject obj)
    {
        obj.GetComponent<BoxCollider>().size = new Vector3(1f, 8, 1f);
        obj.GetComponent<BoxCollider>().center = new Vector3(0f, 4f, 0f);
        StartCoroutine(ResetBox(obj));
    }
    private void UnBlock(GameObject obj)
    {
        obj.GetComponent<BoxCollider>().size = new Vector3(1f, 1f, 1f);
        obj.GetComponent<BoxCollider>().center = new Vector3(0f, 0f, 0f);
    }

    IEnumerator ResetBox(GameObject obj)
    {
        yield return new WaitForSeconds(1f);

        UnBlock(obj);
    }

    protected void ChangeAnim(string animName)
    {
        // ham thay doi trang thai cua anim
        if (currentAnim != animName)
        {
            anim.ResetTrigger(currentAnim);
            currentAnim = animName;
            anim.SetTrigger(currentAnim);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Brick" && other.gameObject.GetComponent<MeshRenderer>().material.color == characterMaterial.color && !bricks.Contains(other.gameObject))
        {
            AddBrick(other.gameObject);
        }
        if (other.tag == "End")
        {
            Debug.Log("finish");
            Clear();
            gameObject.transform.rotation = Quaternion.Euler(0, 180, 0);
            ChangeAnim("Dance");
            if (gameObject.tag == "Player")
            {
                GameManager.instance.isFinish = true;
                UIManager.instance.Win();
               
            }
            if (gameObject.tag == "Enemy")
            {
                this.GetComponent<Enemy>().enemy.isStopped = true;
                this.GetComponent<Enemy>().enemy.enabled = false;
                this.GetComponent<Rigidbody>().Sleep();
                UIManager.instance.Lose();
                
            }
        }
    }
}