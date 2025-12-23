using UnityEngine;

public class Fishes : MonoBehaviour
{
    public float hiz = 2f;             
    public float solSinir = -8f;
    public float sagSinir = 8f;
    public bool sagaGidiyor = true;   

    private bool isCaught = false; // balık yakalandı mı?

    void Update()
    {
        if (isCaught) return; // yakalandıysa hareket etme!

        if (sagaGidiyor)
        {
            transform.Translate(Vector2.right * hiz * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * hiz * Time.deltaTime);
        }

        if (transform.position.x >= sagSinir)
        {
            sagaGidiyor = false;
            Flip(); 
        }
        else if (transform.position.x <= solSinir)
        {
            sagaGidiyor = true;
            Flip(); 
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    // Hook tarafından çağrılacak fonksiyon
    public void StopMoving()
    {
        isCaught = true;
    }
}
