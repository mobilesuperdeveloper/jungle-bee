using UnityEngine;
using System.Collections;

public class moveObject : MonoBehaviour {

    public int m_Speed = 5;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 position = transform.position;
        position = new Vector2(position.x - m_Speed * Time.deltaTime * 10, position.y);

        transform.position = position;

        if (!IsinScreenArea())
        {
            Destroy(gameObject);
        }
	}

    bool IsinScreenArea()
    {
        float width = 1136 / 2;
        //float height = 640 / 2;
        if (transform.localPosition.x <= -width)
        {
            return false;
        }
        if (transform.localPosition.y < -270)
        {
            return false;
        }
        return true;
    }
}
