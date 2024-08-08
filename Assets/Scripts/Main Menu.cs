using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // ���� �� ȣ��Ǵ� �޼���
    void Start()
    {
        // ������ ������Ʈ�� �ʱ�ȭ�մϴ�.
    }

    // �� �����Ӹ��� ȣ��Ǵ� �޼���
    void Update()
    {
        // ������Ʈ ������ �����մϴ�.
    }

    // �� ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnClickNewGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    // �ҷ����� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnClickLoad()
    {
        Debug.Log("������ �ҷ����� ��...");
        // ����� ���� �����͸� �ҷ����� �ڵ带 �߰��մϴ�.
    }

    // �ɼ� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnClickOption()
    {
        Debug.Log("�ɼ� �޴��� ���� �ֽ��ϴ�...");
        // �ɼ� �޴� ���̳� UI�� ���� �ڵ带 �߰��մϴ�.
    }

    // ���� ���� ��ư Ŭ�� �� ȣ��Ǵ� �޼���
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        // Unity �����Ϳ��� ���� ���� ���, ������ �����մϴ�.
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // ������ ���忡�� ���� ���� ���, ���ø����̼��� �����մϴ�.
        Application.Quit();
#endif
    }

}
