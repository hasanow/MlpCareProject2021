using BaseProject.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Utilities.EntityFramework
{
    public static class EfQueryableIslemleri

    {
        public static SayfalamaSonucModel<T> Sayfalama<T>(this IQueryable<T> baseSorgu, SayfalamaIstekModel model, Expression<Func<T, bool>> filterSorgu = null)
        {

            var returnModel = new SayfalamaSonucModel<T>();

            returnModel.ToplamVeriAdet = baseSorgu.Count();


            if (filterSorgu!=null && !string.IsNullOrEmpty(model.AramaMetni) && model.AramaMetni.Length >= 3)
            {
                model.AramaMetni = model.AramaMetni.ToLowerTR();
                baseSorgu = baseSorgu.Where(filterSorgu);
            }

            returnModel.FiltrelenmisVeriAdet = baseSorgu.Count();

            if (model.SiralamaYon == "asc")
                baseSorgu = baseSorgu.OrderBy(o => EF.Property<T>(o, model.SiralananProperty));
            else
                baseSorgu = baseSorgu.OrderByDescending(o => EF.Property<T>(o, model.SiralananProperty));

            if (model.Sayfa < 1) model.Sayfa = 1;

            int tabloBaslangicIndis = (model.Sayfa - 1) * model.SayfaBasiVeriAdet;

            if (returnModel.FiltrelenmisVeriAdet < tabloBaslangicIndis)
            {
                model.Sayfa = 1;
                tabloBaslangicIndis = 0;
            }

            baseSorgu = baseSorgu.Skip(tabloBaslangicIndis);

            returnModel.Veri = baseSorgu.Skip(tabloBaslangicIndis).Take(model.SayfaBasiVeriAdet).ToList();
            returnModel.Sayfa = model.Sayfa;
            return returnModel;
        }
    }
}
