using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // �޴� â GameObject
    public Button continueButton; // Continue ��ư
    public Button mainMenuButton; // Main Menu ��ư

    private bool isPaused = false;

    private void Start()
    {
        // Continue ��ư�� Ŭ�� �̺�Ʈ ����
        continueButton.onClick.AddListener(ContinueGame);
        // Main Menu ��ư�� Ŭ�� �̺�Ʈ ����
        mainMenuButton.onClick.AddListener(LoadMainMenu);

        // �޴� â ��Ȱ��ȭ
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // �޴� â Ȱ��ȭ/��Ȱ��ȭ
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        // �޴� â�� Ȱ��ȭ ���� ���
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        // ���� �ð� ����/�簳
        Time.timeScale = (pauseMenuUI.activeSelf) ? 0f : 1f;

        // �޴� â ���� ������Ʈ
        isPaused = pauseMenuUI.activeSelf;
    }

    public void ContinueGame()
    {
        // �޴� â ��Ȱ��ȭ
        TogglePauseMenu();
    }

    public void LoadMainMenu()
    {
        // �޴� â ��Ȱ��ȭ
        TogglePauseMenu();
        // ���� ��ȣ 0�� �� �ε�
        SceneManager.LoadScene(0);
    }
}
