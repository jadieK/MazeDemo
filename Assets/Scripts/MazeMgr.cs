namespace DefaultNamespace
{
    public class MazeMgr
    {
        private static MazeMgr _instance;
        public static MazeMgr Instance()
        {
            if (_instance == null)
            {
                _instance = new MazeMgr();
            }

            return _instance;
        }

        private MazeMgr()
        {
        }
        
        public void 
    }
}