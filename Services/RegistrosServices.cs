using Lab14.Models;
using Microsoft.Win32;
using System.Text.Json;

namespace Lab14.Services
{
    public class RegistrosServices
    {
        private readonly string _rutaRegistros = "Musica.json";


        public List<Album> ObtenerRegistros()
        {
            if (!File.Exists(_rutaRegistros)) return new List<Album>();

            try
            {
                string json = File.ReadAllText(_rutaRegistros);
                return JsonSerializer.Deserialize<List<Album>>(json) ?? new List<Album>();
            }
            catch
            {
                return new List<Album>();
            }
        }

        public void GuardarRegistro(Album nuevoAlbum)
        {
            var registros = ObtenerRegistros();

            // Asignar nombre genérico si el título está vacío
            if (string.IsNullOrWhiteSpace(nuevoAlbum.tituloAlbum))
            {
                nuevoAlbum.tituloAlbum = $"Album {registros.Count + 1}";
            }

            registros.Add(nuevoAlbum);
            GuardarTodo(registros);
        }

        public void Actualizar(Album albumActualizado)
        {
            var registros = ObtenerRegistros();
            // Buscamos por título el índice en la lista
            int index = registros.FindIndex(a => a.tituloAlbum == albumActualizado.tituloAlbum);

            if (index != -1)
            {
                registros[index] = albumActualizado;
                GuardarTodo(registros);
            }
        }

        public void Eliminar(string tituloAlbum)
        {
            var registros = ObtenerRegistros();
            var album = registros.FirstOrDefault(a => a.tituloAlbum == tituloAlbum);
            if (album != null)
            {
                registros.Remove(album);
                GuardarTodo(registros);
            }
        }

        public void GuardarTodo(List<Album> albums)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(albums, options);
            File.WriteAllText(_rutaRegistros, json);
        }

        public List<Album> FiltrarPorArtista(string nombreArtista)
        {
            var todos = ObtenerRegistros();
            if (string.IsNullOrWhiteSpace(nombreArtista)) return todos;

            return todos.Where(a => a.artistaAlbum.Contains(nombreArtista, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}