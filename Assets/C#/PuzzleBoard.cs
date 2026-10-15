using UnityEngine;
using UnityEngine.InputSystem;

public class PuzzleBoard : MonoBehaviour
{

    int x = 6;
    int y = 12;

    int[,] BoardSize;

    [SerializeField]
    GameObject StoneObject;

    public GameObject[,] PuzzleStones;

    GameObject HoldObject;


    [SerializeField]
    public int StoneSize = 1;

    Vector2 TouchPos;

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
    public void DragStart(InputAction.CallbackContext context)
    {
        Debug.Log(context.started +"start");
        Debug.Log(context.canceled +"cancel");

        if (context.started)
        {
            for (int i = 0; i < y / 2; i++)
            {
                for (int j = 0; j < x; j++)
                {
                        PuzzleStones[j, i].GetComponent<PuzzleStone>().Hold = false;
                }
            }
            TouchPos = Touchscreen.current.position.ReadValue();
            for (int i = 0; i < y / 2; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    if (Vector2.Distance(TouchPos, PuzzleStones[j, i].transform.position) < StoneSize / 2)
                    {
                        Debug.Log("‚Â‚©‚ß‚½");
                        PuzzleStones[j, i].GetComponent<PuzzleStone>().Hold = true;
                        HoldObject = PuzzleStones[j, i];
                        return;
                    }
                }
            }
        }

        if (context.canceled)
        {
            HoldObject.GetComponent<PuzzleStone>().Hold = false;
        }

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

                PuzzleStones[j, i] = CloneStone;

            }
        }
    }

}
