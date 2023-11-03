using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 direction;

	private readonly Vector3 bullet_explosion_area = new Vector2(0.75f, 0.05f);
    private const float speed = 8f;
	private const int tank_layer = 6, destructible_wall_layer = 8;

	private void Awake()
	{
		direction = transform.up;
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		GameObject collided_with = collision.collider.gameObject;
        switch (collided_with.layer) 
		{
			case tank_layer:

				collided_with.GetComponent<Tank>().Kill();

                break;
			case destructible_wall_layer:

				DestructibleWall wall = collided_with.GetComponentInParent<DestructibleWall>();

				Vector3 deleter_position =
					Mathf.Round(direction.x) == 0 ?
					new Vector3(transform.position.x, collided_with.transform.position.y) :
					new Vector3(collided_with.transform.position.x, transform.position.y);

				Collider2D[] to_delete = Physics2D.OverlapBoxAll(deleter_position,
                    bullet_explosion_area, direction.x*90);

				foreach(Collider2D t in to_delete)
				{
					if (t.gameObject.layer != destructible_wall_layer)
						continue;

					wall.Break_Wall_Piece(t.gameObject.GetComponent<SpriteRenderer>());
				}

                break;
		}
		Destroy(gameObject);
	}
}
