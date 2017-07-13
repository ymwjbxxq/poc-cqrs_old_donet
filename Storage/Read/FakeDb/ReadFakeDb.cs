using System.Collections.Generic;

namespace Storage.Read.FakeDb
{
    public class ReadFakeDb
    {
        private static volatile ReadFakeDb _instance;
        private static readonly object SyncRoot = new object();
        public List<PocketFakeTable> Pockets = new List<PocketFakeTable>
        {
            new PocketFakeTable
            {
                Id = 1,
                Money = 0
            }
        };

        public static ReadFakeDb Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new ReadFakeDb();
                    }
                }

                return _instance;
            }
        }
        private ReadFakeDb() { }
    }
}
