using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // �̱��� �ν��Ͻ�

    public AudioSource soundSource; // ���带 ����� AudioSource
    public AudioClip attackSound; // ���� ���� Ŭ��
    public AudioClip dashSound; // �뽬 ���� Ŭ��
    public AudioClip damageSound; // ������ ���� Ŭ��
    public AudioClip deathSound; // ��� ���� Ŭ��

    private void Awake()
    {
        // �̱��� ���� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� ���� �Ŵ��� ����
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ���带 ����ϴ� �޼���
    public void Play(string soundName)
    {
        switch (soundName)
        {
            case "Attack":
                PlaySound(attackSound);
                break;
            case "Dash":
                PlaySound(dashSound);
                break;
            case "Damage":
                PlaySound(damageSound);
                break;
            case "Death":
                PlaySound(deathSound);
                break;
            default:
                Debug.LogWarning("���� �̸��� ã�� �� �����ϴ�: " + soundName);
                break;
        }
    }

    // ���� ��� �޼���
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && soundSource != null)
        {
            soundSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("���� �ҽ� �Ǵ� Ŭ���� �������� �ʾҽ��ϴ�.");
        }
    }
}
