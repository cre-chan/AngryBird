




public interface IControllable {
    //获得输入并处理输入,同时返回下一个状态，为null时不转换
    IControllable GetInput();

    void BindController(Controller controller);

}


public class Controller {
    IControllable control;

    Controller(Existence<IControllable> control) {
        this.control = control.Unwrap();
    }


    //利用该函数，静态地从仍何存在的IControllable建立控制器
    static public Controller From<T>(Existence<T> obj)
        where T : class,IControllable
    {

        return new Controller(
            new Existence<IControllable>((IControllable)obj.Unwrap())
            );
    }


    //为控制器绑定新的可控制对象
    public void BindsTo<T>(Existence<T> obj)
        where T:class,IControllable
    {
        this.control=(IControllable)obj.Unwrap();
    }


    public void GetInput() {
        control.GetInput();
    }
}