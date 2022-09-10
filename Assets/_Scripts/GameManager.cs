using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public ItemManager ItemManager;
    public GameObject QuitPanel;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
        ItemManager = GetComponent<ItemManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleQuitPanel();
        }
    }

    public void ToggleQuitPanel()
    {
        if (QuitPanel != null && !QuitPanel.activeSelf)
        {
            QuitPanel.SetActive(true);
            Time.timeScale = 0;
        }
        else { QuitPanel.SetActive(false);Time.timeScale = 1;}
    }

}
