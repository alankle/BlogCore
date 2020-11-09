using BlogCore.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogCore.AccesosDatos.Data.Repository
{
    public interface ISliderRepository: IRepository<Slider>
    {
        void Update(Slider slider);
    }
}
