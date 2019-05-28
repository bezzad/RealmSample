using System;
using Realms;

namespace RealmSample
{
    public class EntryMetadata : RealmObject
    {
        public DateTimeOffset Date { get; set; }

        public string Author { get; set; }
    }
}