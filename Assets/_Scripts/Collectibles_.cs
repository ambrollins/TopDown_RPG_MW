using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class Collectibles_ : MonoBehaviour
    {
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private Text dialogueText;
        [SerializeField] private string dialogue;

        public bool bPlayerInRange;

        private void Update()
        {
            if (!Input.GetKeyDown(KeyCode.E) || !bPlayerInRange) return;
            if (dialogueBox.activeInHierarchy)
            {
                dialogueBox.SetActive(false);
            }
            else
            {
                dialogueBox.SetActive(true);
                dialogueText.text = dialogue;
            }
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.CompareTag("Player"))
            {
                bPlayerInRange = true;
                print("in range");
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                bPlayerInRange = false;
                dialogueBox.SetActive(false);
                print("not in range");

            }
        }
    }
}
