using System;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace RealmSample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var t = Task.Run(async () => await DoSomeThingAsync());
            t.Wait();
        }

        public static async Task DoSomeThingAsync()
        {
            var config = new RealmConfiguration(Environment.CurrentDirectory + "\\database.realm")
            {
                EncryptionKey = Encoding.ASCII.GetBytes("OIUygsRx8bTuFTSuie5dEFCpV5mIaHXWXu3ru3i5eYFtqwe6FQpUmkXXqp7qUgQy".ToCharArray(), 0, 64),
                // Pass HEX(128bit) for Realm Studio: 4f495579677352783862547546545375696535644546437056356d496148585758753372753369356559467471776536465170556d6b58587170377155675179
                IsDynamic = false,
                IsReadOnly = false,
                SchemaVersion = 2,
                MigrationCallback = (migration, oldSchemaVersion) =>
                {
                    // potentially lengthy data migration
                },
                // ObjectClasses = new[] { typeof(EntryMetadata), typeof(JournalEntry) }, // if is dynamic
                ShouldDeleteIfMigrationNeeded = false
            };
            try
            {
                var realm = await Realm.GetInstanceAsync(config);
                // Realm successfully opened, with migration applied on background thread

                Console.Clear();
                var jur = new JournalEntriesViewModel(realm);
                foreach (var e in jur.Entries)
                {
                    Console.WriteLine(e.Title);
                }
                while (true)
                {
                    var addedEntry = await jur.AddEntryAsync();
                    Console.WriteLine(addedEntry.Title);
                    Console.ReadKey();
                }
            }
            catch (Exception ex)
            {
                // Handle exception that occurred while opening the Realm
                Console.Error.WriteLine(ex.Message);
            }
        }
    }
}
