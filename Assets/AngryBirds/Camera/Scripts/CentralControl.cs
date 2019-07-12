using System.Collections;
using System.Collections.Generic;
using UnityEngine;




partial class CentralControl:MonoBehaviour,IControllable {
    private Bird activeBird;//the currently on-fly bird, add by Watch()
    [SerializeField]
    private float deadSpeed;
    [SerializeField]
    private SlingShot shooter;
    [SerializeField]
    private MyCamara sceneCamera;
    
    [SerializeField]
    private Bird[] birds;//initialized at Start(), all the birds in the scene
                              //access via fetchBird(), the count of birds are used to indicate failure
    private uint currentBird;

    //场景中存在的其他控制
    [SerializeField]
    private Pause pauseMenu;
    [SerializeField]
    private Win winMenu;
    [SerializeField]
    private Fail failMenu;

    [SerializeField]
    private float panSpeed;
    [SerializeField]
    private float scaleRate;
    [SerializeField]
    private Collider2D sceneBorder;

    private Controller activeControl;

    //保证胜利和失败只被调用一次
    InvokerOnce finalAction=new InvokerOnce();
    public enum States
    {
        Free,
        OnWatch
    }

    //tells wehter the current state is free mode or onWatch
    States CurrentState
    {
        get
        {
            return activeBird == null ? States.Free : States.OnWatch;
        }
    }


    //a structure indicating to what extent the camera move and scale
    class Move
    {
        public Vector3 pan;//indicates the direction a camera should go (normalized)
        public float scale;//indicates how much the camera's view should scale
        public bool clickOnAir;//
        public bool escPressed;

        //get those nasty input done.
        public Move(CentralControl cam)
        {
            this.pan = new Vector3(
                Input.GetAxis("Horizontal"),
                Input.GetAxis("Vertical"),
                0
            );
            this.pan.Normalize();



            this.clickOnAir = cam.CurrentState == States.OnWatch && Input.GetMouseButton(0);
            this.scale = -Input.GetAxis("Mouse ScrollWheel");
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
        if (deadSpeed <= 0)
            Debug.LogError("The birds never die when deadSpeed is below 0.0f");
        if (birds.Length <= 0)
            Debug.LogError("No birds in the scene");
        if (sceneBorder == null)
            Debug.LogError("Scene unbounded with potential error");
        if (sceneCamera==null)
            Debug.LogError("No scene camera detected!");

        //初始控制由中央控制脚本本身
        activeControl = Controller.From(new Existence<CentralControl>(this));
        pauseMenu.BindController(activeControl);
        winMenu.BindController(activeControl);
        failMenu.BindController(activeControl);
    }


    // Update is called once per frame
    void Update()
    {
        this.activeControl.GetInput();
    }


    void FixedUpdate() {
        //judge if the bird is within the scene or the speed is too low
        if (activeBird != null){
            var sceneBound = sceneBorder.bounds;
            var scenceRect = Utility.BoundToRect(sceneBound);
            var birdPos = new Vector2(
                activeBird.transform.position.x,
                activeBird.transform.position.y
                );
            //if the bird goes out of scene or it just reached some low speed, stop watch
            if (!scenceRect.Contains(birdPos) ||
                activeBird.GetComponent<Rigidbody2D>().velocity.magnitude < deadSpeed)
            {
                this.Kill(new Existence<Bird>( this.activeBird));
                this.UnWatch();
            }
        }

        if (this.Fail()){
            finalAction.call(
                () => {
                    this.activeControl.BindsTo(
                    new Existence<Fail>(failMenu)
                    );
                    failMenu.Activate();
                }
                );
            return;
        }

        if (this.Win()) {
            finalAction.call(() => {
                var curIndex = LevelLoader.GetCurIndex();
                LevelRecordLoader.GetInstance().CompareMaxRecord(curIndex, 100);
                this.activeControl.BindsTo(
                new Existence<Win>(winMenu)
                );
                winMenu.Activate();
            }
               );
            return;
        }

    }


   //U cannot watch a bird that does not exist
   public void Watch(Existence<Bird> some_bird) {
        if (this.CurrentState == States.Free) {
            activeBird = some_bird.Unwrap();
            shooter.GetComponent<Collider2D>().enabled = false;
            Debug.Log("Watch bird");
        }
    }

    //取消监听鸟
    public void UnWatch() {
        if (this.CurrentState == States.OnWatch){
            activeBird = null;
            shooter.GetComponent<Collider2D>().enabled = true;
            Debug.Log("Unwatched bird");
        }
    }

    //从场景中所有鸟中抽走一只
    public Bird FetchBird() {
        if (currentBird>=birds.Length)
            return null;
        else
            return this.birds[currentBird++];
    }


    //判断玩家是否赢了，通过判断场景里打着猪tag的对象数量是否为0
    private bool Win() {
        return GameObject.FindGameObjectsWithTag("pig").Length == 0;
    }

    //判断玩家是否输了（通过场景里存活的鸟的数量
    private bool Fail() {
        var fail = true;
        foreach (var bird in birds)
            fail = bird.isDead && fail;
        return fail&&!this.Win();
    }


    private void Kill(Existence<Bird> bird) {
        bird.Unwrap().isDead = true;
    }


    public IControllable GetInput() {
        //get some input
        var mov = new Move(this);

        this.sceneCamera.Translate(mov.pan * panSpeed * Time.deltaTime,sceneBorder);
        this.sceneCamera.Scale(mov.scale * scaleRate * Time.deltaTime, sceneBorder);

        //如果在鸟飞行过程中按鼠标左键，则发动能力
        if (mov.clickOnAir)
            activeBird.Superpower();

        if (mov.escPressed) {
            this.activeControl.BindsTo(
                new Existence<Pause>(pauseMenu)
                );
            pauseMenu.Activate();
        }


        return null;
    }

    public void BindController(Controller controller) {

    }
}