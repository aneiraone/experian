using Common.BL;
using System.Collections.Generic;
using System.Linq;

namespace ExperianCore
{
    public class ParametroService
    {
        private ExperianDBContext _context = new ExperianDBContext();
        public List<Parametro> Get() {
           return _context.Parametro.Where(d => d.Activo == true).ToList();
        }
    }
}
