using UnityEngine;

public class Building : MonoBehaviour
{
    public SpriteRenderer renderer;
    public Vector2Int Size = Vector2Int.one;
    public  BoxCollider2D boxCollider;
    public bool Can_builded = false;
    

    public void SetColor(Color c)
    {
        renderer.color = c;
    }
   
    public void Awake()
    {
        renderer = this.GetComponentInChildren<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        SetColor(new Color(1, 0,0,0.5F));
        Can_builded = false;

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        SetColor(new Color(0, 1, 0, 0.5F));
        Can_builded = false;

    }
}