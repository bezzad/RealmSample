using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace RealmSample
{
    public class JournalEntriesViewModel
    {
        // TODO: add UI for changing that.
        private const string AuthorName = "Me";

        private Realm _realm;

        public IEnumerable<JournalEntry> Entries { get; private set; }

        public JournalEntriesViewModel(Realm realm)
        {
            _realm = realm;
            Entries = realm.All<JournalEntry>();
        }

        public async Task<JournalEntry> AddEntryAsync()
        {
            JournalEntry result = null;
            await _realm.WriteAsync(tempRealm =>
            {
                var newE = new JournalEntry
                {
                    Id=  DateTime.Now.GetHashCode(),
                    Title = "Test",
                    BodyText = "TestBody",
                    Metadata = new EntryMetadata
                    {
                        Date = DateTimeOffset.Now,
                        Author = AuthorName
                    }
                };

                newE.Title += newE.Id;
                newE.BodyText += newE.Id;

                result = tempRealm.Add(newE);
            });

            return result;
        }

        internal void EditEntry(JournalEntry entry)
        {
            var transaction = _realm.BeginWrite();

            var model = new JournalEntryDetailsViewModel(entry, transaction);
        }

        private void DeleteEntry(JournalEntry entry)
        {
            _realm.Write(() => _realm.Remove(entry));
        }
    }
}

