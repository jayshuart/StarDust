using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStar : MonoBehaviour
{
    [SerializeField] private TrailRenderer trail;
    [SerializeField] private SpriteRenderer sprite;

    [SerializeField] private Rigidbody2D rigidbody;
    private Vector3 baseSpriteScale;
    private float speed;

    private Vector3 lastPos;
    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        baseSpriteScale = sprite.gameObject.transform.localScale;
        lastPos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        moveToPointer();
        constantSpin();
        spinForMovement();
    }

    void OnCollisionEnter2D(Collision2D col)
    { Debug.Log("hit");
        Vector3 dist = col.gameObject.transform.position - this.transform.position;
        dist.Normalize();
        dist.Scale(new Vector3(-10f, -10f, 1f));

        //this.transform.position = Vector3.Lerp(this.transform.position, dist, speed * .5f * Time.deltaTime);

        rigidbody.AddForce(new Vector2(dist.x, dist.y));
    }

    private void spinForMovement()
    {
        //find dist between last point and this
        Vector3 newPos = this.sprite.gameObject.transform.position;
        float dist = Vector3.Distance(lastPos, newPos);

        //update last
        lastPos = newPos;

        //rotate based on deltaPos
        Vector3 newRot = Vector3.zero;
        newRot.z += (5000f * dist) * Time.deltaTime;
        this.sprite.gameObject.transform.Rotate( newRot);
    }

    private void constantSpin(){
        Vector3 newRot = Vector3.zero;
        newRot.z += 100f * Time.deltaTime;
        this.sprite.gameObject.transform.Rotate( newRot);
    }

    private void moveToPointer(){
        if (!Input.GetButton("Fire1")) { return; }

        Vector3 v3 = Input.mousePosition;
        v3.z = 10.0f;
        v3 = Camera.main.ScreenToWorldPoint(v3);
        this.transform.position = Vector3.Lerp(this.transform.position, v3, speed * Time.deltaTime);
    }
}
