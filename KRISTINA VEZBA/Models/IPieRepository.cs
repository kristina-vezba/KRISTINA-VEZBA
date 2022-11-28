namespace KRISTINA_VEZBA.Models
{
    public interface IPieRepository
    {
        IEnumerable<Pie> AllPies { get; }
        IEnumerable<Pie> PiesOfTheWeek { get; }
        Pie? GetPieById(int pieId);
        IEnumerable<Pie> SearchPies(string searchQuery);
        void CreatePie(Pie pie);
        void UpdatePie(Pie pie);
        void RemovePie(Pie pie);
        
  
    }
}
