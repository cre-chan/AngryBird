using System;
using UI.InGameUIs;

namespace Controllers {

    [Serializable]
    struct InGameUIs {
        //场景中存在的其他控制
        public Pause pauseMenu;
        public Win winMenu;
        public Fail failMenu;
        public ScoreTextController scorer;
    }

}
