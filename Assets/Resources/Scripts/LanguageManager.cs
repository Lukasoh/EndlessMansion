using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LanguageManager : MonoBehaviour
{
    public LanguageData languageData;
    public EnglishData englishData;
    public KoreanData koreanData;
    public ChineseData chineseData;
    public JapaneseData japaneseData;
    public SpanishData spanishData;
    public FrenchData frenchData;

    

    private bool[] languageSelected = new bool[6];

    string[] path = new string[6];

    string currentJsonData;

    SetKeyValManager setKeyValManager;

    public void LoadLanguageDataToJson()
    {
        
        string path = Path.Combine(Application.persistentDataPath, "languageData.json");
        if(!File.Exists(path))
        {
            LanguageData languageData = new LanguageData();
            SetDefaultLanguage();
            string newLangData = JsonUtility.ToJson(languageData, true);         
            File.WriteAllText(path, newLangData);
        }
        string jsonData = File.ReadAllText(path);
        languageData = JsonUtility.FromJson<LanguageData>(jsonData);
    }
    
    public void SaveLanguageDataToJson()
    {
        string jsonData = JsonUtility.ToJson(languageData, true);
        string path = Path.Combine(Application.persistentDataPath, "languageData.json");

        File.WriteAllText(path, jsonData);
    }

    public void LoadLanguageSelectionToJson(string langName)
    {
        string path = Path.Combine(Application.persistentDataPath, langName + "Data.json");
        string jsonData = File.ReadAllText(path);

        if(langName == "english")
        {
            englishData = JsonUtility.FromJson<EnglishData>(jsonData);
        }
        else if (langName == "korean")
        {
            koreanData = JsonUtility.FromJson<KoreanData>(jsonData);
        }
        else if (langName == "chinese")
        {
            chineseData = JsonUtility.FromJson<ChineseData>(jsonData);
        }
        else if (langName == "japanese")
        {
            japaneseData = JsonUtility.FromJson<JapaneseData>(jsonData);
        }
        else if (langName == "spanish")
        {
            spanishData = JsonUtility.FromJson<SpanishData>(jsonData);
        }
        else if (langName == "french")
        {
            frenchData = JsonUtility.FromJson<FrenchData>(jsonData);
        }

        UpdateAllTextsToCurrentLanguage();
        Debug.Log(langName + " Data Loaded");

    }
    
    void CreateJsonFile(string path, int i)
    {
        
        if(i == 0)
        {
            EnglishData englishData = new EnglishData();
            currentJsonData = JsonUtility.ToJson(englishData, true);
            
        }
        else if(i == 1)
        {
            KoreanData koreanData = new KoreanData();
            currentJsonData = JsonUtility.ToJson(koreanData, true);
        }
        else if(i == 2)
        {
            ChineseData chineseData = new ChineseData();
            currentJsonData = JsonUtility.ToJson(chineseData, true);
        }
        else if (i == 3)
        {
            JapaneseData japaneseData = new JapaneseData();
            currentJsonData = JsonUtility.ToJson(japaneseData, true);
        }
        else if (i == 4)
        {
            SpanishData spanishData = new SpanishData();
            currentJsonData = JsonUtility.ToJson(spanishData, true);
        }
        else if(i == 5)
        {
            FrenchData frenchData = new FrenchData();
            currentJsonData = JsonUtility.ToJson(frenchData, true);
        }
        
        File.WriteAllText(path, currentJsonData);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        setKeyValManager = FindObjectOfType<SetKeyValManager>();

        LoadLanguageDataToJson();
        if (languageData.languageOn[0])
        {
            LoadLanguageSelectionToJson("english");
        }
        else if (languageData.languageOn[1])
        {
            LoadLanguageSelectionToJson("korean");
        }
        else if (languageData.languageOn[2])
        {
            LoadLanguageSelectionToJson("chinese");
        }
        else if (languageData.languageOn[3])
        {
            LoadLanguageSelectionToJson("japanese");
        }
        else if (languageData.languageOn[4])
        {
            LoadLanguageSelectionToJson("spanish");
        }
        else if (languageData.languageOn[5])
        {
            LoadLanguageSelectionToJson("french");
        }


        path[0] = Path.Combine(Application.persistentDataPath, "englishData.json");
        path[1] = Path.Combine(Application.persistentDataPath, "koreanData.json");
        path[2] = Path.Combine(Application.persistentDataPath, "chineseData.json");
        path[3] = Path.Combine(Application.persistentDataPath, "japaneseData.json");
        path[4] = Path.Combine(Application.persistentDataPath, "spanishData.json");
        path[5] = Path.Combine(Application.persistentDataPath, "frenchData.json");
        
        for (int i = 0; i < path.Length; i++)
        {
            if (!File.Exists(path[i]))
            {
                CreateJsonFile(path[i], i);
            }
        }
        
    }

    public void SetDefaultLanguage()
    {
        languageData.languageOn[0] = true;
        languageData.languageOn[1] = false;
        languageData.languageOn[2] = false;
        languageData.languageOn[3] = false;
        languageData.languageOn[4] = false;
        languageData.languageOn[5] = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void LanguageBoolSet(int langNum)
    {
        for (int i = 0; i < 6; i++)
        {
            if (i == langNum)
            {
                languageData.languageOn[i] = true;

                for(int j = 0; j < 6; j++)
                {
                    if(j != i)
                    {
                        languageData.languageOn[j] = false;
                    }
                }
            }
        }
        SaveLanguageDataToJson();
    }

    public void SelectLanguage(int langNum)
    {
        LanguageBoolSet(langNum);

        if (languageData.languageOn[0])
        {
            LoadLanguageSelectionToJson("english");
            
        }
        else if (languageData.languageOn[1])
        {
            LoadLanguageSelectionToJson("korean");
        }
        else if (languageData.languageOn[2])
        {
            LoadLanguageSelectionToJson("chinese");
        }
        else if (languageData.languageOn[3])
        {
            LoadLanguageSelectionToJson("japanese");
        }
        else if (languageData.languageOn[4])
        {
            LoadLanguageSelectionToJson("spanish");
        }
        else if (languageData.languageOn[5])
        {
            LoadLanguageSelectionToJson("french");
        }
    }

    public void UseEnglish()
    {
        int langNum = 0;
        SelectLanguage(langNum);
        
    }

    public void UseKorean()
    {
        int langNum = 1;
        SelectLanguage(langNum);
        
    }

    public void UseChinese()
    {
        int langNum = 2;
        SelectLanguage(langNum);
        
    }

    public void UseJapanese()
    {
        int langNum = 3;
        SelectLanguage(langNum);
        
    }

    public void UseSpanish()
    {
        int langNum = 4;
        SelectLanguage(langNum);
        
    }

    public void UseFrench()
    {
        int langNum = 5;
        SelectLanguage(langNum);
        
    }

    public void UpdateAllTextsToCurrentLanguage()
    {
        SetKeyValManager[] allTextManagers = FindObjectsOfType<SetKeyValManager>();
        foreach (SetKeyValManager manager in allTextManagers)
        {
            manager.SetKeyValue();
        }
    }


}
[System.Serializable]
public class LanguageData
{
    public bool[] languageOn = new bool[6];
    
}

[System.Serializable]
public class EnglishData
{
    public string[] key = new string[100];
    public string[] val = new string[100];

    public EnglishData()
    {
        key[0] = "1-continue";
        key[1] = "1-newgame";
        key[2] = "1-setting";
        key[3] = "1-quitgame";
        key[4] = "1-soundtitle";
        key[5] = "1-backgroundsd";
        key[6] = "1-voicesd";
        key[7] = "1-effectsd";
        key[8] = "1-languagetitle";
        key[9] = "1-resettitle";
        key[10] = "1-resetinfo";
        key[11] = "1-resetbtn";
        key[12] = "2-title1";
        key[13] = "2-difficultylv";
        key[14] = "2-easy";
        key[15] = "2-normal";
        key[16] = "2-hard";
        key[17] = "2-about";
        key[18] = "2-info1";
        key[19] = "2-play";
        key[20] = "1-skip";
        key[21] = "2-backhome";
        key[22] = "2-touchguide";
        key[23] = "2-resetinform";
        key[24] = "1-resetwarning";
        key[25] = "voice1";
        key[26] = "voice2";
        key[27] = "voice3";
        key[28] = "voice7";
        key[29] = "voice8";
        key[30] = "voice9";
        key[31] = "voice10";
        key[32] = "voice11";
        key[33] = "voice12";
        key[34] = "h-progress";
        key[35] = "h-hint";
        key[36] = "h-info";
        key[37] = "i-title";
        key[38] = "i-step1";
        key[39] = "i-step2";
        key[40] = "i-step3";
        key[41] = "hint1";
        key[42] = "hint2";
        key[43] = "hint3";


        val[0] = "Continue";
        val[1] = "New Game";
        val[2] = "Setting";
        val[3] = "Quit Game";
        val[4] = "Sound";
        val[5] = "Background Sound";
        val[6] = "Voice Sound";
        val[7] = "Effect Sound";
        val[8] = "Language";
        val[9] = "Reset";
        val[10] = "If you click the button, all current states will be initialized and return to the original state.";
        val[11] = "Reset";
        val[12] = "EndlessMansion";
        val[13] = "Difficulty Level";
        val[14] = "Easy";
        val[15] = "Normal";
        val[16] = "Hard";
        val[17] = "Story";
        val[18] = "Upon opening your eyes, you find yourself trapped in a room within a mansion. With no memory of who you are, use the clues around you to escape the room and attempt to regain your lost memories.";
        val[19] = "PLAY";
        val[20] = "Tap to skip";
        val[21] = "To Title Screen";
        val[22] = "Touch image to start";
        val[23] = "There is already a game in progress";
        val[24] = "Resetting will erase all progress made so far";
        val[25] = "This is not it";
        val[26] = "I need something else";
        val[27] = "Hmm..";
        val[28] = "Hmm, a good item has been made";
        val[29] = "Where can I use this?";
        val[30] = "Great, I succeed!";
        val[31] = "I need to get out of here quickly";
        val[32] = "Good, the door is  open";
        val[33] = "What is this place for?";
        val[34] = "Game Progress";
        val[35] = "Hint";
        val[36] = "Watch the advertisement and get the hint!";
        val[37] = "Item Combination Tutorial";
        val[38] = "Prepare two items that can be combined";
        val[39] = "Drag one item onto the other item";
        val[40] = "Check the combined item";
        val[41] = "Look for a water bottle and dishcloth";
        val[42] = "Combine water bottles and dishcloths";
        val[43] = "Use a wet dishcloth to remove stains from the calendar";

    }
    
}

[System.Serializable]
public class KoreanData
{
    public string[] key = new string[50];
    public string[] val = new string[50];

    public KoreanData()
    {
        key[0] = "1-continue";
        key[1] = "1-newgame";
        key[2] = "1-setting";
        key[3] = "1-quitgame";
        key[4] = "1-soundtitle";
        key[5] = "1-backgroundsd";
        key[6] = "1-voicesd";
        key[7] = "1-effectsd";
        key[8] = "1-languagetitle";
        key[9] = "1-resettitle";
        key[10] = "1-resetinfo";
        key[11] = "1-resetbtn";
        key[12] = "2-title1";
        key[13] = "2-difficultylv";
        key[14] = "2-easy";
        key[15] = "2-normal";
        key[16] = "2-hard";
        key[17] = "2-about";
        key[18] = "2-info1";
        key[19] = "2-play";
        key[20] = "1-skip";
        key[21] = "2-backhome";
        key[22] = "2-touchguide";
        key[23] = "2-resetinform";
        key[24] = "1-resetwarning";
        key[25] = "voice1";
        key[26] = "voice2";
        key[27] = "voice3";
        key[28] = "voice7";
        key[29] = "voice8";
        key[30] = "voice9";
        key[31] = "voice10";
        key[32] = "voice11";
        key[33] = "voice12";
        key[34] = "h-progress";
        key[35] = "h-hint";
        key[36] = "h-info";
        key[37] = "i-title";
        key[38] = "i-step1";
        key[39] = "i-step2";
        key[40] = "i-step3";
        key[41] = "hint1";
        key[42] = "hint2";
        key[43] = "hint3";

        val[0] = "이어서하기";
        val[1] = "새 게임";
        val[2] = "설정";
        val[3] = "게임 종료";
        val[4] = "소리";
        val[5] = "배경 소리";
        val[6] = "음성 소리";
        val[7] = "효과음";
        val[8] = "언어";
        val[9] = "초기화";
        val[10] = "버튼 클릭 시, 현재의 모든 진행 상황이 초기화 되며 데이터를 잃게 됩니다.";
        val[11] = "초기화";
        val[12] = "끝없는 저택";
        val[13] = "난이도";
        val[14] = "쉬움";
        val[15] = "보통";
        val[16] = "어려움";
        val[17] = "이야기";
        val[18] = "눈을 뜨자 당신은 저택 내의 한 방에 갇힌 채로 발견됩니다. 자신이 누구인지 기억하지 못하는 상태에서, 주변의 단서를 사용하여 방에서 탈출하고 잃어버린 기억을 되찾으려고 시도하세요.";
        val[19] = "플레이";
        val[20] = "탭하여 건너뛰기";
        val[21] = "초기 화면으로 돌아가기";
        val[22] = "이미지를 터치해서 시작하세요";
        val[23] = "이미 진행 중인 게임이 있습니다";
        val[24] = "초기화를 진행하면 지금까지의 모든 진행 상황이 삭제됩니다";
        val[25] = "이건 아니야";
        val[26] = "다른게 필요해";
        val[27] = "흠..";
        val[28] = "음, 좋은 아이템이 만들어졌네";
        val[29] = "이걸 어디에 사용할 수 있지?";
        val[30] = "좋아, 성공했어";
        val[31] = "여기서 빨리 나가야 해";
        val[32] = "좋아, 문이 열렸어";
        val[33] = "이곳은 무엇을 위한 곳이지?";
        val[34] = "게임 진행도";
        val[35] = "힌트";
        val[36] = "광고를 시청하고 힌트를 얻으세요!";
        val[37] = "아이템 조합 튜토리얼";
        val[38] = "결합할 수 있는 두 아이템을 준비하세요";
        val[39] = "한 아이템을 다른 아이템 위로 드래그하세요";
        val[40] = "결합된 아이템을 확인하세요";
        val[41] = "물병과 행주를 찾으세요";
        val[42] = "물병과 행주를 결합하세요";
        val[43] = "젖은 행주를 사용하여 달력의 얼룩을 제거하세요";
    }
    
}

[System.Serializable]
public class ChineseData
{
    public string[] key = new string[50];
    public string[] val = new string[50];

    public ChineseData()
    {
        key[0] = "1-continue";
        key[1] = "1-newgame";
        key[2] = "1-setting";
        key[3] = "1-quitgame";
        key[4] = "1-soundtitle";
        key[5] = "1-backgroundsd";
        key[6] = "1-voicesd";
        key[7] = "1-effectsd";
        key[8] = "1-languagetitle";
        key[9] = "1-resettitle";
        key[10] = "1-resetinfo";
        key[11] = "1-resetbtn";
        key[12] = "2-title1";
        key[13] = "2-difficultylv";
        key[14] = "2-easy";
        key[15] = "2-normal";
        key[16] = "2-hard";
        key[17] = "2-about";
        key[18] = "2-info1";
        key[19] = "2-play";
        key[20] = "1-skip";
        key[21] = "2-backhome";
        key[22] = "2-touchguide";
        key[23] = "2-resetinform";
        key[24] = "1-resetwarning";
        key[25] = "voice1";
        key[26] = "voice2";
        key[27] = "voice3";
        key[28] = "voice7";
        key[29] = "voice8";
        key[30] = "voice9";
        key[31] = "voice10";
        key[32] = "voice11";
        key[33] = "voice12";
        key[34] = "h-progress";
        key[35] = "h-hint";
        key[36] = "h-info";
        key[37] = "i-title";
        key[38] = "i-step1";
        key[39] = "i-step2";
        key[40] = "i-step3";
        key[41] = "hint1";
        key[42] = "hint2";
        key[43] = "hint3";

        val[0] = "继续";
        val[1] = "新游戏";
        val[2] = "设置";
        val[3] = "退出游戏";
        val[4] = "声音";
        val[5] = "背景音乐";
        val[6] = "语音音效";
        val[7] = "效果声音";
        val[8] = "语言";
        val[9] = "重置";
        val[10] = "如果点击这个按钮，所有当前状态将被初始化并返回到原始状态。";
        val[11] = "重置";
        val[12] = "无尽的庄园";
        val[13] = "难度等级";
        val[14] = "简单";
        val[15] = "普通";
        val[16] = "困难";
        val[17] = "故事";
        val[18] = "当你睁开眼睛时，发现自己被困在庄园内的一间房间里。在不记得自己是谁的情况下，利用周围的线索逃出房间，并尝试恢复你失去的记忆。";
        val[19] = "开始游戏";
        val[20] = "点击跳过";
        val[21] = "返回标题屏幕";
        val[22] = "触摸图片开始";
        val[23] = "已经有一个游戏正在进行中";
        val[24] = "重置将会删除到目前为止的所有进度";
        val[25] = "这不是它";
        val[26] = "我需要别的东西";
        val[27] = "嗯..";
        val[28] = "嗯，一个好东西被制造出来了";
        val[29] = "我可以在哪里使用这个？";
        val[30] = "很好，成功了！";
        val[31] = "我需要迅速离开这里";
        val[32] = "很好 门开了";
        val[33] = "这个地方是用来做什么的？";
        val[34] = "游戏进度";
        val[35] = "提示";
        val[36] = "观看广告并获得提示!";
        val[37] = "物品组合教程";
        val[38] = "准备两个可以组合的物品";
        val[39] = "将一个物品拖到另一个物品上";
        val[40] = "检查组合后的物品";
        val[41] = "寻找水瓶和抹布";
        val[42] = "结合水瓶和抹布";
        val[43] = "使用湿抹布去除日历上的污渍";

    }
}

[System.Serializable]
public class JapaneseData
{
    public string[] key = new string[50];
    public string[] val = new string[50];

    public JapaneseData()
    {
        key[0] = "1-continue";
        key[1] = "1-newgame";
        key[2] = "1-setting";
        key[3] = "1-quitgame";
        key[4] = "1-soundtitle";
        key[5] = "1-backgroundsd";
        key[6] = "1-voicesd";
        key[7] = "1-effectsd";
        key[8] = "1-languagetitle";
        key[9] = "1-resettitle";
        key[10] = "1-resetinfo";
        key[11] = "1-resetbtn";
        key[12] = "2-title1";
        key[13] = "2-difficultylv";
        key[14] = "2-easy";
        key[15] = "2-normal";
        key[16] = "2-hard";
        key[17] = "2-about";
        key[18] = "2-info1";
        key[19] = "2-play";
        key[20] = "1-skip";
        key[21] = "2-backhome";
        key[22] = "2-touchguide";
        key[23] = "2-resetinform";
        key[24] = "1-resetwarning";
        key[25] = "voice1";
        key[26] = "voice2";
        key[27] = "voice3";
        key[28] = "voice7";
        key[29] = "voice8";
        key[30] = "voice9";
        key[31] = "voice10";
        key[32] = "voice11";
        key[33] = "voice12";
        key[34] = "h-progress";
        key[35] = "h-hint";
        key[36] = "h-info";
        key[37] = "i-title";
        key[38] = "i-step1";
        key[39] = "i-step2";
        key[40] = "i-step3";
        key[41] = "hint1";
        key[42] = "hint2";
        key[43] = "hint3";

        val[0] = "続ける";
        val[1] = "新しいゲーム";
        val[2] = "設定";
        val[3] = "ゲームを終了する";
        val[4] = "サウンド";
        val[5] = "背景音";
        val[6] = "声の音";
        val[7] = "効果音";
        val[8] = "言語";
        val[9] = "リセット";
        val[10] = "このボタンをクリックすると、現在の状態が初期化され、元の状態に戻ります。";
        val[11] = "リセット";
        val[12] = "エンドレスマンション";
        val[13] = "難易度レベル";
        val[14] = "易しい";
        val[15] = "普通";
        val[16] = "難しい";
        val[17] = "ものがたり";
        val[18] = "目を開けると、あなたは屋敷の一室に閉じ込められていることに気がつきます。自分が誰であるかの記憶がない中、周りの手がかりを使って部屋から脱出し、失われた記憶を取り戻そうと試みてください。";
        val[19] = "プレイ";
        val[20] = "タップしてスキップ";
        val[21] = "タイトル画面に戻る";
        val[22] = "画像をタッチして開始";
        val[23] = "すでに進行中のゲームがあります";
        val[24] = "リセットすると、これまでのすべての進行状況が消去されます";
        val[25] = "これではありません";
        val[26] = "他のものが必要です";
        val[27] = "うーん..";
        val[28] = "うーん、良いアイテムが作られた";
        val[29] = "これはどこで使えますか？";
        val[30] = "よし、成功した！";
        val[31] = "ここから早く出なければならない";
        val[32] = "よし、ドアが開いた";
        val[33] = "この場所は何のためのものか？";
        val[34] = "ゲーム進行";
        val[35] = "ヒント";
        val[36] = "広告を見てヒントを得よう！";
        val[37] = "アイテム組み合わせチュートリアル";
        val[38] = "組み合わせ可能な2つのアイテムを用意する";
        val[39] = "一つのアイテムをもう一つのアイテムの上にドラッグする";
        val[40] = "組み合わせたアイテムを確認する";
        val[41] = "水筒と雑巾を探す";
        val[42] = "水筒と雑巾を組み合わせる";
        val[43] = "濡れた雑巾を使ってカレンダーの汚れを取り除く";
    }
}

[System.Serializable]
public class SpanishData
{
    public string[] key = new string[50];
    public string[] val = new string[50];

    public SpanishData()
    {
        key[0] = "1-continue";
        key[1] = "1-newgame";
        key[2] = "1-setting";
        key[3] = "1-quitgame";
        key[4] = "1-soundtitle";
        key[5] = "1-backgroundsd";
        key[6] = "1-voicesd";
        key[7] = "1-effectsd";
        key[8] = "1-languagetitle";
        key[9] = "1-resettitle";
        key[10] = "1-resetinfo";
        key[11] = "1-resetbtn";
        key[12] = "2-title1";
        key[13] = "2-difficultylv";
        key[14] = "2-easy";
        key[15] = "2-normal";
        key[16] = "2-hard";
        key[17] = "2-about";
        key[18] = "2-info1";
        key[19] = "2-play";
        key[20] = "1-skip";
        key[21] = "2-backhome";
        key[22] = "2-touchguide";
        key[23] = "2-resetinform";
        key[24] = "1-resetwarning";
        key[25] = "voice1";
        key[26] = "voice2";
        key[27] = "voice3";
        key[28] = "voice7";
        key[29] = "voice8";
        key[30] = "voice9";
        key[31] = "voice10";
        key[32] = "voice11";
        key[33] = "voice12";
        key[34] = "h-progress";
        key[35] = "h-hint";
        key[36] = "h-info";
        key[37] = "i-title";
        key[38] = "i-step1";
        key[39] = "i-step2";
        key[40] = "i-step3";
        key[41] = "hint1";
        key[42] = "hint2";
        key[43] = "hint3";

        val[0] = "Continuar";
        val[1] = "Nuevo Juego";
        val[2] = "Configuración";
        val[3] = "Salir del Juego";
        val[4] = "Sonido";
        val[5] = "Sonido de Fondo";
        val[6] = "Sonido de Voz";
        val[7] = "Sonido de Efecto";
        val[8] = "Idioma";
        val[9] = "Restablecer";
        val[10] = "Si haces clic en el botón, todos los estados actuales serán inicializados y volverán al estado original.";
        val[11] = "Restablecer";
        val[12] = "Mansión Infinita";
        val[13] = "Nivel de Dificultad";
        val[14] = "Fácil";
        val[15] = "Normal";
        val[16] = "Difícil";
        val[17] = "Historia";
        val[18] = "Al abrir los ojos, te encuentras atrapado en una habitación dentro de una mansión. Sin memoria de quién eres, utiliza las pistas a tu alrededor para escapar de la habitación e intentar recuperar tus recuerdos perdidos.";
        val[19] = "JUGAR";
        val[20] = "Toca para saltar";
        val[21] = "Volver a la pantalla de título";
        val[22] = "Toca la imagen para comenzar";
        val[23] = "Ya hay un juego en curso";
        val[24] = "Restablecer eliminará todo el progreso realizado hasta ahora";
        val[25] = "Esto no es";
        val[26] = "Necesito algo más";
        val[27] = "Hmm..";
        val[28] = "Mmm, se ha creado un buen artículo";
        val[29] = "¿Dónde puedo usar esto?";
        val[30] = "¡Genial, he tenido éxito!";
        val[31] = "Necesito salir de aquí rápidamente";
        val[32] = "Está bien, está abierto";
        val[33] = " ¿Para qué es este lugar?";
        val[34] = "Progreso del juego";
        val[35] = "Pista";
        val[36] = "¡Mira el anuncio y obtén la pista!";
        val[37] = "Tutorial de combinación de objetos";
        val[38] = "Prepara dos objetos que se puedan combinar";
        val[39] = "Arrastra un objeto sobre el otro";
        val[40] = "Verifica el objeto combinado";
        val[41] = "Busca una botella de agua y un trapo";
        val[42] = "Combina botellas de agua y trapos";
        val[43] = "Usa un trapo húmedo para quitar manchas del calendario";
    }
}

[System.Serializable]
public class FrenchData
{
    public string[] key = new string[50];
    public string[] val = new string[50];

    public FrenchData()
    {
        key[0] = "1-continue";
        key[1] = "1-newgame";
        key[2] = "1-setting";
        key[3] = "1-quitgame";
        key[4] = "1-soundtitle";
        key[5] = "1-backgroundsd";
        key[6] = "1-voicesd";
        key[7] = "1-effectsd";
        key[8] = "1-languagetitle";
        key[9] = "1-resettitle";
        key[10] = "1-resetinfo";
        key[11] = "1-resetbtn";
        key[12] = "2-title1";
        key[13] = "2-difficultylv";
        key[14] = "2-easy";
        key[15] = "2-normal";
        key[16] = "2-hard";
        key[17] = "2-about";
        key[18] = "2-info1";
        key[19] = "2-play";
        key[20] = "1-skip";
        key[21] = "2-backhome";
        key[22] = "2-touchguide";
        key[23] = "2-resetinform";
        key[24] = "1-resetwarning";
        key[25] = "voice1";
        key[26] = "voice2";
        key[27] = "voice3";
        key[28] = "voice7";
        key[29] = "voice8";
        key[30] = "voice9";
        key[31] = "voice10";
        key[32] = "voice11";
        key[33] = "voice12";
        key[34] = "h-progress";
        key[35] = "h-hint";
        key[36] = "h-info";
        key[37] = "i-title";
        key[38] = "i-step1";
        key[39] = "i-step2";
        key[40] = "i-step3";
        key[41] = "hint1";
        key[42] = "hint2";
        key[43] = "hint3";

        val[0] = "Continuer";
        val[1] = "Nouveau Jeu";
        val[2] = "Paramètres";
        val[3] = "Quitter le Jeu";
        val[4] = "Son";
        val[5] = "Son d'arrière-plan";
        val[6] = "Son de Voix";
        val[7] = "Son d'Effet";
        val[8] = "Langue";
        val[9] = "Réinitialiser";
        val[10] = "Si vous cliquez sur le bouton, tous les états actuels seront initialisés et retourneront à l'état original.";
        val[11] = "Réinitialiser";
        val[12] = "Manoir Sans Fin";
        val[13] = "Niveau de Difficulté";
        val[14] = "Facile";
        val[15] = "Normal";
        val[16] = "Difficile";
        val[17] = "Histoire";
        val[18] = "En ouvrant les yeux, vous vous retrouvez piégé dans une chambre à l'intérieur d'un manoir. Sans aucun souvenir de qui vous êtes, utilisez les indices autour de vous pour vous échapper de la chambre et tenter de retrouver vos souvenirs perdus.";
        val[19] = "JOUER";
        val[20] = "Appuyez pour passer";
        val[21] = "Retourner à l'écran titre";
        val[22] = "Touchez l'image pour commencer";
        val[23] = "Il y a déjà une partie en cours";
        val[24] = "La réinitialisation effacera toute la progression réalisée jusqu'à présent";
        val[25] = "Ce n'est pas ça";
        val[26] = "J'ai besoin de quelque chose d'autre";
        val[27] = "Hmm..";
        val[28] = "Hmm, un bon objet a été fabriqué";
        val[29] = "Où puis-je utiliser ceci ?";
        val[30] = "Génial, j'ai réussi !";
        val[31] = "Je dois sortir d'ici rapidement";
        val[32] = "La porte est ouverte";
        val[33] = "À quoi sert cet endroit ?";
        val[34] = "Progression du jeu";
        val[35] = "Indice";
        val[36] = "Regardez la publicité et obtenez l'indice!";
        val[37] = "Tutoriel de combinaison d'objets";
        val[38] = "Préparez deux objets qui peuvent être combinés";
        val[39] = "Glissez un objet sur l'autre";
        val[40] = "Vérifiez l'objet combiné";
        val[41] = "Cherchez une bouteille d'eau et un torchon";
        val[42] = "Combinez bouteilles d'eau et torchons";
        val[43] = "Utilisez un torchon humide pour enlever les taches du calendrier";
    }
}





