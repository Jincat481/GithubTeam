using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 메뉴 창 GameObject
    public Button continueButton; // Continue 버튼
    public Button mainMenuButton; // Main Menu 버튼

    private bool isPaused = false;

    private void Start()
    {
        // Continue 버튼의 클릭 이벤트 연결
        continueButton.onClick.AddListener(ContinueGame);
        // Main Menu 버튼의 클릭 이벤트 연결
        mainMenuButton.onClick.AddListener(LoadMainMenu);

        // 메뉴 창 비활성화
        pauseMenuUI.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // 메뉴 창 활성화/비활성화
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        // 메뉴 창의 활성화 상태 토글
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);

        // 게임 시간 정지/재개
        Time.timeScale = (pauseMenuUI.activeSelf) ? 0f : 1f;

        // 메뉴 창 상태 업데이트
        isPaused = pauseMenuUI.activeSelf;
    }

    public void ContinueGame()
    {
        // 메뉴 창 비활성화
        TogglePauseMenu();
    }

    public void LoadMainMenu()
    {
        // 메뉴 창 비활성화
        TogglePauseMenu();
        // 빌드 번호 0번 씬 로드
        SceneManager.LoadScene(0);
    }
}
