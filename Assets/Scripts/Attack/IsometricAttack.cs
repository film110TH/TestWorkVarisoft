using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class IsometricAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform target;
    [SerializeField] Vector2 vector2;
    [SerializeField] IsometricCharacterRenderer isometricCharacterRenderer;

    public readonly Dictionary<int, Vector2> AttackPoin = new Dictionary<int, Vector2>()
    {
        {0, new Vector2(0f,1f) },
        {1, new Vector2(-1f,1f) },
        {2, new Vector2(-1f,0f) },
        {3, new Vector2(-1f,-1f) },
        {4, new Vector2(0f,-1f) },
        {5, new Vector2(1f,-1f) },
        {6, new Vector2(1f,0f) },
        {7, new Vector2(1f,1f) },
    };

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject _bullet = Instantiate(bullet,this.transform.position,Quaternion.identity);
            Rigidbody2D rb2d = _bullet.GetComponent<Rigidbody2D>();
            rb2d.velocity = AttackPoin[isometricCharacterRenderer.lastDirection] *  10;

            Destroy(_bullet, 3f);
        }
    }
}
