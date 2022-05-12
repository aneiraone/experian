using Common.BL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExperianCore
{
    public class DocumentoService
    {
        private ExperianDBContext _context = new ExperianDBContext();
        public void Save(string rut, string razon, int tipo, int folio, string data)
        {
            Documento documento = new Documento()
            {
                Rut = rut,
                TipoDocumento = tipo,
                Folio = folio,
                Razon = razon,
                FechaCreacion = DateTime.Now,
                FechaModificacion = DateTime.Now,
                Estado = Estado.Pendiente,
                Data = data
            };
            _context.Add(documento);
            _context.SaveChanges();
        }

        public bool Update(int id, Estado estado, string error)
        {
            Documento documento = _context.Documento.First(x => x.Id == id);
            documento.FechaModificacion = DateTime.Now;
            documento.Estado = estado;
            documento.Error = error;
            _context.SaveChanges();
            return true;
        }

        public List<Documento> GetPendientes() {
           return _context.Documento.Where(d => d.Estado == Estado.Pendiente).OrderByDescending(d => d.Rut).ToList();
        }
    }
}
