<div align="center">
  
# Penguin Run

[<img src="https://img.shields.io/badge/Github-181717?style=flat&logo=Github&logoColor=white" />]() [<img src="https://img.shields.io/badge/Notion-white?style=flat&logo=notion&logoColor=black" />]() [<img src="https://img.shields.io/badge/Figma-F24E1E?style=flat&logo=figma&logoColor=white" />]()
<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.02.21~2025.02.27-73abf0?style=flat&logo=&logoColor=white" />]()

</div> 


---

## 📂 프로젝트 구조
📦 Penguin Run

 ┣ 📂 Character                # 플레이어 관련 디렉토리

 ┃ ┣ 📜 PlayerController.cs    # 플레이어 조작 - 이우탁, 김지환

 ┃ ┣ 📜 StatHandler.cs         # 플레이어 스탯 관리 - 이우탁,오재원

 ┃ ┣ 📜 AnimationHandler.cs    # 플레이어 애니메이션 관리 - 이우탁
 

 ┣ 📂 Camera                   # 카메라 관련 디렉토리

 ┃ ┣ 📜 FollowCamera.cs        # 플레이어를 추적하는 카메라 클래스 - 조수빈


 ┣ 📂 Manager                  # 게임 관리 디렉토리

 ┃ ┣ 📜 GameManager.cs         # 게임 진행 관리 - 조수빈,오재원

 ┃ ┣ 📜 UIManager.cs           # UI 관리 - 오재원
 
 ┃ ┣ 📜 AchieveManager.cs      # 도전과제 관리 - 박성준
 
 ┃ ┣ 📜 AudioManager.cs        # 사운드 관리 - 조수빈
 
 ┃ ┣ 📜 StartSceneManager.cs   # 시작 씬 관리 - 박성준
 

 ┣ 📂 Object                   # 오브젝트 관련 디렉토리 - 박성준

 ┃ ┣ 📜 EndObject.cs           # 도착 지점  오브젝트

 ┃ ┣ 📜 InteractObject.cs      # 아이템, 오브젝트 베이스 스크립트

 ┃ ┣ 📜 Item.cs                # 아이템

 ┃ ┣ 📜 ItemInspecter.cs       # 아이템 커스텀 인스펙터

 ┃ ┣ 📜 Magnetic.cs            # 자석 아이템 오브젝트

 ┃ ┣ 📜 Obstacle.cs            # 장애물

 ┃ ┣ 📜 Swing.cs               # 스윙 장애물 오브젝트
 

 ┣ 📂 Tutorial                 # 튜토리얼 관련 디렉토리 - 박성준

 ┃ ┣ 📜 EventTrigger.cs        # 이벤트 충돌체

 ┃ ┣ 📜 TutoEventManager.cs    # 이벤트 리스트

 ┃ ┣ 📜 TutorialManager.cs     # 튜토리얼 매니저

 ┃ ┣ 📜 TutoUiManager.cs       # 튜토리얼 UI
 

 ┣ 📂 UI                       # UI 관련 디렉토리 - 박성준
 
 ┃ ┣ 📜 AchievePanel.cs        # 도전과제 UI

 ┃ ┣ 📂 StageScene             # 스테이지씬 관련 디렉토리

 ┃ ┃ ┣ 📜 GameOverUI.cs        # 게임 오버 UI

 ┃ ┃ ┣ 📜 GameUI.cs            # 인게임 UI
 
 ┃ ┃ ┣ 📜 MiniMap.cs           # 미니맵 UI

 ┃ ┃ ┣ 📜 UIFX.cs              # 아이템 흭득 연출
 
 ┃ ┣ 📂 StartScene             # 시작 씬 관련 디렉토리
 
 ┃ ┃ ┣ 📜 SettingUI.cs         # 설정 UI

 ┃ ┃ ┣ 📜 StageSelectUI.cs     # 스테이지 선택 UI

 ┃ ┃ ┣ 📜 StartMenuUI.cs       # 시작 메뉴 UI


 ┣ 📂 Utility                  # 

 ┃ ┣ 📜 FadeHelper.cs          # 페이드 인 아웃 연출 - 오재원

 ┃ ┣ 📜 SoundSource.cs         #  사운드 재생용 - 조수빈
 

---

맵 디자인
튜토리얼 - 박성준
1 스테이지 - 조수빈
2 스테이지 - 김지환

자료 조사 - 김지환

## ⚙ 주요 시스템
**  튜토리얼 **
- 간단한 조작법. 아이템에 대한 숙지.

** 다양한 아이템 **
- 체력 회복, 부스터, 자석 아이템

** 설정  **
- 디테일한 소리 설정 

---