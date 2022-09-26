using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameHolder : MonoBehaviour
{
    public int mode;
    public BoxCollider2D col;

    private float Screen_unit;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 p1 = Camera.main.ScreenToWorldPoint(Vector3.zero);
        Vector3 p2 = Camera.main.ScreenToWorldPoint(Vector3.right);
        Screen_unit = Vector3.Distance(p1, p2);

        if (mode == 1)//Top
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
            col.size = new Vector2(Screen.width * Screen_unit * 1.6f, Screen.height * Screen_unit * 0.1f);
            col.offset = new Vector2(Screen.width * Screen_unit * 0.5f, Screen.height * Screen_unit * 0.05f);
            transform.position = pos;
        }
        if (mode == 2)//Bottom
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 10));
            col.size = new Vector2(Screen.width * Screen_unit * 1.6f, Screen.height * Screen_unit * 0.1f);
            col.offset = new Vector2(Screen.width * Screen_unit * 0.5f, -(Screen.height * Screen_unit * 0.05f));
            transform.position = pos;
        }
        if (mode == 3)//Right
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 10));
            col.size = new Vector2(Screen.height * Screen_unit * 0.1f, Screen.height * Screen_unit * 1.4f);
            col.offset = new Vector2(-(Screen.height * Screen_unit * 0.05f), -(Screen.height * Screen_unit * 0.5f));
            transform.position = pos;
        }
        if (mode == 4)//Left
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 10));
            col.size = new Vector2(Screen.height * Screen_unit * 0.1f, Screen.height * Screen_unit * 1.4f);
            col.offset = new Vector2(Screen.height * Screen_unit * 0.05f, -(Screen.height * Screen_unit * 0.5f));
            transform.position = pos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
}
