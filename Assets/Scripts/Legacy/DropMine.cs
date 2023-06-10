using UnityEngine;
using UnityEngine.UI;

public class DropMine : MonoBehaviour
{
    public Text mineCountText;
    public GameObject noMoreMines;
    public GameObject mine;
    public GameObject pointer;
    public Vector3 offset;
    public int mineCount = 4;

    private void Update()
    {
        mineCountText.text = mineCount.ToString();
        if (Input.GetKeyDown(KeyCode.Space)) MineDrop();
    }

    public void MineDrop()
    {
        Destroy(pointer);

        PlayerControll player = GetComponent<PlayerControll>();
        player.notUIhit = true;

        if (mineCount > 0)
        {
            Instantiate(mine, transform.position + offset, transform.rotation);
            mineCount -= 1;
        }
        else if (mineCount <= 0)
        {
            Instantiate(noMoreMines, transform.position, Quaternion.identity);
            return;
        }

    }
}
