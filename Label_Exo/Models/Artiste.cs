namespace Label_Exo.Models
{
    public class Artiste
    {
        public int Id { get; set; }

        public string NomScenique { get; set; } = string.Empty;

        public string? StyleMusical { get; set; }

        public DateTime? DateSignature { get; set; }

        public int LabelId { get; set; }

        public MusicLabel Label { get; set; } = null!;

        public ICollection<Membre> Membres { get; set; } = new List<Membre>();

        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}