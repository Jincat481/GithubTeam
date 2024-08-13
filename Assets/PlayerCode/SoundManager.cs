using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // 싱글톤 인스턴스

    public AudioSource soundSource; // 사운드를 재생할 AudioSource
    public AudioClip attackSound; // 공격 사운드 클립
    public AudioClip dashSound; // 대쉬 사운드 클립
    public AudioClip damageSound; // 데미지 사운드 클립
    public AudioClip deathSound; // 사망 사운드 클립

    private void Awake()
    {
        // 싱글톤 패턴 구현
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 사운드 매니저 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 사운드를 재생하는 메서드
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
                Debug.LogWarning("사운드 이름을 찾을 수 없습니다: " + soundName);
                break;
        }
    }

    // 사운드 재생 메서드
    private void PlaySound(AudioClip clip)
    {
        if (clip != null && soundSource != null)
        {
            soundSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("사운드 소스 또는 클립이 설정되지 않았습니다.");
        }
    }
}
