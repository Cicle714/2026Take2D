using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
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

    private RectTransform rect;

    private Vector3 StartPos;
    private Vector3 StartMovePos;
    private Vector3 EndPos;


    float MoveTime = 0.1f;

    bool Move;

    public bool Hold;

    private Vector2 TouchPos;

    void Start()
    {
        StartPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        EndPos = new Vector3(PosX * board.StoneSize, PosY * board.StoneSize) + board.transform.position;
        if(transform.position != EndPos && !Move && !Hold){
            Move = true;
            StartCoroutine(StoneMove());
        }
        if (PosY >= 6)
        {
            MySprite.enabled = false;
        }
        else
        {
            MySprite.enabled = true;
        }

        if (Hold)
        {
            TouchPos = Touchscreen.current.position.ReadValue();
            transform.position = TouchPos;
            if (TouchPos.x - EndPos.x > board.StoneSize / 2 && PosX + 1 <= 5)
            {
                GameObject tmp = board.PuzzleStones[PosX, PosY];
                board.PuzzleStones[PosX, PosY] = board.PuzzleStones[PosX + 1, PosY];
                board.PuzzleStones[PosX + 1, PosY] = tmp;
                board.PuzzleStones[PosX, PosY].GetComponent<PuzzleStone>().PosX--;
                PosX++;
            }
            if (TouchPos.x - EndPos.x < -board.StoneSize / 2 && PosX - 1 >= 0)
            {
                GameObject tmp = board.PuzzleStones[PosX, PosY];
                board.PuzzleStones[PosX, PosY] = board.PuzzleStones[PosX - 1, PosY];
                board.PuzzleStones[PosX - 1, PosY] = tmp;
                board.PuzzleStones[PosX, PosY].GetComponent<PuzzleStone>().PosX++;
                PosX--;
            }
            if (TouchPos.y - EndPos.y> board.StoneSize / 2 && PosY + 1 <= 5)
            {
                GameObject tmp = board.PuzzleStones[PosX, PosY];
                board.PuzzleStones[PosX, PosY] = board.PuzzleStones[PosX, PosY + 1];
                board.PuzzleStones[PosX, PosY + 1] = tmp;
                board.PuzzleStones[PosX, PosY].GetComponent<PuzzleStone>().PosY--;
                PosY++;
            }
            if (TouchPos.y - EndPos.y < -board.StoneSize / 2 && PosY - 1 >= 0)
            {
                GameObject tmp = board.PuzzleStones[PosX, PosY];
                board.PuzzleStones[PosX, PosY] = board.PuzzleStones[PosX, PosY - 1];
                board.PuzzleStones[PosX, PosY - 1] = tmp;
                board.PuzzleStones[PosX, PosY].GetComponent<PuzzleStone>().PosY++;
                PosY--;
            }
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
        if (PosX != 0)
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


    IEnumerator StoneMove()
    {
        StartMovePos = transform.position;
        float MoveCount = 0;
        while (MoveCount < MoveTime)
        {
            MoveCount += Time.deltaTime;
            if(MoveCount > MoveTime)
            {
                MoveCount = MoveTime;
            }
            transform.position = Vector3.Lerp(StartMovePos,EndPos,MoveCount/MoveTime);
            yield return null;
        }
        Move = false;
    }

}
