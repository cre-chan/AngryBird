using Utility;

//extends the bird to use some once super power

partial class Bird {
    protected delegate void Action();
    protected ActionOnce _superpower;


    //action that takes place only once
    protected class ActionOnce{
        private Action action;

        public ActionOnce(Existing<Action> action) {
            this.action = action.Unwrap();
        }

        //this won't work twice if it had been called once
        public void Call() {
            if (action != null) {
                action();
                action = null;
            }
        }
    }

}