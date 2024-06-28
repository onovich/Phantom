namespace Phantom{

    public class LoginBusinessContext {

        public LoginEventCenter evt;
        public UIAppContext uiContext;
        public SoundAppContext soundContext;

        public LoginBusinessContext() {
            evt = new LoginEventCenter();
        }

    }

}