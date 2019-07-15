using Utility;
using UnityEngine;
using UI.InGameUIs;



namespace Controllers
{
    using States = BirdWatcher.States;

    partial class CentralControl : MonoBehaviour, IControllable
    {
        [SerializeField]
        private SlingShot shooter;


        [SerializeField]
        private Bird[] birds;//initialized at Start(), all the birds in the scene
                             //access via fetchBird(), the count of birds are used to indicate failure
        private uint currentBird;

        //场景中存在的其他控制
        [SerializeField]
        private InGameUIs UIElements;
        [SerializeField]
        private CameraManipulator camControl;
        [SerializeField]
        private BirdWatcher birdWatcher;


        private Controller activeControl;

        //保证胜利和失败只被调用一次
        InvokerOnce finalAction = new InvokerOnce();
        //tells wehter the current state is free mode or onWatch
        States CurrentState
        {
            get{ return birdWatcher.CurrentState; }
        }


        //a structure indicating to what extent the camera move and scale
        class Move
        {
            public bool clickOnAir;//
            public bool escPressed;

            //get those nasty input done.
            public Move(CentralControl cam)
            {
                this.clickOnAir = cam.CurrentState == States.OnWatch && Input.GetMouseButton(0);
                this.escPressed = Input.GetKeyDown(KeyCode.Escape);
            }
        }


        //used to setup the whole scene
        void Start()
        {
            //初始化状态
            currentBird = 0;
            if (shooter == null)
                Debug.LogError("Link a shooter to the MyCamera component");
            if (birds.Length <= 0)
                Debug.LogError("No birds in the scene");



            //初始控制由中央控制脚本本身
            activeControl = Controller.From(new Existing<CentralControl>(this));
            UIElements.pauseMenu.BindController(activeControl);
            UIElements.winMenu.BindController(activeControl);
            UIElements.failMenu.BindController(activeControl);
        }


        // Update is called once per frame
        void Update()
        {
            this.activeControl.GetInput();
        }


        void FixedUpdate()
        {
            //如果当前有监听对象，且对象不在场景内或者速度过低
            var birdWithinScene = birdWatcher.BirdWithinScene();
            if (birdWithinScene.HasValue && birdWithinScene == false) {
                this.Kill(new Existing<Bird>( birdWatcher.BirdUnderWatch));
                this.UnWatch();
            }

            if (this.Fail())
            {
                finalAction.call(
                    () =>{
                        this.activeControl.BindsTo(
                        new Existing<Fail>(UIElements.failMenu)
                        );
                        UIElements.failMenu.OpenMenu();
                    }
                );
                return;
            }

            if (this.Win())
            {
                finalAction.call(
                    () =>{
                        var curIndex = LevelLoader.GetCurIndex();
                        LevelRecordLoader.GetInstance().CompareMaxRecord(curIndex, UIElements.scorer.GetScore());//记录当前关卡分数
                        var next_level = LevelLoader.GetNextLevelIndex();
                        LevelRecordLoader.GetInstance().curLevel = next_level.HasValue ? next_level.Value : curIndex;
                        this.activeControl.BindsTo(
                            new Existing<Win>(UIElements.winMenu)
                        );
                        UIElements.winMenu.OpenMenu();
                    }
                );

                //end
                return;
            }

        }


        //U cannot watch a bird that does not exist
        public void Watch(Existing<Bird> some_bird)
        {
            if (!birdWatcher.BirdWithinScene().HasValue) {
                birdWatcher.Watch(some_bird);
                shooter.GetComponent<Collider2D>().enabled = false;
                Debug.Log("Watch bird");

            }
        }

        //取消监听鸟
        public void UnWatch()
        {
            if (this.birdWatcher.BirdWithinScene().HasValue)
            {
                birdWatcher.UnWatch();
                shooter.GetComponent<Collider2D>().enabled = true;
                Debug.Log("Unwatched bird");
            }
        }

        //从场景中所有鸟中抽走一只
        public Bird FetchBird()
        {
            if (currentBird >= birds.Length)
                return null;
            else
                return this.birds[currentBird++];
        }


        //判断玩家是否赢了，通过判断场景里打着猪tag的对象数量是否为0
        private bool Win()
        {
            return GameObject.FindGameObjectsWithTag("pig").Length == 0;
        }

        //判断玩家是否输了（通过场景里存活的鸟的数量
        private bool Fail()
        {
            var fail = true;
            foreach (var bird in birds)
                fail = bird.isDead && fail;
            return fail && !this.Win();
        }


        private void Kill(Existing<Bird> bird)
        {
            bird.Unwrap().isDead = true;
        }


        public void GetInput()
        {
            //get some input
            var mov = new Move(this);

            camControl.MoveAndScale();

            //如果在鸟飞行过程中按鼠标左键，则发动能力
            if (mov.clickOnAir)
                birdWatcher.BirdUnderWatch.UseBirdPower();

            if (mov.escPressed)
            {
                this.activeControl.BindsTo(
                    new Existing<Pause>(UIElements.pauseMenu)
                    );
                UIElements.pauseMenu.OpenMenu();
            }

        }

        public void BindController(Controller controller){ }
    }

}