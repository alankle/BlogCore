using BlogCore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;


namespace BlogCore.AccesosDatos.Data.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        IEnumerable<SelectListItem> GetListCategorias();

        void Update(Categoria categoria);


    }
}
