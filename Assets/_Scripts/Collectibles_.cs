using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Scripts
{
    public class Collectibles_ : MonoBehaviour
    {
        //player walks in near to the collectibles
        //! pops up to notify player about it
        //when pressed E and in range player can pick that item up
        //when taken to players inventory delete from environment
        
        [FormerlySerializedAs("collectableTypes")] public CollectableType collectableType;
        
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private Text dialogueText;
        [SerializeField] private string dialogue;
        public Sprite iconSprite;
        

        public bool bPlayerInRange;
        private void Start()
        {
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E) && bPlayerInRange)
            {
                print("E is Working");
                //PlayerController.Instance.rocksCollectedCount++;
                PlayerController.Instance.Inventory.AddItems(this);
                Debug.Log(PlayerController.Instance);
                Destroy(this.gameObject);  
            }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController playerController)) 
            {
                bPlayerInRange = true;
                print("in range");
                playerController._image.gameObject.SetActive(true);
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController playerController)) 
            {
                playerController._image.gameObject.SetActive(false);
                bPlayerInRange = false;
                print("not in range");
            }
        }
    }
}

public enum CollectableType
{
    None,Rocks
}
