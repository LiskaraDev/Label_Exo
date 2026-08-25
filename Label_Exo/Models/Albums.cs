namespace Label_Exo.Models
{
    public class Album
    {
        public int Id { get; set; }

        public string Titre { get; set; } = string.Empty;

        public DateTime? DateSortie { get; set; }

        public decimal? PrixVente { get; set; }

        public int ArtisteId { get; set; }

        public Artiste Artiste { get; set; } = null!;

        public ICollection<Piste> Pistes { get; set; } = new List<Piste>();
    }
}