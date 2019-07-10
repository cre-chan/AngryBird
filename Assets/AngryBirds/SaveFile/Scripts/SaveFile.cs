﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;





namespace Assets.AngryBirds.SaveFile.Scripts.SaveFile
{
    //储存单个levelrecord的类
    //用maxLevelRecord是否为空表示某关是否打过
    [Serializable]
    class LevelRecord
    {
        private uint index;
        private Nullable<int> maxLevelRecord;

        public uint INDEX
        {
            get { return index; }
        }

        public Nullable<int> MAXLEVELRECORD
        {
            get { return maxLevelRecord; }
            set { maxLevelRecord = value; }
        }

        public LevelRecord(uint newIndex, Nullable<int> newRecord)
        {
            index = newIndex;
            maxLevelRecord = newRecord;
        }
    }


    //LevelRecord的控制器类
    //交互的时候通常只需要让外界调用:   
    //    1.CompareMaxRecord(uint,Nullable<int>)来判断是否刷新了记录
    //    2.ShowAllLevelRecord()来显示所有record信息
    //这两个函数
    //在构造函数中有用来初始化的InitializeLevelRecord(int numberOfRecord),需要重新初始化的时候吧构造函数里的注释去掉再启动就能初始化
    class LevelRecordLoader
    {
        [SerializeField] private List<LevelRecord> progress;//储存所有levelrecord信息
        private readonly string informationPath = Application.dataPath + "/AngryBirds/SaveFile/SaveData" + "/SaveData.txt";//文件储存的地址
       
        //构造函数,第一个if是重新初始化，平时请关掉。第二个if是加载record
        public LevelRecordLoader(int levelCount)
        {


            try
            {
                LoadLevelRecord();
            }
            catch (Exception err) {
                Debug.Log("Internal error occured."+err.Message);
                progress = new List<LevelRecord>(levelCount);
            }
        }

        //读取所有levelrecord,不处理异常，让调用者处理异常（内部错误无法修复，应该在更高层处理
        private void LoadLevelRecord()
        {
            using (var fs = File.Open(informationPath, FileMode.Open))
            {
                var bf = new BinaryFormatter();
                progress = (List<LevelRecord>)bf.Deserialize(fs);
                Debug.Log("存档读取成功");
            }
        }

        //保存所有levelrecord到磁盘
        private void SaveLevelRecord()
        {
            using (var fs = File.Create(informationPath)) {
                var bf = new BinaryFormatter();
                bf.Serialize(fs, progress);
                Debug.Log("存档");
            }
        }

        //显示所有的levelrecord
        public void ShowAllLevelRecord()
        {
            foreach (var temp in progress)
            {
                Debug.Log(temp.INDEX + "/" + temp.MAXLEVELRECORD);
            }
            Debug.Log("已显示所有levelrecord");
        }

        //比较record大小，如果currRecord大，或者没打过这关，则更新对应关卡的记录。不大则不做操作。因为要保证打完游戏后立即关闭游戏记录还在，所以每次更新都执行一次存档。这点文件应该不会影响性能
        public bool CompareMaxRecord(uint index, int currRecord)
        {
            Nullable<int> maxRecord = GetMaxRecord(index);

            if(!maxRecord.HasValue)
            {
                Debug.Log("没打过这关，新纪录");
                SetMaxRecord(index, currRecord);
                return true;
            }
            else if (currRecord > maxRecord)
            {
                Debug.Log("恭喜你打破了记录");
                SetMaxRecord(index, currRecord);
                return true;
            }
            else
            {
                Debug.Log("没能打破记录");
                return false;
            }
        }

        //获得maxrecord
        private Nullable<int> GetMaxRecord(uint index)
        {
            try {
                return progress[(int)index].MAXLEVELRECORD;
            } catch (IndexOutOfRangeException err) {
                Debug.LogError("访问不存在的关卡数据，严重内部错误!");
                throw err;
            }
            
        }

        //更改maxrecord
        private void SetMaxRecord(uint index, int newRecord)
        {
            //尝试访问关卡改变最大记录，若访问越界，意味着严重的内部错误
            try
            {
               var current = progress[(int)index].MAXLEVELRECORD;
                if (!current.HasValue || newRecord > current.Value)
                    progress[(int)index].MAXLEVELRECORD = newRecord;
            }
            catch (IndexOutOfRangeException err) {
                Debug.LogError("访问不存在的关卡数据，严重内部错误!");
                throw err;
            }
            
        }

    }





}



  