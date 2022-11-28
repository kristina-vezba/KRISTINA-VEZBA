using KRISTINA_VEZBA.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO.Pipelines;

namespace KRISTINA_VEZBA.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly BethanysPieShopDbContext _bethanysPieShopDbContext;
        public PieRepository(BethanysPieShopDbContext bethanysPieShopDbContext)
        {
            _bethanysPieShopDbContext = bethanysPieShopDbContext;
        }
        public IEnumerable<Pie> AllPies
        {
            get 
            { 
                return _bethanysPieShopDbContext.Pies.Include(c => c.Category); 
            }
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _bethanysPieShopDbContext.Pies.Include(c => c.Category).Where(p => p.IsPieOfTheWeek);
            }
        }

        public Pie? GetPieById(int pieId)
        {
            return _bethanysPieShopDbContext.Pies.FirstOrDefault(p => p.PieId == pieId);
        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _bethanysPieShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }

        public void CreatePie(Pie pie)
        {
            _bethanysPieShopDbContext.Pies.Add(pie);
            _bethanysPieShopDbContext.SaveChanges();
        }

        
        public void UpdatePie(Pie pie)
        {  
                _bethanysPieShopDbContext.Pies.Update(pie);
                _bethanysPieShopDbContext.SaveChanges();
        }

        public void RemovePie(Pie pie)
        {
            var selectedItem = _bethanysPieShopDbContext.Pies.SingleOrDefault(p => p.PieId == pie.PieId);

            if (selectedItem != null)
            {
                _bethanysPieShopDbContext.Pies.Remove(selectedItem);
                _bethanysPieShopDbContext.SaveChanges();
            }
        }

        
    }
}
