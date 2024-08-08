using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // 시작 시 호출되는 메서드
    void Start()
    {
        // 변수나 컴포넌트를 초기화합니다.
    }

    // 매 프레임마다 호출되는 메서드
    void Update()
    {
        // 업데이트 로직을 수행합니다.
    }

    // 새 게임 시작 버튼 클릭 시 호출되는 메서드
    public void OnClickNewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // 불러오기 버튼 클릭 시 호출되는 메서드
    public void OnClickLoad()
    {
        Debug.Log("게임을 불러오는 중...");
        // 저장된 게임 데이터를 불러오는 코드를 추가합니다.
    }

    // 옵션 버튼 클릭 시 호출되는 메서드
    public void OnClickOption()
    {
        Debug.Log("옵션 메뉴를 열고 있습니다...");
        // 옵션 메뉴 씬이나 UI를 여는 코드를 추가합니다.
    }

    // 게임 종료 버튼 클릭 시 호출되는 메서드
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        // Unity 에디터에서 실행 중인 경우, 게임을 중지합니다.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // 독립형 빌드에서 실행 중인 경우, 애플리케이션을 종료합니다.
        Application.Quit();
#endif
    }

}
