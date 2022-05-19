namespace ChallangeTehnic.Models.Models
{
    /// <summary>
    /// Clasa simpla
    /// Folosita ca si return din servicii
    /// </summary>
    public class RaspunsModel<T>
    {
        public T? Data { get; set; }

        public int HttpStatusCode { get; set; }
        public string? Mesaj { get; set; }
    }
}