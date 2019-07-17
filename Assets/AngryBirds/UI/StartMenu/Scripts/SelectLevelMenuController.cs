using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;





namespace UI.StartupMenus
{

    class LevelInformation
    {
        private string levelName;
        private Nullable<int> levelRecord;

        public string LEVELNAME
        {
            get { return levelName; }
            set { levelName = value; }
        }

        public Nullable<int> LEVELRECORD
        {
            get { return levelRecord; }
            set { levelRecord = value; }
        }

        public LevelInformation(string name, Nullable<int> record)
        {
            levelName = name;
            levelRecord = record;
        }
    }


    //因为startmenu菜单有要调用本菜单的函数的情况，所以，一定要保证在ide中selectmenu是可用的，不要把√取消掉
    public class SelectLevelMenuController : MonoBehaviour
    {
        [SerializeField]
        private Button[] buttonCollect;//按钮集合
        [SerializeField]
        private StartMenuController startMenu;//另一个菜单，用来两个菜单之间互相唤醒
        [SerializeField]
        private Button right;//右翻页按钮
        [SerializeField]
        private Button left;//左翻页按钮


        private LevelRecordLoader levelRecordLoader;//levelrecordloader实例
        private List<LevelInformation> levelInformation;//储存信息
        private int levelCount;//记录关卡数量
        private uint onePageCount;//记录一页有多少关
        private uint page;//记录当前页数
        private uint nextChallengeLevelIndex;

        void Awake()
        {


        }
        // Use this for initialization
        void Start()
        {
            levelCount = LevelLoader.LevelCount;//从levelloader中得到关卡总数量;
            levelRecordLoader = LevelRecordLoader.GetInstance();
            levelInformation = new List<LevelInformation>();
            onePageCount = (uint)buttonCollect.Length;


            //初始化关卡信息
            for (uint i = 0; i < levelCount; i++)
            {
                string name = LevelLoader.GetName(i);//得到名字
                Nullable<int> record = levelRecordLoader.GetMaxRecord(i);//得到record
                levelInformation.Add(new LevelInformation(name, record));
            }

            nextChallengeLevelIndex = GetnextChallengeLevelIndex();
            page = 0;
            Refresh();

            //对按钮的消息进行绑定
            for (int i = 0; i < onePageCount; i++)
            {
                Transform target = buttonCollect[i].transform;
                buttonCollect[i].GetComponent<Button>().onClick.AddListener(
                    () => HandlerNotification(target)
                    );
            }



        }


        public void RightTrun()
        {
            if ((page + 1) * onePageCount > levelCount)
            {
                Debug.Log("页数太大，无法翻页");
                return;
            }
            else
            {
                page++;
                Refresh();
            }

        }

        public void leftTrun()
        {
            if ((int)page - 1 < 0)
            {
                Debug.Log("页数太小，无法翻页");
                return;
            }
            else
            {
                page--;
                Refresh();
            }

        }

        //刷新按钮集合
        private void Refresh()
        {
            int i = (int)(onePageCount * page);

            //将button重置一下
            for (int j = 0; j < onePageCount; j++)
            {
                buttonCollect[j].GetComponent<Image>().color = Color.white;
                buttonCollect[j].GetComponent<Button>().interactable = true;
            }

            for (int j = 0; j < onePageCount; j++)
            {
                //先判断是否越界，越界了代表没关了，写上无关卡
                if (i + j > levelCount - 1)
                {
                    buttonCollect[j].transform.GetChild(1).GetComponent<Text>().text = "无关卡";
                    buttonCollect[j].transform.GetChild(0).GetComponent<Text>().text = "  ";
                    buttonCollect[j].GetComponent<Image>().color = Color.gray;
                    buttonCollect[j].GetComponent<Button>().interactable = false;
                    continue;
                }

                //设定每个按钮的名字
                string name = levelInformation[i + j].LEVELNAME;
                buttonCollect[j].transform.GetChild(1).GetComponent<Text>().text = name;

                //设定最高分，如果是第一次见到null，则认为这关没挑战过，设为新关卡。之后再见到null则认为是无法挑战的关卡
                Nullable<int> record = levelInformation[i + j].LEVELRECORD;
                if (i + j > nextChallengeLevelIndex)
                {
                    buttonCollect[j].transform.GetChild(1).GetComponent<Text>().text = "无法挑战";
                    buttonCollect[j].transform.GetChild(0).GetComponent<Text>().text = "  ";
                    buttonCollect[j].GetComponent<Image>().color = Color.gray;
                    buttonCollect[j].GetComponent<Button>().interactable = false;

                }
                else if (i + j == nextChallengeLevelIndex)
                {
                    buttonCollect[j].transform.GetChild(0).GetComponent<Text>().text = "新关卡";
                    buttonCollect[j].GetComponent<Image>().color = Color.yellow;
                }
                else
                {
                    buttonCollect[j].transform.GetChild(0).GetComponent<Text>().text = "最高分:" + record.ToString();
                }
            }





        }

        //返回开始界面
        public void ReturnStart()
        {
            startMenu.gameObject.SetActive(true);
            this.gameObject.SetActive(false);
        }

        //加载点击按钮,只有有效范围内的按钮可以点击
        public void LoadClickLevel(uint num)
        {
            uint i = onePageCount * page;
            LevelLoader.Load(num + i);
            LevelRecordLoader.GetInstance().curLevel = num + i;//设置存档进度
        }

        //加载第一个未通过的关卡
        public void LoadContinueLevel()
        {
            LevelLoader.Load(nextChallengeLevelIndex);
            Debug.Log("NextLevel:");
            Debug.Log(nextChallengeLevelIndex);
        }

        //关卡选择按钮点击监听(可以的话可以吧所有按钮都监听上)
        public void HandlerNotification(Transform target)
        {
            string name = target.name;
            int intName = System.Int32.Parse(name);
            if (intName >= 0 && intName < onePageCount)
            {
                Debug.Log("点击了关卡选择按钮:" + intName);
                LoadClickLevel((uint)intName);
            }

        }

        //返回第一个未挑战的关卡的index
        private uint GetnextChallengeLevelIndex()
        {
            //寻找第一个null，找得到就返回他，找不到就返回++，代表全都过关
            for (uint i = 0; i < levelCount; i++)
            {
                if (!levelInformation[(int)i].LEVELRECORD.HasValue)
                {
                    return i;
                }
            }
            return (uint)(levelCount + 1);
        }

    }

}