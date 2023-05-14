using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    public GameDefine.ColorType colorType;
    [SerializeField] public List<Material> colorMaterials;
    [SerializeField] public MeshRenderer meshRenderer;

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public void OnInit()
    {
        colorType = (GameDefine.ColorType)Random.Range(0, 4);
        SetColor((int)colorType);
    }

    public void SetColor(int color)
    {
        meshRenderer.material = colorMaterials[color];
    }

    // Update is called once per frame
    void Update()
    {

    }
}
