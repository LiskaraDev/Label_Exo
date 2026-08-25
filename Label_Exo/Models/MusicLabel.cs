namespace Label_Exo.Models
{
    public class MusicLabel
    {
        public int Id { get; set; }

        public string Nom { get; set; } = string.Empty;

        public string? GenrePrincipal { get; set; }

        public int? AnneeCreation { get; set; }

        public string? Adresse { get; set; }

        public ICollection<Artiste> Artistes { get; set; } = new List<Artiste>();
    }
}