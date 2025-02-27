<div align="center">
  
# Penguin Run

[<img src="https://img.shields.io/badge/Github-181717?style=flat&logo=Github&logoColor=white" />]() [<img src="https://img.shields.io/badge/Notion-white?style=flat&logo=notion&logoColor=black" />]() [<img src="https://img.shields.io/badge/Figma-F24E1E?style=flat&logo=figma&logoColor=white" />]()
<br/> [<img src="https://img.shields.io/badge/프로젝트 기간-2025.02.21~2025.02.27-73abf0?style=flat&logo=&logoColor=white" />]()

</div> 


---

## 📂 프로젝트 구조
📦 Penguin Run

 ┣ 📂 Character                # 플레이어 관련 디렉토리

 ┃ ┣ 📜 PlayerController.cs    # 플레이어 조작

 ┃ ┣ 📜 StatHandler.cs         # 플레이어 스탯 관리

 ┃ ┣ 📜 AnimationHandler.cs    # 플레이어 애니메이션 관리
 

 ┣ 📂 Camera                   # 카메라 관련 디렉토리

 ┃ ┣ 📜 FollowCamera.cs        # 플레이어를 추적하는 카메라 클래스


 ┣ 📂 Manager                  # 게임 관리 디렉토리

 ┃ ┣ 📜 GameManager.cs         # 게임 진행 관리

 ┃ ┣ 📜 UIManager.cs           # UI 관리
 
 ┃ ┣ 📜 AchieveManager.cs      # 도전과제 관리
 
 ┃ ┣ 📜 AudioManager.cs        # 사운드 관리
 
 ┃ ┣ 📜 StartSceneManager.cs   # 시작 씬 관리
 

 ┣ 📂 Object                   # 오브젝트 관련 디렉토리

 ┃ ┣ 📜 EndObject.cs           # 도착 지점  오브젝트

 ┃ ┣ 📜 InteractObject.cs      # 

 ┃ ┣ 📜 Item.cs                # 아이템

 ┃ ┣ 📜 ItemInspecter.cs       # 

 ┃ ┣ 📜 Magnetic.cs            # 자석 아이템 오브젝트

 ┃ ┣ 📜 Obstacle.cs            # 장애물

 ┃ ┣ 📜 Swing.cs               # 스윙 장애물 오브젝트
 

 ┣ 📂 Tutorial                 # 튜토리얼 관련 디렉토리

 ┃ ┣ 📜 EventTrigger.cs        # 게임 씬 관리

 ┃ ┣ 📜 TutoEventManager.cs    # 던전 탐험 씬

 ┃ ┣ 📜 TutorialManager.cs     # 전투 씬

 ┃ ┣ 📜 TutoUiManager.cs       # 인벤토리 씬
 

 ┣ 📂 UI                       # UI 관련 디렉토리
 
 ┃ ┣ 📜 AchievePanel.cs        # 도전과제 UI

 ┃ ┣ 📂 StageScene             # 스테이지씬 관련 디렉토리

 ┃ ┃ ┣ 📜 GameOverUI.cs        # 게임 오버 UI

 ┃ ┃ ┣ 📜 GameUI.cs            # 인게임 UI

 ┃ ┃ ┣ 📜 HealthUI.cs          # 체력 UI
 
 ┃ ┃ ┣ 📜 MiniMap.cs           # 미니맵 UI

 ┃ ┃ ┣ 📜 UIFX.cs              # 
 
 ┃ ┣ 📂 StartScene             # 시작 씬 관련 디렉토리
 
 ┃ ┃ ┣ 📜 SettingUI.cs         # 설정 UI

 ┃ ┃ ┣ 📜 StageSelectUI.cs     # 스테이지 선택 UI

 ┃ ┃ ┣ 📜 StartMenuUI.cs       # 시작 메뉴 UI


 ┣ 📂 Utility                  # 

 ┃ ┣ 📜 FadeHelper.cs          # 

 ┃ ┣ 📜 SoundSource.cs         # 
 

---


## ⚙ 주요 시스템
**  튜토리얼 **
- 턴제 전투 시스템 구현
- 플레이어와 적이 번갈아 가며 공격
- 스킬 및 아이템 사용 가능

** 다양한 아이템 **
- 체력 회복, 부스터, 자석 아이템
- 탐험 시 다양한 보상 획득 가능
- 탐험 시 경험치 획득

**  **
- 
- 
- 

**  **
- 
- 
-  

---