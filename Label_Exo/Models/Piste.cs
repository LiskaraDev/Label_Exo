namespace Label_Exo.Models
{
    public class Piste
    {
        public int Id { get; set; }

        public string Titre { get; set; } = string.Empty;

        public int? DureeSecondes { get; set; }

        public int AlbumId { get; set; }

        public Album Album { get; set; } = null!;
    }
}