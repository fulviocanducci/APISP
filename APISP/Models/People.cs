namespace APISP.Models
{
   public class People
   {
      public People() { }

      public People(string name, bool active)
      {
         Id = 0;
         Name = name;
         Active = active;
      }

      public People(int id, string? name, bool active)
      {
         Id = id;
         Name = name;
         Active = active;
      }

      public int Id { get; set; }
      public string? Name { get; set; } = null!;
      public bool Active { get; set; }
   }
}
