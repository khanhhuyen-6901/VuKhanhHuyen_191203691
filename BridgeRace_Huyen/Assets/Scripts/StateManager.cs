using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{

    int numberBricks = 15;
    [SerializeField] int col;
    [SerializeField] int row;
    [SerializeField] Vector2 offset;


    public List<Vector3> listPosition = new List<Vector3>();
    private List<Material> listMaterials = new List<Material>();
    public Dictionary<Color, List<GameObject>> listBrick = new Dictionary<Color, List<GameObject>>();

    // Start is called before the first frame update
    private void Start()
    {
        Position();
    }
    public void Character(Material mat)
    {
        if (!listMaterials.Contains(mat))
        {
            listMaterials.Add(mat);
            listBrick[mat.color] = new List<GameObject>();
            SpawnBrick(mat.color);

        }
    }
    public void RemoveCharacter(Material mat, Stack<GameObject> bricks)
    {
        foreach (GameObject obj in listBrick[mat.color])
        {
            if (!bricks.Contains(obj))
            {
                BrickPooling.instance.Return(obj);
            }
        }
        listMaterials.Remove(mat);
        listBrick.Remove(mat.color);
    }
    public void Position()
    {
        Vector2 startSpawnPos = -new Vector2((col - 1) * offset.x / 2 - transform.position.x, (row - 1) * offset.y / 2 - transform.position.z);
        for (int i = 0; i < col; i++)
        {
            for (int j = 0; j < row; j++)
            {
                Vector3 position = new Vector3(startSpawnPos.x + i * offset.x, 1f, startSpawnPos.y + j * offset.y);

                listPosition.Add(position);
            }
        }
    }
    public void SpawnBrick(Color color)
    {
        for (int i = 0; i < numberBricks; i++)
        {
            int randomPosition = Random.Range(0, listPosition.Count);

            GameObject obj = BrickPooling.instance.Spawn();
            obj.GetComponent<MeshRenderer>().material.color = color;
            obj.SetActive(true);
            obj.transform.SetPositionAndRotation(listPosition[randomPosition], Quaternion.identity);
            listBrick[color].Add(obj);
            listPosition.RemoveAt(randomPosition);
        }

    }
    public void ReSpawnBrick()
    {
        int randomColor = Random.Range(0, listMaterials.Count);
        int rd = Random.Range(0, listPosition.Count);
        Vector3 pos = listPosition[rd];
        listPosition.RemoveAt(rd);
        GameObject obj = BrickPooling.instance.Spawn();
        obj.GetComponent<MeshRenderer>().material.color = listMaterials[randomColor].color;
        obj.SetActive(true);
        obj.transform.SetPositionAndRotation(pos, Quaternion.identity);
        listBrick[listMaterials[randomColor].color].Add(obj);
    }
    public void RemoveBrick(GameObject obj)
    {
        foreach (Material mat in listMaterials)
        {
            if (obj.GetComponent<MeshRenderer>().material.color == mat.color)
            {
                listBrick[mat.color].Remove(obj);
            }
        }
        BrickPooling.instance.Return(obj);
    }

}
