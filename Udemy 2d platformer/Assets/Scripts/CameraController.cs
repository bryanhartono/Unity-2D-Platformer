using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;

    public float minHeight;
    public float maxHeight;
    public bool stopFollow;

    public Transform target;
    public Transform farBackground;
    public Transform middleBackground;

    private Vector2 lastPos;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        lastPos = transform.position;
    }

    void Update()
    {
        if(!stopFollow)
        {
            // set camera to follow target -> player
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            // set minimum and maximum y values for camera
            float clampedY = Mathf.Clamp(transform.position.y, minHeight, maxHeight);
            transform.position = new Vector3(target.position.x, clampedY, transform.position.z);

            // moves background by parallax ---------------------------------------------
            float amountX = transform.position.x - lastPos.x;
            float amountY = transform.position.y - lastPos.y;
            Vector2 amountToMove = new Vector2(amountX, amountY);

            farBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f);
            middleBackground.position += new Vector3(amountToMove.x, amountToMove.y, 0f) * 0.5f;
            // --------------------------------------------------------------------------

            lastPos = transform.position;
        }
    }
}
