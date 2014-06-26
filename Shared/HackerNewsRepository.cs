using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Shared
{
    public class HackerNewsRepository
    {
        public async Task<IEnumerable<Entry>> TopEntriesAsync()
        {
            var client = new HttpClient();

            var result = await client.GetStringAsync("http://api.ihackernews.com/page/");

            return JsonConvert.DeserializeObject<Page>(result).Items;
        }
    }

    public class Entry
    {
        public int Id { get; set; }
        public int Points { get; set; }
        public string PostedAgo { get; set; }
        public string PostedBy { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
    }
    public class Page
    {
        public int? Next { get; set; }
        public IEnumerable<Entry> Items { get; set; }
    }
}