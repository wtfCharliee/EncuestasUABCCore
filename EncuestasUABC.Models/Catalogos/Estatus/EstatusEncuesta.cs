﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace EncuestasUABC.Models.Catalogos.Estatus
{
    public class EstatusEncuesta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Estatus { get; set; }

        public virtual ICollection<Encuesta> Encuestas { get; set; }
    }
}