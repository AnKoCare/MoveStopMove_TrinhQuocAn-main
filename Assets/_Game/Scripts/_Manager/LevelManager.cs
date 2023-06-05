using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class LevelManager : Singleton<LevelManager>
{
    public Bot BotPrefabs;
    public int maxBot;
    public int currentBot;
    public ColorData colorDataManager;
    [SerializeField] private Level currentLevel;
    private int levelIndex;
    private LevelData currentData;
    public Level[] levelPrefabs;
    public PlayerController player;
    public List<LevelData> DataManager;
    public Bounds Map;
    public GameObject Ground;
    private float xMin;
    private float zMin;
    private float xMax;
    private float zMax;
    private NavMeshHit navHit;
    public List<string> botNames; // Danh sách tên của bot
    public List<string> usedNames = new List<string>(); // Danh sách các tên đã được sử dụng
    public GameObject BotHold;
    public GameObject MissionWayPoint1Hold;
    public GameObject MissionWayPoint2Hold;

    private float minTime = 1f; // Thời gian tối thiểu (giây)
    private float maxTime = 3f; // Thời gian tối đa (giây)

    private void Update() 
    {
        if(currentBot <= 10 && maxBot >= 11)
        {
            SpawnBot(10, player.sizeCharacter, player.sizeRing, player.LevelCharacter, player.moveSpeed);
            currentBot = 20;
        }
    }

    public override void OnInit()
    {
        currentData = DataManager[levelIndex];

        maxBot = currentData.CountEnemy;
        currentLevel = Instantiate(Resources.Load<Level>("Level/Ground_" + levelIndex));

        Map =  currentLevel._renderer.bounds;

        xMin = Map.min.x;
        zMin = Map.min.z;

        xMax = Map.max.x;
        zMax = Map.max.z;
        currentBot = 20;
        
    }

    private void Start() 
    {
        SpawnBot(currentBot, player.sizeCharacter, player.sizeRing, player.LevelCharacter, player.moveSpeed);
        StartCoroutine(PerformContinuousAction());
    }

    public void LoadLevel(int level)
    {
        if (currentLevel != null)
        {
            Destroy(currentLevel.gameObject);
        }

        if (level < levelPrefabs.Length)
        {
            currentLevel = Instantiate(levelPrefabs[level]);
        }
        else
        {
            //TODO: level vuot qua limit
        }
    }

    private void SpawnBot(int numberBot, float sizeCharacter, float sizeRing, int levelCharacter, float speed)
    {
        for(int i = 0; i < numberBot; i ++)
        {
            float x = Random.Range(xMin, xMax);
            float z = Random.Range(zMin, zMax);

            Vector3 posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
            Vector3 navPos = Vector3.zero;
            bool checkPos = false; 

            while(!checkPos)
            {
                if(NavMesh.SamplePosition(posBot, out navHit ,5f, NavMesh.AllAreas))
                {
                    navPos = navHit.position;
                    if(Vector3.Distance(player.TF.position, navPos) > 15f)
                    {
                        checkPos = true;
                    }
                    else
                    {
                        x = Random.Range(xMin, xMax);
                        z = Random.Range(zMin, zMax);
                        posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
                    }
                }
                else
                {
                    x = Random.Range(xMin, xMax);
                    z = Random.Range(zMin, zMax);
                    posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
                }   
            }

            Bot bots = SimplePool.Spawn<Bot>(PoolType.Bot);
            if(navPos != Vector3.zero)
            {
                bots.transform.position = navPos;
                bots.navMeshAgent.SetDestination(bots.transform.position);
            }
            bots.sizeCharacter = player.sizeCharacter;
            bots.sizeRing = player.sizeRing;
            bots.LevelCharacter = player.LevelCharacter;
            bots.moveSpeed = player.moveSpeed - 2f;
            
            bots.OnInit();
        }
    }

    public void SpawnNotice(string killer, string victim)
    {
        UIManager.Ins.GetUI<CvGameplay>(UIID.Gameplay).SpawnNotice(killer,victim);
    }

    private IEnumerator PerformContinuousAction()
    {
        while (true)
        {
            // Thực hiện hành động ngẫu nhiên
            SpawnGiftRandom();

            // Chờ trong khoảng thời gian ngẫu nhiên
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
        }
    }

    private void SpawnGiftRandom()
    {
        if(!GameManager.Ins.IsState(GameState.Gameplay)) return;
        Gift gift = SimplePool.Spawn<Gift>((PoolType)Random.Range(13,16));

        float x = Random.Range(xMin, xMax);
        float z = Random.Range(zMin, zMax);

        Vector3 posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
        Vector3 navPos = Vector3.zero;
        bool checkPos = false; 

        while(!checkPos)
        {
            if(NavMesh.SamplePosition(posBot, out navHit ,5f, NavMesh.AllAreas))
            {
                navPos = navHit.position;
                if(Vector3.Distance(player.TF.position, navPos) > 15f)
                {
                    checkPos = true;
                }
                else
                {
                    x = Random.Range(xMin, xMax);
                    z = Random.Range(zMin, zMax);
                    posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
                }
            }
            else
            {
                x = Random.Range(xMin, xMax);
                z = Random.Range(zMin, zMax);
                posBot = Vector3.up * 1.58f + Vector3.forward * z + Vector3.right * x;
            }   
        }

        gift.TF.position = Vector3.right * navPos.x + Vector3.up * 30f + Vector3.forward * navPos.z;
    }

    public void ReloadGame()
    {
        usedNames.Clear();
        
        SimplePool.CollectAll();
        
        player.OnInit();
        player.gameObject.SetActive(true);

        Destroy(currentLevel);
        OnInit();
        SpawnBot(currentBot, player.sizeCharacter, player.sizeRing, player.LevelCharacter, player.moveSpeed);
    }

    public void RevivePlayer()
    {
        if(player.coin < 150) return;
        else
        {
            player.BuyItemRevive(150);
        }
        player.OnInitRevive();
        player.gameObject.SetActive(true);
    }

}
