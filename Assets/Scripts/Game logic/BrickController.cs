using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for building the brick grid
/// </summary>
public class BrickController : MonoBehaviour
{
    public static BrickController Instance { get; private set; }

    [SerializeField]
    private float horizontalSpacing=0.2f, verticalSpacing=0.2f;

    public GameObject defaultBrickPrefab;
    public GameObject emeraldBrickPrefab;

    private List<GameObject> bricks = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    public int ResetLevel(int round)
    {
        ClearBricks();
        BuildLevel(round);
        return bricks.Count;
    }


    //Instantiates brick prefabs within a grid based on offsetts and prefab dimensions
    //Gameround defines numbers of rows
    public void BuildLevel(int round)
    {
        int scale = 2;
        int columnsPerSide = 4;

        for (int row = 0; row < round; row++)
        {
            GameObject tBrick = defaultBrickPrefab;
            if (row % 2 == 0)
            {
                tBrick = emeraldBrickPrefab;
            }

            var prefabLength = tBrick.GetComponent<SpriteRenderer>().bounds.size.x * scale;
            var prefabHeight = tBrick.GetComponent<SpriteRenderer>().bounds.size.y * scale;

            for (int column = -columnsPerSide; column < columnsPerSide; column++)
            {
                var offs = 0.24f * scale;
                if (column >= 0)
                {
                    offs = 0.5f * scale;
                }

                Vector2 spawnPos = new Vector2((column * (prefabLength + horizontalSpacing)) + offs,
                     (-row * (prefabHeight + verticalSpacing)));
                
                GameObject brick = Instantiate(tBrick, this.transform);
                brick.transform.localPosition = spawnPos;
                brick.transform.localScale = new Vector3(scale, scale, 1);
                bricks.Add(brick);
            }
        }
    }
    private void ClearBricks()
    {
        for(int i = 0; i < bricks.Count; i++)
        {
            Destroy(bricks[i]);
        }

        bricks.Clear();
    }
}