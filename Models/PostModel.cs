using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MVC_laboration.Models
{
    public class PostModel
    {
        public PostModel() // skapa tom json-fil om den inte finns
        {
            if (!File.Exists("data.json"))
            {
                List<Post> NewList = new List<Post>();
                string EmptyJson = JsonSerializer.Serialize(NewList);
                File.WriteAllText("data.json", EmptyJson);
            }
        }
        public List<Post> GetPosts() // hämta och deserialisera json
        {
            string Json = File.ReadAllText("data.json");

            return JsonSerializer.Deserialize<List<Post>>(Json);
        }

        public void AddPost(Post Post) // skapa inlägg
        {
            //if (Author == null) { Author = "Anonym"; }

            //Timestamp = DateTime.Now.ToString();

            List<Post> Posts = GetPosts();

            Posts.Add(Post);

            IndexAndWrite(Posts);
        }

        public bool DeletePost(int Id) // ta bort inlägg
        {
            List<Post> Posts = GetPosts();

            if (Id >= 0 && Id < Posts.Count)
            {
                Posts.RemoveAt(Id);
                IndexAndWrite(Posts);
                return true;
            }
            else
            {
                return false;
            }
        }

        // Indexera listan, konvertera till JSON och spara på fil
        private void IndexAndWrite(List<Post> Posts)
        {
            int Index = 0;

            foreach (Post Post in Posts)
            {
                Post.Id = Index;
                Index++;
            }

            string NewJson = JsonSerializer.Serialize(Posts);

            File.WriteAllText("data.json", NewJson);
        }
    }
}
