using System.Collections.Generic;

namespace Storage.Write.FakeDb
{
    public class WriteFakeDb
    {
        private static volatile WriteFakeDb _instance;
        private static readonly object SyncRoot = new object();
        public List<PocketFakeTable> Pockets = new List<PocketFakeTable>();

        public static WriteFakeDb Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new WriteFakeDb();
                    }
                }

                return _instance;
            }
        }
        private WriteFakeDb() { }
    }
}
