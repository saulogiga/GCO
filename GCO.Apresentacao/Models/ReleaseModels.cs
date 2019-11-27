using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;

namespace GCO.Apresentacao.Models
{
    public class ReleaseModels
    {
        public string Nome { get; set; }
        public DateTime DataCriacao { get; set; }
        public string Caminho { get; set; }
        public string Tipo { get; set; }
        public string BuildStatus { get; set; }
    }
}