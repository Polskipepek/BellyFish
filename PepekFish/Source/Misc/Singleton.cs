namespace BellyFish.Source.Misc {
    class Singleton<T> where T : Singleton<T>, new() {
        public static T Instance {
            get {
                if (_instance == null)
                    _instance = new T();
                return _instance;
            }
            set { _instance = value; }
        }
        static T _instance;
    }
}
