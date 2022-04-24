using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBtn : MonoBehaviour
{

    [SerializeField]
    private GameObject towerPre;

    public GameObject TowerPre { get => towerPre; }

    [SerializeField]
    private int towerCost;

    [SerializeField]
    private Texture2D cursor;

    [SerializeField]
    private string towerName;

    [SerializeField]
    private Text textField;


    public int TowerCost => towerCost;
    public string TowerName { get => towerName; }

    // Start is called before the first frame update
    void Start()
    {
        textField.text = "" + towerCost;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Texture2D getCursor()
    {
        return cursor;
    }
}
