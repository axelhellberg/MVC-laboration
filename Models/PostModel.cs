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
        public int Id { get; set; }

        [DisplayName("Namn")]
        [StringLength(20)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Du måste ange ett meddelande!")]
        [StringLength(100)]
        [DisplayName("Meddelande (krävs)")]
        public string Content { get; set; }

        public string Timestamp { get; set; }

        [DisplayName("Humör")]
        public string Mood { get; set; }

        [DisplayName("Kom ihåg mig")]
        public bool RememberMe { get; set; }

        public PostModel() // skapa tom json-fil om den inte finns
        {
            if (!File.Exists("data.json"))
            {
                List<PostModel> NewList = new List<PostModel>();
                string EmptyJson = JsonSerializer.Serialize(NewList);
                File.WriteAllText("data.json", EmptyJson);
            }
        }
        public List<PostModel> GetPosts() // hämta och deserialisera json
        {
            string Json = File.ReadAllText("data.json");

            return JsonSerializer.Deserialize<List<PostModel>>(Json);
        }

        public void AddPost() // skapa inlägg
        {
            if (Author == null) Author = "Anonym";

            Timestamp = DateTime.Now.ToString();

            List<PostModel> Posts = GetPosts();

            Posts.Add(this);

            IndexAndWrite(Posts);
        }

        public bool DeletePost() // ta bort inlägg
        {
            List<PostModel> Posts = GetPosts();

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
        private void IndexAndWrite(List<PostModel> Posts)
        {
            int Index = 0;

            foreach (PostModel Element in Posts)
            {
                Element.Id = Index;
                Index++;
            }

            string NewJson = JsonSerializer.Serialize(Posts);

            File.WriteAllText("data.json", NewJson);
        }
    }
}
