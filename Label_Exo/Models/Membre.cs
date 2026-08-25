namespace Label_Exo.Models
{
    public class Membre
    {
        public int Id { get; set; }

        public string Nom { get; set; } = string.Empty;

        public string Prenom { get; set; } = string.Empty;

        public string? Instrument { get; set; }

        public DateTime? DateNaissance { get; set; }

        public int ArtisteId { get; set; }

        public Artiste? Artiste { get; set; }
    }
}