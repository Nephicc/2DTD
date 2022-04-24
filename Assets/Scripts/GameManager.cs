using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

    public TowerBtn ClickedBtn { get; private set; }

    private int gold;

    public int Gold {
        get { return gold; } 
        
        set { 
            gold = value;
            goldText.text = "Gold: " + gold;
        } 
    }

    [SerializeField]
    private Text goldText;

    [SerializeField]
    private GameObject enemie;

    // Start is called before the first frame update
    void Start()
    {
        Gold = 10000;
        enemie = Instantiate(enemie, new Vector3(-8, 4.5f, -2), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (ClickedBtn != null && ClickedBtn.getCursor() != null)
        {
            Cursor.SetCursor(ClickedBtn.getCursor(), Vector2.zero, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
        }

        HandleEscape();
        HandleRClick();
        TestEnemy();
    }

    

    public void PickTower(TowerBtn TowerBtn)
    {
        if(Gold >= TowerBtn.TowerCost)
        {
            ClickedBtn = TowerBtn;
        }
        
    }

    public void BuyTower()
    {
        Gold -= ClickedBtn.TowerCost;
        Debug.Log(Gold);
        ClickedBtn = null;
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(ClickedBtn != null)
            {
                ClickedBtn = null;
            }
        }
    }
    private void HandleRClick()
    {
        if (Input.GetMouseButtonDown(2))
        {
            if (ClickedBtn != null)
            {
                ClickedBtn = null;
            }
        }
    }
    
    private void TestEnemy()
    {
        enemie.transform.Translate(new Vector3(1*Time.deltaTime, 0, 0));
    }
}
