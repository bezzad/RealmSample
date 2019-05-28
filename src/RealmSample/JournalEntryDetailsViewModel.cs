using System.Windows.Input;
using Realms;

namespace RealmSample
{
    public class JournalEntryDetailsViewModel
    {
        private Transaction _transaction;

        public JournalEntry Entry { get; private set; }


        public ICommand SaveCommand { get; private set; }

        public JournalEntryDetailsViewModel(JournalEntry entry, Transaction transaction)
        {
            Entry = entry;
            _transaction = transaction;
            Save();
        }

        private void Save()
        {
            _transaction.Commit();
        }

        internal void OnDisappearing()
        {
            _transaction.Dispose();
        }
   }
}

