using System.Runtime.CompilerServices;

namespace Lab14.Models

{
    public class Album
    {
        public string tituloAlbum { get; set; } = string.Empty;
        public string artistaAlbum { get; set; } = string.Empty;

        public List<Canciones> ListaCanciones { get; set; } = new List<Canciones>();

        public DateTime AlbumDate { get; set;} 
    }

    public class Canciones
    {
        public string tituloCancion { get; set; } = string.Empty;
        public string artistaCancion { get; set; } = string.Empty;
        public string tiempoDuracion { get; set; } = string.Empty; 
    }
}
