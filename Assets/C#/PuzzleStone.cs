using UnityEngine;
using UnityEngine.UI;

public class PuzzleStone : MonoBehaviour
{

    PuzzleBoard board;

    public enum PuzzleAttribute
    {
        Fire = 0,
        Watter = 1,
        Wood = 2,
        Right = 3,
        Dark = 4,
    }

    public int PosX;
    public int PosY;


    [SerializeField]
    public PuzzleAttribute MyAttribute;

    [SerializeField]
    private Sprite[] Sprites;

    private Image MySprite;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if(PosY >= 6)
        {
            MySprite.enabled = false;
        }
        else
        {
            MySprite.enabled = true;
        }
    }

    public void RandomStone()
    {
        board = GetComponentInParent<PuzzleBoard>();
        MySprite = GetComponent<Image>();
        int RandomNumber = Random.Range(0, 5);
        MyAttribute = (PuzzleAttribute)RandomNumber;

        if (PosY != 0)
        {
            PuzzleStone otherStone1 = board.PuzzleStones[PosX, PosY - 1].GetComponent<PuzzleStone>();


            while (MyAttribute == otherStone1.MyAttribute)
            {
                RandomNumber = Random.Range(0, 5);
                MyAttribute = (PuzzleAttribute)RandomNumber;
            }

        }
        if(PosX != 0)
        {

            PuzzleStone otherStone2 = board.PuzzleStones[PosX - 1, PosY].GetComponent<PuzzleStone>();
            while (MyAttribute == otherStone2.MyAttribute)
            {
                RandomNumber = Random.Range(0, 5);
                MyAttribute = (PuzzleAttribute)RandomNumber;
            }

        }
        MySprite.sprite = Sprites[RandomNumber];


    }

}
