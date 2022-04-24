using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    //The tiles we can use
    [SerializeField]
    private GameObject[] tilePrefabs;

    [SerializeField]
    private CameraMovment cameraMovment;

    private Point spawn, end;

    [SerializeField]
    private GameObject spawnPre, endPre;

    public Dictionary<Point, TileScript> Tiles { get; set; }

    [SerializeField]
    private Transform map;


    public float TileSize
    {
        get { return tilePrefabs[0].GetComponent<SpriteRenderer>().sprite.bounds.size.x; }
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateLevel();
    }

    // Update is called once per frame
    void Update()
    {
        //cameraMovment.MoveCamera("d");
    }

    private void CreateLevel()
    {
        Tiles = new Dictionary<Point, TileScript>();

        //Get map data
        string[] mapData = ReadLevelFile();

        //Get map length/height
        int mapX = mapData[0].ToCharArray().Length;
        int mapY = mapData.Length;

        //Last tile position
        Vector3 maxTile = Vector3.zero;

        //Get top left world cordiates
        Vector3 worldStart = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height));
        //Vector3 worldStart = Camera.main.ViewportToWorldPoint(new Vector3(0, 1));

        //Log values to see
        //Debug.Log(worldStart);
        //Debug.Log(Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2)));
        //Debug.Log(Screen.width + " " + Screen.height);

        //Place all tiles
        for (int y = 0; y < mapY; y++)
        {
            char[] row = mapData[y].ToCharArray();
            for (int x = 0; x < mapX; x++)
            {
                string tile = row[x].ToString();
                PlaceTile(tile,x, y,worldStart);
            }
        }

        maxTile = Tiles[new Point(mapX - 1, mapY - 1)].transform.position;

        cameraMovment.SetLimit(new Vector3(maxTile.x + TileSize, maxTile.y - TileSize));

        SpawnPortals();
    }

    /**
     * Par x = Amount of tiles placed to the left of this
     * Par y = Amount of tiles placed above this
     * Par worldStart = position to start placing the first tile
     */
    private void PlaceTile(string tileType, int x, int y, Vector3 worldStart)
    {
        int tileIndex = int.Parse(tileType);

        TileScript newTile = Instantiate(tilePrefabs[tileIndex]).GetComponent<TileScript>();
        
        newTile.Setup(new Point(x, y), new Vector3(worldStart.x + (TileSize * x), worldStart.y - (TileSize * y), 0), map);

        
    }

    private string[] ReadLevelFile()
    {
        //read file
        TextAsset file = Resources.Load("map1") as TextAsset;

        //remove newline
        string data = file.text.Replace(Environment.NewLine, string.Empty);

        //return as string arrary split by value
        return data.Split('-');
    }

    private void SpawnPortals()
    {
        spawn = new Point(0, 0);

        GameObject a = Instantiate(spawnPre, Tiles[spawn].transform.position, Quaternion.identity);
        
        a.transform.position = new Vector3(a.transform.position.x, a.transform.position.y, -1);

        end = new Point(15, 3);

        GameObject b = Instantiate(endPre, Tiles[end].transform.position, Quaternion.identity);

        b.transform.position = new Vector3(b.transform.position.x, b.transform.position.y, -1);
    }
}
