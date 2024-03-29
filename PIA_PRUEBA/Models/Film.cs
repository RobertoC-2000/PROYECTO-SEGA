//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PIA_PRUEBA.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;


    public partial class Film
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Film()
        {
            this.Inventories = new HashSet<Inventory>();
        }

        [Required]
        [Display(Name = "Pelicula_ID")]
        public int iFilm_ID { get; set; }

        [Required]
        [Display(Name = "Titulo")]
        public string vTitle { get; set; }

        [Required]
        [Display(Name = "Año de lanzamiento")]
        [Range(1799, 2022, ErrorMessage = "Año inválido")]
        public short sRelease_Year { get; set; }

        [Required]
        [Display(Name = "Idioma_ID")]
        public Nullable<int> iLanguage_ID { get; set; }

        [Required]
        [Display(Name = "Dias maximos de renta")]
        [Range(1, 10, ErrorMessage = "Ingrese un valor del 1 al 10")]
        public Nullable<byte> tRental_Duration_Days { get; set; }

        [Required]
        [Display(Name = "Duracion(minutos)")]
        [Range(0, 1000, ErrorMessage = "Valor inválido")]
        public short sLength_In_Minutes { get; set; }

        [Required]
        [Display(Name = "Costo de reemplazo")]
        [Range(0, 5000, ErrorMessage = "Valor inválido")]
        public short sReplacement_Cost { get; set; }

        [Required]
        [Display(Name = "Valoracion")]
        [Range(0, 10, ErrorMessage = "Ingrese un valor de 0 a 10")]
        public byte tRating { get; set; }
    
        public virtual Language Language { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
