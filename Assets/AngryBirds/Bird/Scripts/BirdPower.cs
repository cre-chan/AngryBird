using System.Collections.Generic;
using UnityEngine;

//extends the bird to use some once super power

partial class Bird {
    private delegate void Action();
    private ActionOnce _superpower;


    //action that takes place only once
    private class ActionOnce{
        private Action action;

        public ActionOnce(Existence<Action> action) {
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