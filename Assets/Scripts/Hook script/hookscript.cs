using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookScript : MonoBehaviour
{
    [SerializeField]
    private Transform itemHolder;

    private bool itemAttached;

    private HookMovement hookMovement;
   // private PlayerAnimation playerAnim;
    private Collider2D hookCollider;

    void Awake()
    {
        hookMovement = GetComponentInParent<HookMovement>();
      //  playerAnim = GetComponentInParent<PlayerAnimation>();
        hookCollider = GetComponent<Collider2D>();
    }

    void OnTriggerEnter2D(Collider2D target)
    {
       // if (itemAttached)
       //     return; // Eğer zaten bir şey takılıysa, başka bir şeye dokunma
        
        if (target.CompareTag(Tags.SMALL_GOLD) || target.CompareTag(Tags.MIDDLE_GOLD) ||
            target.CompareTag(Tags.LARGE_GOLD) || target.CompareTag(Tags.LARGE_STONE) ||
            target.CompareTag(Tags.TREASURE))
        {
            itemAttached = true;

            // Hook'un collider'ını devre dışı bırak
            hookCollider.enabled = false;

            target.transform.parent = itemHolder;
            target.transform.position = itemHolder.position;
            var fish = target.GetComponent<Fishes>();
            if (fish != null)
                {
                fish.StopMoving();
                Debug.Log("Balık yakalandı");
                }   

            hookMovement.move_Speed = target.GetComponent<ItemScript>().hook_Speed;
            hookMovement.HookAttachedItem();

          //  playerAnim.PullingItemAnimation();

            if (target.CompareTag(Tags.SMALL_GOLD) || target.CompareTag(Tags.MIDDLE_GOLD) ||
                target.CompareTag(Tags.LARGE_GOLD) || target.CompareTag(Tags.TREASURE))
            {
                SoundManager.instance.HookGrab_Gold();
            }
            else if (target.CompareTag(Tags.LARGE_STONE))
            {
                SoundManager.instance.HookGrab_Stone();
            }

            SoundManager.instance.PullSound(true);
        }

        if (target.CompareTag(Tags.DELIVER_ITEM))
        {
            if (itemAttached)
            {
                itemAttached = false;

                Transform objChild = itemHolder.GetChild(0);
                objChild.parent = null;
                objChild.gameObject.SetActive(false);

             //   playerAnim.IdleAnimation();
                SoundManager.instance.PullSound(false);

                // Teslim edildiğinde collider'ı yeniden aktif et
                hookCollider.enabled = true;
            }
        }
    }
}
