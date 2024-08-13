한아전 팀프로젝트입니다.

유니티 사용하는 버전은 [2022.3.29f1](https://unity.com/kr/releases/editor/whats-new/2022.3.29#installs)입니다.

추가로 패키지 매니저에서 com.unity.render-pipelines.core 검색하셔서 다운 받으세요.

[스파인 다운로드](https://ko.esotericsoftware.com/spine-unity-download/)

다들 계획에 차질없게 진행하시길 바랍니다.

이슈에서 기획분들 원하는 구현, 수정 사항 부분이 있을 경우 수정요청하시면 되고 사용법은 [깃허브 사용법](https://www.youtube.com/watch?v=wBsSUBEUYV4&list=PLmUIPs_NMNt_TcEGa3qzlAmnPa4FlgPEI&index=6)
여기서 영상 보시고 사용하시면 됩니다.

기획, 프로그래밍, 그래픽분들 모두 프로젝트 누르셔서 일정 확인 및 작업 사항 체크 해주시길 바랍니다.

---
# 목차
1. 프로그래밍
  * [보스 구현 사항](#보스-구현-사항)
     * [2024/08/02](#20240802-현재-프로토타입-버전)
     * [2024/08/05](#20240805-현재-프로토타입-버전)
  * [몬스터 스포너 구현 사항](#몬스터-스포너-구현-사항)
  * [몬스터 구현 사항](#몬스터-구현-사항)
  * [플레이어 구현 사항](#플레이어-구현-사항)
2. [그래픽](#그래픽)
  * 
3. [기획](#기획)
  * 
4. [팀원](#팀원)

---
# 보스 구현 사항
## 2024/08/02 현재 프로토타입 버전
* 스킬 1
  
  투사체를 날리기 전 플레이어의 위치를 받아와서 투사체를 플레이어의 위치에 맞게 회전시키고 발사함
  
* 스킬 2
  
  플레이어 위치를 기준으로 경고선을 하나 생성하고 90도씩 회전시켜서 하나씩 배치함
  경고선을 1.5(N)초 후 삭제 시키고 피사체를 생성해서 반시계 방향으로 돌림
  
* 스킬 3
  
  고정된 벡터 값으로 발사시키기 위해 인스펙터 창에서 벡터 값을 입력받고 현재 테스트 기준 8방향으로 정상적으로 발사함
---
## 2024/08/05 현재 프로토타입 버전
* 스킬 3

  벡터값으로 입력하게 되면 상하좌우 4방향, 대각선 4방향으로만 발사되어 오른쪽을 기준으로 발사되고 추가 각도를 설정하여 원하는 갯수만큼 발사되게 해결
* 스킬 4
  
  플레이어의 반대편에 부채꼴 안전 구역을 생성하고 애니메이션을 실행 하기 전 적을 일정한 위치에 생성해서 플레이어가 일반 몹에게 연속으로 대쉬해서 도망갈 수 있게 만듦 
---
## 2024/08/06 현재 프로토타입 버전
* 스킬 5

  플레이어 위치에 큰 장판을 생성하는 스킬 구현
---
## 2024/08/07 현재 프로토타입 버전
* 충돌 박스 설정

  테스트 용도로 여러 개의 콜라이더를 겹쳐서 보스 충돌처리를 했음
---
## 2024/08/08
* 보스 사망 및 포탈 구현
---
# 몬스터 스포너 구현 사항
## 2024/08/02 현재 프로토타입 버전
  * 인스펙터 창에서 설정할 수 있는 것들
    * 몬스터 타입 - 현재 두 가지를 정할 수 있음
    * 원거리 몬스터 최대 마리 수 - 인스펙터창에서 원하는 마리 수를 설정하면 됨
    * 스폰 위치 - 게임 오브젝트를 원하는 위치에 놓고 인스펙터 창으로 끌어다 놓으면 됨
---
## 2024/08/05 현재 프로토타입 버전
  * 부울 값을 추가한 다른 코드를 만들어서 스킬을 실행할 때마다 몹을 원하는 위치에 생성할 수 있게 만듦
---
# 몬스터 구현 사항
  ## 2024/08/02 현재 프로토타입 버전
  * 근거리 몬스터

   ~~플레이어 추적 및 충돌 시 플레이어의 체력 감소 구현~~
   
---
## 2024/08/07 현재 프로토타입 버전
* 근거리 몬스터
  
  플레이어 추적
  * 추가 수정 사항
    
    플레이어와 똑같은 Position값이 될 때까지 비비는 문제가 생김 트리거 충돌 시 멈췄다가 트리거를 벗어났을 때 다시 쫓아가도록 구현을 해야 될 것 같음
---
## 2024/08/08 현재 프로토타입 버전
* 원거리 몬스터

  발사체가 구현되어 데미지가 정상적으로 적용되며, 벽에 닿으면 사라짐
 
* 근거리 몬스터

  보스가 있을 경우 돌아서 가게 만듦
---
## 2024/08/09
* 근거리 몬스터

  추적, 대기, 피격, 사망 애니메이션 구현
---
## 2024/08/12
* 근거리 몬스터

  충돌 데미지 변수 추가 및 충돌 시 데미지 처리
---
## 2024/08/13
* 근거리 몬스터

  피격 사운드, 사망 사운드 구현 및 버그 수정
---
# 플레이어 구현 사항
## 2024/08/02 현재 프로토타입 버전
  * 공격 관련 버그로 인해 처음부터 다시 구현하는 중
## 2024/08/13 
  * 사운드 매니저 구현 플레이어 이펙터 구현 장판형 근접 공격 구현


---
## 2024/08/08 현재 프로토타입 버전
* 기본 공격과 대쉬 기능이 구현됨 보스와 잡몹 데미지를 받고 데미지를 입힐 수 있다.
---
# 그래픽


---
# 기획

### 홍석윤-PC담당

* [능력치 비율표](https://1drv.ms/p/s!AjVAqgUDPzKVlhne_ImWUL81t-4Y) (크기/공격력/체력 등 변수 비율 확인용 표)

* [보스 스킬 예시](https://1drv.ms/p/s!AjVAqgUDPzKVlhoMPutUopMv_Qct) (범위 공격/ 장판) 
 
* [PC 일반공격 예시](https://1drv.ms/p/s!AjVAqgUDPzKVlhuXhrqrm6UY946D) (타겟팅)(임시보류중)
 
* [2024-08-08 팀플 파일](https://drive.google.com/file/d/1F5m3buIhUa8frPhTY7O66XNq5T25jXvO/view?usp=drive_link)
---
# 팀원
기획 :

프로그래밍 : 

그래픽 :

