namespace ORM
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    [Table("Livre")]
    public partial class Livre
    {
        [Key]
        public int IdLivre { get; set; }

        [Required]
        [StringLength(50)]
        public string Titre { get; set; }

        public double Prix { get; set; }

        public int IdAuteur { get; set; }

        [Required]
        [StringLength(50)]
        public string Genre { get; set; }

        public virtual Auteur Auteur { get; set; }
    }
}
