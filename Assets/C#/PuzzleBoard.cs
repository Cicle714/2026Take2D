using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{

    int x = 6;
    int y = 12;

    int[,] BoardSize;

    [SerializeField]
    GameObject StoneObject;

    public GameObject[,] PuzzleStones;


    [SerializeField]
    private int StoneSize = 1;


    void Start()
    {
        BoardSize = new int[x, y / 2];
        PuzzleStones = new GameObject[x, y];

        PuzzleStoneSpawner();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void PuzzleStoneSpawner()
    {
        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {
                GameObject CloneStone = Instantiate(StoneObject, transform.position + new Vector3(j * StoneSize, i * StoneSize), Quaternion.identity);
                CloneStone.transform.parent = transform;
                CloneStone.GetComponent<PuzzleStone>().PosY = i;
                CloneStone.GetComponent<PuzzleStone>().PosX = j;
                CloneStone.GetComponent<PuzzleStone>().RandomStone();

                PuzzleStones[j,i] = CloneStone;

            }
        }
    }

}
