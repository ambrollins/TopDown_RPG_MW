using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts
{
    public class Collectibles_ : MonoBehaviour
    {
        //player walks in near to the collectibles
        //! pops up to notify player about it
        //when pressed E and in range player can pick that item up
        //when taken to players inventory delete from environment
        [SerializeField] private GameObject dialogueBox;
        [SerializeField] private Text dialogueText;
        [SerializeField] private string dialogue;
        
        public TextMeshProUGUI _textMeshPro;

        public bool bPlayerInRange;
        private void Start()
        {
            PlayerController playerController = gameObject.GetComponent<PlayerController>();
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PlayerController.Instance.rocksCollectedCount++;
                Debug.Log(PlayerController.Instance);
                Destroy(this.gameObject);
            }
            // if (!Input.GetKeyDown(KeyCode.E) || !bPlayerInRange) return;
            // if (dialogueBox.activeInHierarchy)
            // {
            //     dialogueBox.SetActive(false);
            // }
            // else
            // {
            //     dialogueBox.SetActive(true);
            //     dialogueText.text = dialogue;
            // }
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.TryGetComponent(out PlayerController playerController)) 
            {
                bPlayerInRange = true;
                print("in range");
                _textMeshPro.gameObject.SetActive(true);
               
            }
        }
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.TryGetComponent(out PlayerController playerController)) 
            {
                _textMeshPro.gameObject.SetActive(false);
                bPlayerInRange = false;
                dialogueBox.SetActive(false);
                print("not in range");
            }
        }
    }
}
