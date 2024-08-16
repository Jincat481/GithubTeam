한아전 팀프로젝트입니다.

유니티 사용하는 버전은 [2022.3.29f1](https://unity.com/kr/releases/editor/whats-new/2022.3.29#installs)입니다.

추가로 패키지 매니저에서 com.unity.render-pipelines.core 검색하셔서 다운 받으세요.

[스파인 다운로드](https://ko.esotericsoftware.com/spine-unity-download/)

다들 계획에 차질없게 진행하시길 바랍니다.

이슈에서 기획분들 원하는 구현, 수정 사항 부분이 있을 경우 수정요청하시면 되고 사용법은 [깃허브 사용법](https://www.youtube.com/watch?v=wBsSUBEUYV4&list=PLmUIPs_NMNt_TcEGa3qzlAmnPa4FlgPEI&index=6)
여기서 영상 보시고 사용하시면 됩니다.

기획, 프로그래밍, 그래픽분들 모두 프로젝트 누르셔서 일정 확인 및 작업 사항 체크 해주시길 바랍니다.

---
월요일 회의 이번 주 할 작업 회의, 목표 (진척 관리, 작업 못 한 것 계속 유지)

매일 시간별로 작업 계획서 및 작업 후기 작성, 작업 결과물 깃허브에 올리기

매일 금요일 7시 ~ 8시 진척 보고
발표자 5분 내외로 준비(교수님 참관가능하게 최대한 노력해봄)

스케줄 관리는 무조건 팀장의 허락하에 변경

팀장은 팀원이 뭘 하는지 알고 팀원이 이걸 할 타이밍인지 아닌지 판단

![image](https://github.com/user-attachments/assets/e0819a17-d6b8-4855-876b-7f3589156769)


---
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
## 2024/08/15
* 보스 스킬 테스트용 부울 값 추가 및 버튼 키 입력으로 스킬 시전하게 구현
* 스킬 4,5 게임 실행 도중 값 변경가능하게 수정
* 기획분께서 제작해주신 파티클 투사체로 적용
* 이펙트 시스템이 들어가서 문제인지 콜라이더 자체로 충돌처리는 되지 않고 파티클 시스템 내에 콜리젼을 키고 콜리젼 내에서 충돌 처리할 레이어를 선택함
* 코드내에서 OnParticleCollision(GameObject 변수이름)을 활용하여 데미지 및 파괴 구현, Radius Scale 0.05로 설정해서 트리거와 비슷하게 제작
* 스킬 2 피사체 추가 각도 설정 및 오브젝트 개수 설정 인스펙터 창에서 수정 가능하게 추가
* 스킬 5 파티클 추가
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
---
## 2024/08/08 현재 프로토타입 버전
* 기본 공격과 대쉬 기능이 구현됨 보스와 잡몹 데미지를 받고 데미지를 입힐 수 있다.
---
## 2024/08/13 
  * 사운드 매니저 구현 플레이어 이펙터 구현 장판형 근접 공격 구현
---
## 2024/08/14
  * 근접 공격 버그 플레이어 부딪 히면 공격이 들어감 콜라이더 문제로 보임 
---

---
# 그래픽
## 김성철
## 맵
## ~2024/08/15
![배경-1sss](https://github.com/user-attachments/assets/01105d8a-0e44-4e3b-9a1f-a2df45890d3d)
![배경-1ss](https://github.com/user-attachments/assets/c859690c-8cc3-498f-8b3e-ad7f8d7e75df)
![배경](https://github.com/user-attachments/assets/46f6ad95-a26c-4380-95a2-bd068c51d466)

----------------------------------------------------------------

# PC 컨셉시안
## ~2024/08/15
![pc-1](https://github.com/user-attachments/assets/ffc7fdf1-fc37-415c-89a4-a3a65eef3ab2)
![pc-2](https://github.com/user-attachments/assets/866a433a-8349-46b6-bb38-8c200d0ffce4)
![pc-3](https://github.com/user-attachments/assets/86d8b50b-5ee5-4f60-8ef4-4720b14336d5)


----------------------------------------------------------------

# BOSS 컨셉시안
## ~2024/08/15
![보스-1](https://github.com/user-attachments/assets/63383ae9-30ff-4f81-9a38-6ec1137850e3)
![음악가](https://github.com/user-attachments/assets/1cc3fbc3-b92b-42bb-a692-a117012f4a2a)


-------------------------------------------------

# BOSS 디폼 채색
## ~2024/08/15
![다크판타지](https://github.com/user-attachments/assets/5103c1c4-41c3-4a25-8143-d9b0ce83cf0a)
![돼지](https://github.com/user-attachments/assets/c622e818-a70a-4801-8be0-99a03f994aff)
![뱀파이어](https://github.com/user-attachments/assets/1be5a7da-bb86-456f-b484-b48fedaa7333)
![파라오](https://github.com/user-attachments/assets/028680d4-72ee-4429-a315-62fa27fd2972)
![파리 스케치완2](https://github.com/user-attachments/assets/6de48799-12db-4290-bae1-7b88d9a7a852)
![파리 스케치완](https://github.com/user-attachments/assets/57e80f70-cc7d-4eaf-bcb0-dbbef6d7731a


-------------------------------------------------------------------

# GUI
## ~2024/08/15
![boss hp](https://github.com/user-attachments/assets/51c735b8-68dd-432c-9797-3c2218ca8c6b)
![pc hp](https://github.com/user-attachments/assets/f0ef911a-66d7-441f-9d54-d71f67b83e85)
![hp바](https://github.com/user-attachments/assets/15edf53c-603a-43f8-9a94-895a16d8b70d)

# ppt용 시놉시스 컷
## 2024/08/15
![시놈시스 컷](https://github.com/user-attachments/assets/8781f2c3-a604-4ee1-9ca0-38bb85d6341d)



---

## 강정윤
## ~2024/08/15
![1](https://github.com/user-attachments/assets/f6622987-9fba-4aab-bb96-55c30909e8a8)
![2](https://github.com/user-attachments/assets/ecb0250a-10a7-4c41-b254-6c970d7515e6)
![3](https://github.com/user-attachments/assets/34c3069b-ca7f-4a7d-b45e-04e46809495e)
![3-1](https://github.com/user-attachments/assets/5628e0c2-d653-40ce-8ccb-71c9813e688b)
![4](https://github.com/user-attachments/assets/516ccb12-1cbf-4a09-88c1-397ce9982500)
![5](https://github.com/user-attachments/assets/b6d46415-8e23-4587-b3d9-7ac2a6421012)
![6](https://github.com/user-attachments/assets/4bedb3d5-6327-4f46-aff4-40e51da6d384)
![7](https://github.com/user-attachments/assets/39b5560f-6ca5-422b-abf4-cf68f5bb620e)



---
## 2024/08/15
![8](https://github.com/user-attachments/assets/893d89d0-a730-404e-a6b9-e78134bf846c)
![9](https://github.com/user-attachments/assets/41dcdd89-3ef7-4128-b826-29e550bb0d1e)



---
# 기획

## 홍석윤-PC담당

* [능력치 비율표](https://1drv.ms/p/s!AjVAqgUDPzKVlhne_ImWUL81t-4Y) (크기/공격력/체력 등 변수 비율 확인용 표)

* [보스 스킬 예시](https://1drv.ms/p/s!AjVAqgUDPzKVlhoMPutUopMv_Qct) (범위 공격/ 장판) 
 
* [PC 일반공격 예시](https://1drv.ms/p/s!AjVAqgUDPzKVlhuXhrqrm6UY946D) (타겟팅)(임시보류중)
 
* [2024-08-08 팀플 파일](https://drive.google.com/file/d/1F5m3buIhUa8frPhTY7O66XNq5T25jXvO/view?usp=drive_link)

* 2024-08-15 보스 스킬 1/2/3/4/5 수치 조정함. / 상시 스포너 수치 조정함 / 유니티 프리팹,부모자식 시스템 공부함 

## 고아라
### 2024.08.15
* 발표회용 ppt + 사용한 폰트 포함 [ppt발표용자료.zip](https://github.com/user-attachments/files/16624472/ppt.zip)

* 유니티 공부

## 김혜진
### 2024.08.15
- [발표회 제출용 리플렛 작업](https://drive.google.com/file/d/1-QgjVnAt8K0XwzP-HhdcocrJ5zjn2r0O/view?usp=drive_link)
- [영상 편집 공부1](https://youtu.be/YA2s5DcC9-Y?si=8fGkPiPyjYovsZlt)
- [영상 편집 공부2](https://youtu.be/nnFdtKA8acM?si=0FRxltdq7a3kgS3z)
- [영상에 사용하려는 bgm](https://www.youtube.com/watch?v=YyknBTm_YyM)

## 장기훈
### 2024.08.15
* [보스 스킬 이펙트 ]
* -장판 이펙트 (메테리얼)[effects smoke.zip](https://github.com/user-attachments/files/16631850/effects.smoke.zip)
* -투사체 이펙트[effects1.zip](https://github.com/user-attachments/files/16631843/effects1.zip)
* -레이저 이펙트및 몇몇 이펙트 [effects2.zip](https://github.com/user-attachments/files/16631845/effects2.zip)

---
# 팀원
기획 :

프로그래밍 : 

그래픽 :

